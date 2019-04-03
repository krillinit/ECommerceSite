<%@ Page Language="VB" MasterPageFile="~/Layout.master" AutoEventWireup="false" CodeFile="Payment.aspx.vb" Inherits="Payment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" Runat="Server">
    <form id="form1" runat="server">   
        <div class="jumbotron">
            <h3 class="display-3 text-center">
                Payment Center
            </h3>
            <div class="container"> <asp:TextBox ID="tb1" runat="server"></asp:TextBox>
            <asp:Button ID="btnSubmit" runat="server" Text="Purchase" />

            </div>
           
        </div>
    </form>
</asp:Content>
