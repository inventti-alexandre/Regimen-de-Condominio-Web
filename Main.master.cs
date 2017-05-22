using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Main : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session[Constant.KeyUserSession] = "MNIETO - Manuel Nieto";

        if (Session[Constant.KeyUserSession] != null)
        {
            string vUser = GetSessionUser();            

            pUserLogin.InnerHtml = vUser;

            if (Session[Constant.KeyEsAdmin] == null)
                RevisarPermisos();
            else
            {
                bool esAdmin = (bool)Session[Constant.KeyEsAdmin];

                if (!esAdmin)
                    HideAdminLink();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    private void RevisarPermisos()
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        using (SQL_Connector conn = new SQL_Connector(connectionString))
        {
            List<string> resultRows = new List<string>();

            conn.Select(Queries.GetAdmins + string.Format(" WHERE CLA_USUARIO = '{0}' AND ESTATUS = 1", this.GetSessionUser()), out resultRows, '|');

            if(resultRows.Count > 0)
            {
                Session[Constant.KeyEsAdmin] = true;
            }
            else
            {
                Session[Constant.KeyEsAdmin] = false;
                HideAdminLink();
            }
        }
    }

    private void HideAdminLink()
    {
        liUsuarios.Visible = false;
        liVariables.Visible = false;
    }

    public bool IsUserAdmin()
    {
        return (bool)(Session[Constant.KeyEsAdmin] ?? false);
    }

    public string GetSessionUser()
    {
        string Usuario = "";

        string[] detailSession = Session[Constant.KeyUserSession].ToString().Split('-');

        if (detailSession.Count() > 0)
            Usuario = detailSession[0];

        return Usuario.ToUpper();
    }

    public string GetSessionUserName()
    {
        string Nombre = "";

        string[] detailSession = Session[Constant.KeyUserSession].ToString().Split('-');

        if (detailSession.Count() > 0)
            Nombre = detailSession[detailSession.Count() - 1];

        return Nombre;
    }

    public bool IsUserLogged()
    {
        return Session[Constant.KeyUserSession] != null;
    }

    public void SetActiveClasses(string activeLink)
    {
        string activeClass = "active";

        if (activeLink == liHome.ClientID)
        {
            liHome.Attributes["class"] = activeClass;
            liMachotes.Attributes["class"] = "";
            liUsuarios.Attributes["class"] = "";
            liVariables.Attributes["class"] = "";
            liReportes.Attributes["class"] = "";
        }
        else if (activeLink == liMachotes.ClientID)
        {
            liMachotes.Attributes["class"] = activeClass;
            liHome.Attributes["class"] = "";
            liUsuarios.Attributes["class"] = "";
            liVariables.Attributes["class"] = "";
            liReportes.Attributes["class"] = "";
        }
        else if (activeLink == liUsuarios.ClientID)
        {
            liUsuarios.Attributes["class"] = activeClass;
            liMachotes.Attributes["class"] = "";
            liHome.Attributes["class"] = "";            
            liVariables.Attributes["class"] = "";
            liReportes.Attributes["class"] = "";
        }
        else if (activeLink == liVariables.ClientID)
        {
            liVariables.Attributes["class"] = activeClass;
            liMachotes.Attributes["class"] = "";
            liHome.Attributes["class"] = "";
            liUsuarios.Attributes["class"] = "";
            liReportes.Attributes["class"] = "";
        }
        else if (activeLink == liReportes.ClientID)
        {
            liReportes.Attributes["class"] = activeClass;
            liVariables.Attributes["class"] = "";
            liMachotes.Attributes["class"] = "";
            liHome.Attributes["class"] = "";
            liUsuarios.Attributes["class"] = "";
        }
    }

    protected void logoutRef_ServerClick(object sender, EventArgs e)
    {
        Session[Constant.KeyUserSession] = null;
        Response.Redirect("~/Login.aspx", true);
    }
}
