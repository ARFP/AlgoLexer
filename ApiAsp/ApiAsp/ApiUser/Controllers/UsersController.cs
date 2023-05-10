using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiUser.Db;
using ApiUser.Models;
using ApiUser.Extensions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace ApiUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserDbContext _context;

        public UsersController()
        {
            _context = new UserDbContext();
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReadViewModel>>> GetUsers()
        {
            if (_context.Users == null)
            {
                  return NotFound();
            }

            List<UserReadViewModel> list = new List<UserReadViewModel>();

            await _context.Users.ForEachAsync(u => {
                UserReadViewModel r = new() { 
                    Id = u.Id, 
                    UserName = u.UserName
                };

                list.Add(r);
            });

            return list;

            /*foreach(User u in _context.Users)
            {
                UserReadViewModel r = new()
                {
                    Id = u.Id,
                    UserName = u.UserName
                };

                list.Add(r);
            } */


        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadViewModel>> GetUser(int id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return new UserReadViewModel()
            {
                Id = user.Id,
                UserName = user.UserName
            };
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserUpdateViewModel user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            User dbUser = _context.Users.FirstOrDefault(u => u.Id == id);

            if (dbUser is User)
            {
                dbUser.UserName = user.UserName;
            }
            else
            {
                return NotFound();
            }


            /*if (_context.Users.FirstOrDefault(u => u.Id == id) is User dbUser)
            {

            }*/


            _context.Entry(dbUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'UserDbContext.Users'  is null.");
          }
            
            user.Password = user.Password.ToPassword();

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
