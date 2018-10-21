using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vega.Controllers.Resources
{
    public class VehicleResources
    {
        public int Id { get; set; }
        public KeyValuePairResource Model { get; set; }
        public KeyValuePairResource Make { get; set; }
        public bool IsRegistered { get; set; }
        public ContactResources  Contact { get; set; }
        public DateTime LastUdpate { get; set; }

        public ICollection<KeyValuePairResource> Features { get; set; }
        public VehicleResources()
        {
            Features = new Collection<KeyValuePairResource>();
        }
    }
}