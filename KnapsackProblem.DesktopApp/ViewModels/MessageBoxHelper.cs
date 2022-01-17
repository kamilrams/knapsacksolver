namespace KnapsackProblem.DesktopApp.ViewModels
{
    using System.Linq;
    using System.Threading.Tasks;
    using Avalonia.Controls;
    using Avalonia.Controls.ApplicationLifetimes;
    using MessageBox.Avalonia;
    using MessageBox.Avalonia.DTO;

    internal static class MessageBoxHelper
    {
        public static async Task ShowMessage(string title, string message,
            MessageBox.Avalonia.Enums.Icon icon = MessageBox.Avalonia.Enums.Icon.Info)
        {
            var parameters = new MessageBoxStandardParams
            {
                ContentTitle = title,
                ContentMessage = message,
                ButtonDefinitions = MessageBox.Avalonia.Enums.ButtonEnum.Ok,
                Topmost = true,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Icon = icon,
                CanResize = false,
            };

            var lifetime = Avalonia.Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;

            if (lifetime == null)
            {
                return;
            }

            var dialog = MessageBoxManager.GetMessageBoxStandardWindow(parameters);

            await dialog.ShowDialog(lifetime.MainWindow);
        }

        public static async Task<string?> ShowOpenFileDialog()
        {
            var lifetime = Avalonia.Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;

            if (lifetime == null)
            {
                return null;
            }

            var openFileDialog = new OpenFileDialog()
            {
                AllowMultiple = false,
            };

            var filePaths = await openFileDialog.ShowAsync(lifetime.MainWindow);

            return filePaths?.FirstOrDefault();
        }
    }
}
