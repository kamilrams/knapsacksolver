namespace KnapsackProblem.DesktopApp.ViewModels
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using Microsoft.Toolkit.Mvvm.Input;
    using Avalonia.Collections;
    using KnapsackProblem.DesktopApp.ViewModels.Data;
    using KnapsackProblem.Solver;
    using KnapsackProblem.Solver.Model;

    public enum SolverProcessingState
    {
        NotStarted,
        Processing,
        Completed,
        Error
    }

    internal class SolverRunnerViewModel : ObservableObject
    {
        private readonly SolverOptionsViewModel solverOptions;
        private readonly SolverInputViewModel solverInput;

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
    }
}
