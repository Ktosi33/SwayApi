namespace SwayApi.Services.Interfaces
{
    public interface IToDoTaskService
    {
        public IEnumerable<ToDoTask> GetAll();
        public ToDoTask GetById(int id);
        public IEnumerable<ToDoTask> GetAllByUserId(int id);
        public void AddTask(ToDoTaskDto dto);
        public void DeleteTask(int id);
        public void UpdateTask(UpdateToDoTaskDto dto, int id);
        public void UpdateTaskState(int id, bool? state, int? userId);
    }
}
