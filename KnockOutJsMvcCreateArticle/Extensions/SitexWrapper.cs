using System;
using System.Xml;
using System.Linq;
using System.ServiceModel;
using KnockOutJsMvcCreateArticle.SitexSoapReference;
using System.Net;
using System.Diagnostics;
using System.IO;
using System.Web.Configuration;

namespace KnockOutJsMvcCreateArticle.Extensions
{
    
        public class SitexMethods
        {
            string serviceKey = "98B609B5-5483-40B5-8FD4-B4D9993DD8A4";
            string SitexEndPointUrl;
            private SitexMethods() { }
            public SitexMethods(string sitexEndPointUrl)
            {
                SitexEndPointUrl = sitexEndPointUrl;
            }

            SitexAPISoapClient _SiteServiceclient;
            public SitexAPISoapClient SiteServiceClient(string endPoint = "http://rc.api.sitexdata.com/sitexapi/sitexapi.asmx")
            {
                if (_SiteServiceclient == null)
                {
                    EndpointAddress endpointAddress = new EndpointAddress(endPoint);
                    BasicHttpBinding binding = new BasicHttpBinding();
                    if (endpointAddress.Uri.ToString().ToLower().StartsWith("https"))
                        binding.Security.Mode = BasicHttpSecurityMode.Transport;
                    _SiteServiceclient = new SitexAPISoapClient(binding, endpointAddress);
                }
                return _SiteServiceclient;
            }

            /// <summary>
            /// Returns the response status message from the BKFS output
            /// </summary>
            /// <param name="fips"></param>
            /// <param name="apn"></param>
            /// <returns></returns>
            public string GetAVMProfilePath(string fips, string apn)
            {
                string response = string.Empty;
                SitexSoapReference.SitexAPISoapClient srv = SiteServiceClient(SitexEndPointUrl);
                string clientRefxml = Guid.NewGuid().ToString();
                // clientRefxml= "98B609B5-5483-40B5-8FD4-B4D9993DD8A4";
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | (SecurityProtocolType)3072;
                var svcResp = srv.ApnSearch(serviceKey, fips, apn, "145", clientRefxml);

                XmlDocument doc = new XmlDocument();
                doc.Load(svcResp.ReportURL);

                XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
                nsmgr.AddNamespace("sitex", "http://rc.api.sitexdata.com/GetReport");
                string name = "//sitex:SiteXValueReport/sitex:Status/text()";

                if (doc == null || doc.DocumentElement == null)
                {
                    response = "Invalid AVM";
                }
                else
                if (doc.SelectSingleNode(name, nsmgr) != null && doc.SelectSingleNode(name, nsmgr).Value.Trim().Length > 0
                    && doc.SelectSingleNode(name, nsmgr).Value.Trim().ToUpper() != "OK")
                {
                    Debug.Write(Environment.NewLine + Environment.NewLine + "**** XML Out: " + Environment.NewLine + doc.InnerXml);
                    response = doc.SelectSingleNode(name, nsmgr).Value.Trim();
                }
                else
                {
                    Debug.Write(Environment.NewLine + Environment.NewLine + "**** XML Out: " + Environment.NewLine + doc.InnerXml);
                    response = Environment.NewLine + Environment.NewLine + "Found AVM";
                }

            //string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "GPS AVM Files", $@"{fips}-{apn}.xml");
              string path = Path.Combine(WebConfigurationManager.AppSettings["SavedXMLFilePath"], "GPS AVM Files", $@"{fips}-{apn}.xml");
            
            if (Directory.Exists(Path.GetDirectoryName(path)) == false)
                    Directory.CreateDirectory(Path.GetDirectoryName(path));

                File.WriteAllText(path, doc.InnerXml);
                Debug.Write(Environment.NewLine + Environment.NewLine + $"File written to:{path} ");
                Console.WriteLine(Environment.NewLine + Environment.NewLine + $"File written to:{path} ");

                return path;
            }

            

        }
    
}