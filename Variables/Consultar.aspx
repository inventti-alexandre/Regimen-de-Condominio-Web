<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" 
    CodeFile="Consultar.aspx.cs" Inherits="Variables_Consultar"  Async="true"%>

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
                 <b> <span class="glyphicon glyphicon-text-background"></span> Variables</b>
            </div>
            <div class="panel-body">
                <div class="row" style="margin-bottom: 10px;">
                    <div class="col-md-4">
                        <asp:LinkButton runat="server" PostBackUrl="~/Variables/Editar.aspx" CssClass="btn btn-primary d-inline"><span class="glyphicon glyphicon-plus-sign"></span> Nueva</asp:LinkButton>
                    </div>
                    <div class="col-md-4">
                        </div>
                    <div class="col-md-4 form-horizontal">
                        <asp:TextBox runat="server" CssClass="form-control rounded" placeholder="Buscar..." id="txtSearch" style="border-radius: 5px;" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged">
                        </asp:TextBox>                       
                    </div>
                </div>
                <div class="row">                    
                    <div class="col-md-12">
                    <asp:GridView runat="server" CssClass="table table-striped table-bordered" Width="100%"
                        Height="50%" id="gridVariables" AutoGenerateColumns="false" AllowPaging ="true" PageSize="10" AllowSorting="true"
                         OnRowEditing="gridVariables_RowEditing" OnPageIndexChanging="gridVariables_PageIndexChanging">   
                       <Columns>
                           <asp:BoundField DataField="ID_VAR" HeaderText="ID" ControlStyle-CssClass="form-control"/>
                           <asp:BoundField DataField="NOM_CORTO" HeaderText="Nombre Corto" ControlStyle-CssClass="form-control"/>
                           <asp:BoundField DataField="NOM_VAR" HeaderText="Descripción" ControlStyle-CssClass="form-control"/>
                           <asp:BoundField DataField="VALOR" HeaderText="Valor" ControlStyle-CssClass="form-control"/>
                           <asp:BoundField DataField="NOM_TIPO_BLOQUE" HeaderText="Bloque" ControlStyle-CssClass="form-control"/>
                           <asp:BoundField DataField="DESC_UNIDAD" HeaderText="Unidad" ControlStyle-CssClass="form-control"/>
                           <asp:CheckBoxField DataField="ES_CALCULADO" HeaderText="Calculado" />                                                     
                           <asp:CheckBoxField DataField="CONV_LETRA" HeaderText="A Letra" />
                           <asp:CheckBoxField DataField="ESTATUS" HeaderText="Estatus"/>
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
<!--/.Panel-->   

</asp:Content>

