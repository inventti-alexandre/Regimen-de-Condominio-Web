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
}