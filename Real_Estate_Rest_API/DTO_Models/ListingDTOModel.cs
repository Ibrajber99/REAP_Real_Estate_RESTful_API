using Real_Estate_Rest_API.Data.Entities.Customer_Related;
using Real_Estate_Rest_API.Data.Entities.Listing_Related;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Real_Estate_Rest_API.DTO_Models
{
    public class ListingDTOModel
    {
        public int ID { get; set; }

        [Required]
        public int customerId { get; set; }

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


        public List<int> Heatings { get; set; }

        public List<int> Features { get; set; }

    }
}