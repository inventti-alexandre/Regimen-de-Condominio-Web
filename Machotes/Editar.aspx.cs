using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Machotes_Editar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Master.SetActiveClasses("liMachotes");        

        if (!IsPostBack)
        {
            //Reviso que si haya un usuario logeado
            if (Session[Constant.KeyUserSession] != null)
            {
                //Si existe Id dentro de la URL
                if (Request.QueryString["Id"] != null)
                {
                    int IdParameter;

                    //Reviso que sea un Id Válido
                    if (int.TryParse(Request.QueryString["Id"], out IdParameter))
                    {
                        //Realizo al consulta para obtener el dato                  
                        new SqlTransaction(IdParameter, EjecutaQuery, ResultadosQuery).Run();
                    }
                    else
                    {
                        //Si el Id no es válido sólo lleno los combobox
                        new SqlTransaction(null, EjecutaQuery, ResultadosQuery).Run();
                    }
                }
                else
                {
                    //Si el Id no es válido sólo lleno los combobox
                    new SqlTransaction(null, EjecutaQuery, ResultadosQuery).Run();
                }
            }
        }
    }    

    private void ResultadosQuery(object input)
    {
        if(input != null)
        {
            DataSet dtSet = (DataSet)input;

            for(int i = 0; i < dtSet.Tables.Count; i++)
            {
                DataTable dt = dtSet.Tables[i];

                switch (i)
                {
                    case 0:
                        ddlCantVivs.DataSource = dt;
                        ddlCantVivs.DataBind();
                        ddlCantVivs.Items.Insert(0, new ListItem("--Selecciona Cantidad de Viviendas--", "0"));
                        ddlCantVivs.SelectedIndex = 0;
                        break;
                    case 1:
                        ddlUEN.DataSource = dt;
                        ddlUEN.DataBind();
                        ddlUEN.Items.Insert(0, new ListItem("--Selecciona UEN--", "0"));
                        ddlUEN.SelectedIndex = 0;
                        break;
                    case 2:
                        ddlPPTipoBloque.DataSource = dt;
                        ddlPPTipoBloque.DataBind();

                        if (ddlPPTipoBloque.Items.Count > 0)
                            ddlPPTipoBloque.SelectedValue = "APARTAMENTO";

                        break;
                    case 3:
                        DataView dtView = new DataView(dt);

                        dtView.RowFilter = "NOM_TIPO_BLOQUE = 'GLOBAL'";

                        if (dtView.Count > 0)
                        {
                            ddlDescVars.DataSource = dtView;
                            ddlDescVars.DataBind();                            
                            ddlPCVars.DataSource = dtView;
                            ddlPCVars.DataBind();
                        }

                        dtView.RowFilter = "";

                        if (ddlPPTipoBloque.Items.Count > 0)
                        {
                            dtView.RowFilter = "ID_TIPO_BLOQUE = " + ddlPPTipoBloque.SelectedValue;

                            if (dtView.Count > 0)
                            {
                                ddlPPVars.DataSource = dtView;
                                ddlPPVars.DataBind();
                            }
                        }
                           
                        break;
                    case 4:

                        if(dt.Rows.Count > 0)
                        {
                            DataRow dtRow = dt.Rows[0];

                            headText.Text = (dtRow["ENC_MACHOTE"] ?? "").ToString();
                            txtPrototipo.Value = (dtRow["PROTOTIPO"] ?? "").ToString();
                            ddlCantVivs.SelectedValue = (dtRow["CANT_VIVS"] ?? "").ToString();
                            ddlUEN.SelectedValue = (dtRow["ID_DR_SEMBRADO"] ?? "").ToString();
                            cActivo.Checked = (bool) (dtRow["ESTATUS_MACHOTE"] ?? false);
                        }

                        break;
                    case 5:

                        if(dt.Rows.Count > 0)
                        {
                            for(int j = 0; j < dt.Rows.Count; j++)
                            {
                                DataRow dtRow = dt.Rows[j];

                                int orden;

                                if(int.TryParse(dtRow["ORDEN"].ToString(), out orden))
                                {                                                                     
                                    switch (orden)
                                    {                                        
                                        case 1:
                                            txtDescripcion.Text = ReemplazoSaltos(dtRow["DESCRIPCION"].ToString());
                                            break;
                                        case 2:

                                            txtAreaPP.Text = ReemplazoSaltos(dtRow["DESCRIPCION"].ToString());
                                            break;
                                        case 3:
                                            txtAreaPC.Text = ReemplazoSaltos(dtRow["DESCRIPCION"].ToString());
                                            break;
                                    }


                                }

                            }
                        }
                        break;
                }
            }
        }
    }

    private object EjecutaQuery(SQL_Connector conn, object input, BackgroundWorker bg)
    {
        string  query = "",
                paramUsuario = "@ClaUsuario",
                paramIdMachote = "@IdMachote",
                Usuario = this.Master.GetSessionUser();

        Dictionary<string, object> dicParams = new Dictionary<string, object>()
        {
            { paramUsuario, Usuario }            
        };

        query = string.Join(" ",    Constant.GetDescViviendas,
                                    Constant.GetDRUsuario + " WHERE CLA_USUARIO = " + paramUsuario,
                                    Constant.GetTipoBloque,
                                    Constant.GetVariables);

        if(input != null)
        {
            int idMachote = (int)input;
            
            //Agrego el parámetro para la consulta
            dicParams.Add(paramIdMachote, idMachote);

            query = query + string.Format(" {0} WHERE CLA_USUARIO = {1} AND ID_MACHOTE = {2}", Constant.GetEncMachotes, paramUsuario, paramIdMachote);

            query = query + string.Format(" {0} WHERE ID_MACHOTE = {1} ORDER BY ORDEN", Constant.GetBloquesBase, paramIdMachote);
        }

        return conn.SelectTables(query, dicParams);
    } 
    
    
    private string ReemplazoSaltos(string descripcion)
    {
        string configSaltoLinea = "", configSaltoParrafo = "", dobleSalto = "";

        configSaltoLinea = ConfigurationManager.AppSettings["SaltoConLineas"];
        configSaltoParrafo = ConfigurationManager.AppSettings["SaltoParrafo"];

        dobleSalto = Environment.NewLine + Environment.NewLine;

        descripcion = descripcion.Replace(Environment.NewLine, "");
        descripcion = descripcion.Replace(configSaltoLinea, dobleSalto + configSaltoLinea + dobleSalto);
        descripcion = descripcion.Replace(configSaltoParrafo, dobleSalto + configSaltoParrafo + dobleSalto);

        return descripcion;
    }

    protected void btnEnviar_ServerClick(object sender, EventArgs e)
    {
        int resultId;

        if (this.Master.IsUserLogged())
        {
            if (Request.QueryString["Id"] != null)
            {
                if (int.TryParse(Request.QueryString["Id"], out resultId))
                {
                    if (HasNoEmptyFields())
                        new SqlTransaction(resultId, UpsertMachote, ResultadoActualizacion).Run();
                }
            }
            else
            {
                if (HasNoEmptyFields())
                    new SqlTransaction(null, UpsertMachote, ResultadoActualizacion).Run();
            }
        }

    }

    private bool HasNoEmptyFields()
    {
        bool isNotEmpty = true;

        int valorInt;

        if (string.IsNullOrWhiteSpace(txtPrototipo.Value))
            isNotEmpty = false;
        else if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            isNotEmpty = false;
        else if (string.IsNullOrWhiteSpace(txtAreaPP.Text))
            isNotEmpty = false;
        else if (string.IsNullOrWhiteSpace(txtAreaPC.Text))
            isNotEmpty = false;
        else if (!int.TryParse(ddlCantVivs.SelectedValue, out valorInt))
            isNotEmpty = false;
        else if (!int.TryParse(ddlUEN.SelectedValue, out valorInt))
            isNotEmpty = false;

        return isNotEmpty;
    }

    private object UpsertMachote(SQL_Connector conn, object input, BackgroundWorker bg)
    {
        string  procedure = "sp_MergeMachotes",
                returnMsg = "";

        Dictionary<string, object> dicParameters = new Dictionary<string, object>()
        {
            {"@NomPrototipo", txtPrototipo.Value },
            {"@CantVivs", ddlCantVivs.SelectedValue },
            {"@IdDR", ddlUEN.SelectedValue},
            {"@Estatus", cActivo.Checked },
            {"@Descripcion",txtDescripcion.Text.Replace(Environment.NewLine, "") },
            {"@PropiedadPrivada",txtAreaPP.Text.Replace(Environment.NewLine, "") },
            {"@PropiedadComun", txtAreaPC.Text.Replace(Environment.NewLine, "") },            
            {"@ClaUsuario", this.Master.GetSessionUser() },
            {"@IdMachote", DBNull.Value }
        };

        if (input != null)
            dicParameters["@IdMachote"] = (int)input;

        int rowsAffected = conn.spMachotes(procedure, dicParameters, out returnMsg);

        return returnMsg;
    }

    private void ResultadoActualizacion(object input)
    {
        if(input != null)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + input.ToString() + "')", true);
        }
    }

    protected void ddlPPTipoBloque_SelectedIndexChanged(object sender, EventArgs e)
    {
        int idTipoBloque;

        if(ddlPPTipoBloque.SelectedIndex != -1)
        {
            if(int.TryParse(ddlPPTipoBloque.SelectedValue, out idTipoBloque))
            {
                new SqlTransaction(idTipoBloque, ActualizaVars, ResultadoVars).Run();
            }
           
        }
    }

    private object ActualizaVars(SQL_Connector conn, object input, BackgroundWorker bg)
    {
        string paramIdTipoBloque = "@IdTipoBloque", query = "";

        Dictionary<string, object> dicParams = new Dictionary<string, object>()
        {
            {paramIdTipoBloque, Convert.ToInt32(input) }
        };

        query = Constant.GetVariables + " WHERE ID_TIPO_BLOQUE = " + paramIdTipoBloque;

        DataSet dtSet = conn.SelectTables(query, dicParams);

        if (dtSet.Tables.Count > 0)
            return dtSet.Tables[0];
        else
            return null;
    }

    private void ResultadoVars(object input)
    {
        if(input != null)
        {
            DataTable dt = (DataTable)input;

            ddlPPVars.DataSource = dt;
            ddlPPVars.DataBind();
        }
    }   
}