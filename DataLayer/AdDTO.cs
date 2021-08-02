using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class AdDTO
    {
        public string Adtype { get; set; }
        public string Category { get; set; }
        public int Cost { get; set; }
        public string ContentStructure { get; set; }
        public string ContentLink { get; set; }
        public string Tags { get; set; }
       
        
    }
}
