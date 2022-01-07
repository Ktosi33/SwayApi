
namespace SwayApi.Services
{
    public class ToDoTaskStateService
    {
        private readonly SwayDbContext dbContext;

        public ToDoTaskStateService(SwayDbContext dbContext)
        {
            this.dbContext = dbContext;

        }

        public void isDone(ChangeStateToDoTaskDto dto)
        {
            var toDoTask = dbContext.ToDoTasks.FirstOrDefault(t => t.Id == dto.Id);
            if(toDoTask is null)
            {
                throw new NotFoundException($"Nie znaleziono zadnaia o id {dto.Id}");

            }

            toDoTask.EndedDate = dto.EndDate;
            toDoTask.IsCompleted = true;
            dbContext.SaveChanges();

        }

        public void isNotDone(ChangeStateToDoTaskDto dto)
        {
            var toDoTask = dbContext.ToDoTasks.FirstOrDefault(t => t.Id == dto.Id);
            if (toDoTask is null)
            {
                throw new NotFoundException($"Nie znaleziono zadnaia o id {dto.Id}");

            }

            toDoTask.EndedDate = DateTime.MinValue;
            toDoTask.IsCompleted = false;
            dbContext.SaveChanges();

        }
    }
}
