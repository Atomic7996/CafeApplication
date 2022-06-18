<%@ Page Title="Блюда" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs"
    Inherits="CafeWebApplication.About" %>

    <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <div style="text-align: center; font-size: 50px; margin: 30px;">Блюда</div>
        <section style="display: grid; grid-template-columns: 1fr 1fr;">
            <asp:ListView runat="server" ID="lvProducts" ItemType="ClassLibraryCafe.Product" DataKeyNames="ProductID"
                SelectMethod="lvProducts_GetData">
                <ItemTemplate>

                    <div class="card text-white bg-dark mb-3"
                        style="width: 18rem; margin: 0 auto; border-radius: 10px;">
                        <div class="card-img-top"
                            style="background-color: #fff; border-radius: 10px 10px 0 0; display: flex; justify-content: center; padding: 5px;">
                            <asp:Image runat="server" ImageUrl='<%# Eval("ValidImage") %>' Width="200" Height="200" />
                        </div>
                        <div class="card-body">
                            <h5 class="card-title">
                               <b> <%# Eval("ProductType") %> | <%# Eval("Title") %></b>
                            </h5>
                            <p class="card-text">
                            <p style="margin: 0;"><b>Описание</b> - <%# Eval("Description") %>
                            </p>
                            <p style="margin: 0;"><b>Состав</b> - <%# Eval("FoodStaffList") %>
                            </p>
                            <p style="margin: 0;"><b>Стоимость</b> -  <%# Eval("Cost") %> ₽</p>
                            </p>
                        </div>

                    </div>
                </ItemTemplate>
            </asp:ListView>
        </section>
    </asp:Content>