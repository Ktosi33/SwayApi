using Microsoft.AspNetCore.Mvc;
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

    [HttpDelete("{id}")]
    public ActionResult DeleteUser([FromRoute] int id)
        {
            usersService.DeleteUserById(id);
            return Ok();
        }


      
    }
}
