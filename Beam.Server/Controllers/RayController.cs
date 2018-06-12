﻿using Beam.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Beam.Server.Mappers;

namespace Beam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RayController : ControllerBase
    {
        Data.BeamContext _context;
        public RayController(Data.BeamContext context)
        {
            _context = context;
        }

        [HttpGet("{FrequencyId}")]
        public List<Ray> Rays(int FrequencyId)
        {
            return GetRays(FrequencyId);
        }

        private List<Ray> GetRays(int FrequencyId)
        {
            return _context.Rays.Where(r => r.FrequencyId == FrequencyId)
                .Select(r => r.ToShared()).ToList();
        }

        [HttpPost("[action]")]
        public List<Ray> Add([FromBody] Ray ray)
        {
            _context.Add(ray.ToData());
            _context.SaveChanges();
            return GetRays(ray.FrequencyId);
        }

    }
}