using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reportes_Consultar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Master.SetActiveClasses("liReportes");

        if (!Page.IsPostBack)
        {            
            if (this.Master.IsUserLogged())
            {
                new SqlTransaction(this.Master.GetSessionUser(), obtieneReportes, Resultado).Run();
            }
        }
    }

    private void Resultado(object input)
    {
        if(input != null)
        {
            DataTable dt = (DataTable)input;

            gridReportes.DataSource = dt;
            gridReportes.DataBind();

            if (dt.Rows.Count <= 0)
                divAlert.Visible = true;
            else
                divAlert.Visible = false;
        }
    }

    private object obtieneReportes(SQL_Connector conn, object input, BackgroundWorker bg)
    {
        DataSet dtSet = new DataSet();

        if(input != null)
        {
            string paramUser = "@ClaUsuario", query = "";

            Dictionary<string, object> dicParams = new Dictionary<string, object>()
            {
                { paramUser,input.ToString() }
            };

            query = Queries.GetEncMachotes + string.Format(" WHERE CLA_USUARIO = {0} AND ID_MACHOTE IN ({1})", paramUser, Queries.GetDistMachotesGenerados);

            dtSet = conn.SelectTables(query, dicParams);
        }

        if (dtSet.Tables.Count > 0)
            return dtSet.Tables[0];
        else
            return null;
    }

    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        if (Session[Constant.KeyUserSession] != null)
        {
            new SqlTransaction(this.Master.GetSessionUser(), Filtrado, Resultado).Run();
        }
    }

    protected void gridReportes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridReportes.PageIndex = e.NewPageIndex;
        new SqlTransaction(null, Filtrado, Resultado).Run();
    }

    private object Filtrado(SQL_Connector conn, object input, BackgroundWorker bg)
    {
        string keyParamBuscar = "@Buscador",
                keyParamClaUsuario = "@ClaUsuario";

        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            { keyParamBuscar, '%'+ (txtSearch.Text ?? "")+'%' },
            { keyParamClaUsuario, input.ToString() }
        };

        string query = "",
               where = "";

        where = string.Format(" WHERE CLA_USUARIO = {0} AND ID_MACHOTE IN ({1}) AND (ENC_MACHOTE like {2} OR NOM_DR like {2} OR CANT_VIVS like {2}) ORDER BY ID_MACHOTE", keyParamClaUsuario, Queries.GetDistMachotesGenerados , keyParamBuscar);

        query = Queries.GetEncMachotes + where;

        DataSet dtSet = conn.SelectTables(query, parameters);

        if (dtSet.Tables.Count > 0)
            return dtSet.Tables[0];
        else
            return null;
    }

    private void ResultadoWord(object input)
    {
        if(input != null)
        {
            string fileCreated = input.ToString();

            //Si no se encontró un error en la creación del archivo
            if (!fileCreated.ToUpper().Contains("ERROR"))
            {
                dwlLink.NavigateUrl = "~/Reportes/GetFile.aspx?File=" + fileCreated;
                dwlLink.Text = fileCreated;
                dwlLink.Visible = true;
                
                return;
            }
        }
    }

    private object GenerarWord(SQL_Connector conn, object input, BackgroundWorker bg)
    {
        int idMachote = (int)input;

        string paramIdMachote = "@IdMachote",
                query = "";

        Dictionary<string, object> dicParams = new Dictionary<string, object>()
        {
            { paramIdMachote, idMachote }
        };

        query = Queries.GetMachotesCalc + " WHERE ID_MACHOTE = " + paramIdMachote + " ORDER BY ORDEN ASC";

        DataSet dtSet = conn.SelectTables(query, dicParams);        

        if (dtSet.Tables.Count > 0)
        {
            DataTable dt = dtSet.Tables[0];

            if (dt.Rows.Count > 0)
            {
                string  encMachote = dt.Rows[0]["ENC_MACHOTE"].ToString(),                        
                        fileCreated = "";

                if (Office.FileFolderExist())                
                    fileCreated = Office.ExportToWord(dt, encMachote);                                

                return fileCreated;
            }
            else
                return null;           
        }
        else
            return null;
    }

    protected void btnExportarWord_Click(object sender, ImageClickEventArgs e)
    {
        int indexId = 0,
            idMachote,
            rowIndex;
       
        ImageButton btnImage = sender as ImageButton;

        rowIndex = Convert.ToInt32(btnImage.Attributes["RowIndex"]);

        string txt = gridReportes.Rows[rowIndex].Cells[indexId].Text;

        if (this.Master.IsUserLogged())
        {
            if (int.TryParse(txt, out idMachote))
            {                
                new SqlTransaction(idMachote, GenerarWord, ResultadoWord).Run();                                
            }
        }
    }
}