Imports CC
Public Class CurrencyConverter
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnConvert_Click(sender As Object, e As EventArgs) Handles btnConvert.Click
        Dim objCC As ServiceReference = New ServiceReference()
        lblCurrencyOut.Text = objCC.CurrencyConvert(ddlcurrencytype.selecteItem.Value, tbCurrencyIn.Text)
    End Sub
End Class