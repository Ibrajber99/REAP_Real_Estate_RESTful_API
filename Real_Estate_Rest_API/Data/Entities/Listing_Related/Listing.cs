using Real_Estate_Rest_API.Data.Entities.Customer_Related;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Real_Estate_Rest_API.Data.Entities.Listing_Related
{
    public class Listing
    {
        public Listing()
        {
            Heatings = new List<HeatingTypes>();
            Features = new List<PropertyFeatures>();
            Customer = new Customer();
        }

        public int ID { get; set; }

        [Required]
        public Customer Customer { get; set; }

        [Required]
        public ListingAddress Address { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal SquareFootage { get; set; }

        [Required]
        public int NumOfBeds { get; set; }

        [Required]
        public int NumofBaths { get; set; }

        [Required]
        public int NumofStories { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public  ICollection<HeatingTypes> Heatings { get; set; }

        public  ICollection<PropertyFeatures> Features { get; set; }
    }
}