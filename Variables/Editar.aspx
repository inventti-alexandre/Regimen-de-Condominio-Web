<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" 
    CodeFile="Editar.aspx.cs" Inherits="Variables_Editar" Async="true" %>

<%@ MasterType virtualpath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainBlock" Runat="Server">       
    <div class="panel panel-default center_div">
            <div class="panel-heading">               
                <b><asp:Label runat="server" ID="headVar" Text="Nueva Variable"></asp:Label></b>
            </div>
            <div class="panel-body">
              <form class="form-horizontal" runat="server">
                    <div class="form-group">
                        <label class="control-label col-sm-2" for="txtIdVar">ID</label>
                        <div class="col-sm-2">
                            <input type="number" class="form-control" id="txtIdVar"  runat="server" disabled="disabled"/>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-2" for="txtNomCorto">Nombre Corto</label>
                        <div class="col-sm-8">
                            <input type="text" class="form-control" id="txtNomCorto" runat="server" style="border-radius:5px;" required="required" placeholder="Ingresa Valor"/>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-2" for="txtNomVariable">Descripción</label>
                        <div class="col-sm-8">
                            <input type="text" class="form-control" id="txtNomVariable" runat="server" style="border-radius:5px;" required="required" placeholder="Ingresa Valor"/>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-2" for="txtValor">Valor</label>
                        <div class="col-sm-8">
                            <input type="text" class="form-control" id="txtValor" runat="server" style="border-radius:5px;" placeholder="Ingresa Valor"/>
                            <asp:RequiredFieldValidator ID="rfvSubject" ControlToValidate="txtValor" ErrorMessage="Se debe de ingresar valor" runat="server" ForeColor="Red" Display="Dynamic"/>
                        </div>
                    </div>
                    <div class="form-group">   
                        <div class="col-md-2"></div>                     
                        <div class="col-md-8">
                            <div>
                                <input type="checkbox" id="cActivo" runat="server" value=""/>
                                <label for="cActivo">Activo</label>
                            </div>
                            <div>
                                <input type="checkbox" id="cCalculado" runat="server" disabled="disabled"/>
                                <label for="cCalculado">Calculado</label>                                
                            </div>
                            <div>
                                <input type="checkbox" id="cConvLetra" runat="server" value=""/>
                                <label for="cConvLetra">Convertir a Letra</label>                            
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-2" for="cUnidad">Unidad</label>
                        <div class="col-sm-8">
                            <asp:DropDownList runat="server" 
                                 DataTextField ="DESC_UNIDAD"
                                 DataValueField ="ID_UNIDAD"
                                CssClass="form-control" id="cUnidad"/>                            

                            <asp:RequiredFieldValidator InitialValue="0" ID="Req_ID" Display="Dynamic"
                                runat="server" ForeColor="Red" ControlToValidate="cUnidad"
                                Text="Favor de seleccionar Unidad" ErrorMessage="ErrorMessage"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-2" for="cTipoBloque">Tipo de Bloque</label>
                        <div class="col-sm-8">
                            <asp:DropDownList runat="server" 
                                 DataTextField ="NOM_TIPO_BLOQUE"
                                 DataValueField ="ID_TIPO_BLOQUE"
                                CssClass="form-control" id="cTipoBloque"/> 
                            <asp:RequiredFieldValidator InitialValue="0" ID="RequiredFieldValidator1" Display="Dynamic"
                                runat="server" ForeColor="Red" ControlToValidate="cTipoBloque"
                                Text="Favor de seleccionar Tipo de Bloque" ErrorMessage="ErrorMessage"></asp:RequiredFieldValidator>                         
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-2"></div>
                        <div class="col-sm-8">
                            <asp:Button runat="server" CssClass="btn btn-primary" Text="Enviar" ID="btnEnviar" OnClick="btnEnviar_Click"/>
                            <asp:Button runat="server" CssClass="btn btn-danger" Text="Cancelar" ID="btnCancelar" OnClick="btnCancelar_Click"/>
                        </div>
                    </div>
                  
                </form>
            </div>
            <div class="panel-footer">
                <b> <span class="glyphicon glyphicon-text-background"></span> Variables</b>                
            </div>
        </div>
</asp:Content>

