<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Editar.aspx.cs" Inherits="Machotes_Editar" Async="true" %>

<%@ MasterType 
    virtualpath="~/Main.master"
%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainBlock" Runat="Server">
<script type="text/javascript">
    function insertAtCaret(areaId) {
        
        var txtarea = document.getElementById(areaId);        

        if (!txtarea) { return; }                 

        var SelcV = function (areaId) {
            if (areaId === 'txtDescripcion') {
                return document.getElementById("<%=ddlDescVars.ClientID%>").options[document.getElementById("<%=ddlDescVars.ClientID%>").selectedIndex].value;
            }
            else if (areaId === 'txtAreaPP') {
                return document.getElementById("<%=ddlPPVars.ClientID%>").options[document.getElementById("<%=ddlPPVars.ClientID%>").selectedIndex].value;
            }
            else if (areaId === 'txtAreaPC') {
                return document.getElementById("<%=ddlPCVars.ClientID%>").options[document.getElementById("<%=ddlPCVars.ClientID%>").selectedIndex].value;
            }
            else {
                return '';
            }
        }        

        var text = "[" + SelcV(areaId).toString() + "]";

        var scrollPos = txtarea.scrollTop;
        var strPos = 0;
        var br = ((txtarea.selectionStart || txtarea.selectionStart == '0') ?
			"ff" : (document.selection ? "ie" : false));
        if (br == "ie") {
            txtarea.focus();
            var range = document.selection.createRange();
            range.moveStart('character', -txtarea.value.length);
            strPos = range.text.length;
        } else if (br == "ff") {
            strPos = txtarea.selectionStart;
        }

        var front = (txtarea.value).substring(0, strPos);
        var back = (txtarea.value).substring(strPos, txtarea.value.length);
        txtarea.value = front + text + back;
        strPos = strPos + text.length;
        if (br == "ie") {
            txtarea.focus();
            var ieRange = document.selection.createRange();
            ieRange.moveStart('character', -txtarea.value.length);
            ieRange.moveStart('character', strPos);
            ieRange.moveEnd('character', 0);
            ieRange.select();
        } else if (br == "ff") {
            txtarea.selectionStart = strPos;
            txtarea.selectionEnd = strPos;
            txtarea.focus();
        }

        txtarea.scrollTop = scrollPos;
    }   
</script>
        <div class="panel panel-default">
            <div class="panel-heading">
                 <b><asp:Label runat="server" ID="headText" Text="Nuevo Machote"></asp:Label></b>
            </div>
            <div class="panel-body">               			                               
                <div class="card">
                    <form runat="server">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                    <ul class="nav nav-tabs" role="tablist">
                        <li role="presentation" class="active"><a href="#Encabezado" aria-controls="Encabezado" role="tab" data-toggle="tab">Datos generales</a></li>
                        <li role="presentation"><a href="#Cuerpo" aria-controls="Cuerpo" role="tab" data-toggle="tab">Descriptivo</a></li>                    
                        <li class="pull-right"><input type="submit" class="btn btn-primary" value="Enviar" id="btnEnviar" runat="server" onserverclick="btnEnviar_ServerClick"/></li>
                    </ul>
                    <!-- Tab panes -->
                    
                        <div class="tab-content">
                            <div role="tabpanel" class="tab-pane active" id="Encabezado">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-2" for="txtPrototipo">Prototipo</label>
                                        <div class="col-sm-8">
                                            <input type="text" class="form-control" id="txtPrototipo" runat="server" style="border-radius:5px;" required="required" placeholder="Ingresa Valor"/>                                            
                                            <asp:RequiredFieldValidator ID="rfvSubject" ControlToValidate="txtPrototipo" 
                                                ErrorMessage="Se debe de ingresar Prototipo" runat="server" 
                                                ForeColor="Red" Display="Dynamic"/>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-2" for="ddlCantVivs"># Viviendas</label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList runat="server" 
                                                 DataTextField ="DESCRIPCION"
                                                 DataValueField ="CANT_VIVS"
                                                CssClass="form-control" id="ddlCantVivs"/>                                                                    
                                             <asp:RequiredFieldValidator InitialValue="0" ID="Req_ID" Display="Dynamic"
                                                runat="server" ForeColor="Red" ControlToValidate="ddlCantVivs"
                                                Text="Favor de seleccionar" ErrorMessage="ErrorMessage"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-2" for="ddlUEN">UEN</label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList runat="server" 
                                                 DataTextField ="NOM_DR"
                                                 DataValueField ="ID_DR_SEMBRADO"
                                                CssClass="form-control" id="ddlUEN"/> 
                                             <asp:RequiredFieldValidator InitialValue="0" ID="RequiredFieldValidator4" Display="Dynamic"
                                                runat="server" ForeColor="Red" ControlToValidate="ddlUEN"
                                                Text="Favor de seleccionar" ErrorMessage="ErrorMessage"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">   
                                        <div class="col-md-2"></div>                     
                                        <div class="col-md-8">
                                            <div>
                                                <input type="checkbox" id="cActivo" runat="server" value="" checked="checked"/>
                                                <label for="cActivo">Activo</label>
                                            </div>                                                                               
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div role="tabpanel" class="tab-pane" id="Cuerpo">
                                <div class="panel" >
                                    <div class="panel-heading clearfix">                                        
                                        <h4 class="pull-left text-primary">Descripción</h4>
                                        <div class="btn-group pull-right">
                                            <div class="form-inline">
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlDescVars" ClientIDMode="Static"
                                                DataTextField="NOM_VAR" DataValueField="NOM_CORTO">                                                
                                            </asp:DropDownList>
                                            <a class="btn btn-default greenText bold" runat="server" id="btnAddDesc" 
                                                onclick="insertAtCaret('txtDescripcion');return false;" >
                                                Agregar <span class="glyphicon glyphicon-plus"></span>
                                            </a>                                            
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-body" style="border-color: lightgray;">
                                        <div class="form-group">
                                           <asp:TextBox runat="server" class="form-control" ClientIDMode="Static"
                                               style="border-radius:5px;" id="txtDescripcion" required="required" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtDescripcion" 
                                                ErrorMessage="Se debe de ingresar Descripción" runat="server" 
                                                ForeColor="Red" Display="Dynamic"/>
                                        </div>                            
                                    </div>
                                </div> 
                                <div class="panel" >
                                    <div class="panel-heading clearfix">
                                        <h4 class="pull-left text-primary">Propiedad Privada</h4>
                                        <div class="btn-group pull-right">
                                            <div class="form-inline">
                                                <asp:UpdatePanel runat="server" ID="updPanel">
                                                    <ContentTemplate>
                                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlPPTipoBloque"
                                                            DataValueField="ID_TIPO_BLOQUE" DataTextField="NOM_TIPO_BLOQUE" OnSelectedIndexChanged="ddlPPTipoBloque_SelectedIndexChanged" AutoPostBack="true">                                                    
                                                        </asp:DropDownList>
                                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlPPVars"
                                                             DataTextField="NOM_VAR" DataValueField="NOM_CORTO">                                                    
                                                        </asp:DropDownList>
                                                        <a class="btn btn-default greenText bold" onclick="insertAtCaret('txtAreaPP');return false;">Agregar <span class="glyphicon glyphicon-plus"></span></a>                                            
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-body" style="border-color: lightgray;">
                                        <div class="form-group">
                                           <asp:TextBox TextMode="MultiLine" id="txtAreaPP" runat="server" ClientIDMode="Static"
                                               style="Height:300px;"  class="form-control" required="required" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtAreaPP" 
                                                ErrorMessage="Se debe de ingresar Bloque de Propiedad Privada" runat="server" 
                                                ForeColor="Red" Display="Dynamic"/>
                                        </div>                            
                                    </div>                                    
                                </div>
                                <div class="panel" >
                                    <div class="panel-heading clearfix">
                                        <h4 class="pull-left text-primary">Propiedad Común</h4>
                                        <div class="btn-group pull-right">
                                            <div class="form-inline">
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlPCVars"
                                                DataTextField="NOM_VAR" DataValueField="NOM_CORTO">                                                
                                            </asp:DropDownList>
                                            <a href="#" class="btn btn-default greenText bold" onclick="insertAtCaret('txtAreaPC');return false;">Agregar <span class="glyphicon glyphicon-plus"></span></a>                                            
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-body" style="border-color: lightgray;">
                                        <div class="form-group">
                                           <asp:TextBox id="txtAreaPC" TextMode="MultiLine" ClientIDMode="Static"
                                               runat="server" style="Height:200px;"   CssClass="form-control" required="required"/>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtAreaPP" 
                                                ErrorMessage="Se debe de ingresar Bloque de Bienes de Propiedad Común" runat="server" 
                                                ForeColor="Red" Display="Dynamic"/>
                                        </div>                            
                                    </div>                                    
                                </div>
                            </div>                        
                        </div>
                    </form>              
                </div>
            </div>
            <div class="panel-footer">
                <b> <span class="glyphicon glyphicon-book"></span> Machote</b>                
            </div>
        </div>
</asp:Content>

