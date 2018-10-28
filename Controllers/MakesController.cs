using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vega.Core.Models;
using vega.Persistence;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using vega.Controllers.Resources;

namespace vega.Controllers
{
    public class MakesController : Controller
    {
        private readonly VegaDbContext context;
        private readonly IMapper mapper;

        public MakesController(VegaDbContext Context,IMapper mapper)
        {
            context = Context;
            this.mapper = mapper;
        }
        [HttpGet("api/Makes")]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
            var make= await context.Makes.Include(m => m.Models).ToListAsync();
            return mapper.Map<List<Make>,List<MakeResource>>(make);

        }
    }
}