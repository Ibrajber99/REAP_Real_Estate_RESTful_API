using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Real_Estate_Rest_API.Data.Entities.Listing_Related
{
    public class PropertyFeatures
    {
        public PropertyFeatures()
        {
            Listings = new List<Listing>();
        }

        public int ID { get; set; }

        [Required]
        public string FeatureType { get; set; }

        public ICollection<Listing> Listings { get; set; }
    }
}