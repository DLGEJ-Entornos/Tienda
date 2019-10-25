<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageAdm.Master" AutoEventWireup="true" CodeBehind="PedidosPorCliente.aspx.cs" Inherits="GesTienda.PedidosPorCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="InfoContenido" runat="server">
    <h1>Pedidos realizados por los clientes</h1>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [IdCliente], [NomCli], [PobCli], [CorCli] FROM [CLIENTE]"></asp:SqlDataSource>
    <asp:GridView ID="grdClientes" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="IdCliente" DataSourceID="SqlDataSource1" OnPageIndexChanged="grdClientes_PageIndexChanged" OnSelectedIndexChanged="grdClientes_SelectedIndexChanged">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="IdCliente" HeaderText="Id Cliente" ReadOnly="True" SortExpression="IdCliente" />
            <asp:BoundField DataField="NomCli" HeaderText="Nombre" SortExpression="NomCli" />
            <asp:BoundField DataField="PobCli" HeaderText="Población" SortExpression="PobCli" />
            <asp:BoundField DataField="CorCli" HeaderText="Corrreo electrónico" SortExpression="CorCli" />
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerSettings FirstPageText="Primero" LastPageText="Último" Mode="NextPreviousFirstLast" NextPageText="Siguiente" PreviousPageText="Anterior" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>

    <asp:Label ID="lblResultado" runat="server" Text=""></asp:Label> <br />
    <asp:Label ID="lblMensajes" runat="server" Text=""></asp:Label> <br />
    <asp:Label ID="lblTotal" runat="server" Text="lblTotal" Visible="False"></asp:Label>
</asp:Content>
