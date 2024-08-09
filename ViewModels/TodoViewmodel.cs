using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTodo.Client1.ViewModels;

public class TodoViewmodel : INotifyPropertyChanged {

    long _id;
    string _title;  


    public long Id {
        get => _id
            ; set
        {
            if (_id == value)  return;  
            _id = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
            
        }
    }

    public string Title { get => _title; set
        {
            if( _title == value) return;
            _title = value;
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(Title)));
        } } 


    public event PropertyChangedEventHandler? PropertyChanged;
}
