using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.ComponentModel;
using System.Configuration;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session[Constant.KeyUserSession] = null;
        Session[Constant.KeyEsAdmin] = null;
    }

    protected void btnEnviarLogin_ServerClick(object sender, EventArgs e)
    {
        bool isAuthenticated, isLocked;

        string  vUser = (usuario.Text ?? ""),
                vPassword = (password.Text ?? ""),
                vOutErrorLock = "",
                vOutErrorAuth = "",
                DefaultDomain = ConfigurationManager.AppSettings["Domain"];

        lblMessageError.Visible = false;

        isLocked = ActiveDirectory.IsLocked(vUser, DefaultDomain, out vOutErrorLock);        

        if (!isLocked)
        {
            isAuthenticated = ActiveDirectory.Authenticate(vUser, vPassword, DefaultDomain, out vOutErrorAuth);
            
            if (isAuthenticated)
            {
                string vName = ActiveDirectory.GetName(vUser, DefaultDomain);
                lblMessageError.ForeColor = System.Drawing.Color.Green;
                lblMessageError.Visible = true;
               
                Session[Constant.KeyUserSession] = vUser + "-" + vName;
                
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                if (vOutErrorAuth != "")
                    lblMessageError.Text = "Error en Login: " + vOutErrorAuth;
                else
                    lblMessageError.Text = "El usuario o la contraseña es incorrecto, intentar de nuevo";

                lblMessageError.ForeColor = System.Drawing.Color.Red;
                lblMessageError.Visible = true;
            }            
        }
        else
        {
            if (string.IsNullOrWhiteSpace(vOutErrorLock))
                lblMessageError.Text = "Su cuenta de red esta bloqueada";
            else
                lblMessageError.Text = vOutErrorLock;

            lblMessageError.ForeColor = System.Drawing.Color.Red;
            lblMessageError.Visible = true;
        }

        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);

    }
}