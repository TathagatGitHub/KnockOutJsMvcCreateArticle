using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using KnockOutJsMvcCreateArticle.Models;
using System.Data;

namespace KnockOutJsMvcCreateArticle.Extensions
{
    public class XMLHelper
    {
        /// <summary>  
        /// Return list of products from XML.  
        /// </summary>  
        /// <returns>List of products</returns>  
        public List<Products> RetrunListOfProducts()
        {
            string xmlData = HttpContext.Current.Server.MapPath("~/App_Data/ProductsData.xml");//Path of the xml script  
            DataSet ds = new DataSet();//Using dataset to read xml file  
            ds.ReadXml(xmlData);
            var products = new List<Products>();
            products = (from rows in ds.Tables[0].AsEnumerable()
                        select new Products
                        {
                            ProductId = Convert.ToInt32(rows[0].ToString()), //Convert row to int  
                            ProductName = rows[1].ToString(),
                            ProductCost = rows[2].ToString(),
                        }).ToList();
            return products;
        }  
    }
}