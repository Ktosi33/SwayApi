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
        [Authorize(Roles = "Manager")]
        [HttpGet("OnlyManager")]
        public ActionResult onlyManager()
        {
            return Ok();
        }
        [Authorize(Roles = "Employee")]
        [HttpGet("OnlyEmployee")]
        public ActionResult onlyEmployee()
        {
            return Ok();
        }
        [Authorize(Roles = "Employee,Manager")]
        [HttpGet("OnlyRoles")]
        public ActionResult onlyRoles()
        {
            return Ok();
        }

        
        [HttpGet("{id}")]
        public ActionResult getRoleId([FromRoute] int id)
        {
            return Ok(roleService.getRoleId(id));
        }

        [Authorize(Roles = "Manager")]
        [HttpPut]
        public ActionResult setRoleUserMail([FromBody] setRoleDto dto)
        {
            roleService.setRoleUserMail(dto);
            return Ok();
        }
    }
}
