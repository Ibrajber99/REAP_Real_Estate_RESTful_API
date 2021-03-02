using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Real_Estate_Rest_API.DTO_Models
{
    public class HeatingDTOModel
    {
        public int ID { get; set; }

        [Required]
        public string HeatingType { get; set; }

    }
}