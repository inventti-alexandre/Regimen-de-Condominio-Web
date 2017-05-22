<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Editar.aspx.cs" Inherits="Usuarios_Editar" 
    Async="true" %>

<%@ MasterType 
    virtualpath="~/Main.master"
%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainBlock" Runat="Server">
    <div class="panel panel-default center_div">
            <div class="panel-heading">               
                <b><asp:Label runat="server" ID="headVar" Text="Usuario no encontrado"></asp:Label></b>                                                                         
            </div>
            <div class="panel-body">

              <form class="form-horizontal" runat="server"> 
                  <asp:ScriptManager runat="server"></asp:ScriptManager>                                    
                  <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                    <div class="panel" >
                        <div class="panel-heading">
                            <h4 runat="server" id="headUsuario" class="text-primary">Edición</h4> 
                            
                        </div>
                        <div class="panel-body" style="border-color: lightgray;">
                            <div class="form-group">
                                <label class="control-label col-sm-2" for="ddlDRSembrado">UEN</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList runat="server" 
                                         DataTextField ="NOM_DR"
                                         DataValueField ="VALORES_DR"
                                        CssClass="form-control" id="ddlDRSembrado" OnSelectedIndexChanged="ddlDRSembrado_SelectedIndexChanged" AutoPostBack="true"/>                          
                                </div>
                            </div>
                            <div class="form-group">   
                                <div class="col-md-2"></div>                     
                                <div class="col-md-8">
                                    <div>
                                        <input type="checkbox" id="cActivo" runat="server" value=""/>
                                        <label for="cActivo" style="font-weight: normal;">Activo</label>
                                    </div>
                                    <div>
                                        <input type="checkbox" id="cSincroniza" runat="server"/>
                                        <label for="cSincroniza" style="font-weight: normal;">Sincronizado con Sembrado</label>                                
                                    </div>                                    
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-2"></div>
                                <div class="col-sm-8">
                                    <asp:Button runat="server" CssClass="btn btn-primary" Text="Actualizar" ID="btnEnviar" OnClick="btnEnviar_Click"/>                                    
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-2"></div>
                                <div class="col-sm-8">                                    
                                    <asp:TextBox runat="server" CssClass="form-control pull-right" Width="30%" placeholder="Buscar..." id="txtSearch" style="border-radius: 5px;" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged">
                                    </asp:TextBox>                            
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-sm-2">Fraccionamientos</label>
                                <div class="col-sm-8">
                                    <asp:GridView runat="server" ID="gridFraccs" CssClass="table table-hover table-bordered table-responsive" Width="100%"
                                    Height="50%" AutoGenerateColumns="false" AllowPaging ="true" PageSize="10" OnPageIndexChanging="gridFraccs_PageIndexChanging" AllowSorting="true"> 
                                        <Columns>
                                            <asp:BoundField DataField="ID_SEMBRADO" HeaderText="Id" ControlStyle-CssClass="form-control"/>                               
                                            <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" ControlStyle-CssClass="form-control"/>
                                        </Columns>                                                           
                                        <PagerStyle CssClass="pagination-ys" />
                                        <PagerSettings FirstPageText="Inicio" LastPageText="Fin" Mode="NumericFirstLast" />                       
                                    </asp:GridView>
                                    <div class="alert alert-info text-center" id="divAlert" runat="server">
                                        <strong>Sin Registros</strong> Revisar criterio de búsqueda
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>                                      
                    </ContentTemplate>
                  </asp:UpdatePanel>
                </form>
            </div>
            <div class="panel-footer">
                <b> <span class="glyphicon glyphicon-user"></span> Usuarios</b>                
            </div>
        </div>
</asp:Content>

