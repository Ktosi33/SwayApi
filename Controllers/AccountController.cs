using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SwayApi.Models;
namespace SwayApi.Controllers
{
    [Route("v1/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
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
