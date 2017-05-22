using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Variables_Editar : System.Web.UI.Page
{
    private int IdParameter;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Master.SetActiveClasses("liVariables");

        if (!IsPostBack)
        {
            //Reviso que si haya un usuario logeado
            if (Session[Constant.KeyUserSession] != null)
            {                
                if (this.Master.IsUserAdmin())
                {
                    //Si existe Id dentro de la URL
                    if (Request.QueryString["Id"] != null)
                    {
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
                else
                    Response.Redirect("~/Default.aspx");
            }
        }
    }

    private object EjecutaQuery(SQL_Connector conn, object input, BackgroundWorker bg)
    {
        Dictionary<string, object> paramId = new Dictionary<string, object>();

        string queryCatalogos = string.Join(" ", Queries.GetUnidades, Queries.GetBloques);

        string nameIdParam = "@Id";

        if (input != null)
        {
            string queryOneVar = Queries.GetVariables + " WHERE ID_VAR = " + nameIdParam;

            queryCatalogos = queryCatalogos + " " + queryOneVar;

            paramId.Add(nameIdParam, IdParameter.ToString());

            return conn.SelectTables(queryCatalogos, paramId);
        }

        return conn.SelectTables(queryCatalogos);
    }

    private void ResultadosQuery(object input)
    {
        if (input != null)
        {
            DataSet ds = (DataSet)input;

            if (ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    DataTable dt = ds.Tables[i];
                    switch (i)
                    {
                        case 0://Primero tomo la tabla de Unidades
                            cUnidad.DataSource = dt;
                            cUnidad.DataBind();
                            cUnidad.Items.Insert(0, new ListItem("--Selecciona Unidad--", "0"));
                            cUnidad.SelectedIndex = 0;
                            break;
                        case 1://Segundo tomo la tabla de Tipos de Bloque  
                            cTipoBloque.DataSource = dt;
                            cTipoBloque.DataBind();
                            cTipoBloque.Items.Insert(0, new ListItem("--Selecciona Tipo de Bloque--", "0"));
                            cTipoBloque.SelectedIndex = 0;
                            break;
                        case 2://Al final tomo las variables si se encontró Id Válido
                            AsignarVariable(dt);
                            break;
                    }
                }

            }
        }
    }

    private void AsignarVariable(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            DataRow dtRow = dt.Rows[0];

            string idVariable = (dtRow["ID_VAR"] ?? "").ToString(),
                    nomCorto = (dtRow["NOM_CORTO"] ?? "").ToString(),
                    nomVariable = (dtRow["NOM_VAR"] ?? "").ToString(),
                    valorVariable = (dtRow["VALOR"] ?? "").ToString();

            bool    Estatus = (bool)(dtRow["ESTATUS"] ?? false),
                    EsCalculado = (bool)(dtRow["ES_CALCULADO"] ?? false),
                    ConvLetra = (bool)(dtRow["CONV_LETRA"] ?? false);

            int selUnidad = (int)(dtRow["ID_UNIDAD"] ?? 0),
                selTipoBloque = (int)(dtRow["ID_TIPO_BLOQUE"] ?? 0);

            txtIdVar.Value = idVariable;
            txtNomCorto.Value = nomCorto;
            txtNomVariable.Value = nomVariable;
            txtValor.Value = valorVariable;

            cActivo.Checked = Estatus;
            cCalculado.Checked = EsCalculado;
            cConvLetra.Checked = ConvLetra;

            rfvSubject.Enabled = !cCalculado.Checked;

            headVar.Text = nomVariable;

            cUnidad.SelectedValue = selUnidad.ToString();
            cTipoBloque.SelectedValue = selTipoBloque.ToString();

            if(EsCalculado)
            {
                txtNomCorto.Disabled = true;
                txtValor.Disabled = true;
                cUnidad.Enabled = false;
                cTipoBloque.Enabled = false;
            }
        }

    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Consultar.aspx");
    }

    protected void btnEnviar_Click(object sender, EventArgs e)
    {
        int resultId;

        if(Request.QueryString["Id"] != null)
        {
            if (int.TryParse(Request.QueryString["Id"], out resultId))
            {
                new SqlTransaction(resultId, ActualizaVar, ResultadoActualizacion).Run();
            }
        }
        else
        {
            new SqlTransaction(null, InsertaVar, ResultadoActualizacion).Run();
        }
        
    }

    private object InsertaVar(SQL_Connector conn, object input, BackgroundWorker bg)
    {
        string  Usuario = this.Master.GetSessionUser(),
                msg = "";

        Dictionary<string, object> dictionaryParams = new Dictionary<string, object>()
        {
            { "@NomCorto", txtNomCorto.Value ?? "" },
            { "@NomVar", txtNomVariable.Value ?? "" },
            { "@Valor", txtValor.Value ?? "" },
            { "@Estatus", cActivo.Checked ? 1 : 0 },
            { "@EsCalculado", 0},
            { "@ConvLetra", cConvLetra.Checked ? 1 : 0 },
            { "@IdTipoBloque", cTipoBloque.SelectedValue},
            { "@IdUnidad", cUnidad.SelectedValue },
            { "@UsuarioMod", Usuario },
            { "@FechaMod", DateTime.Now }           
        };

        int idInserted = conn.Command(Queries.InsertVars, dictionaryParams, true);

        if (idInserted > 0)
        {
            msg = "Se insertó el registro de manera correcta";
            txtIdVar.Value = idInserted.ToString();
            headVar.Text = txtNomVariable.Value;
        }
        else
            msg = "No se pudo insertar el registro";

        return msg;
    }

    private void ResultadoActualizacion(object input)
    {
        if(input != null)
        {
            string msg = (string)(input ?? "");

            if (msg == "")
                msg = "Se actualizaron registros";

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('"+ msg +"')", true);
        }
    }

    private object ActualizaVar(SQL_Connector conn, object input, BackgroundWorker bg)
    {
        string Usuario = this.Master.GetSessionUser(),
                msg = "";

        Dictionary<string, object> dictionaryParams = new Dictionary<string, object>()
        {
            { "@NomCorto", txtNomCorto.Value ?? "" },
            { "@NomVar", txtNomVariable.Value ?? "" },
            { "@Valor", txtValor.Value ?? "" },
            { "@Estatus", cActivo.Checked ? 1 : 0 },
            { "@EsCalculado", cCalculado.Checked ? 1 : 0 },
            { "@ConvLetra", cConvLetra.Checked ? 1 : 0 },
            { "@IdTipoBloque", cTipoBloque.SelectedValue},
            { "@IdUnidad", cUnidad.SelectedValue },
            { "@UsuarioMod", Usuario },
            { "@FechaMod", DateTime.Now },
            { "@IdVar", (int)input }            
        };

        int rowsAffected = conn.Command(Queries.UpdateVars, dictionaryParams);

        if (rowsAffected == 1)
            msg = "Se actualizó 1 registro";
        else
            msg = "Se actualizaron " + rowsAffected + "registros";

        return msg;
    }

   
}


 

