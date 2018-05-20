using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vega.Models;
using vega.Persistence;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using vega.Controllers.Resources;

namespace vega.Controllers
{
    public class FeaturesController : Controller
    {
        private readonly VegaDbContext context;
        private readonly IMapper mapper;

        public FeaturesController(VegaDbContext Context,IMapper mapper)
        {
            context = Context;
            this.mapper = mapper;
        }
        [HttpGet("api/Features")]
        public async Task<IEnumerable<FeatureResource>> GetFeatures()
        {
            var featrue= await context.Features.ToListAsync();
            return mapper.Map<List<Feature>,List<FeatureResource>>(featrue);
        }
    }
}