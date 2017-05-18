using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Machotes_Consultar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Master.SetActiveClasses("liMachotes");

        if (!Page.IsPostBack && Session[Constant.KeyUserSession] != null)
        {
            new SqlTransaction(this.Master.GetSessionUser(), obtenerEncabezados, Resultado).Run();
        }
    }

    private void Resultado(object input)
    {
        if(input != null)
        {
            DataTable dt = (DataTable)input;
            gridMachotes.DataSource = dt;
            gridMachotes.DataBind();

            if (dt.Rows.Count <= 0)
                divAlert.Visible = true;
            else
                divAlert.Visible = false;
        }
    }

    private object obtenerEncabezados(SQL_Connector conn, object input, BackgroundWorker bg)
    {
        string keyParamUsuario = "@ClaUsuario",
            query = "";

        Dictionary<string, object> dicParams = new Dictionary<string, object>()
        {
            {keyParamUsuario, input.ToString() }
        };

        query = Constant.GetEncMachotes + " WHERE CLA_USUARIO = " + keyParamUsuario + " ORDER BY ID_MACHOTE";

        DataSet dtSet = conn.SelectTables(query, dicParams);

        if (dtSet.Tables.Count > 0)
            return dtSet.Tables[0];
        else
            return null;
    }

    protected void gridMachotes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void gridMachotes_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int indexId = 0;

        string txt = gridMachotes.Rows[e.NewEditIndex].Cells[indexId].Text;

        if (txt != null)
            Response.Redirect("Editar.aspx?Id=" + txt);
        else
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No se pudo ingresar a la página')", true);
    }

    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        if (Session[Constant.KeyUserSession] != null)
        {
            new SqlTransaction(this.Master.GetSessionUser(), Filtrado, Resultado).Run();
        }
    }

    private object Filtrado(SQL_Connector conn, object input, BackgroundWorker bg)
    {
        string  keyParamBuscar = "@Buscador",
                keyParamClaUsuario = "@ClaUsuario";

        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            { keyParamBuscar, '%'+txtSearch.Text+'%' },
            { keyParamClaUsuario, input.ToString() }
        };

        string query = "",
               where = "";

        where = string.Format(" WHERE CLA_USUARIO = {1} AND (ENC_MACHOTE like {0} OR NOM_DR like {0} OR CANT_VIVS like {0}) ORDER BY ID_MACHOTE", keyParamBuscar, keyParamClaUsuario);

        query = Constant.GetEncMachotes + where;
        
        DataSet dtSet = conn.SelectTables(query, parameters);

        if (dtSet.Tables.Count > 0)
            return dtSet.Tables[0];
        else
            return null;
    }
}