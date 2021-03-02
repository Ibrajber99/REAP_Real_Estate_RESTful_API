using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Real_Estate_Rest_API.Data.Entities.Listing_Related
{
    public class HeatingTypes
    {
        public HeatingTypes()
        {
            Listings = new List<Listing>();
        }

        public int ID { get; set; }

        [Required]
        public string HeatingType { get; set; }

        public ICollection<Listing> Listings { get; set; }
    }
}