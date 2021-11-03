using System;
using System.Collections;
using System.Threading.Tasks;
using AdultsWebAPI.Data;
using AdultsWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdultsWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class UsersController:ControllerBase
    {
        private IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<User>> ValidateUser([FromQuery] string userName, [FromQuery] string password)
        {
            try
            {
                User user = await userService.ValidateUserAsync(userName, password);
                return Ok(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
            
        }

    }
}