using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace vega.Models
{
    [Table("Vehicles")]
    public class Vehicle
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public Model Model { get; set; }
        public bool IsRegistered { get; set; }
        public string  ContactName { get; set; }
        public string  ContactEmail { get; set; }
        public string  ContactPhone { get; set; }
        public DateTime LastUdpate { get; set; }

        public ICollection<VehicleFeature> Features { get; set; }
        public Vehicle()
        {
            Features = new Collection<VehicleFeature>();
        }

    }
}

