using Microsoft.AspNetCore.Mvc;
using SwayApi.Services.Interfaces;
namespace SwayApi.Controllers
{
    [Route("v1/[controller]")]
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
        [HttpGet("{id}")]
        public ActionResult GetById([FromRoute]int id)
        {
            return Ok(toDoTaskService.GetById(id));
        }

        [HttpPost]
        public ActionResult AddTask([FromBody] ToDoTaskDto dto)
        {
            toDoTaskService.AddTask(dto);
            return Ok();
        }
        [HttpPut("{id}")]
        public ActionResult UpdateTask([FromBody] UpdateToDoTaskDto dto, [FromRoute]int id)
        {
            toDoTaskService.UpdateTask(dto, id);
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
