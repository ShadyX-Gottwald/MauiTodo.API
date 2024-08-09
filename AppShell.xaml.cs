using MauiTodo.Client1.Views;

namespace MauiTodo.Client1; 

public partial class AppShell : Shell {
    public AppShell() {
        InitializeComponent();   

        Routing.RegisterRoute(nameof(ManageTodos) , typeof(ManageTodos));
    }
}
