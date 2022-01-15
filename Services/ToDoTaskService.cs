using SwayApi.Exceptions;
using System.Text.Encodings.Web;
using System.Net;
using System.Security.Claims;

namespace SwayApi.Services
{
    public class ToDoTaskService : IToDoTaskService
    {
        private readonly SwayDbContext dbContext;
      
        public ToDoTaskService(SwayDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddTask(ToDoTaskDto dto)
        {
            
           
            
            var newTask = new ToDoTask()
            {
                Title = WebUtility.HtmlEncode(dto.Title),
                Description = WebUtility.HtmlEncode(dto.Description),
                IsCompleted = dto.IsCompleted,
                CreatedDate = dto.CreatedDate, 
                
            };
            
            dbContext.ToDoTasks.Add(newTask);
            dbContext.SaveChanges();
        }
        
        public void DeleteTask(int id)
        {
            var tasks = dbContext.ToDoTasks.FirstOrDefault(t => t.Id == id);
            if (tasks == null)
            {
                throw new NotFoundException($"Nie znaleziono żadnego zadania o podanym id {id}");
            }
            dbContext.ToDoTasks.Remove(tasks);
            dbContext.SaveChanges();
        }

        public IEnumerable<ToDoTask> GetAll()
        {
            var tasks = dbContext.ToDoTasks;
            if(!tasks.Any())
            {
                throw new NotFoundException("Nie znaleziono żadnego zadania");
            }
           foreach (ToDoTask t in tasks)
            {
               t.Title = t.Title;
               t.Description = t.Description; 
            } 
            return tasks.ToList();
        }

        public ToDoTask GetById(int id)
        {
            var toDoTask = dbContext.ToDoTasks.FirstOrDefault(t => t.Id == id);
            if (toDoTask is null)
            {
                throw new NotFoundException($"Nie znaleziono zadania o id {id}");
            }

            toDoTask.Title = WebUtility.HtmlDecode(toDoTask.Title);
            toDoTask.Description = WebUtility.HtmlDecode(toDoTask.Description);
            return toDoTask;

        }

        public IEnumerable<ToDoTask> GetAllByUserId(int id)
        {

            var toDoTask = dbContext.ToDoTasks.Where<ToDoTask>(t => t.userId == id);
            if (!toDoTask.Any())
            {
                throw new NotFoundException($"Nie znaleziono żadnego zadania");
            }
            foreach (ToDoTask t in toDoTask)
            {
                t.Title = t.Title;
                t.Description = t.Description;
            }
            return toDoTask.ToList();

        }
        //WebUtility.HtmlEncode(toDoTask.Description)
        public void UpdateTask(UpdateToDoTaskDto dto, int id)
        {
            var toDoTask = dbContext.ToDoTasks.FirstOrDefault(t => t.Id == id);
            if(toDoTask is null)
            {
                throw new NotFoundException($"Nie znaleziono zadania o id {id}");
            }    
            toDoTask.Title = WebUtility.HtmlEncode(dto.Title);
            toDoTask.Description = WebUtility.HtmlEncode(dto.Description);

            dbContext.SaveChanges();
        }

        public void UpdateTaskState(int id, bool? state, int? userId)
        {
            var toDoTask = dbContext.ToDoTasks.FirstOrDefault(t => t.Id == id);
            if (state is null)
            {
                throw new BadRequestException($"Wystapił nieoczekiwany błąd");

            }

            if (toDoTask is null)
            {
                throw new NotFoundException($"Nie znaleziono zadnaia o id {id}");
            }

                if (state == true)
            {
                toDoTask.EndedDate = DateTime.Now;
                toDoTask.IsCompleted = true;
                //toDoTask.userId = 
                toDoTask.userId = userId;
            }
            else
            {
                toDoTask.EndedDate = DateTime.MinValue;
                toDoTask.IsCompleted = false;
                toDoTask.userId = null;
            }
            dbContext.SaveChanges();
        }
    }
}
