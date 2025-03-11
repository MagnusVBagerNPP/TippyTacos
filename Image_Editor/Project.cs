using Avalonia;
using Avalonia.Controls;

namespace ImageEditor
{
    public class Project
    {
        public static void Main(string[] args)
        {
            AppBuilder.Configure<Application>().UsePlatformDetect().Start(AppMain, args);
        }

        public static void AppMain(Application app, string[] args)
        {
            app.Styles.Add(new Avalonia.Themes.Fluent.FluentTheme());
            app.RequestedThemeVariant = Avalonia.Styling.ThemeVariant.Light;

            var imageEditor = new ImageEditor();
            imageEditor.grid_cols = 5;
            imageEditor.grid_rows = 5;
            app.Run(imageEditor.Win);
        }
    }
}
