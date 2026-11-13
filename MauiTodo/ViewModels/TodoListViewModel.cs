using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiTodo.Models;
using MauiTodo.Services;

namespace MauiTodo.ViewModels;

// (ITodoService todoService) is a primary constructor: DI hands us the kitchen.
public partial class TodoListViewModel(ITodoService todoService) : ObservableObject
{
    // The list shown on screen (the WhatsApp group from step 2).
    public ObservableCollection<TodoItem> Items { get; } = [];

    // Bound to the text box. When it changes, re-check whether Add is allowed.
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddCommand))]
    private string newTitle = string.Empty;

    // A computed line like "2 of 3 left".
    public string Summary => Items.Count == 0
        ? "All clear"
        : $"{Items.Count(i => !i.IsDone)} of {Items.Count} left";

    // [RelayCommand] on LoadAsync generates a property called LoadCommand.
    [RelayCommand]
    private async Task LoadAsync()
    {
        foreach (var old in Items)
            old.PropertyChanged -= OnItemChanged;
        Items.Clear();

        foreach (var item in await todoService.GetAllAsync())
            Track(item);

        OnPropertyChanged(nameof(Summary));
    }

    // AddCommand. CanExecute = the "kitchen is open" check: the button greys out when it returns false.
    [RelayCommand(CanExecute = nameof(CanAdd))]
    private async Task AddAsync()
    {
        var item = new TodoItem { Title = NewTitle.Trim() };
        await todoService.AddAsync(item);
        Track(item);
        NewTitle = string.Empty;
        OnPropertyChanged(nameof(Summary));
    }

    private bool CanAdd() => !string.IsNullOrWhiteSpace(NewTitle);

    // DeleteCommand, used by swipe-to-delete. The item comes in as the command parameter.
    [RelayCommand]
    private async Task DeleteAsync(TodoItem item)
    {
        await todoService.DeleteAsync(item);
        item.PropertyChanged -= OnItemChanged;
        Items.Remove(item);
        OnPropertyChanged(nameof(Summary));
    }

    // Listen to each item's buzzer so ticking a box updates the summary and gets saved.
    void Track(TodoItem item)
    {
        item.PropertyChanged += OnItemChanged;
        Items.Add(item);
    }

    async void OnItemChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender is not TodoItem item) return;
        await todoService.UpdateAsync(item);
        OnPropertyChanged(nameof(Summary));
    }
}