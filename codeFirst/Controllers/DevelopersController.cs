using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using codeFirst.Models;

namespace codeFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevelopersController : ControllerBase
    {
        private readonly DeveloperDbContext _context;

        public DevelopersController(DeveloperDbContext context)
        {
            _context = context;
        }

        // GET: api/Developers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Developer>>> GetDeveloper()
        {

			//Eager Loading

			return await _context.Developer
										 .Include(dev => dev.Country)
										 .ToListAsync();

		}

        [HttpGet("object")]
        public async Task<ActionResult<IEnumerable<Object>>> GetDeveloperLinQ()
        {
            //LinQ

            var query = await (from dev in _context.Developer
                        join co in _context.Country on dev.Country.CountryId equals co.CountryId
                        where co.Name == "Canada"
                        select new 
                        { 
                            dev.FirstName, 
                            dev.Skills, 
                            co.Name 
                        }).ToListAsync();

            return query;

        }

        public class BetterDev
        {
            public string firstName { get; set; }
            public string skill { get; set; }
            public string name { get; set; }
        }

        [HttpGet("better")]
        public async Task<ActionResult<IEnumerable<BetterDev>>> GetDeveloperTest()
        {
            //LinQ

            var query = await (from dev in _context.Developer
                               join co in _context.Country on dev.Country.CountryId equals co.CountryId
                               where co.Name == "Canada"
                               select new BetterDev
                               {
                                   firstName = dev.FirstName,
                                   skill = dev.Skills,
                                   name = co.Name
                               }).ToListAsync();

            return query;

        }

        [HttpGet("join")]
        public async Task<ActionResult<IEnumerable<Object>>> GetDeveloperJoin()
        {
            //LinQ

            var query = await (_context.Developer.Join(_context.Country, 
                dev => dev.Country.CountryId,
                co => co.CountryId,
                (developer, country) => new 
                { 
                    Developer = developer.FirstName, 
                    Country = country.Name, 
                    Population = country.Population 
                })).ToListAsync();

            return query;

        }

        // GET: api/Developers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Developer>> GetDeveloper(int id)
        {
            // Explicit Loading
            var developer = await _context.Developer.SingleAsync(dev => dev.DeveloperId == id);

            // One relationship
            //_context.Entry(developer)
            //        .Reference(dev => dev.Country)
            //        .Load();

            //Two
            _context.Entry(developer)
                   .Reference(dev => dev.Country)
                   .Query()
                   .Include(co => co.Developers)
                   .Load();

            //With Collection
            //_context.Entry(developer)
            //       .Collection(dev => dev.Country)
            //       .Query()
            //       .Include(co => co.Developers)
            //       .Load();

            if (developer == null)
            {
                return NotFound();
            }

            return developer;
        }

        // PUT: api/Developers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")] 
        public async Task<IActionResult> PutDeveloper(int id, Developer developer)
        {
            if (id != developer.DeveloperId)
            {
                return BadRequest();
            }

            _context.Entry(developer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeveloperExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Developers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Developer>> PostDeveloper(Developer developer)
        {
            _context.Developer.Add(developer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDeveloper", new { id = developer.DeveloperId }, developer);
        }

        // DELETE: api/Developers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeveloper(int id)
        {
            var developer = await _context.Developer.FindAsync(id);
            if (developer == null)
            {
                return NotFound();
            }

            _context.Developer.Remove(developer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeveloperExists(int id)
        {
            return _context.Developer.Any(e => e.DeveloperId == id);
        }
    }
}
