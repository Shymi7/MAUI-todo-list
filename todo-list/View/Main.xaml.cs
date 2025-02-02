using todo_list.ViewModel;

namespace todo_list.View;

public partial class Main : ContentView
{
	public Main()
	{
		InitializeComponent();
        BindingContext = new MainVM();

    }
}