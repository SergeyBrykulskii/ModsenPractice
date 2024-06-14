using Calculator.Services;
using Calculator.ViewModels;
using Calculator.Views;
using Microsoft.Extensions.Logging;


namespace Calculator
{
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

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<MathExpressionViewModel>();
            builder.Services.AddSingleton<CalculatorMainPage>();
            builder.Services.AddSingleton<RpnService>();
            builder.Services.AddSingleton<InputPreprocessingService>();
            return builder.Build();
        }
    }
}
