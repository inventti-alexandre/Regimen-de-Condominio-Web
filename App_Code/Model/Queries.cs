using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Queries
/// </summary>
public static class Queries
{    

    private static string getVariables = "select ID_VAR, NOM_CORTO, NOM_VAR, ESTATUS, ES_CALCULADO,"
                                                + "CONV_LETRA, VALOR, ID_TIPO_BLOQUE, NOM_TIPO_BLOQUE,ID_UNIDAD,"
                                                + "DESC_UNIDAD from viVariablesActivas";

    private static string getUnidades = "select ID_UNIDAD, DESC_UNIDAD from REG_UNIDADES";

    private static string getBloques = "select ID_TIPO_BLOQUE, NOM_TIPO_BLOQUE from REG_TIPOS_BLOQUE";

    private static string getUsuariosConteo = "SELECT CLA_USUARIO, CONTEO_FRACC, CONTEO_DR from viContAccesoUsuarios";

    private static string updateVars = "UPDATE REG_VARS_BASE SET NOM_CORTO = @NomCorto, NOM_VAR = @NomVar, VALOR = @Valor, " +
                                        "ESTATUS = @Estatus, CONV_LETRA = @ConvLetra, ID_TIPO_BLOQUE = @IdTipoBloque, " +
                                        "ID_UNIDAD = @IdUnidad, USUARIO_MOD = @UsuarioMod, FECHA_MOD = @FechaMod WHERE ID_VAR = @IdVar";

    private static string insertVars = "INSERT INTO REG_VARS_BASE (NOM_CORTO,NOM_VAR, VALOR, ESTATUS, CONV_LETRA,ES_CALCULADO, ID_TIPO_BLOQUE, ID_UNIDAD, " +
                                                                "USUARIO_CREACION, USUARIO_MOD, FECHA_CREACION, FECHA_MOD)" +
                                        " VALUES(@NomCorto, @NomVar, @Valor, @Estatus, @ConvLetra, @EsCalculado, @IdTipoBloque, @IdUnidad, @UsuarioMod, @UsuarioMod, @FechaMod, @FechaMod); " +
                                        "SELECT SCOPE_IDENTITY();";

    private static string getFraccsUsuario = "SELECT ID_SEMBRADO, NOMBRE from viFraccsUsuario";

    private static string getDRUsuario = "SELECT Convert(nvarchar(50), ID_DR_SEMBRADO) + '-' + Convert(nvarchar(1),ESTATUS_DR) + '-' + Convert(nvarchar(1),SINC_SEMBRADO) AS VALORES_DR, ID_DR_SEMBRADO, NOM_DR  from viDRUsuarios";

    private static string updateDRUsuario = "UPDATE SEM_USUARIOS_DR SET ESTATUS = @Estatus,SINC_SEMBRADO = @SincSembrado, USUARIO_MOD = @UsuarioMod, FECHA_MOD = @FechaMod WHERE ID_DR_SEMBRADO = @IdDR AND CLA_USUARIO = @ClaUsuario";

    private static string getEncMachotes = "SELECT ID_MACHOTE ,ENC_MACHOTE , PROTOTIPO, DESCRIPCION, CANT_VIVS ,ESTATUS_MACHOTE ,NOM_DR, ID_DR_SEMBRADO FROM viMachotesUsuario";

    private static string getDistMachotesGenerados = "SELECT DISTINCT(ID_MACHOTE) FROM REG_BLOQUES_BASE WHERE ID_BLOQUE IN (SELECT ID_BLOQUE FROM REG_MACHOTES_CALC)";

    private static string getMachotesCalc = "select ORDEN, DESCRIPCION_CALC, NOM_TIPO_BLOQUE,ENC_MACHOTE from dbo.viMachotesCalc";

    private static string getDescViviendas = "SELECT DESCRIPCION, CANT_VIVS from REG_DESC_VIVS";

    private static string getTipoBloque = "SELECT ID_TIPO_BLOQUE, NOM_TIPO_BLOQUE from REG_TIPOS_BLOQUE";

    private static string getBloquesBase = "SELECT ID_BLOQUE, DESCRIPCION, ID_MACHOTE, ORDEN FROM REG_BLOQUES_BASE";

    private static string getAdmins = "SELECT ID_REG,CLA_USUARIO FROM REG_ADMIN";

    public static string GetDistMachotesGenerados
    {
        get
        {
            return getDistMachotesGenerados;
        }
    }

    public static string GetVariables
    {
        get
        {
            return getVariables;
        }
    }

    public static string GetUnidades
    {
        get
        {
            return getUnidades;
        }
    }

    public static string GetBloques
    {
        get
        {
            return getBloques;
        }
    }

    public static string GetUsuariosConteo
    {
        get
        {
            return getUsuariosConteo;
        }
    }

    public static string UpdateVars
    {
        get
        {
            return updateVars;
        }
    }

    public static string InsertVars
    {
        get
        {
            return insertVars;
        }
    }

    public static string GetFraccsUsuario
    {
        get
        {
            return getFraccsUsuario;
        }
    }

    public static string GetDRUsuario
    {
        get
        {
            return getDRUsuario;
        }
    }

    public static string UpdateDRUsuario
    {
        get
        {
            return updateDRUsuario;
        }
    }

    public static string GetEncMachotes
    {
        get
        {
            return getEncMachotes;
        }
    }

    public static string GetDescViviendas
    {
        get
        {
            return getDescViviendas;
        }
    }

    public static string GetTipoBloque
    {
        get
        {
            return getTipoBloque;
        }
    }

    public static string GetBloquesBase
    {
        get
        {
            return getBloquesBase;
        }
    }

    public static string GetMachotesCalc
    {
        get
        {
            return getMachotesCalc;
        }

        set
        {
            getMachotesCalc = value;
        }
    }

    public static string GetAdmins
    {
        get
        {
            return getAdmins;
        }

        set
        {
            getAdmins = value;
        }
    }
}