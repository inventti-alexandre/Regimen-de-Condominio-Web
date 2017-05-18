using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Master.SetActiveClasses("liHome");

        if (Session[Constant.KeyUserSession] != null)
        {         
            string Nombre = "";

            string[] detailSession = Session[Constant.KeyUserSession].ToString().Split('-');

            if (detailSession.Count() > 0)
                Nombre = detailSession[detailSession.Count() - 1];

            h2Welcome.InnerHtml = "Bienvenido(a) " +  Nombre;
        }
    }
}