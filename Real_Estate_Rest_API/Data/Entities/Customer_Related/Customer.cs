using Real_Estate_Rest_API.CustomeValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Real_Estate_Rest_API.Data.Entities.Customer_Related
{

    public class Customer
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        public CustomerAddress CustomerAddress { get; set; }//not virtual to prevent lazy loading

        [Required]
        [RegularExpression(@"^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$",
        ErrorMessage = "Please Provide a valid Phone number")]
        [Display(Name = "Cell Phone")]
        public string PhoneNumber { get; set; }

        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",
          ErrorMessage = "Please provide a valid email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Min19YearsOfAge]
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime dateOfBirth { get; set; }

        [Display(Name = "Created On")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Updated On")]
        public DateTime DateUpdated { get; set; }

        public bool IsActive { get; set; }
    }
}