using MauiTodo.Client1.Models;
using MauiTodo.Client1.Services;
using System.Diagnostics;

namespace MauiTodo.Client1.Views;

[QueryProperty(nameof(Todo) , "Todo")]
public partial class ManageTodos : ContentPage
{
    private readonly IRestDataService _service;
	Todo _todo;
	bool _isNew;

	public Todo Todo {
		get => _todo;
		set
		{
			_isNew = IsNew(value);
			OnPropertyChanged();
		}
	}

    public ManageTodos(IRestDataService service)
	{
		InitializeComponent();

		_service = service;
		BindingContext = this;
	}

	bool IsNew(Todo todo) {
		if (todo.Id == 0) return true;
		return false;
				}

	public async void OnCanceledClicked(object sender , EventArgs e) {
		await Shell.Current.GoToAsync("..");
	}  

	public async void OnDeleteClicked(object sender, EventArgs e) {
		await _service.DeleteTodoAsync(Todo.Id);
	}

    public async void OnSaveClicked(object sender, EventArgs e) {
		if (_isNew) {
			Debug.WriteLine("--> Add new Item"); 

		   await _service.AddTodoAsync(Todo);
		}
		else {
            Debug.WriteLine("--> Update Item");

            await _service.UpdateTodoAsync(Todo);
        }
        await Shell.Current.GoToAsync("..");
    }

}