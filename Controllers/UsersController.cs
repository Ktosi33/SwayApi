using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace SwayApi.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UsersService usersService;

        public UsersController(UsersService usersService)
        {
            this.usersService = usersService;
        }
    [HttpGet]
    public ActionResult GetAllWithoutPassword()
        {
            return Ok(usersService.GetAllWithoutPassword());
        }

     [Authorize]
     [HttpGet("current")]
     public ActionResult GetCurrentWithoutPassword()
     { 
            return Ok(new UserInformationDto()
            {
                Id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                Email = User.FindFirstValue(ClaimTypes.Email),
                RoleName = User.FindFirstValue(ClaimTypes.Role)
            });
     }


        [HttpDelete("{id}")]
    public ActionResult DeleteUser([FromRoute] int id)
        {
            usersService.DeleteUserById(id);
            return Ok();
        }


      
    }
}
