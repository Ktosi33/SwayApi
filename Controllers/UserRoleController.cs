using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
namespace SwayApi.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    [Authorize]
    public class UserRoleController : ControllerBase
    {
        private readonly UserRoleService roleService;

        public UserRoleController(UserRoleService roleService)
        {
            this.roleService = roleService;
        }

        [HttpGet]
        public ActionResult getRole()
        {
            string role = User.FindFirstValue(ClaimTypes.Role);
            return Ok(role);
        }

        [HttpGet("{id}")]
        public ActionResult getRoleId([FromRoute] int id)
        {
            return Ok(roleService.getRoleId(id));
        }

        [HttpPut]
        public ActionResult setRoleUserMail([FromBody] setRoleDto dto)
        {
            roleService.setRoleUserMail(dto);
            return Ok();
        }
    }
}
