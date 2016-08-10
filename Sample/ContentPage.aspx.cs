using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sample
{
    public partial class ContentPage : System.Web.UI.Page
    {
        protected System.Web.UI.HtmlControls.HtmlInputFile File1;
        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private string GetUserChoice()
        {
            
            if (html.Checked)
            {
                return "html";
            }
            else if (bmp.Checked)
            {
                return "bmp";
            }
            else if (doc.Checked)
            {
                return "doc";
            }
            else if (docm.Checked)
            {
                return "docm";
            }
            else if (docx.Checked)
            {
                return "docx";
            }
            else if (dot.Checked)
            {
                return "dot";
            }
            else if (dotm.Checked)
            {
                return "dotm";
            }
            else if (dotx.Checked)
            {
                return "dotx";
            }
            else if (emf.Checked)
            {
                 return "emf";
            }
            else if (epub.Checked)
            {
                return "epub";
            }
            else if (flatopc.Checked)
            {
                return "html";
            }
            else if (jpeg.Checked)
            {
                return "jpeg";
            }
            else if (pdf.Checked)
            {
                return "pdf";
            }
            else
            {
                return "docx";
            }
           
        }
        protected void btnUploadToDb_Click(object sender, EventArgs e)
        {
            if (uplFileUploader.HasFile)
            {
                try
                {
                    string strTestFilePath = uplFileUploader.PostedFile.FileName; // This gets the full file path on the client's machine ie: c:\test\myfile.txt
                    string strTestFileName = Path.GetFileName(strTestFilePath); // use the System.IO Path.GetFileName method to get specifics about the file without needing to parse the path as a string
                    Int32 intFileSize = uplFileUploader.PostedFile.ContentLength;
                    string orignalfileName = "Test";
                    string ext = GetUserChoice();
                    try
                    {
                        var split = strTestFileName.Split('.');
                        orignalfileName = split[0];
                    }
                    catch (Exception)
                    { 
                    }
                    
                    string strContentType = uplFileUploader.PostedFile.ContentType;

                    // Convert the uploaded file to a byte stream to save to your database. This could be a database table field of type Image in SQL Server
                    Stream strmStream = uplFileUploader.PostedFile.InputStream;
                    Int32 intFileLength = (Int32)strmStream.Length;
                    byte[] bytUpfile = new byte[intFileLength + 1];
                    strmStream.Read(bytUpfile, 0, intFileLength);
                    Converter c = new Converter();
                    Stream stream = new MemoryStream(bytUpfile);
                    string pdfStr3 = Convert.ToBase64String(bytUpfile);
                    c.File = pdfStr3;
                    c.Fileformate = ext;
                   
                   

                    JavaScriptSerializer js = new JavaScriptSerializer();
                    js.MaxJsonLength = Int32.MaxValue;
                    String result = string.Empty;
                    var webAddr = "http://localhost:13020/v1.2/words";
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.MediaType = "application/json";
                    httpWebRequest.Accept = "application/json";
                    httpWebRequest.Method = "POST";
                    String dir = "/MyFiles/";
                    String dateTime = string.Format("{0:dd-MM-yyyy_hh-mm-ss}", DateTime.UtcNow);
                    string path = HttpContext.Current.Server.MapPath(dir + "/");
                    DirectoryInfo info = new DirectoryInfo(path);
                    if (!info.Exists)
                    {
                        info.Create();
                    }
                    
                    try
                    {
                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            String docs = js.Serialize(c);
                            var myJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(c);
                            streamWriter.Write(myJsonString);
                            streamWriter.Flush();
                        }


                        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                        var resultedJosn = string.Empty;
                        if (httpResponse.StatusCode.ToString().ToLower().Equals("OK", StringComparison.OrdinalIgnoreCase))
                        {
                            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                            {
                                resultedJosn = streamReader.ReadToEnd();
                            }

                            if (!String.IsNullOrEmpty(resultedJosn))
                            {

                                result = js.Deserialize<String>(resultedJosn);

                            }

                        }
                        byte[] results = Convert.FromBase64String(result);
                        FileStream fs = new FileStream(path + orignalfileName + "."+ext, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        fs.Write(results, 0, results.Length);
                        strmStream.Close();
                    }
                    catch (Exception )
                    {
                        if (e.Equals("Unauthorized"))
                        {
                            result = "error";
                            //// some error massage of time out if api not responding
                        }
                    }
                    //saveFileToDb(strTestFileName, intFileSize, strContentType, bytUpfile); // or use uplFileUploader.SaveAs(Server.MapPath(".") + "filename") to save to the server's filesystem.
                    lblUploadResult.Text = "Converted Successfuly. Please find your file from the following Path. " + path;
                }
                catch (Exception err)
                {
                    lblUploadResult.Text = "The file was not updloaded because the following error happened: " + err.ToString();
                }
            }
            else
            {
                lblUploadResult.Text = "No File Uploaded because none was selected.";
            }
        }
    }
}