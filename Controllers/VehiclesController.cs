﻿using System;
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
        public VehiclesController(VegaDbContext pContext,IMapper pMapper)
        {
            this.mapper=pMapper;
            this.context=pContext;
        }
        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] VehicleResources VR)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var model=await context.Models.FindAsync(VR.ModelId);
            if (model==null)
            {
                ModelState.AddModelError("ModelId","Invalid Model");
                return BadRequest(ModelState);
            }

            var vehicle = mapper.Map<VehicleResources,Vehicle>(VR);
            vehicle.LastUdpate=DateTime.Now;

            context.Vehicles.Add(vehicle);
            await context.SaveChangesAsync();
            var result=mapper.Map<Vehicle,VehicleResources>(vehicle);
            return Ok(result);
        }
        [HttpPut("{id}")] // / api/vehicles/{id}

        public async Task<IActionResult> UpdateVehicle(int id,[FromBody] VehicleResources VR)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var vehicle = await context.Vehicles.Include(v => v.Features).FirstOrDefaultAsync(v => v.Id==id);
            if (vehicle==null) {
                return NotFound();
            }
            mapper.Map<VehicleResources,Vehicle>(VR,vehicle);
            vehicle.LastUdpate=DateTime.Now;

            await context.SaveChangesAsync();
            var result=mapper.Map<Vehicle,VehicleResources>(vehicle);
            return Ok(result);
        }
        [HttpDelete("{id}")] // / api/vehicles/{id}
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle=await context.Vehicles.FindAsync(id);
            if (vehicle==null) {
                return NotFound();
            }
            context.Vehicles.Remove(vehicle);
            await context.SaveChangesAsync();
            return Ok(id);
        }
        [HttpGet("{id}")] // / api/vehicles/{id}
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle=await context.Vehicles.Include(v => v.Features).SingleOrDefaultAsync(v => v.Id == id);
            if (vehicle==null) {
                return NotFound();
            }
            var VehicleResource = mapper.Map<Vehicle,VehicleResources>(vehicle);
            return Ok(VehicleResource);
        }
    }
}