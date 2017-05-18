using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reportes_Consultar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Master.SetActiveClasses("liReportes");
    }

    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {

    }

    protected void gridReportes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void gridReportes_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
}