<%@ Page Title="" Language="C#" MasterPageFile="~/Start.master" 
    AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>


<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">    
    <img src="Recursos/Imagenes/layersM.png"
        class="center-block remove-float img-centrada top-space"/>
    <asp:label id="lblMessageError" Visible="false" Cssclass="text-center center-block" runat="server"></asp:label><br />
    <div class="col-md-4 remove-float center-block border-dv">
    <form id="form1" runat="server">            
        <div class="form-group">            
            <label for="usuario" class="text-center">Usuario</label>
            <asp:Textbox name="usuario" placeholder="Usuario" ToolTip="Ingresa Usuario de Active Directory" 
                 id="usuario" runat="server" CssClass="form-control img-en-izq-user rounded" style="border-radius:5px"/>            
            <asp:RequiredFieldValidator id="ValidateUser" runat="server"
                ControlToValidate="usuario"
                ErrorMessage="Se debe de ingresar usuario"
                ForeColor="Red">
            </asp:RequiredFieldValidator>
        </div>
        <div class="form-group">
            <label for="password" class="text-center">Contraseña</label>
            <asp:Textbox textMode="Password" name="password" placeholder="Contraseña"
                Cssclass="form-control img-en-izq-pass" id="password" runat="server"/>
            <asp:RequiredFieldValidator id="ValidatePassword" runat="server"
                ControlToValidate="password"
                ErrorMessage="Se debe de ingresar contraseña"
                ForeColor="Red">
                 </asp:RequiredFieldValidator>
        </div>

        <div class="row top-space text-center">
            <div class="col-xs-4 col-sm-6 centered">
                <input type="button" value="Iniciar sesión" class="btn btn-default text-center greenText bold"
                    runat="server" id="btnEnviarLogin" onserverclick="btnEnviarLogin_ServerClick" />                
            </div>
            <div class="col-xs-4 col-sm-6 centered"></div>
        </div>
    </form>   
        </div>
</asp:Content>

