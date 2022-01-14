using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SwayApi.Services.Interfaces;
using System.Text.Encodings.Web;
using System.Security.Claims;

namespace SwayApi.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    [Authorize]
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
        public ActionResult GetById([FromRoute] int id)
        {
            return Ok(toDoTaskService.GetById(id));
        }
        [Authorize(Roles = "Manager")]
        [HttpPost]
        public ActionResult AddTask([FromBody] ToDoTaskDto dto)
        {
            toDoTaskService.AddTask(dto);
            return Ok();
        }
        [Authorize(Roles = "Manager,Employee")]
        [HttpPut("{id}")]
        public ActionResult UpdateTask([FromBody] UpdateToDoTaskDto? dto, [FromRoute] int id, [FromQuery(Name = "state")] bool? state)
        {
            if (state == null)
            {
                if (User.IsInRole("Manager"))
                {                    
                    toDoTaskService.UpdateTask(dto, id);
                }
                else
                {
                    return Forbid();
                }
            }
            else
            {
                toDoTaskService.UpdateTaskState(id, state, int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            }

            return Ok();
        }
        [Authorize(Roles = "Manager")]
        [HttpDelete("{id}")]
        public ActionResult DeleteTask([FromRoute] int id)
        {
            toDoTaskService.DeleteTask(id);
            return Ok();
        }
     
    }
}
