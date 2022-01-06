using Microsoft.AspNetCore.Mvc;
using SwayApi.Services.Interfaces;
namespace SwayApi.Controllers
{
    [Route("v1/todotask")]
    [ApiController]
    public class ToDoTaskController : ControllerBase
    {
        private readonly IToDoTaskService toDoTaskService;

        public ToDoTaskController(IToDoTaskService toDoTaskService)
        {
            this.toDoTaskService = toDoTaskService;
        }

        [HttpGet]
        public ActionResult GetALl()
        {
            return Ok(toDoTaskService.GetAll());
        }

        [HttpPost]
        public ActionResult AddTask([FromBody] ToDoTaskDto dto)
        {
            toDoTaskService.AddTask(dto);
            return Ok();
        }
        [HttpPut]
        public ActionResult updateTask([FromBody] ToDoTaskDto dt)
        {

            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteTask([FromRoute]int id)
        {
            toDoTaskService.DeleteTask(id);
            return Ok();
        }

    }
}
