using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vega.Controllers.Resources
{
  
    public class SaveVehicleResources
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
      
        [Required]
        public ContactResources Contact { get; set; }
        public ICollection<int> Features { get; set;}

        public SaveVehicleResources()
        {
            Features= new Collection<int>();
        }
    }
}