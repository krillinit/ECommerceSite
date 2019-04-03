Imports System.Data
Imports System.Data.SqlClient
Partial Class ViewCart
    Inherits System.Web.UI.Page
    Public strCartNo As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If HttpContext.Current.Request.Cookies("CartNo") Is Nothing Then

        Else
            Dim CookieBack As HttpCookie
            CookieBack = HttpContext.Current.Request.Cookies("CartNo")
            strCartNo = CookieBack.Value
            SqlDSCart1.SelectCommand = "Select * From Cart Where CartNo = '" & strCartNo & "'"
            SqlDSCart1.DataBind()
            gvCart.DataBind()

        End If

        ' 
    End Sub

    Protected Sub lvCart_OnItemCommand(ByVal sender As Object, ByVal e As ListViewCommandEventArgs)
        If e.CommandName = "cmdUpdate" Then
            Dim strWebID As String = e.CommandArgument
            Dim tbQuantity As TextBox = CType(e.Item.FindControl("tbQuantity"), TextBox)
            Dim strSQL As String = "Update Cart set Quantity = '" & CInt(tbQuantity.Text) & "' where WebID = '" & strWebID & "' and CartNo  = '" & strCartNo & "'"
            ' update
            Dim strConn As String = System.Configuration.ConfigurationManager.ConnectionStrings("ConnectionStringOnlineStore").ConnectionString
            Dim connCart As SqlConnection
            Dim cmdCart As SqlCommand
            Dim drCart As SqlDataReader
            connCart = New SqlConnection(strConn)
            cmdCart = New SqlCommand(strSQL, connCart)
            connCart.Open()
            drCart = cmdCart.ExecuteReader(CommandBehavior.CloseConnection)
            sqlDSCart2.DataBind()
            lvCart.DataBind()
        ElseIf e.CommandName = "cmdDelete" Then
            Dim strWebID As String = e.CommandArgument
            Dim tbQuantity As TextBox = CType(e.Item.FindControl("tbQuantity"), TextBox)
            Dim strSQL As String = "Delete FROM Cart WHERE WebID = '" & strWebID & "' and CartNo  = '" & strCartNo & "'"
            Dim strConn As String = System.Configuration.ConfigurationManager.ConnectionStrings("ConnectionStringOnlineStore").ConnectionString
            Dim connCart As SqlConnection
            Dim cmdCart As SqlCommand
            Dim drCart As SqlDataReader
            connCart = New SqlConnection(strConn)
            cmdCart = New SqlCommand(strSQL, connCart)
            connCart.Open()
            drCart = cmdCart.ExecuteReader(CommandBehavior.CloseConnection)
            sqlDSCart2.DataBind()
            lvCart.DataBind()
        End If

    End Sub

    Protected Sub DataCart_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataCart.PreRender
        Dim total_pages As Integer
        Dim current_page As Integer
        Dim grand_total As Integer
        grand_total = 0

        sqlDSCart2.SelectCommand = "Select * From Cart Where CartNo = '" & strCartNo & "'"
        sqlDSCart2.DataBind()
        lvCart.DataBind()
        'For Each itm In lvCart.Items
        'grand_total += CInt(itm.ToString())
        'Dim lblA As Label = lvCart.FindControl("Label1")
        'lblA.Text = grand_total.ToString

        'Next



        total_pages = DataCart.TotalRowCount / DataCart.PageSize
        current_page = DataCart.StartRowIndex / DataCart.PageSize + 1
        If DataCart.TotalRowCount Mod DataCart.PageSize <> 0 Then
            total_pages = total_pages + 1
        End If
        If CInt(lvCart.Items.Count) <> 0 Then
            Dim lbl As Label = lvCart.FindControl("lblPage")
            lbl.Text = "Page " + CStr(current_page) + " of " + CStr(total_pages) + " (Total items: " + CStr(DataCart.TotalRowCount) + ")"
        End If
        If CInt(lvCart.Items.Count) = 0 Then
            DataCart.Visible = False
            show_next.Visible = False
            show_prev.Visible = False
        End If
    End Sub

    Protected Sub show_prev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles show_prev.Click
        Dim pagesize As Integer = DataCart.PageSize
        Dim current_page As Integer = DataCart.StartRowIndex / DataCart.PageSize + 1
        Dim total_pages As Integer = DataCart.TotalRowCount / DataCart.PageSize
        Dim last As Integer = total_pages \ 3
        last = last * 3
        Do While current_page < last
            last = last - 3
        Loop
        If last < 3 Then
            last = 0
        Else
            last = last - 3
        End If
        DataCart.SetPageProperties(last * pagesize, pagesize, True)
    End Sub

    Protected Sub show_next_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles show_next.Click
        Dim last As Integer = 3
        Dim pagesize As Integer = DataCart.PageSize
        Dim current_page As Integer = DataCart.StartRowIndex / DataCart.PageSize + 1
        Dim total_pages As Integer = DataCart.TotalRowCount / DataCart.PageSize
        Do While current_page > last
            last = last + 3
        Loop
        If last > total_pages Then
            last = total_pages
        End If
        DataCart.SetPageProperties(last * pagesize, pagesize, True)
    End Sub
    Protected Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Dim strSQL As String = "Delete FROM Cart WHERE CartNo  = '" & strCartNo & "'"
        Dim strConn As String = System.Configuration.ConfigurationManager.ConnectionStrings("ConnectionStringOnlineStore").ConnectionString
        Dim connCart As SqlConnection
        Dim cmdCart As SqlCommand
        Dim drCart As SqlDataReader
        connCart = New SqlConnection(strConn)
        cmdCart = New SqlCommand(strSQL, connCart)
        connCart.Open()
        drCart = cmdCart.ExecuteReader(CommandBehavior.CloseConnection)
        sqlDSCart2.DataBind()
        lvCart.DataBind()

    End Sub
    Protected Sub gvCart_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Dim grdTotal As Integer
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim rowTotal As Integer =
           DataBinder.Eval(e.Row.DataItem, "TotalText")
            grdTotal = grdTotal + rowTotal
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            Dim lbl As Label = DirectCast(e.Row.FindControl("totalLabel"), Label)
            lbl.Text = grdTotal.ToString("c")
            Label1.Text = lbl.Text
        End If


    End Sub
End Class
