using System.Threading.Tasks;
using vega.Core.Models;

namespace vega.Core
{
    public interface IVehicleRepository
    {
         Task<Vehicle> GetVehicle(int id,bool includedRelated = true);
         Task<Model> GetModel(int id);
         void Add(Vehicle vehile);
         void Remove(Vehicle vehile);
    }
}