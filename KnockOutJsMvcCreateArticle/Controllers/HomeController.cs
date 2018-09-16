using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Data;
using KnockOutJsMvcCreateArticle.Models;
using KnockOutJsMvcCreateArticle.Extensions;
using KnockOutJsMvcCreateArticle.Utility;
using KnockOutJsMvcCreateArticle.SitexSoapReference;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using System.Web.Configuration;
//using System.Web.Helpers;
public static class JsonExtensions
{
    public static dynamic ToDynamic(this JToken token)
    {
        if (token == null)
            return null;
        else if (token is JObject)
            return token.ToObject<ExpandoObject>();
        else if (token is JArray)
            return token.ToObject<List<ExpandoObject>>().Cast<dynamic>().ToList();
        else if (token is JValue)
            return ((JValue)token).Value;
        else
            // JConstructor, JRaw
            throw new JsonSerializationException(string.Format("Token type not implemented: {0}", token));
    }
       
}
public class ResponseData
{
    public List<Property> ResponseItem { get; set; }
}

//public class AddressResponse
//{
//    public string Address { get; set; }
//    public string APN { get; set; }
//    public string Zip { get; set; }
//}
namespace KnockOutJsMvcCreateArticle.Controllers
{
    
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }
        // Administration Section
        public ActionResult xmlToHTML()
        {
            pdfConverter var = new pdfConverter();
           String path = var.xmlToPdf(@"\Projects\KnockOutJsMvcCreateArticle\KnockOutJsMvcCreateArticle\Content\Files\XMLFile1.xml", 
               @"\Projects\KnockOutJsMvcCreateArticle\KnockOutJsMvcCreateArticle\Content\Files\XSLTFile1.xslt",
               WebConfigurationManager.AppSettings["SavedHTMLProfileFilePath"]);
                 return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Administration()
        {
            ViewBag.Message = "Administration";
            List<string> allRoles = new List<string>();
            List<string> allUsers = new List<string>();
             allRoles = System.Web.Security.Roles.GetAllRoles().ToList();
             ViewBag.allRoles = allRoles;
                    
          //   Dropdown users and roles

             dynamic users;
             int ct;
             using (var db = WebMatrix.Data.Database.Open("ArticleDBContexConnection"))
             {
                 users = db.Query("SELECT username FROM aspnet_Users").ToList();
                 
             }
             string st = users[0].username.ToString();
             List<SelectListItem> items = new List<SelectListItem>();
             String selecteditemtext="xx";
             for (int i = 0; i < users.Count; i++)
             {
                 allUsers.Add(users[i].username.ToString());
                 String xx = users[i].username.ToString();
                 items.Add(new SelectListItem { Text = xx, Value = xx });
                 if(i == 1)
	                {
                        selecteditemtext=xx; 
                    }

             }

            ViewBag.allUsers = allUsers;
             ViewBag.dropdowusers = items;
             ViewBag.SelectedItem = selecteditemtext; // use this only, If you want to have a selected item by default
             List<SelectListItem> roleitems = new List<SelectListItem>();
             for (int i = 0; i < allRoles.Count; i++ )
             {
                 String xx = allRoles[i];
                 roleitems.Add(new SelectListItem { Text = xx, Value = xx });
             }
             ViewBag.dropdownroles = roleitems;

                 return View();
        }
        //[Authorize(Roles = "Administrator")]
        public ActionResult CreateRole(string roleName)
        {
            ViewBag.Message = "Create Role.";
            if (!System.Web.Security.Roles.RoleExists(roleName)) // this line throws the error.
            {
                System.Web.Security.Roles.CreateRole(roleName);
            }

            string[] userRoles = System.Web.Security.Roles.GetRolesForUser("tathagat");
            userRoles = System.Web.Security.Roles.GetRolesForUser();
           
        
            List<string> listOfRoles = new List<string>();
            listOfRoles = System.Web.Security.Roles.GetAllRoles().ToList();
            ViewBag.Roles = listOfRoles;
            return RedirectToAction("Administration");           
           // return View("Administration", listOfRoles);
        }

        public ActionResult AssingRole(string dropdowusers, string dropdownroles)
        {
            if (!System.Web.Security.Roles.IsUserInRole(dropdownroles))
            {
                System.Web.Security.Roles.AddUserToRole(dropdowusers, dropdownroles);
            }
            return RedirectToAction("Administration");
        }
        public ActionResult RemoveRole(string dropdowusers, string dropdownroles)
        {
            if (System.Web.Security.Roles.IsUserInRole(dropdownroles))
            {
                System.Web.Security.Roles.RemoveUserFromRole(dropdowusers, dropdownroles);
            }
            return RedirectToAction("Administration");
        }

        public ActionResult RemoveUser(string dropdowusers)
        {
            try
            {
                // TODO: Add delete logic here
                if (Roles.GetRolesForUser(dropdowusers).Count() > 0)
                {
                    Roles.RemoveUserFromRoles(dropdowusers, Roles.GetRolesForUser(dropdowusers));
                }
          
                using (var db = WebMatrix.Data.Database.Open("ArticleDBContexConnection"))
                {
                    db.Query("delete aspnet_Users where username='" + dropdowusers+"'");

                }
                return RedirectToAction("Administration");
            }
            catch
            {
                return RedirectToAction("Administration");
            }
            return RedirectToAction("Administration");
        }

        [OutputCache(Duration = 60, VaryByParam = "none")]
        public ActionResult ReturnDateTime()
        {
            ViewBag.Message = DateTime.Now.ToString();
            return View();
        }

        public ActionResult ReadXMLFromFile()
        {
            XMLHelper readXML = new XMLHelper();
            var data = readXML.RetrunListOfProducts();
            return View(data.ToList());
        } 

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult WSExamples()
        {
            return View();
        }
        // return View(); is not going to work, since have not created a view and passed any model. 
        //So far just to test if ws call is working. Using break points to check the return value.
        public ActionResult ConvertTemp()
        {

            var service = new TempConvertReference.TempConvertSoapClient("TempConvertSoap");
            var result = service.CelsiusToFahrenheit("20");
            ViewBag.Celsius = 20;
            ViewBag.Fahrenheit = result;
            return View("_ConvertTemp");
        
        }

        // return View(); is not going to work, since have not created a view and passed any model. 
        //So far just to test if ws call is working. Using break points to check the return value.
  public ActionResult RestApiWebClient()
 {          
    var client = new System.Net.WebClient();
    var json = client.DownloadString("http://api.apixu.com/v1/current.json?key=9a61e0ad6654495394d20958152311&q=90620");
    // this is return jason
    //{"location":{"name":"Paris","region":"Ile-De-France","country":"France","lat":48.87,"lon":2.33,"tz_id":"Europe/Paris","localtime_epoch":1448248228,"localtime":"2015-11-23 3:10"},"current":{"last_updated_epoch":1448247610,"last_updated":"2015-11-23 03:00","temp_c":1.0,"temp_f":33.8,"condition":{"text":"Overcast","icon":"//cdn.apixu.com/weather/64x64/night/122.png","code":1009},"wind_mph":4.3,"wind_kph":6.8,"wind_degree":340,"wind_dir":"NNW","pressure_mb":1020.0,"pressure_in":30.6,"precip_mm":0.7,"precip_in":0.03,"humidity":86,"cloud":100,"feelslike_c":-1.2,"feelslike_f":29.9}}
    
    // below two lines are used for parsing json and building dynamic object. Good way is to define a model and then parse into to it.
    
    // return View(); is not going to work, since have not created a view and passed any model. 
    //So far just to test if ws call is working. Using break points to check the return value.
    JavaScriptSerializer js = new JavaScriptSerializer();
    dynamic weather = js.Deserialize<dynamic>(json);
   // Get the Location node
    var loc = weather["location"];
   // Get the Current node
    var current = weather["current"];
   
    // Get the Condition node
  //  var condition = weather["condition"];

      // Location fileds
   string name = (string)loc["name"];
   ViewBag.name = name;
   string country = (string)loc["country"];
   ViewBag.country = country;

      // Current fields

   decimal temp_f = current["temp_f"];
   ViewBag.temp = temp_f;

   string last_updated = current["last_updated"];
   ViewBag.last_updated = last_updated;

      // Condition fields

   decimal wind_mph = current["wind_mph"];
   ViewBag.wind_mph = wind_mph;

   string wind_dir = current["wind_dir"];
   ViewBag.wind_dir = wind_dir;

   decimal humidity = current["humidity"];
   ViewBag.humidity = humidity;

   decimal cloud = current["cloud"];
   ViewBag.cloud = cloud;
   return View("_RestApiWebClient");
        
//}
}
public async System.Threading.Tasks.Task<ActionResult> HttpClientMethod()
{
    string url = "http://api.apixu.com/v1/current.json?key=9a61e0ad6654495394d20958152311&q=90620";
    using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
    {
        client.BaseAddress = new Uri(url);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        System.Net.Http.HttpResponseMessage response = await client.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            Weather weather = (Weather)JsonConvert.DeserializeObject(data, (typeof(Weather)));
            return View("WeatherDetail", weather);       
        
        }
    }
    return View("Detail");
}

public async System.Threading.Tasks.Task<ActionResult> HttpClientMethodAPNSearch()
{
    string url = "https://gpsapi.fnf.com/api/ReportClientApi/RequestReport/";
        var obj = JsonConvert.DeserializeObject("{"
                + "Authentication:    {"
                + "SessionId: '',"
                + "Username: 'gsuser',"
                + "Password: 'testing1',"
                + "IPAddress: ''"
                + "},"
                + "Data:    {"
                + "ReportId: 85,"
                + "ReportOptions:       {"
                + "Address1: '7187 Santa Lucia Cir',"
                + "City: 'Buena Park',"
                + "State: 'CA',"
                + "Zip: '',"
                + "ClientReference:'' "
                + "}"
                + "}"
                + " }");

    using (var client = new System.Net.Http.HttpClient())
    {
        //Passing service base url  
        client.BaseAddress = new Uri(url);

        client.DefaultRequestHeaders.Clear();
        //Define request data format  
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                List<Property> listProperty = new List<Property>();
        using (System.Net.Http.HttpResponseMessage Res = await client.PostAsJsonAsync(url, obj))
        {
             var data = await Res.Content.ReadAsAsync<ResponseData>();

             if (Res.IsSuccessStatusCode)
             {
                        string endPointUrl = WebConfigurationManager.AppSettings["SiteXRCWebServiceURL"];
                        SitexMethods sitexMethodsInstance= new SitexMethods(endPointUrl);
                        int count= data.ResponseItem.Count();
                        for (int i = 0; i < count; i++)
                        {
                            String address = data.ResponseItem[i].Address;
                            String ZIP = data.ResponseItem[i].Zip;
                            String APN = data.ResponseItem[i].APN;
                            String FIPS = "06059";
                            String AVMProfilePath = sitexMethodsInstance.GetAVMProfilePath(FIPS,APN);
                            data.ResponseItem[i].ProfilePath = AVMProfilePath;
                            listProperty.Add(data.ResponseItem[i]);
                        } 
             }
            //returning the employee list to view  
            return View("~/Views/Property/Index.cshtml", listProperty);
        }
    }
    
}

  
        // return View(); is not going to work, since have not created a view and passed any model. 
        //So far just to test if ws call is working. Using break points to check the return value.

        public ActionResult StockQuote()
      
        {

            ViewBag.StockName = "MSFT";
            var service = new StockQuoteService.StockQuoteSoapClient("StockQuoteSoap");
            var fragment = service.GetQuote("MSFT");
            var xml = XDocument.Parse(fragment);
            var stockQuote = xml.Descendants("StockQuotes").Descendants("Stock").Select(quote => new
            {
                Symbol = quote.Element("Symbol").Value,
                Last = quote.Element("Last").Value,
                Time = quote.Element("Time").Value,
                Change = quote.Element("Change").Value,
                Open = quote.Element("Open").Value,
                Low = quote.Element("Low").Value,
                Volume = quote.Element("Volume").Value,
                MktCap = quote.Element("MktCap").Value,
                PreviousClose = quote.Element("PreviousClose").Value,
                PercentageChange = quote.Element("PercentageChange").Value,
                AnnRange = quote.Element("AnnRange").Value,
                Earns = quote.Element("Earns").Value,
                PE = quote.Element("P-E").Value,
                Name = quote.Element("Name").Value
            }).FirstOrDefault();

            var last = stockQuote.Last;
            ViewBag.LastQuote = last;
            var change = stockQuote.Change;
            ViewBag.QuoteChange = change;
            var vol = stockQuote.Volume;
            ViewBag.Quotevol = vol;
            return View("_StockQuote");
        }


        [Authorize]
        public async System.Threading.Tasks.Task<ActionResult> HttpClientXMLStockQuote()
        {
            ViewBag.StockName = "UNH";
            string url = "http://finance.yahoo.com/webservice/v1/symbols/UNH/quote?format=xml";
            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/xml"));
                System.Net.Http.HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    XElement config = XElement.Parse(data);
                    var xml = XDocument.Parse(data);
                    var dataList = new List<dynamic>();

                    foreach (XElement child in config.Descendants("resources"))
                    {
	                    int ct = (int) child.Attribute ("count");   
	
		                foreach (XElement child2 in child.Descendants("resource"))
		                {
			                string xx = (string) child2.Attribute ("classname");
			                
			                foreach (XElement child3 in child2.Descendants("field"))
			                 {
			                    string yy = (string) child3.Attribute ("name"); 
			                    string val=child3.Value.ToString();
                                if (yy == "name") { ViewBag.Name = val; }
                                if (yy == "price") { ViewBag.Price = val; }
                                if (yy == "symbol") { ViewBag.Symbol = val; }
                                if (yy == "type") { ViewBag.Type = val; }
                                if (yy == "volume") { ViewBag.Volume = val; }
                               
			                }
		                }
	
	                }

                }
            }
            return View("_HttpClientXMLStockQuote");
        }
        [Authorize]
        public async System.Threading.Tasks.Task<ActionResult> LINQ_TO_XML_ELEMENTS()
        {
            string url = "http://finance.yahoo.com/webservice/v1/symbols/UNH/quote?format=xml";
            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/xml"));
                System.Net.Http.HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    XElement config = XElement.Parse(data);
                    var xml = XDocument.Parse(data);
                    var dataList = new List<dynamic>();

                    foreach (XElement child in config.Elements())
                    {
                        
                        foreach (XElement child2 in child.Elements())
                        {
                            //child2.Name.Dump ("Child element name");//LINQ editor command
                            foreach (XElement child3 in child2.Elements())
                            {
                               //string attr= child3.Attribute("name").ToString();
                                string attr = child3.Attribute("name").Value;
                                string val=child3.Value.ToString(); //LINQ editor command
                                if (attr == "name") { ViewBag.Name = val; }
                                if (attr == "price") { ViewBag.Price = val; }
                                if (attr == "symbol") { ViewBag.Symbol = val; }
                                if (attr == "type") { ViewBag.Type = val; }
                                if (attr == "volume") { ViewBag.Volume = val; }
                            }
                        }

                    }


                }
            }
            return View("_LinqToXmlElements");
        }
        [Authorize]
        public async System.Threading.Tasks.Task<ActionResult> HttpClientXMLXPathParsing()
        {
            string url = "http://finance.yahoo.com/webservice/v1/symbols/UNH/quote?format=xml";
            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/xml"));
                System.Net.Http.HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                   // XElement config = XElement.Parse(data);
                 //  XDocument xml = XDocument.Parse(data);
                   // var dataList = new List<dynamic>();
                    System.Xml.XmlDocument xml = new System.Xml.XmlDocument();
                    xml.LoadXml(data); // suppose that myXmlString contains "<Names>...</Names>"

                    System.Xml.XmlNodeList xnList = xml.SelectNodes("resources/resource");
                    foreach (System.Xml.XmlNode xn in xnList)
                    {
                        string field = xn["field"].InnerText;
                       // string lastName = xn["LastName"].InnerText;
                        Console.WriteLine("Name: {0}", field);
                        //Console.WriteLine("Name: {0} {1}", firstName, lastName);
                    }


                }
            }
            return View();
        }

        
    }
}
