using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;

namespace StarChart.Controllers
{
    [Route("")]
    [ApiController]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            var celestialObject = _context.CelestialObjects.Find(id);
            var allObjects = _context.CelestialObjects.Select(all => all);

            if(celestialObject == null)
            {
                return NotFound();
            }

            foreach(var obojectToFind in allObjects)
            {
                if(obojectToFind.OrbitedObjectId == id)
                {
                    obojectToFind.Satellites.Add(celestialObject);
                }
            }
            

            return Ok(celestialObject);
        }
    }
}
