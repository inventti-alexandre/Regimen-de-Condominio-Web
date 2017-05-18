using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserAD
/// </summary>
public class Constant
{    
    private static string defaultDomain = "GRUPOJAVER";

    private static string keyUserSession = "CveUsuario";

    private static string getVariables =   "select ID_VAR, NOM_CORTO, NOM_VAR, ESTATUS, ES_CALCULADO,"
                                                +"CONV_LETRA, VALOR, ID_TIPO_BLOQUE, NOM_TIPO_BLOQUE,ID_UNIDAD,"
                                                +"DESC_UNIDAD from viVariablesActivas";

    private static string getUnidades = "select ID_UNIDAD, DESC_UNIDAD from REG_UNIDADES";

    private static string getBloques = "select ID_TIPO_BLOQUE, NOM_TIPO_BLOQUE from REG_TIPOS_BLOQUE";

    private static string getUsuariosConteo = "SELECT CLA_USUARIO, CONTEO_FRACC, CONTEO_DR from viContAccesoUsuarios";

    private static string updateVars = "UPDATE REG_VARS_BASE SET NOM_CORTO = @NomCorto, NOM_VAR = @NomVar, VALOR = @Valor, " +
                                        "ESTATUS = @Estatus, CONV_LETRA = @ConvLetra, ID_TIPO_BLOQUE = @IdTipoBloque, "+
                                        "ID_UNIDAD = @IdUnidad, USUARIO_MOD = @UsuarioMod, FECHA_MOD = @FechaMod WHERE ID_VAR = @IdVar";

    private static string insertVars = "INSERT INTO REG_VARS_BASE (NOM_CORTO,NOM_VAR, VALOR, ESTATUS, CONV_LETRA,ES_CALCULADO, ID_TIPO_BLOQUE, ID_UNIDAD, "+ 
                                                                "USUARIO_CREACION, USUARIO_MOD, FECHA_CREACION, FECHA_MOD)"+
                                        " VALUES(@NomCorto, @NomVar, @Valor, @Estatus, @ConvLetra, @EsCalculado, @IdTipoBloque, @IdUnidad, @UsuarioMod, @UsuarioMod, @FechaMod, @FechaMod); "+
                                        "SELECT SCOPE_IDENTITY();";

    private static string getFraccsUsuario = "SELECT ID_SEMBRADO, NOMBRE from viFraccsUsuario";

    private static string getDRUsuario = "SELECT Convert(nvarchar(50), ID_DR_SEMBRADO) + '-' + Convert(nvarchar(1),ESTATUS_DR) + '-' + Convert(nvarchar(1),SINC_SEMBRADO) AS VALORES_DR, ID_DR_SEMBRADO, NOM_DR  from viDRUsuarios";

    private static string updateDRUsuario = "UPDATE SEM_USUARIOS_DR SET ESTATUS = @Estatus,SINC_SEMBRADO = @SincSembrado, USUARIO_MOD = @UsuarioMod, FECHA_MOD = @FechaMod WHERE ID_DR_SEMBRADO = @IdDR AND CLA_USUARIO = @ClaUsuario";

    private static string getEncMachotes = "SELECT ID_MACHOTE ,ENC_MACHOTE , PROTOTIPO, DESCRIPCION, CANT_VIVS ,ESTATUS_MACHOTE ,NOM_DR, ID_DR_SEMBRADO FROM viMachotesUsuario";

    private static string getDescViviendas = "SELECT DESCRIPCION, CANT_VIVS from REG_DESC_VIVS";

    private static string getTipoBloque = "SELECT ID_TIPO_BLOQUE, NOM_TIPO_BLOQUE from REG_TIPOS_BLOQUE";

    private static string getBloquesBase = "SELECT ID_BLOQUE, DESCRIPCION, ID_MACHOTE, ORDEN FROM REG_BLOQUES_BASE";

    private static string regexBrackets = @"\[([^]]*)\]";

    public static string DefaultDomain
    {
        get
        {
            return defaultDomain;
        }

        set
        {
            defaultDomain = value;
        }
    }


    public static string KeyUserSession
    {
        get
        {
            return keyUserSession;
        }

        set
        {
            keyUserSession = value;
        }
    }

    public static string GetVariables
    {
        get
        {
            return getVariables;
        }

        set
        {
            getVariables = value;
        }
    }

    public static string GetUnidades
    {
        get
        {
            return getUnidades;
        }

        set
        {
            getUnidades = value;
        }
    }

    public static string GetBloques
    {
        get
        {
            return getBloques;
        }

        set
        {
            getBloques = value;
        }
    }

    public static string GetUsuariosConteo
    {
        get
        {
            return getUsuariosConteo;
        }

        set
        {
            getUsuariosConteo = value;
        }
    }

    public static string UpdateVars
    {
        get
        {
            return updateVars;
        }

        set
        {
            updateVars = value;
        }
    }

    public static string InsertVars
    {
        get
        {
            return insertVars;
        }

        set
        {
            insertVars = value;
        }
    }

    public static string GetFraccsUsuario
    {
        get
        {
            return getFraccsUsuario;
        }

        set
        {
            getFraccsUsuario = value;
        }
    }

    public static string GetDRUsuario
    {
        get
        {
            return getDRUsuario;
        }

        set
        {
            getDRUsuario = value;
        }
    }

    public static string UpdateDRUsuario
    {
        get
        {
            return updateDRUsuario;
        }

        set
        {
            updateDRUsuario = value;
        }
    }

    public static string GetEncMachotes
    {
        get
        {
            return getEncMachotes;
        }

        set
        {
            getEncMachotes = value;
        }
    }

    public static string GetDescViviendas
    {
        get
        {
            return getDescViviendas;
        }

        set
        {
            getDescViviendas = value;
        }
    }

    public static string GetTipoBloque
    {
        get
        {
            return getTipoBloque;
        }

        set
        {
            getTipoBloque = value;
        }
    }

    public static string GetBloquesBase
    {
        get
        {
            return getBloquesBase;
        }

        set
        {
            getBloquesBase = value;
        }
    }

    public static string RegexBrackets
    {
        get
        {
            return regexBrackets;
        }

        set
        {
            regexBrackets = value;
        }
    }
}