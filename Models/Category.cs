using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaltaDataAccess.Models
{
    public class Category
    {
        public Guid ID { get; set; }
        public string TITLE { get; set; }
        public string  URL { get; set; }
        public string SUMARRY { get; set; }
        public int ORDER { get; set; }
        public string DESCRIPTION { get; set; }
        public bool FEATURED { get; set; }


    }
}
