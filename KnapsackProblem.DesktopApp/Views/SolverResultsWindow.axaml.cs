namespace KnapsackProblem.DesktopApp.Views
{
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Markup.Xaml;
    using ScottPlot.Avalonia;
    using KnapsackProblem.DesktopApp.ViewModels;

    public partial class SolverResultsWindow : Window
    {
        internal SolverResultsWindow(SolverRunnerViewModel solverRunner) : this()
        {
            this.DataContext = solverRunner;

            AvaPlot avaPlot1 = this.Find<AvaPlot>("AvaPlot1");

            solverRunner.SetupPlot(avaPlot1.Plot);
            
            avaPlot1.Refresh();
        }

        public SolverResultsWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
