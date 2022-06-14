<%@ Page Title="Блюда" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="CafeWebApplication.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Блюда</h2>

    <asp:ListView runat="server" ID="lvProducts" 
        ItemType="ClassLibraryCafe.Product" DataKeyNames="ProductID"
        SelectMethod="lvProducts_GetData">
        <ItemTemplate>
            <span>
                <asp:Image runat="server" ImageUrl='<%# Eval("ValidImage") %>' Width="100" Height="100"/>
                <b>
                    <%# Eval("ProductType") %> | <%# Eval("Title") %>
                </b> <br />
                <small>
                    Описание - <%# Eval("Description") %> <br />
                    Состав - <%# Eval("FoodStaffList") %> <br />
                    Стоимость: <%# Eval("Cost") %> руб. <br />
                </small> <hr /> <br />
            </span>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
