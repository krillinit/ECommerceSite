<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Layout.master" CodeBehind="CurrencyConverter.aspx.vb" Inherits="WebApplication4.CurrencyConverter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <form>
  <div>
    <asp:TextBox ID="tbCurrencyIn" runat="server"></asp:TextBox>
    <br />
    <asp:DropDownList ID="ddlcurrencytype" runat="server">
        <asp:ListItem Selected="True" Value="ca">can</asp:ListItem>
        <asp:ListItem Value="jp">JP</asp:ListItem>

    </asp:DropDownList>
    <br/>
    <asp:Button ID="btnConvert"  runat="server" Text="convert" />
    <br />
    <asp:Label ID="lblCurrencyOut" runat="server" Text=""></asp:Label>
        </div>
          </form>
</asp:Content>
