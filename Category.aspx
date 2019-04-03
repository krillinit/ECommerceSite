<%@ Page Title="" Language="VB" MasterPageFile="~/Layout.master" AutoEventWireup="false" CodeFile="Category.aspx.vb" Inherits="Category" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" Runat="Server">
    <div class="col-sm-3">
	<div class="left-sidebar">
		<h2><asp:Label ID="lblCategory" runat="server" Text=""></asp:Label></h2>
		<div class="panel-group category-products" id="accordian"><!--category-productsr-->
	        <asp:SqlDataSource ID="SqlDSSubCategory" runat="server" 
		        ConnectionString="<%$ ConnectionStrings:ConnectionStringOnlineStore %>" 
		        SelectCommand=""></asp:SqlDataSource>
	        <asp:DataList ID="dlSubCategory" runat="server" DataSourceID="SqlDSSubCategory" 
		        RepeatDirection="Vertical">
		        <ItemTemplate>
			        <div class="panel panel-default">
				        <div class="panel-heading">
					        <h4 class="panel-title"><a href="Category.aspx?MainCategoryID=<%# CStr(Eval("Parent")) %>&MainCategoryName=<% = Request.QueryString("MainCategoryName") %>&SubCategoryID=<%# CStr(Eval("ID")) %>&SubCategoryName=<%# Eval("CategoryName") %>"><%# Eval("CategoryName") %></a></h4>
				        </div>
			        </div>			                           
		        </ItemTemplate>
	        </asp:DataList>


		</div><!--/category-productsr-->
					

	</div>
</div>
				
    <div class="col-sm-9 padding-right">
	    <div class="features_items"><!--features_items-->
		    <h2 class="title text-center"><asp:Label ID="lblProductList" runat="server" Text="Featured Products"></asp:Label></h2>
	        <asp:SqlDataSource ID="SqlDSProductList" runat="server" 
		        ConnectionString="<%$ ConnectionStrings:ConnectionStringOnlineStore %>" 
		        SelectCommand=""></asp:SqlDataSource>
	        <asp:Repeater ID="rptFeaturedProduct" runat="server" DataSourceID="SqlDSProductList" Visible="True">
		        <ItemTemplate>
                <div class="col-sm-4">
			        <div class="product-image-wrapper">
				    <div class="single-products">
					    <div class="productinfo text-center">
						    <img src="images/product/<%# Trim(Eval("WebID")) %>.jpg" width="150" alt="" />
						    <h2>$<%# Eval("Price") %></h2>
						    <p><a href="ProductDetail.aspx?ProductID=<%# cstr(Eval("ID")) %>"><%# Eval("ProductName") %></a></p>
						    <a href="#" class="btn btn-default add-to-cart"><i class="fa fa-shopping-cart"></i>Add to cart</a>
					    </div>
				    </div>
				    <div class="choose">
					    <ul class="nav nav-pills nav-justified">
						    <li><a href=""><i class="fa fa-plus-square"></i>Add to wishlist</a></li>
						    <li><a href=""><i class="fa fa-plus-square"></i>Add to compare</a></li>
					    </ul>
				    </div>
			    </div>
		        </div>			                           
		        </ItemTemplate>
	        </asp:Repeater>		    
            
        
		   
						
		    <ul class="pagination">
			    <li class="active"><a href="">1</a></li>
			    <li><a href="">2</a></li>
			    <li><a href="">3</a></li>
			    <li><a href="">&raquo;</a></li>
		    </ul>
	    </div><!--features_items-->
    </div>

</asp:Content>

