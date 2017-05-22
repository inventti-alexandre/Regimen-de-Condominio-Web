using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Usuarios_Editar : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Master.SetActiveClasses("liUsuarios");

        if (!IsPostBack)
        {
            //Reviso que si haya un usuario logeado
            if (Session[Constant.KeyUserSession] != null)
            {
                if (this.Master.IsUserAdmin())
                {
                    //Si existe Id dentro de la URL
                    if (HasUserParam())
                    {
                        string usuarioActual = GetUserParam();
                        headVar.Text = usuarioActual;
                        divAlert.Visible = false;
                        new SqlTransaction(usuarioActual, SelectDirRegional, ResultadosDR).Run();
                    }
                }
                else
                    Response.Redirect("~/Default.aspx");
            }
        }
    }

    private object SelectDirRegional(SQL_Connector conn, object input, BackgroundWorker bg)
    {
        string usuarioBusqueda = input.ToString(),
                keyUserParam = "@Usuario",
                query = Queries.GetDRUsuario + " WHERE CLA_USUARIO = " + keyUserParam + " ORDER BY ID_DR_SEMBRADO";

        Dictionary<string, object> dictionaryParam = new Dictionary<string, object>()
        {
            {keyUserParam, usuarioBusqueda}
        };

        DataSet dtSet = conn.SelectTables(query, dictionaryParam);

        if (dtSet.Tables.Count > 0)
            return dtSet.Tables[0];
        else
            return null;        
    }

    private void ResultadosDR(object input)
    {
        if(input != null)
        {
            DataTable dtTable = (DataTable)input;

            if (dtTable.Rows.Count > 0)
            {
                ddlDRSembrado.DataSource = dtTable;
                ddlDRSembrado.DataBind();

                ddlDRSembrado_SelectedIndexChanged(ddlDRSembrado, new EventArgs());
            }
            else
                divAlert.Visible = true;
        }
    }

    protected void ddlDRSembrado_SelectedIndexChanged(object sender, EventArgs e)
    {
        int idDR;                      

        if (ddlDRSembrado.SelectedIndex != -1)
        {
            string valueDDL = "",
                    strId = "";
                                      
            valueDDL = ddlDRSembrado.SelectedValue;

            string[] splitValue = valueDDL.Split('-');

            if (splitValue.Count() > 0)
            {
                //El primer valor representa el ID
                strId = splitValue[0];

                //El segundo valor representa el Checkbox ESTATUS
                cActivo.Checked = splitValue[1] == "1" ? true : false ;

                //El tercer valor representa el Checkbox SINC_SEMBRADO
                cSincroniza.Checked = splitValue[2] == "1" ? true : false;

                if (int.TryParse(strId, out idDR))
                {
                    new SqlTransaction(idDR, SelectFraccs, ResultadoFraccs).Run();
                }
            }
        }
    }

    private object SelectFraccs(SQL_Connector conn, object input, BackgroundWorker bg)
    {
        if(input != null && HasUserParam())
        {
            int idDR = (int)input;

            string Usuario = GetUserParam(),
                    keyParamUsuario = "@Usuario",
                    keyParamId = "@IdDR",
                    query = Queries.GetFraccsUsuario + string.Format(" WHERE CLA_USUARIO = {0} AND ID_DR_SEMBRADO = {1}", keyParamUsuario, keyParamId);
            
            Dictionary<string, object> dictionaryParams = new Dictionary<string, object>()
            {
                { keyParamUsuario, Usuario },
                { keyParamId, idDR }
            };

            DataSet dtSet = conn.SelectTables(query, dictionaryParams);

            if (dtSet.Tables.Count > 0)
                return dtSet.Tables[0];
            
        }

        return null;
    }

    private void ResultadoFraccs(object input)
    {
        if(input != null)
        {
            DataTable dt = (DataTable)input;
            gridFraccs.DataSource = dt;
            gridFraccs.DataBind();            
        }
    }

    protected void gridFraccs_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        int resultId;
        string valueDDL = ddlDRSembrado.SelectedValue;

        string[] splitValue = valueDDL.Split('-');

        if(int.TryParse(splitValue[0], out resultId))
        {
            gridFraccs.PageIndex = e.NewPageIndex;
            new SqlTransaction(resultId, SelectFraccs, ResultadoFraccs).Run();
        }

        
    }

    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        if (HasUserParam()) 
        {
            new SqlTransaction(GetUserParam(), Filtrado, ResultadoFraccs).Run();
        }
    }

    private object Filtrado(SQL_Connector conn, object input, BackgroundWorker bg)
    {
        string  keyParamSearch = "@Buscador",
                keyParamUser = "@Usuario",
                keyParamIDDR = "@IdDR",
                idDR = ddlDRSembrado.SelectedValue.Split('-')[0];

        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            { keyParamSearch, '%'+txtSearch.Text+'%' },
            {  keyParamUser, input.ToString() },
            { keyParamIDDR, idDR }
        };

        string query = "",
               where = "";

        where = string.Format(" WHERE CLA_USUARIO = {0} AND ID_DR_SEMBRADO = {1} AND NOMBRE LIKE {2} ORDER BY ID_SEMBRADO", keyParamUser, keyParamIDDR, keyParamSearch);

        query = Queries.GetFraccsUsuario + where;

        DataSet dtSet = conn.SelectTables(query, parameters);

        if (dtSet.Tables.Count > 0)
            return dtSet.Tables[0];
        else
            return null;
    }

    protected void btnEnviar_Click(object sender, EventArgs e)
    {
        if(HasUserParam() && Session[Constant.KeyUserSession] != null)
        {
            string[] values = ddlDRSembrado.SelectedValue.Split('-');

            string strId = values[0];

            int idDR;

            if(int.TryParse(strId, out idDR))
            {
                new SqlTransaction(idDR, UpdateUser, UpdatedFinish).Run();
            }
        }
    }

    private void UpdatedFinish(object input)
    {
        string msg = (string)(input ?? "");

        if (msg == "")
            msg = "Error desconocido: Contactar al administrador";

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msg + "')", true);
    }

    private object UpdateUser(SQL_Connector conn, object input, BackgroundWorker bg)
    {
        int idDR = (int)input;

        string  userToUpdate = GetUserParam(),
                msg = "",
                mainUser = Session[Constant.KeyUserSession].ToString();

        Dictionary<string, object> dicParams = new Dictionary<string, object>()
        {
            {"@IdDR",idDR },
            {"@ClaUsuario",userToUpdate },
            {"@Estatus", cActivo.Checked },
            {"@SincSembrado ", cSincroniza.Checked },
            { "@UsuarioMod",(mainUser.Split('-')[0]).ToUpper() },
            {"@FechaMod", DateTime.Now }
        };

        int rowsAffected = conn.Command(Queries.UpdateDRUsuario, dicParams);

        if (rowsAffected == 1)
            msg = "Se actualizó 1 registro de exitosamente";
        else
            msg = "Se actualizaron " + rowsAffected +" registros exitosamente";

        return msg;
    }

    private bool HasUserParam()
    {
        return Request.QueryString["Usuario"] != null;
    }

    private string GetUserParam()
    {
        return Request.QueryString["Usuario"];
    }
}