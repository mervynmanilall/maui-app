using MauiTodo.ViewModels;

namespace MauiTodo.Views;

public partial class TodoListPage : ContentPage
{
    readonly TodoListViewModel viewModel;

    // DI (the staffing agency) hands the page its view model.
    public TodoListPage(TodoListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = this.viewModel = viewModel;   // "my bindings read from this object"
    }

    // Load the todos the first time the page appears.
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (viewModel.Items.Count == 0)
            await viewModel.LoadCommand.ExecuteAsync(null);
    }
}