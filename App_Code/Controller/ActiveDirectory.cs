using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices.AccountManagement;

/// <summary>
/// Clase para manejar la autenticación hacia AD de Usuarios Windows
/// </summary>
public static class ActiveDirectory
{
    public static bool Authenticate(string user, string password, string domain)
    {
        bool isValid = false;

        try {
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, domain))
            {
                isValid = pc.ValidateCredentials(user, password);
            }
        }catch(Exception)
        {

        }

        return isValid;
    }

    public static string GetDisplayName(string user, string domain)
    {
        string DisplayName = "";
        try {
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, domain))
            {
                UserPrincipal usr = UserPrincipal.FindByIdentity(pc, user);
                DisplayName = usr.DisplayName;
            }
        }catch(Exception)
        {

        }
        return DisplayName;
    }

    public static string GetName(string user, string domain)
    {
        string GivenName = "";
        try
        {
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, domain))
            {
                UserPrincipal usr = UserPrincipal.FindByIdentity(pc, user);
                GivenName = usr.GivenName;
            }
        }
        catch (Exception)
        {

        }
        return GivenName;
    }

    public static bool IsLocked(string user, string domain)
    {
        bool isLocked = false;
        try {
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, domain))
            {
                UserPrincipal usr = UserPrincipal.FindByIdentity(pc, user);
                if (usr != null)
                    isLocked = usr.IsAccountLockedOut();
            }

        }catch(Exception)
        {
            
        }
        return isLocked;
    }

}