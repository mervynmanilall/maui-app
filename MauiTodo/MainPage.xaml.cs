using System.Collections.ObjectModel;
using MauiTodo.Models;

namespace MauiTodo;

// The "quick" way: event handlers in code-behind.
public partial class MainPage : ContentPage
{
    // The WhatsApp group: the list on screen is told about every add/remove.
    readonly ObservableCollection<TodoItem> todos =
    [
        new TodoItem { Title = "Submit .NET Conf slides" },
        new TodoItem { Title = "Charge the demo phone", IsDone = true },
    ];

    public MainPage()
    {
        InitializeComponent();
        TodoList.ItemsSource = todos;   // "list, show this collection"
    }

    // Called by the Add button AND by pressing Enter in the text box.
    void OnAddClicked(object? sender, EventArgs e)
    {
        var title = NewTodoEntry.Text?.Trim();
        if (string.IsNullOrEmpty(title)) return;   // ignore empty input

        todos.Add(new TodoItem { Title = title });   // the list updates itself
        NewTodoEntry.Text = string.Empty;            // clear the box
    }
}