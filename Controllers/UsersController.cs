using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace SwayApi.Controllers
{
    
    [Route("v1/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly UsersService usersService;
        private readonly IToDoTaskService toDoTaskService;

        public UsersController(UsersService usersService, IToDoTaskService toDoTaskService)
        {
            this.usersService = usersService;
            this.toDoTaskService = toDoTaskService;
        }
        [Authorize(Roles = "Manager")]
        [HttpGet]
    public ActionResult GetAllWithoutPassword()
        {
            return Ok(usersService.GetAllWithoutPassword());
        }
        [Authorize]
        [HttpGet("tdtsdone")]
        public ActionResult GetAllDoneTdt()
        {
            return Ok(toDoTaskService.GetAllByUserId(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))));
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

        [Authorize(Roles = "Manager")]
        [HttpDelete("{id}")]
    public ActionResult DeleteUser([FromRoute] int id)
        {
            usersService.DeleteUserById(id);
            return Ok();
        }


      
    }
}
