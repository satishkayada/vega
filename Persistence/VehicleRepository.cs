using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vega.Core.Models;
using vega.Core;
namespace vega.Persistence
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VegaDbContext context;
        public VehicleRepository(VegaDbContext Context)
        {
            this.context = Context;
        }
        public async Task<Vehicle> GetVehicle()
        {
            return await GetVehicle(0, false);
        }
        public async Task<Vehicle> GetVehicle(int id,bool includedRelated = true)
        {
            if (!includedRelated)
                return await context.Vehicles.FindAsync(id);

            return await context.Vehicles
                .Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature)
                .Include(vf => vf.Model)
                    .ThenInclude(m => m.Make)  
                .SingleOrDefaultAsync(v => v.Id == id);
        }
        public async Task<Model> GetModel(int id)
        {
            return await context.Models
                .FirstAsync(V => V.Id==id);
        }
        public void Add(Vehicle vehicle)
        {
            context.Vehicles.Add(vehicle);
        }
        public void Remove(Vehicle vehicle)
        {
            context.Vehicles.Remove(vehicle);
        }
    }
}