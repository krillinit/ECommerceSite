Imports System.Data
Imports System.Data.SqlClient
Partial Class ProductDetail
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Request.QueryString("ProductID") <> "" Then
            Dim strConn As String = System.Configuration.ConfigurationManager.ConnectionStrings("ConnectionStringOnlineStore").ConnectionString
            Dim connProduct As SqlConnection
            Dim cmdProduct As SqlCommand
            Dim drProduct As SqlDataReader
            Dim strSQL As String = "Select * from Product Where ID = " & Request.QueryString("ProductID")
            connProduct = New SqlConnection(strConn)
            cmdProduct = New SqlCommand(strSQL, connProduct)
            connProduct.Open()
            drProduct = cmdProduct.ExecuteReader(CommandBehavior.CloseConnection)
            If drProduct.Read() Then
                lblProductName.Text = drProduct.Item("ProductName")
                lblPrice.Text = drProduct.Item("Price")
                lblProductID.Text = drProduct.Item("WebID")
                imgProductImage.ImageUrl = "images/product/" + Trim(drProduct.Item("WebID")) + ".jpg"
            End If
            'connProduct.Close()
        End If
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim drCart As SqlDataReader
        Dim strSQLStatement As String
        Dim cmdSQL As SqlCommand
        Dim strConnectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("ConnectionStringOnlineStore").ConnectionString
        Dim conn As New SqlConnection(strConnectionString)
        conn.Open()
        ' *** get product price
        strSQLStatement = "SELECT * FROM Product WHERE WebID = '" & lblProductID.Text & "'"
        cmdSQL = New SqlCommand(strSQLStatement, conn)
        drCart = cmdSQL.ExecuteReader()
        Dim sngPrice As Single
        If drCart.Read() Then
            sngPrice = drCart.Item("Price")
        End If
        '*** get CartNo
        Dim strCartNo As String
        If HttpContext.Current.Request.Cookies("CartNo") Is Nothing Then
            strCartNo = GetRandomCartNoUsingGUID(10)
            Dim CookieTo As New HttpCookie("CartNo", strCartNo)
            HttpContext.Current.Response.AppendCookie(CookieTo)
        Else
            Dim CookieBack As HttpCookie
            CookieBack = HttpContext.Current.Request.Cookies("CartNo")
            strCartNo = CookieBack.Value
        End If
        conn.Close()


        conn.Open()
        strSQLStatement = "SELECT * FROM CART WHERE CartNo ='" & strCartNo & "' and WebID = '" & lblProductID.Text & "'"
        cmdSQL = New SqlCommand(strSQLStatement, conn)
        drCart = cmdSQL.ExecuteReader()

        Dim quan As Single
        If drCart.Read() Then
            quan = drCart.Item("Quantity")
        End If
        conn.Close()

        'strSQLStatement = "INSERT INTO Cart (CartNo, WebID, ProductName, Quantity, Price) values('" & strCartNo & "', '" & lblProductID.Text & "', '" & lblProductName.Text & "', " & CInt(tbQuantity.Text) & ", " & sngPrice & ")"
        'cmdSQL = New SqlCommand(strSQLStatement, conn)
        'conn.Open()
        'Response.Write(strSQLStatement)
        'drCart = cmdSQL.ExecuteReader(CommandBehavior.CloseConnection)
        'Response.Redirect("ViewCart.aspx")

        Dim strSQL As String
        If quan < 1 Then
            strSQL = "INSERT INTO Cart (CartNo, WebID, ProductName, Quantity, Price) values('" & strCartNo & "', '" & lblProductID.Text & "', '" & lblProductName.Text & "', " & CInt(tbQuantity.Text) & ", " & sngPrice & ")"

        Else
            quan += CInt(tbQuantity.Text)
            strSQL = "UPDATE Cart SET Quantity='" & quan & "' WHERE CartNo= '" & strCartNo & "' and  WebID = '" & lblProductID.Text & "'"
        End If

        cmdSQL = New SqlCommand(strSQL, conn)
        quan = 0
        conn.Open()
        Response.Write(strSQL)
        drCart = cmdSQL.ExecuteReader(CommandBehavior.CloseConnection)
        Response.Redirect("ViewCart.aspx")

    End Sub
    Public Function GetRandomCartNoUsingGUID(ByVal length As Integer) As String
        'Get the GUID
        Dim guidResult As String = System.Guid.NewGuid().ToString()
        'Remove the hyphens
        guidResult = guidResult.Replace("-", String.Empty)
        'Make sure length is valid
        If length <= 0 OrElse length > guidResult.Length Then
            Throw New ArgumentException("Length must be between 1 and " & guidResult.Length)
        End If
        'Return the first length bytes
        Return guidResult.Substring(0, length)
    End Function


End Class
