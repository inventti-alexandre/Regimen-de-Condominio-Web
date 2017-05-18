<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Consultar.aspx.cs" Inherits="Usuarios_Consultar"
    Async="true" %>

<%@ MasterType 
    virtualpath="~/Main.master"
%>


<asp:Content ID="Content1" ContentPlaceHolderID="MainBlock" Runat="Server">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="upPanel">
            <ContentTemplate>
                <div class="panel panel-default">
                    <div class="panel-heading">
                         <b> <span class="glyphicon glyphicon-user"></span> Usuarios</b>
                    </div>
                <div class="panel-body">
                        <div class="row" style="margin-bottom: 10px;">
                            <div class="col-md-4">
                        
                            </div>
                            <div class="col-md-4">
                                </div>
                            <div class="col-md-4 form-horizontal">
                                <asp:TextBox runat="server" CssClass="form-control rounded" placeholder="Buscar Usuario..." id="txtSearch" style="border-radius: 5px;" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged">
                                </asp:TextBox>                       
                            </div>
                        </div>
                        <div class="row">                    
                            <div class="col-md-12">                        
                                <asp:GridView runat="server" CssClass="table table-striped table-bordered" Width="100%"
                                    Height="50%" id="gridUsuarios" AutoGenerateColumns="false" AllowPaging ="true" PageSize="25"
                                     OnPageIndexChanging="gridUsuarios_PageIndexChanging" OnRowEditing="gridUsuarios_RowEditing">   
                                   <Columns>
                                       <asp:BoundField DataField="CLA_USUARIO" HeaderText="Usuario" ControlStyle-CssClass="form-control"/>                               
                                       <asp:BoundField DataField="CONTEO_DR" HeaderText="Cantidad UENs" ControlStyle-CssClass="form-control"/>                               
                                       <asp:BoundField DataField="CONTEO_FRACC" HeaderText="Cantidad Fraccionamientos" ControlStyle-CssClass="form-control"/>
                                       <asp:CommandField ShowEditButton="true" />        
                                   </Columns>      
                                    <PagerStyle CssClass="pagination-ys" />
                                    <PagerSettings FirstPageText="Inicio" LastPageText="Fin" Mode="NumericFirstLast" />
                                </asp:GridView>
                                <div class="alert alert-info" id="divAlert" visible="false" runat="server">
                                    <strong>Sin Registros</strong> Revisar criterio de búsqueda
                                </div>
                            </div>
                    
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</asp:Content>

