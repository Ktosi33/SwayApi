using Microsoft.AspNetCore.Mvc;
namespace SwayApi.Controllers
{
    [Route("v1/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public TestController()
        {

        }
        [HttpGet]
        public ActionResult get()
        {
            return Ok("Dziala wszystko");
        }

    }
}
