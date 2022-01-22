namespace KnapsackProblem.DesktopApp.Views
{
    using System;
    using System.Threading;
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Markup.Xaml;
    using KnapsackProblem.DesktopApp.ViewModels;
    using KnapsackProblem.Solver;

    public partial class SolvingProgressIndicator : Window, IDisposable
    {
        internal SolvingProgressIndicator(KnapsackSolver solver, CancellationTokenSource cts) : this()
        {
            this.DataContext = new SolvingProgressIndicatorViewModel(solver, cts);
        }

        public SolvingProgressIndicator()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        public void Dispose()
        {
            this.Close();

            GC.SuppressFinalize(this);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
