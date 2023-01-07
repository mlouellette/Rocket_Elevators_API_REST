#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rocket_Elevators_Rest_API.Models;

namespace Rocket_Elevators_Rest_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElevatorsController : ControllerBase
    {
        private readonly RocketElevatorsContext _context;

        public ElevatorsController(RocketElevatorsContext context)
        {
            _context = context;
        }

        // GET: api/Elevators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Elevator>>> Getelevators()
        {
            return await _context.elevators.ToListAsync();
        }

        // https://localhost:7234/api/elevators/column/{id}
        [HttpGet("column/{id}")]
        public async Task<ActionResult<IEnumerable<Elevator>>> GetElevatorsByColumn(int id)
        {
            List<Elevator> elevatorsList = await _context.elevators.ToListAsync();
            List<Elevator> filteredList = new List<Elevator>();
            filteredList = elevatorsList.Where(elevator => elevator.column_id == id).ToList();

            if (filteredList == null)
            {
                return NotFound();
            }
            else
            {
                return filteredList;
            }
        }

        // GET: https://localhost:7234/api/elevators/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Elevator>> Get(int id)
        {
            var elevator = await _context.elevators.FindAsync(id);

            if (elevator == null)
            {
                return NotFound();
            }

            return elevator;
        }

        [HttpPut("{id}/status/{status}")]
        public async Task<ActionResult<Elevator>> Put(int id, string status)
        {
            // grab battery with id id
            var elevator = await _context.elevators.FindAsync(id);

            if (elevator == null)
            {
                return NotFound();
            }
            // change status of battery
            elevator.elevator_status = status;
            _context.SaveChanges();

            return elevator;
        }

        // https://localhost:7234/api/Columns/customer/{email}
        [HttpGet("customer/{email}")]
        public async Task<ActionResult<IEnumerable<Elevator>>> GetElevatorsByCustomer(string email)
        {

            var customer = await _context.customers.Where(c => c.EmailCompanyContact == email).FirstOrDefaultAsync();
            return await _context.elevators.Where(b => b.id == customer.CustomerID).ToListAsync();

        }

        // PUT /api/Elevators/status
        [HttpGet("status")]
        public async Task<ActionResult<IEnumerable<Elevator>>> GetElevatorStatId()
        {
            return await _context.elevators.Where(e => (e.elevator_status == "Inactive")).ToListAsync();
        }


        // DELETE: api/Elevators/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
