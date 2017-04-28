using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;



public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session[Constant.KeyUserSession] != null)
        {
            Response.Redirect("~/Default.aspx");
        }
    }

    protected void btnEnviarLogin_ServerClick(object sender, EventArgs e)
    {
        bool isAuthenticated, isLocked;

        string  vUser = (usuario.Text ?? ""),
                vPassword = (password.Text ?? "");

        lblMessageError.Visible = false;

        isLocked = ActiveDirectory.IsLocked(vUser, Constant.DefaultDomain);        

        if (!isLocked)
        {
            isAuthenticated = ActiveDirectory.Authenticate(vUser, vPassword, Constant.DefaultDomain);

            if (isAuthenticated)
            {
                string vName = ActiveDirectory.GetName(vUser, Constant.DefaultDomain);
                lblMessageError.ForeColor = System.Drawing.Color.Green;
                lblMessageError.Visible = true;
               
                Session[Constant.KeyUserSession] = vUser + "-" + vName;
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Password incorrecto')", true);            
                lblMessageError.Text = "El usuario o la contraseña es incorrecto, intentar de nuevo";
                lblMessageError.ForeColor = System.Drawing.Color.Red;
                lblMessageError.Visible = true;
            }            
        }
        else
        {
            lblMessageError.Text = "Su cuenta de red esta bloqueada";
            lblMessageError.ForeColor = System.Drawing.Color.Red;
            lblMessageError.Visible = true;
        }

        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);

    }


}