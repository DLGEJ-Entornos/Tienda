<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageAdm.Master" AutoEventWireup="true" CodeBehind="ProductosMantener.aspx.cs" Inherits="GesTienda.ProductosMantener" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body{
            text-align:center;
            /*margin: 0 auto;*/
        }
        #dProd{
            display: inline;
            float: left;
            min-width: 40%;
            max-width: 60%;
            height:300px;
            border: 1px solid green;
            padding: -50%;
        }
        /*#grdProductos{
            width:150%;
        }*/
        #form{
            /*display: inline;*/
            float: right;
            width:400px;
            height:200px;
            border: 1px solid red;
            margin-right: 20%;

            /*margin: 0 auto;*/
        }
        
        #form label{
            /*display: inline;*/
            /*text-align: right;*/
            /*top: -3px;*/
            /*float: left;
            vertical-align:middle;*/
        }
        .box{
            /*display: inline-block;*/
            margin-top: 10px;
            /*text-align: left;
            float: right;*/
            width: 40%;
            /*float:right;*/
        }
        #txtDesPro{
            width:60%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="InfoContenido" runat="server">
    <h1>Mantenimiento de Productos</h1>

    <asp:Label ID="lblResultado" runat="server" Text=""></asp:Label> <br />
    <asp:Label ID="lblMensajes" runat="server" Text=""></asp:Label>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [IdProducto], [DesPro], [PrePro] FROM [PRODUCTO]"></asp:SqlDataSource>
    <div id="dProd">
        <asp:GridView ID="grdProductos" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="IdProducto" DataSourceID="SqlDataSource1">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="IdProducto" HeaderText="IdProducto" ReadOnly="True" SortExpression="IdProducto" />
                <asp:BoundField DataField="DesPro" HeaderText="DesPro" SortExpression="DesPro" />
                <asp:BoundField DataField="PrePro" HeaderText="PrePro" SortExpression="PrePro" />
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
    </div>

    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [UNIDAD]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [TIPO]"></asp:SqlDataSource>

    <div id="form">
        <asp:Label ID="lblIdProducto" runat="server" Text="Id. Producto: "></asp:Label>
        <asp:TextBox ID="txtIdProducto" CssClass="box" Enabled="false" runat="server"></asp:TextBox> <br />

        <asp:Label ID="lblDesPro" runat="server" Text="Descripción: "></asp:Label>
        <asp:TextBox ID="txtDesPro" CssClass="box" Enabled="false" runat="server"></asp:TextBox> <br />

        <asp:Label ID="lblPrePro" runat="server" Text="Precio: "></asp:Label>
        <asp:TextBox ID="txtPrePro" CssClass="box" Text="0" Enabled="false" runat="server"></asp:TextBox> <br />

        <asp:Label ID="lblIdUnidad" runat="server" Text="Unidad: "></asp:Label>
        <asp:DropDownList ID="ddlIdUnidad" CssClass="box" Enabled="False" runat="server" DataSourceID="SqlDataSource2" DataTextField="IdUnidad" DataValueField="IdUnidad"></asp:DropDownList> <br />

        <asp:Label ID="lblIdTipo" runat="server" Text="Tipo Producto: "></asp:Label>
        <asp:DropDownList ID="ddlIdTipo" CssClass="box" Enabled="False" runat="server" DataSourceID="SqlDataSource3" DataTextField="DesTip" DataValueField="IdTipo"></asp:DropDownList> <br />

        <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" Visible="True" />
        <asp:Button ID="btnEditar" runat="server" Text="Editar" Visible="False" />
        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" Visible="False" />
        <asp:Button ID="btnInsertar" runat="server" Text="Insertar" Visible="False" />
        <asp:Button ID="btnModificar" runat="server" Text="Modificar" Visible="False" />
        <asp:Button ID="btnBorrar" runat="server" Text="Borrar" Visible="False" />
        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" Visible="False" />

    </div>

</asp:Content>
