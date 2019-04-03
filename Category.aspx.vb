
Partial Class Category
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Request.QueryString("MainCategoryID") <> "" Then
            ' display sub categories
            SqlDSSubCategory.SelectCommand = "Select * From Category Where Parent = " & CInt(Request.QueryString("MainCategoryID"))
            dlSubCategory.DataBind()
            ' display main category name
            lblCategory.Text = Request.QueryString("MainCategoryName")
            If Request.QueryString("SubCategoryID") <> "" Then
                ' display all products of the sub category
                SqlDSProductList.SelectCommand = "Select * From Product Where SubCategoryID = " & CInt(Request.QueryString("SubCategoryID"))
                rptFeaturedProduct.DataBind()
                lblProductList.Text = "Produc List of " + Request.QueryString("SubCategoryName")
            Else
                ' display featured products of main category
                SqlDSProductList.SelectCommand = "Select * From Product Where MainCategoryID = " & CInt(Request.QueryString("MainCategoryID")) & " and FeaturedMainCategory = 'Y'"
                rptFeaturedProduct.DataBind()
            End If

            'Response.Write(SqlDSFeaturedProduct.SelectCommand)
        End If
    End Sub
End Class
