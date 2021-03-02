using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Real_Estate_Rest_API.Data.Entities.Customer_Related
{
    public class CustomerAddress
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        [Required]
        [Display(Name = "Municipality")]
        public string Municipality { get; set; }

        [Required]
        public string Province { get; set; }

        [Required]
        [Display(Name = "Postal Code")]
        [RegularExpression(@"^([a-zA-Z]\d[a-zA-Z]( )?\d[a-zA-Z]\d)$",
            ErrorMessage = "Please provide a valid Postal code")]
        public string PostalCode { get; set; }

    }
}