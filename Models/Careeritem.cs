using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaltaDataAccess.Models
{
    public class Careeritem
    {
        public Guid ID { get; set; }
        public string TITLE { get; set; }
        public Course COURSE { get; set; }
    }
}
