using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiTodo.Models;

// ObservableObject gives the class its "buzzer" (INotifyPropertyChanged).
// [ObservableProperty] turns each private field into a public property that buzzes when it changes.
public partial class TodoItem : ObservableObject
{
    public int Id { get; set; }

    [ObservableProperty]
    private string title = string.Empty;   // generates: public string Title { get; set; }

    [ObservableProperty]
    private bool isDone;                   // generates: public bool IsDone { get; set; }
}