using MKPApp.Data;

namespace MKPApp;

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
                fonts.AddFont("Lato-Regular.ttf", "LatoRegular");

            });

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddSingleton<WeatherForecastService>();

		return builder.Build();
	}
}
