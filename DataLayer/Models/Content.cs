using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Content
    {
        public int Id { get; set; }
        public string Structure { get; set; }
        public string Link { get; set; }
        public ICollection<Ad> Ads { get; set; }

    }
}
