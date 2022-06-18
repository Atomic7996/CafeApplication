<%@ Page Title="Главная" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="CafeWebApplication._Default" %>

    <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

        <div class="jumbotron">
            <div class="header">
                <img class="logo" src="/Images/cafe.png" alt="">
                <h1 class="title">Кафе</h1>
            </div>
                <p class="description">
                    Автоматизация бизнес-процессов кафе позволит сэкономить время и средства, затрачиваемые на заполнение  и поиск документов, проводить анализ продаж и работы кафе в целом, повысить удобство работы сотрудников и их производительность.
                </p>
        </div>


        <div class="home">
            <div class="wrapper">
                <h2 class="subtitle mb-5">Меню</h2>
                <div class="content">
                    <a class="link" href="/Products">Блюда</a>
                    <a class="link" href="/Combos">Наборы</a>
                </div>
                <hr>
                <h2 class="subtitle mb-5">Промокоды</h2>
                <section id="sidebar" class="sidebar">

                    <asp:ListView runat="server" ID="lvProducts" ItemType="ClassLibraryCafe.Coupon"
                        DataKeyNames="CouponID" SelectMethod="lvProducts_GetData1">
                        <ItemTemplate>
                            <div class="sidebar__item">
                                <div class="sidebar__item-left">
                                    <%# Eval("PromoCode") %>
                                        <span>Категория: <%# Eval("ProductType") %></span>
                                </div>
                                <div class="sidebar__item-right">
                                    Скидка <%# Eval("Sale") %>%
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:ListView>

                </section>
            </div>
        </div>























        <%--<div class="row">
            <div class="col-md-4">
                <h2>Getting started</h2>
                <p>
                    ASP.NET Web Forms lets you build dynamic websites using a familiar drag-and-drop, event-driven
                    model.
                    A design surface and hundreds of controls and components let you rapidly build sophisticated,
                    powerful UI-driven sites with data access.
                </p>
                <p>
                    <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301948">Learn more
                        &raquo;</a>
                </p>
            </div>
            <div class="col-md-4">
                <h2>Get more libraries</h2>
                <p>
                    NuGet is a free Visual Studio extension that makes it easy to add, remove, and update libraries and
                    tools in Visual Studio projects.
                </p>
                <p>
                    <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301949">Learn more
                        &raquo;</a>
                </p>
            </div>
            <div class="col-md-4">
                <h2>Web Hosting</h2>
                <p>
                    You can easily find a web hosting company that offers the right mix of features and price for your
                    applications.
                </p>
                <p>
                    <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301950">Learn more
                        &raquo;</a>
                </p>
            </div>
            </div>--%>

    </asp:Content>