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
        if(Session[Constant.KeyUserSession] != null)
        {
            string Usuario = "";

            string[] detailSession = Session[Constant.KeyUserSession].ToString().Split('-');

            if (detailSession.Count() > 0)
                Usuario = detailSession[0];
            
            pUserLogin.InnerHtml = Usuario.ToUpper();
        }
    }
}
