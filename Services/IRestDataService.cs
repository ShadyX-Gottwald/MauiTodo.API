using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MauiTodo.Client1.Models;

namespace MauiTodo.Client1.Services; 

public interface IRestDataService {  

    Task<List<Todo>> GetAllTodosAsync();  
    Task AddTodoAsync(Todo todo);
    Task DeleteTodoAsync(long id);
    Task UpdateTodoAsync(Todo todo); 

}

public class RestDataService : IRestDataService {
    private readonly HttpClient _httpClient;
    private readonly string _baseAddress;
    private readonly string _url;  
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public RestDataService() { 
        _httpClient = new HttpClient();
        // _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ?
        //   "https://s62mwcgx-7282.euw.devtunnels.ms" :  
        //   "https://localhost:7282";

        _baseAddress = "https://5xcsbfwx-7282.euw.devtunnels.ms";


        _url = $"{_baseAddress}";
        _jsonSerializerOptions = new JsonSerializerOptions {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,

        };

    }
    public async Task AddTodoAsync(Todo todo) {

        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet) {
            Debug.WriteLine("---> No internet access...");

        }
        try { 

            string jsonTodo = JsonSerializer.Serialize(todo,_jsonSerializerOptions);
            StringContent content = new StringContent(jsonTodo ,Encoding.UTF8 , "application/json");

            HttpResponseMessage res = await _httpClient.PostAsync($"{_url}/todo", content);

            if (res.IsSuccessStatusCode) {
                Debug.WriteLine("Successfully Created Todo");
            }
        } catch (Exception ex) {

            Debug.WriteLine("---> Non Http 2xx response");
        }

    }

    public async Task DeleteTodoAsync(long id) {

        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet) {
            Debug.WriteLine("---> No internet access...");

        }

        try { 

            var res = await _httpClient.DeleteAsync($"{_url}/deleteTodo/{id}");

            if (res.IsSuccessStatusCode) {
                Debug.WriteLine("Successfully Deleted Todo");

            } else {
                Debug.WriteLine("---> Deleting Failed");
            }

        } catch (Exception ex) {
            Debug.WriteLine("---> Whoops Something went Wrong.");

        }

    }

    public async Task<List<Todo>> GetAllTodosAsync() {
        List<Todo> todoList = new List<Todo>();
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet) {
            Debug.WriteLine("---> No internet access...");

            return todoList;
        }

        try {
            var response = await _httpClient.GetAsync($"{_url}/todos");

            if (response.IsSuccessStatusCode) { 
                string content = await response.Content.ReadAsStringAsync();

                todoList = JsonSerializer.Deserialize<List<Todo>>(content ,_jsonSerializerOptions);  

                Debug.WriteLine("-->" + todoList!.ToString());

                return todoList;


            }
            else {
                Debug.WriteLine("---> Non Http 2xx response");
                return new List<Todo>();
            }


        } catch (Exception ex) { 

            Debug.WriteLine("--> " + ex.Message);
            return new List<Todo>();
        }
    }

    public async Task UpdateTodoAsync(Todo todo) {

        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet) {
            Debug.WriteLine("---> No internet access...");

        }
        try {
            string jsonTodo = JsonSerializer.Serialize(todo, _jsonSerializerOptions);
            StringContent content = new StringContent(jsonTodo, Encoding.UTF8, "application/json");

            HttpResponseMessage res = await _httpClient.PutAsync($"{_url}/todo{todo.Id}", content);

            if (res.IsSuccessStatusCode) {
                Debug.WriteLine("Successfully Created Todo");
            }
        } catch (Exception ex) {

            Debug.WriteLine("---> Non Http 2xx response");
        }
    }
}