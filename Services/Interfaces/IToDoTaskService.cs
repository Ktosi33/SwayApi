namespace SwayApi.Services.Interfaces
{
    public interface IToDoTaskService
    {
        public IEnumerable<ToDoTask> GetAll();
        public void AddTask(ToDoTaskDto dto);
        void DeleteTask(int id);
    }
}
