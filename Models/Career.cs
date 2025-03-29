using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaltaDataAccess.Models
{
    public class Career
    {
        public Career()
        {
            ITEMS = new List<Careeritem>();
        }

        public Guid ID { get; set; }
        public string TITLE { get; set; }
        public IList<Careeritem> ITEMS { get; set; }

    }
}
