using System.Threading.Tasks;
using vega.Models;

namespace vega.Persistence
{
    public interface IVehicleRepository
    {
         Task<Vehicle> GetVehicle(int id,bool includedRelated = true);
         void Add(Vehicle vehile);
         void Remove(Vehicle vehile);
    }
}