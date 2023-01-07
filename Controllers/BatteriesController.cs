using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rocket_Elevators_Rest_API.Models;

namespace Rocket_Elevators_Rest_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatteriesController : ControllerBase
    {
        private readonly RocketElevatorsContext _context;

        public BatteriesController(RocketElevatorsContext context)
        {
            _context = context;
        }

        // GET: api/<Batteries>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Battery>>> GetBatteries()
        {
            return await _context.batteries.ToListAsync();
        }

        // https://localhost:7234/api/batteries/building/{id}
        [HttpGet("building/{id}")]
        public async Task<ActionResult<IEnumerable<Battery>>> GetBatteriesByBuild(int id)
        {
            List<Battery> batteriesList = await _context.batteries.ToListAsync();
            List<Battery> filteredList = new List<Battery>();
            filteredList = batteriesList.Where(battery => battery.building_id == id).ToList();

            if (filteredList == null)
            {
                return NotFound();
            }
            else
            {
                return filteredList;
            }
        }


        // GET api/Batteries/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Battery>> Get(int id)
        {
            var battery = await _context.batteries.FindAsync(id);
            if(battery == null) return NotFound();
            return battery;
        }

        // PUT api/batteries/id/status/status
        [HttpPut("{id}/status/{status}")]
        public async Task<ActionResult<Battery>> Put(int id, string status)
        {
            // grab battery with id id
            var battery = await _context.batteries.FindAsync(id);
            
            if(battery == null) {
                return NotFound();
            }
            // change status of battery
            battery.status = status;
            _context.SaveChanges();

            return battery;
        }

        // /api/batteries/building/

        [HttpGet("/batteries/building_id")]
        public async Task<ActionResult<IEnumerable<Battery>>> GetBatteriesByBuilding(int id)
        {

            var battery = await _context.batteries.Where(c => c.building_id == id).FirstOrDefaultAsync();
            return await _context.batteries.Where(b => b.building_id == battery.building_id).ToListAsync();

        }

        // Get https://localhost:7234/api/Batteries/customer/{email}
        [HttpGet("customer/{email}")]
        public async Task<ActionResult<IEnumerable<Battery>>> GetBatteriesByCustomer(string email)
        {

            var customer = await _context.customers.Where(c => c.EmailCompanyContact == email).FirstOrDefaultAsync();
            return await _context.batteries.Where(b => b.id == customer.CustomerID).ToListAsync();

        }

        // DELETE api/<BatteriesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
