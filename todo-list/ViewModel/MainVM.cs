using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using todo_list.Model;

namespace todo_list.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        public TaskList CurrentTasks { get; set; }
        public TaskList FinishedTasks { get; set; }

        private string _newTaskTitle;
        public string NewTaskTitle
        {
            get => _newTaskTitle;
            set
            {
                _newTaskTitle = value;
                OnPropertyChanged(nameof(NewTaskTitle));
            }
        }

        private string _newTaskDescription;
        public string NewTaskDescription
        {
            get => _newTaskDescription;
            set
            {
                _newTaskDescription = value;
                OnPropertyChanged(nameof(NewTaskDescription));
            }
        }

        public ICommand AddTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }
        public ICommand MarkTaskAsCompletedCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainVM()
        {
            CurrentTasks = new TaskList(new ObservableCollection<Model.Task>());
            FinishedTasks = new TaskList(new ObservableCollection<Model.Task>());

            AddTaskCommand = new Command(OnAddTask);
            DeleteTaskCommand = new Command<string>(OnDeleteTask);
            MarkTaskAsCompletedCommand = new Command<string>(OnMarkTaskAsCompleted);
        }


        private void OnAddTask()
        {
            if (string.IsNullOrWhiteSpace(NewTaskTitle))
                return;

            CurrentTasks.AddTask(NewTaskTitle, NewTaskDescription);
            NewTaskTitle = string.Empty;
            NewTaskDescription = string.Empty;
            OnPropertyChanged(nameof(CurrentTasks));
        }

        private void OnDeleteTask(string taskId)
        {
            if (CurrentTasks.DeleteTask(taskId))
            {
                OnPropertyChanged(nameof(CurrentTasks));
                return;
            }

            FinishedTasks.DeleteTask(taskId);
            OnPropertyChanged(nameof(FinishedTasks));

        }

        private void OnMarkTaskAsCompleted(string taskId)
        {
            var taskReference = CurrentTasks.GetTaskById(taskId);

            if (taskReference == null)
                return;

            FinishedTasks.AddTask(taskReference.Title, taskReference.Description, taskReference.Id);
            CurrentTasks.DeleteTask(taskId);

            OnPropertyChanged(nameof(CurrentTasks));
        }


    }
}
