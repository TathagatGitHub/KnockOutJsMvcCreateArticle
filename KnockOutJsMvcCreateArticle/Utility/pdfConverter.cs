using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Xsl;
using System.Text;
using System.Xml;


namespace KnockOutJsMvcCreateArticle.Utility
{
    public class pdfConverter
    {

        // Getting file path     
        public String xmlToPdf(string xmlpath, string xslpath, string htmlPathtoSave)
        {
            //string strXSLTFile = "Content/Files/XSLTFile1.xslt";
            //string strXMLFile = "Content/Files/XMLFile1.xml";
            string strXSLTFile = xslpath;
            string strXMLFile = xmlpath;

            // Creating XSLCompiled object     
            XslCompiledTransform objXSLTransform = new XslCompiledTransform();
            objXSLTransform.Load(strXSLTFile);
            // Execute the transform and output the results to a file.
            //string htmlFilePath = HttpContext.Current.Server.MapPath("~/App_Data/SavedProfiles");//Path of the xml script  
            string htmlfile = htmlPathtoSave + "XMLFile1.html";
            
                objXSLTransform.Transform(strXMLFile, htmlfile);
            //objXSLTransform.Transform(strXMLFile, @"\Projects\KnockOutJsMvcCreateArticle\KnockOutJsMvcCreateArticle\Content\Files\XMLFile1.html");

            return htmlfile;
            
        }
        
    }
}