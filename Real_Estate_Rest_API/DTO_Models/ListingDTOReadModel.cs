using Real_Estate_Rest_API.Data.Entities.Customer_Related;
using Real_Estate_Rest_API.Data.Entities.Listing_Related;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Real_Estate_Rest_API.DTO_Models
{
    public class ListingDTOReadModel
    {
        public int ID { get; set; }

        public Customer customer { get; set; }

        public ListingAddress Address { get; set; }

        public decimal Price { get; set; }

        public decimal SquareFootage { get; set; }

        public int NumOfBeds { get; set; }

        public int NumofBaths { get; set; }

        public int NumofStories { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public List<HeatingTypes> Heatings { get; set; }

        public List<PropertyFeatures> Features { get; set; }

    }
}