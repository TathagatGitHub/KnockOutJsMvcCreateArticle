using System;
using System.Collections.Generic;
using System.Linq.Dynamic;
using System.Web;
using System.Data.SqlClient;
using KnockOutJsMvcCreateArticle.Models;

namespace KnockOutJsMvcCreateArticle.Models
{
    public class QueryOptions
    {
        public QueryOptions()
        {
            SortField = "Id";
            SortOrder = SortOrder.ASC;
        }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public string SortField { get; set; }
        public SortOrder SortOrder { get; set; }
        public string Sort
        {
            get
            {
                return string.Format("{0} {1}", SortField, SortOrder.ToString());
            }
        }
    }
}