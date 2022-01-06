namespace KnapsackProblem.DesktopApp.ViewModels
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using Microsoft.Toolkit.Mvvm.Input;
    using Avalonia.Collections;
    using Avalonia.Controls.ApplicationLifetimes;
    using ScottPlot;
    using KnapsackProblem.DesktopApp.ViewModels.Data;
    using KnapsackProblem.Solver;
    using KnapsackProblem.Solver.Model;
    using KnapsackProblem.DesktopApp.Views;

    public enum SolverProcessingState
    {
        NotStarted,
        Processing,
        Completed,
        Error
    }

    internal class SolverRunnerViewModel : ObservableObject
    {
        private SolverOptionsViewModel solverOptions;
        private SolverInputViewModel solverInput;

        private GenerationViewModel? finalSolution;
        private SolverProcessingState processingState;
        private string? errorMessage;

        public AsyncRelayCommand RunSolverCommand { get; }

        public AvaloniaList<GenerationViewModel> AllGenerations { get; } = new AvaloniaList<GenerationViewModel>();

        public GenerationViewModel? FinalSolution
        {
            get => this.finalSolution;
            set => this.SetProperty(ref this.finalSolution, value);
        }

        public SolverProcessingState ProcessingState
        {
            get => this.processingState;
            private set => this.SetProperty(ref this.processingState, value);
        }

        public string? ErrorMessage
        {
            get => this.errorMessage;
            private set => this.SetProperty(ref this.errorMessage, value);
        }

        public SolverRunnerViewModel(SolverOptionsViewModel solverOptions, SolverInputViewModel solverInput)
        {
            this.solverOptions = solverOptions;
            this.solverInput = solverInput;

            this.RunSolverCommand = new AsyncRelayCommand(this.ExecuteRunSolverCommand);
        }

        public void SetupPlot(Plot plot)
        {
            plot.Title("Solutions over generations", false);

            var generations = this.AllGenerations.Select(generation => (double)generation.Number).ToArray();
            var values = this.AllGenerations.Select(generation => generation.GetTotalValue()).ToArray();
            var weights = this.AllGenerations.Select(generation => generation.GetTotalWeight()).ToArray();

            plot.AddScatter(generations, values, label: "Total value of the selected items");
            plot.AddScatter(generations, weights, label: "Total weight of the selected items");

            plot.AddHorizontalLine(this.solverInput.KnapsackCapacity,
                color: Color.Red,
                style: LineStyle.Dash,
                label: "Backpack capacity");

            plot.XAxis.Label("Generation");
            plot.XAxis.ManualTickSpacing(1.0);

            plot.Legend();
        }

        private async Task ExecuteRunSolverCommand()
        {
            this.FinalSolution = null;
            this.AllGenerations.Clear();

            this.ProcessingState = SolverProcessingState.Processing;

            try
            {
                var result = await this.SolveAndGetResult();

                this.ProcessingState = SolverProcessingState.Completed;

                this.FinalSolution = new GenerationViewModel(result.FinalSolution);
                this.AllGenerations.AddRange(result.AllGenerations.Select(item => new GenerationViewModel(item)));

                this.ShowResultsWindow();
            }
            catch (Exception ex)
            {
                this.ProcessingState = SolverProcessingState.Error;
                this.ErrorMessage = ex.Message;
            }
        }

        private Task<SolverResult> SolveAndGetResult()
        {
            return Task.Run(() =>
            {
                var options = this.solverOptions.ToModel();
                var input = this.solverInput.ToModel();
                var solver = new KnapsackSolver(options);

                return solver.Solve(input);
            });
        }

        private void ShowResultsWindow()
        {
            var window = new SolverResultsWindow(this);

            if (Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                window.ShowDialog(desktop.MainWindow);
            }
        }
    }
}
