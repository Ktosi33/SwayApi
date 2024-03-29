﻿namespace SwayApi.Entities
{
    public class ToDoTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EndedDate { get; set; }
        public int? userId { get; set; }
        public virtual User? User { get; set; }

    }
}
