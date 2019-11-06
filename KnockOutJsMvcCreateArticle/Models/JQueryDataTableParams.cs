﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnockOutJsMvcCreateArticle.Models
{
    public class JQueryDataTableParams
    {
        /// <summary>
        /// Request sequence number sent by DataTable,
        /// same value must be returned in response
        /// </summary>       
        public string sEcho { get; set; }

        /// <summary>
        /// Text used for filtering
        /// </summary>
        public string sSearch { get; set; }

        /// <summary>
        /// Number of records that should be shown in table
        /// </summary>
        public int iDisplayLength { get; set; }

        /// <summary>
        /// First record that should be shown(used for paging)
        /// </summary>
        public int iDisplayStart { get; set; }

        /// <summary>
        /// Number of columns in table
        /// </summary>
        public int iColumns { get; set; }

        /// <summary>
        /// Number of columns that are used in sorting
        /// </summary>
        public int iSortingCols { get; set; }

        /// <summary>
        /// Comma separated list of column names
        /// </summary>
        public string sColumns { get; set; }

        /// <summary>
        /// Order no of the column that is used to do sorting
        /// </summary>
        public int iSortCol_0 { get; set; }

        /// <summary>
        /// Sort direction
        /// </summary>
        public string sSortDir_0 { get; set; }
    }
}