using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Ad
    {
        public int Id { get; set; }
        public int AdTypeId { get; set; }
        public AdType AdType{ get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int Cost { get; set; }
        public int ContentId { get; set; }
        public Content Content { get; set; }
        public int ViewingNumber { get; set; }
        public bool CurrentAd { get; set; }

        public ICollection<AdTag> AdTags { get; set; }
    }
}
