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
    public class UserController : ControllerBase
    {
        private readonly RocketElevatorsContext _context;

        public UserController(RocketElevatorsContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.customers.ToListAsync();
            return Ok(users);
        }

        [HttpGet("/userobj/{email}")]
        public IActionResult GetUserObject(string EmailCompanyContact)
        {
            var user = _context.customers.Where(c => c.EmailCompanyContact == EmailCompanyContact);
            return Ok(user);
        }

        // GET /api/users/{email}
        [HttpGet("/api/users/{email}")]
        public async Task<ActionResult<User>> GetUser(string email)
        {
            var user = await _context.users.Where(c => c.email == email).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            } else {

            return user;
            }

        }

        // GET /api/user/email/{email}
        [HttpGet("Email/{email}")]
        public async Task<ActionResult<User>> GetUserEmail(string email)
        {
            IEnumerable<User> usersAll = await _context.users.ToListAsync();

            foreach (User user in usersAll)
            {
                if (user.email == email)
                {
                    return user;
                }
            }
            return NotFound();
        }

        // GET /api/user/email/{email}
        [HttpGet("{email}")]
        public bool CheckEmail(string email)
        {
            return _context.users.Any(u => u.email == email);
        }

        private bool UsersExists(long id)
        {
            return _context.users.Any(e => e.id == id);
        }

    }
}
