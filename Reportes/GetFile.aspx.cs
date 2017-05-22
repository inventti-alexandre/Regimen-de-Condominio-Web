using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TmpMachotesCalc_GetFile : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["File"] != null)
        {            
            

            
            string  name = Request.QueryString["File"],
                    folderPath = ConfigurationManager.AppSettings["FolderFilesPath"],
                    contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    fullPath = "";

            fullPath = folderPath + "\\" + name;

            HttpResponse responseC = HttpContext.Current.Response;

            if (File.Exists(fullPath))
            {               
                // Send the file to the browser
                responseC.AddHeader("Content-type", contentType);
                responseC.AddHeader("Content-Disposition", "attachment; filename=" + name + ";");
                responseC.TransmitFile(fullPath);
                responseC.Flush();                
            }

            responseC.End();
        }
    }
}