<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" 
    CodeFile="Consultar.aspx.cs" Inherits="Reportes_Consultar" Async="true" %>

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
                         <b> <span class="glyphicon glyphicon-file"></span> Reportes Creados</b>
                    </div>
                    <div class="panel-body"> 
                        <div class="row" style="margin-bottom: 10px;">
                            <div class="col-md-4"></div>
                            <div class="col-md-4"></div>
                            <div class="col-md-4 form-horizontal">
                                <asp:TextBox runat="server" CssClass="form-control rounded" placeholder="Buscar..." id="txtSearch" style="border-radius: 5px;" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged">
                                </asp:TextBox>                       
                            </div>
                        </div>
                        <div class="row">                    
                            <div class="col-md-12">
                            <asp:GridView runat="server" CssClass="table table-striped table-bordered" Width="100%"
                                Height="50%" id="gridReportes" AutoGenerateColumns="false" AllowPaging ="true" PageSize="10"
                                 OnPageIndexChanging="gridReportes_PageIndexChanging" OnRowEditing="gridReportes_RowEditing">   
                               <Columns>
                                   <asp:BoundField DataField="ID_MACHOTE" HeaderText="ID" ControlStyle-CssClass="form-control"/>
                                   <asp:BoundField DataField="ENC_MACHOTE" HeaderText="Encabezado" ControlStyle-CssClass="form-control"/>
                                   <asp:BoundField DataField="CANT_VIVS" HeaderText="Cantidad de Viviendas" ControlStyle-CssClass="form-control"/>                           
                                   <asp:BoundField DataField="NOM_DR" HeaderText="UEN" ControlStyle-CssClass="form-control"/>                           
                                   <asp:CheckBoxField DataField="ESTATUS_MACHOTE" HeaderText="Estatus"/>
                                   <asp:CommandField ShowEditButton="true" EditText="Descargar" />                                           
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

