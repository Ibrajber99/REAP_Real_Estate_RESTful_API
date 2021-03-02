using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Real_Estate_Rest_API.Data.Entities.Listing_Related
{
    public class ListingAddress
    {
        public int ID { get; set; }

        [Required]
        public string StreetAddress { get; set; }

        [Required]
        public string Province { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public string Municiplity { get; set; }
    }
}