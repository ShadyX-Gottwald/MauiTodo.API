
using MauiTodo.Client1.Services;
using System.Diagnostics;
using MauiTodo.Client1.Views;
using MauiTodo.Client1.Models;
namespace MauiTodo.Client1 {
    public partial class MainPage : ContentPage {

        private readonly IRestDataService _service;

        public MainPage(IRestDataService service) {
            InitializeComponent();
            _service = service; 
        }

        protected async override void OnAppearing() {
            base.OnAppearing();
            collectionView.ItemsSource = await _service.GetAllTodosAsync();
        }

        async void OnAddTodoClick(object sender , EventArgs e) {
            Debug.WriteLine("---> Add Btn Clicked");

            var data = new Dictionary<string, object>() {
                { nameof(Todo),new Todo()}
            };

            await Shell.Current.GoToAsync(nameof(ManageTodos) , data);

        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            Debug.WriteLine("---> Item Changed Clicked!");

            var data = new Dictionary<string, object>() {
                { nameof(Todo),e.CurrentSelection?.FirstOrDefault() as Todo}
            };

            await Shell.Current.GoToAsync(nameof(ManageTodos), data);

        }


    }

}
