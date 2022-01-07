namespace SwayApi.Models
{
    public class ChangeStateToDoTaskDto
    {
        public int Id { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime EndDate { get; set; } = DateTime.Now;
    }
}
