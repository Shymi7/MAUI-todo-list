namespace todo_list.Model
{
    public class Task
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }

        public Task(string title, string description, string id = null)
        {
            Title = title;
            Description = description;

            Created = DateTime.Now;

            if (id != null)
                Id = id;
            else
                Id = Guid.NewGuid().ToString("D");
        }


    }
}
