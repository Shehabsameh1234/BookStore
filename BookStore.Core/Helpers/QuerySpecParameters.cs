﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Helpers
{
    public class QuerySpecParameters
    {
        private const int MaxPageSize = 10;
        private int pageSize=5;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value >MaxPageSize ? MaxPageSize :value; }
        }
        public int PageIndex { get; set; } = 1;
        public string? Sort { get; set; }
        public int? CategoryId { get; set; }
        
    }
}
