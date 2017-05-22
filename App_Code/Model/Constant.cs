using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserAD
/// </summary>
public class Constant
{        

    private static string keyUserSession = "CveUsuario";

    private static string keyEsAdmin = "EsAdmin";

    private static string regexBrackets = @"\[([^]]*)\]";

    private static string lineasXParrafo = "---------------------------------------------------------------------------------------------------------------------------------------";

    private static string propiedadPrivada = "------------------------------------------------Bienes de Propiedad Privada------------------------------------------------";

    private static string propiedadComun = "------------------------------------------------Bienes de Propiedad Común------------------------------------------------";


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

    public static string RegexBrackets
    {
        get
        {
            return regexBrackets;
        }
    }

    public static string LineasXParrafo
    {
        get
        {
            return lineasXParrafo;
        }

        set
        {
            lineasXParrafo = value;
        }
    }

    public static string PropiedadPrivada
    {
        get
        {
            return propiedadPrivada;
        }

        set
        {
            propiedadPrivada = value;
        }
    }

    public static string PropiedadComun
    {
        get
        {
            return propiedadComun;
        }

        set
        {
            propiedadComun = value;
        }
    }

    public static string KeyEsAdmin
    {
        get
        {
            return keyEsAdmin;
        }

        set
        {
            keyEsAdmin = value;
        }
    }
}