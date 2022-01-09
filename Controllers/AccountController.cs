using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using SwayApi.Models;
namespace SwayApi.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }
        [Authorize]
        [HttpGet]
        public ActionResult GetInformation()
        {
            return Ok(accountService.GetInformation(
                int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))
                ));
        }
        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody]RegisterUserDto dto)
        {
          accountService.RegisterUser(dto);
          return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody]LoginDto dto)
        {
            string token = accountService.GenerateJwt(dto);
            return Ok(token);
        }

        [Authorize]
        [HttpGet("isLogged")]
        public ActionResult isLogged()
        {
            return Ok();
        }
    }
}
