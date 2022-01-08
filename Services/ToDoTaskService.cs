using SwayApi.Exceptions;

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
                Title = dto.Title,
                Description = dto.Description,
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

            return tasks.ToList();
        }

        public ToDoTask GetById(int id)
        {
            var toDoTask = dbContext.ToDoTasks.FirstOrDefault(t => t.Id == id);
            if (toDoTask is null)
            {
                throw new NotFoundException($"Nie znaleziono zadania o id {id}");
            }
            return toDoTask;

        }

        public void UpdateTask(UpdateToDoTaskDto dto, int id)
        {
            var toDoTask = dbContext.ToDoTasks.FirstOrDefault(t => t.Id == id);
            if(toDoTask is null)
            {
                throw new NotFoundException($"Nie znaleziono zadania o id {id}");
            }    
            toDoTask.Title = dto.Title;
            toDoTask.Description = dto.Description;

            dbContext.SaveChanges();
        }

        public void UpdateTaskState(int id, bool? state)
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
            }
            else
            {
                toDoTask.EndedDate = DateTime.MinValue;
                toDoTask.IsCompleted = false;
            }
            dbContext.SaveChanges();
        }
    }
}
