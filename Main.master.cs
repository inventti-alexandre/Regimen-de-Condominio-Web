using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Main : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session[Constant.KeyUserSession] = "MNIETO-Manuel Alejandro Nieto Lara";

        if(Session[Constant.KeyUserSession] != null)
        {
            string Usuario = "";

            string[] detailSession = Session[Constant.KeyUserSession].ToString().Split('-');

            if (detailSession.Count() > 0)
                Usuario = detailSession[0];
            
            pUserLogin.InnerHtml = Usuario.ToUpper();
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void logout_ServerClick(object sender, EventArgs e)
    {
        
    }

    public string GetSessionUser()
    {
        string Usuario = "";

        string[] detailSession = Session[Constant.KeyUserSession].ToString().Split('-');

        if (detailSession.Count() > 0)
            Usuario = detailSession[0];

        return Usuario.ToUpper();
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
        Response.Redirect("~/Login.aspx");
    }
}
