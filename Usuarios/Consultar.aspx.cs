using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Usuarios_Consultar : System.Web.UI.Page
{
    private string Usuario = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Master.SetActiveClasses("liUsuarios");

        if (!Page.IsPostBack && Session[Constant.KeyUserSession] != null)
        {            
            string[] detailSession = Session[Constant.KeyUserSession].ToString().Split('-');

            if (detailSession.Count() > 0)
                Usuario = detailSession[0];            

            new SqlTransaction(null, EjecutarQuery, Resultado).Run();
        }
    }

    private void Resultado(object input)
    {
        if(input != null)
        {
            DataSet ds = (DataSet)input;

            if(ds.Tables.Count> 0)
            {
                DataTable dt = ds.Tables[0];
                gridUsuarios.DataSource = dt;
                gridUsuarios.DataBind();

                if (dt.Rows.Count <= 0)                
                    divAlert.Visible = true;                
                else
                    divAlert.Visible = false;
            }
        }
    }

    private object EjecutarQuery(SQL_Connector conn, object input, BackgroundWorker bg)
    {
        return conn.SelectTables(Constant.GetUsuariosConteo + " ORDER BY CLA_USUARIO");
    }

    protected void gridUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridUsuarios.PageIndex = e.NewPageIndex;
        new SqlTransaction(null, EjecutarQuery, Resultado).Run();
    }

    private object Filtrado(SQL_Connector conn, object input, BackgroundWorker bg)
    {
        string keyParam = "@Buscador";

        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            { keyParam, '%'+txtSearch.Text+'%' }
        };

        string query = "";

        query = Constant.GetUsuariosConteo + " WHERE CLA_USUARIO LIKE " + keyParam + " ORDER BY CLA_USUARIO";

        return conn.SelectTables(query, parameters);
    }

    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        new SqlTransaction(null, Filtrado, Resultado).Run();
    }

    protected void gridUsuarios_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int indexUsuario = 0;

        string txt = gridUsuarios.Rows[e.NewEditIndex].Cells[indexUsuario].Text;

        if (txt != null)
            Response.Redirect("Editar.aspx?Usuario=" + txt);
        else
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No se pudo actualizar registro')", true);
    }
}