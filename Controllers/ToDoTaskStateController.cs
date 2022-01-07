using Microsoft.AspNetCore.Mvc;
namespace SwayApi.Controllers
{   [Route("v1/[controller]")]
    [ApiController]
    public class ToDoTaskStateController : ControllerBase
    {
        private readonly ToDoTaskStateService toDoTaskWorkService;

        public ToDoTaskStateController(ToDoTaskStateService toDoTaskWorkService)
        {
            this.toDoTaskWorkService = toDoTaskWorkService;
        }
        [HttpPut("true")]
        public ActionResult isDone(ChangeStateToDoTaskDto dto)
        {
            toDoTaskWorkService.isDone(dto);
            return Ok();
        }
        [HttpPut("false")]
        public ActionResult isNotDone(ChangeStateToDoTaskDto dto)
        {
            toDoTaskWorkService.isNotDone(dto);
            return Ok();
        }
    }
}
