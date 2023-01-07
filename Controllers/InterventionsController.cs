using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rocket_Elevators_Rest_API.Models;

namespace Rocket_Elevators_Rest_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterventionsController : ControllerBase
    {
        private readonly RocketElevatorsContext _context;

        public InterventionsController(RocketElevatorsContext context)
        {
            _context = context;
        }

        // GET: api/<Interventions>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Intervention>>> GetInterventions()
        {
            return await _context.interventions.ToListAsync();
        }

        // GET api/Interventions/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Intervention>> Get(int id)
        {
            var intervention = await _context.interventions.FindAsync(id);
            if(intervention == null) return NotFound();
            return intervention;
        }
        
        // Get api/Interventions/customer/{email}
        [HttpGet("customer/{email}")]
        public async Task<ActionResult<IEnumerable<Intervention>>> GetInterventionsByCustomer(string email)
        {

            var customer = await _context.customers.Where(c => c.EmailCompanyContact == email).FirstOrDefaultAsync();
            return await _context.interventions.Where(b => b.CustomerID == customer.CustomerID).ToListAsync();

        }

        // PUT api/interventions/id/status/status
        [HttpPut("{id}/Status/InProgress")]
        public async Task<ActionResult<Intervention>> Put(int id)
        {
        
            var intervention = await _context.interventions.FindAsync(id);
            DateTime now = DateTime.Now;
           
            
            if(intervention == null) {
                return NotFound();
            }
            // change Status of intervention
            intervention.Start = now;
            intervention.Status = "InProgress";
            _context.SaveChanges();

            return intervention;
        }

        [HttpPut("{id}/Status/Completed")]
        public async Task<ActionResult<Intervention>> PutCompleted(int id)
        {
        
            var intervention = await _context.interventions.FindAsync(id);
            DateTime now = DateTime.Now;
            
            if(intervention == null) {
                return NotFound();
            }
            // change Status of intervention
            intervention.End = now;
            intervention.Status = "Completed";
            _context.SaveChanges();

            return intervention;
        }

         // PUT api/interventions
        [HttpPost]
        public async Task<ActionResult<Intervention>> PostInterventions(Intervention intervention)
        {
            intervention.Status = "InProgress";
            intervention.EmployeeID = null;
            intervention.Start = DateTime.Now;
            _context.interventions.Add(intervention);
            await _context.SaveChangesAsync();
            return intervention;

        }

        // DELETE api/<InterventionsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        
    }
}
