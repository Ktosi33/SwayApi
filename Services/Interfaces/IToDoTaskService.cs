namespace SwayApi.Services.Interfaces
{
    public interface IToDoTaskService
    {
        public IEnumerable<ToDoTask> GetAll();
        public ToDoTask GetById(int id);
        public void AddTask(ToDoTaskDto dto);
        public void DeleteTask(int id);
        public void UpdateTask(UpdateToDoTaskDto dto, int id);
    }
}
