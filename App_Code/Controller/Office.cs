using Novacode;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// Manejo de Herramientas de Microsoft Office
/// </summary>
public static class Office
{
    public static string ExportToWord(DataTable dt, string machote)
    {
        string  folderPath = ConfigurationManager.AppSettings["FolderFilesPath"],
                fullPath = "",
                identifier = "",
                fileName = "";

        identifier = DateTime.Now.ToString("dd-MM-yyyy-HHmmss");

        fileName = string.Format("{0}_{1}.docx", machote, identifier);

        fullPath = string.Format("{0}\\{1}", folderPath, fileName);

        try {
            // Create a document in memory:
            DocX doc = DocX.Create(fullPath);

            //Formato que debe de tener la hoja del Word
            doc.PageWidth = 595;
            doc.MarginLeft = 72;
            doc.MarginRight = 72;

            Formatting  redFormat = new Formatting(){ FontColor = Color.Red, FontFamily = new FontFamily("Arial"), Size = 10 },
                        normalFormat = new Formatting() { FontColor = Color.Black, FontFamily = new FontFamily("Arial"), Size = 10 },
                        boldFormat = new Formatting() { FontColor = Color.Black, FontFamily = new FontFamily("Arial"), Bold = true, Size = 10 },
                        boldRedFormat = new Formatting() { FontFamily = new FontFamily("Arial"), Size = 10, Bold = true, FontColor = Color.Red };

            Func<string, string> funcReplace = RegexHandler;

            bool insertoSubtitulo = false;

            for(int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dtRow = dt.Rows[i];

                string description = dtRow["DESCRIPCION_CALC"].ToString();

                Paragraph pg = doc.InsertParagraph();

                if (i == 0)
                {
                    pg.InsertText(description, false, boldFormat);
                    pg.InsertText("\n" + Constant.PropiedadPrivada, false, normalFormat);

                    pg.Alignment = Alignment.left;

                    pg.ReplaceText(Constant.RegexBrackets, funcReplace, false,
                        RegexOptions.None, boldRedFormat, null, MatchFormattingOptions.SubsetMatch);
                }
                else
                {

                    if (dtRow["NOM_TIPO_BLOQUE"].ToString() != "APARTAMENTO" && !insertoSubtitulo)
                    {
                        pg.InsertText(Constant.PropiedadComun + "\n", false, normalFormat);
                        insertoSubtitulo = true;
                    }

                    pg.Alignment = Alignment.both;

                    string[] paragraphs = description.Split(new string[] { "[" + Constant.LineasXParrafo + "]", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string paragraph in paragraphs)
                    {
                        string formatedParagraph = paragraph + "\n" + Constant.LineasXParrafo;

                        pg.InsertText(formatedParagraph, false, normalFormat);

                        pg.ReplaceText(Constant.RegexBrackets, funcReplace, false,
                                RegexOptions.None, redFormat, null, MatchFormattingOptions.SubsetMatch);
                    }

                }
            }

            doc.Save();

            return fileName;
        }
        catch(IOException ex)
        {
            return "ERROR: Se encontró un error al guardar archivo, contactar administrador \n" + ex.Message;
        }
        catch(Exception ex)
        {
            return "ERROR Se encontró un error de sistema, contactar administrador: "+ex.Message;
        }
    }

    public static bool FileFolderExist()
    {
        string folderPath = (ConfigurationManager.AppSettings["FolderFilesPath"]);

        if (!Directory.Exists(folderPath))
        {
            try {
                Directory.CreateDirectory(folderPath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        else
            return true;
    }

    private static string RegexHandler(string input)
    {
        return input;
    }

    public static void DeleteTempFile(string fileCreated)
    {
        if (File.Exists(fileCreated))
        {
            File.Delete(fileCreated);
        }        
    }
}