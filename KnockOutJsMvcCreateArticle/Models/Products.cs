using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization; //Add 

namespace KnockOutJsMvcCreateArticle.Models
{
    [Serializable]
    [XmlRoot("Products"), XmlType("Products")]
    public class Products
    {
        public int ProductId { get; set; }
        
       // public string Type { get; set; }
        public string ProductName { get; set; }
        public string ProductCost { get; set; }
      //  public string Description { get; set; }

    }  
}