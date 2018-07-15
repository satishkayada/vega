using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vega.Controllers.Resources
{
    public class ContactResources
    {
        [Required]
        public string  Name { get; set; }
        public string  Email { get; set; }
        public string  Phone { get; set; }

    }
}