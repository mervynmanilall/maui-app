using Microsoft.Extensions.Logging;
using MauiTodo.Services;
using MauiTodo.ViewModels;
using MauiTodo.Views;

namespace MauiTodo;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // The kitchen. Swap this ONE line to store todos elsewhere (e.g. SQLite).
        builder.Services.AddSingleton<ITodoService, InMemoryTodoService>();

        // The waiter and the dining room.
        builder.Services.AddSingleton<TodoListViewModel>();
        builder.Services.AddSingleton<TodoListPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
