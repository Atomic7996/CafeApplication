<%@ Page Title="Наборы" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Combos.aspx.cs" Inherits="CafeWebApplication.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:ListView runat="server" ID="lvCombos" 
        ItemType="ClassLibraryCafe.Combo" DataKeyNames="ComboID"
        SelectMethod="lvCombos_GetData">
        <ItemTemplate>
            <span>
                <asp:Image runat="server" ImageUrl='<%# Eval("ValidImage") %>' Width="100" Height="100"/>
                <b>
                    <%# Eval("Title") %>
                </b> <br />
                <small>
                    Состав - <%# Eval("ProductList") %> <br />
                    Цена: <%# Eval("Cost") %> руб. <br />
                </small> <hr /> <br />
            </span>

        </ItemTemplate>
    </asp:ListView>
    
    
    
    
    
    
    
    
    
    
    
    
    <%--<h2><%: Title %>.</h2>
    <h3>Your contact page.</h3>
    <address>
        One Microsoft Way<br />
        Redmond, WA 98052-6399<br />
        <abbr title="Phone">P:</abbr>
        425.555.0100
    </address>

    <address>
        <strong>Support:</strong>   <a href="mailto:Support@example.com">Support@example.com</a><br />
        <strong>Marketing:</strong> <a href="mailto:Marketing@example.com">Marketing@example.com</a>
    </address>--%>
</asp:Content>
