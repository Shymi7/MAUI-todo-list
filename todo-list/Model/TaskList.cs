using System.Collections.ObjectModel;

namespace todo_list.Model
{
    public class TaskList
    {

        public ObservableCollection<Task> List { get; set; }
        public TaskList(ObservableCollection<Task> list)
        {
            List = list ?? new ObservableCollection<Task>();
        }

        public void AddTask(string title, string description, string id = null!)
        {
            var newTask = new Task(title, description, id);
            List.Add(newTask);
        }

        public bool DeleteTask(string taskId)
        {
            var taskToRemove = List.FirstOrDefault(t => t.Id == taskId);

            if (taskToRemove != null)
            {
                List.Remove(taskToRemove);
                return true;
            }

            return false;
        }

        public Task? GetTaskById(string taskId)
        {
            return List.FirstOrDefault(t => t.Id == taskId);
        }


        //public bool MarkTaskAsCompleted(string taskId)
        //{
        //    var taskToComplete = List.FirstOrDefault(t => t.Id == taskId);
        //    if (taskToComplete != null)
        //    {
        //        taskToComplete.IsCompleted = true;
        //        return true;
        //    }
        //    return false;
        //}
    }
}
