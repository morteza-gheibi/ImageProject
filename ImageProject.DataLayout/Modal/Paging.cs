using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class Paging
    {
        public int page { get; set; }
        public int limit { get; set; }
        public string sortBy { get; set; }
    public string direction { get; set; } = "DESC";
    }
