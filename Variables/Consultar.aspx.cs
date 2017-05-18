using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Variables_Consultar : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Master.SetActiveClasses("liVariables");    
                   
        if (!Page.IsPostBack && Session[Constant.KeyUserSession] != null )
        {
            new SqlTransaction(null, obtieneVariables, Resultado).Run();            
        }
    }

    private void Resultado(object input)
    {
        if(input != null)
        {
            ds = (DataSet)input;
            

            if (ds.Tables.Count > 0)
            {
                DataTable dtActual = ds.Tables[0];
                dtActual.DefaultView.Sort = "ID_VAR ASC";
                gridVariables.DataSource = dtActual;                
                gridVariables.DataBind();

                if (dtActual.Rows.Count <= 0)
                    divAlert.Visible = true;
                else
                    divAlert.Visible = false;
            }
        }
    }

    private object obtieneVariables(SQL_Connector conn, object input, BackgroundWorker bg)
    {
        return conn.SelectTables(Constant.GetVariables);
    }

    protected void gridVariables_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int indexId = 0;

        string txt = gridVariables.Rows[e.NewEditIndex].Cells[indexId].Text;

        if (txt != null)
            Response.Redirect("Editar.aspx?Id=" + txt);
        else
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No se pudo actualizar registro')", true);
    }


    protected void gridVariables_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridVariables.PageIndex = e.NewPageIndex;
        new SqlTransaction(null, Filtrado, Resultado).Run();        
    }

    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        new SqlTransaction(null, Filtrado, Resultado).Run();
    }

    private object Filtrado(SQL_Connector conn, object input, BackgroundWorker bg)
    {
        string keyParam = "@Buscador";

        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            { keyParam, '%'+(txtSearch.Text ?? "")+'%' }
        };

        string query = "",
               where = "";

        where = string.Format(" WHERE NOM_CORTO like {0} OR NOM_VAR like {0} OR NOM_TIPO_BLOQUE like {0} OR DESC_UNIDAD like {0} ORDER BY ID_VAR", keyParam);

        query = Constant.GetVariables + where;

        return conn.SelectTables(query, parameters);
    }
}