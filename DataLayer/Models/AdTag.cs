using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class AdTag
    {
        public int Id { get; set; }
        public int? Ad_Id { get; set; }
        public int Tag_Id { get; set; }
        public Ad Ad { get; set; }
        public Tag Tag { get; set; }

    }
}
