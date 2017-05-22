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
            string Nombre = this.Master.GetSessionUserName();

            h2Welcome.InnerHtml = "Bienvenido(a) " +  Nombre;
        }
    }
}