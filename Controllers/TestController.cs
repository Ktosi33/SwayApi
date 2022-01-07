using Microsoft.AspNetCore.Mvc;
namespace SwayApi.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
       
        [HttpGet]
        public ActionResult get()
        {
            return Ok("Dziala wszystko");
            
        }

    }
}
