using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vega.Controllers.Resources;
using vega.Models;
using AutoMapper;
using vega.Persistence;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace vega.Controllers
{
    [Produces("application/json")]
    [Route("api/vehicles")]
    public class VehiclesController : Controller
    {
        private readonly IMapper mapper;
        private readonly VegaDbContext context;
        private readonly IVehicleRepository repository;
        public VehiclesController(VegaDbContext pContext, IMapper pMapper, IVehicleRepository repository)
        {
            this.repository = repository;
            this.mapper = pMapper;
            this.context = pContext;
        }
        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResources VR)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = await context.Models.FindAsync(VR.ModelId);
            if (model == null)
            {
                ModelState.AddModelError("ModelId", "Invalid Model");
                return BadRequest(ModelState);
            }

            var vehicle = mapper.Map<SaveVehicleResources, Vehicle>(VR);
            vehicle.LastUdpate = DateTime.Now;
            //context.Vehicles.Add(vehicle);
            repository.Add(Vehicle);
            await context.SaveChangesAsync();

            // This will include all the other name properties form id
            vehicle = await repository.GetVehicle(vehicle.Id);
            var result = mapper.Map<Vehicle, VehicleResources>(vehicle);
            return Ok(result);
        }
        [HttpPut("{id}")] // / api/vehicles/{id}

        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResources VR)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicle = await context.Vehicles.Include(v => v.Features).FirstOrDefaultAsync(v => v.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }
            mapper.Map<SaveVehicleResources, Vehicle>(VR, vehicle);
            vehicle.LastUdpate = DateTime.Now;

            await context.SaveChangesAsync();

            // This will include all the other name properties form id
            vehicle = await repository.GetVehicle(vehicle.Id);

            var result = mapper.Map<Vehicle, VehicleResources>(vehicle);
            return Ok(result);
        }
        [HttpDelete("{id}")] // / api/vehicles/{id}
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await repository.GetVehicle(id,includedRelated:false);
            if (vehicle == null)
            {
                return NotFound();
            }
            repository.Remove(vehicle);
            await context.SaveChangesAsync();
            return Ok(id);
        }
        [HttpGet("{id}")] // / api/vehicles/{id}
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await repository.GetVehicle(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            var VehicleResource = mapper.Map<Vehicle, VehicleResources>(vehicle);
            return Ok(VehicleResource);
        }
    }
}