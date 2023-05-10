using ApiUser.Db;
using ApiUser.Extensions;
using ApiUser.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserDbContext _context;

        public LoginController()
        {
            _context = new UserDbContext();
        }

        [HttpPost]
        public async Task<ActionResult<UserReadViewModel?>> Login(User userToLogin)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userToLogin.UserName);

            if (user != null && user.Password.CheckPassword(userToLogin.Password))
            {
                return new UserReadViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName
                };
            }

            return NotFound();             
        }
    }
}
