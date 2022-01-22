namespace KnapsackProblem.DesktopApp.ViewModels
{
    using System;
    using System.Threading;
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using KnapsackProblem.Solver;
    using KnapsackProblem.Solver.Model;
    using Microsoft.Toolkit.Mvvm.Input;

    internal class SolvingProgressIndicatorViewModel : ObservableObject, IDisposable
    {
        private readonly KnapsackSolver knapsackSolver;
        private readonly CancellationTokenSource cts;

        private string stepDescription = "";

        public string StepDescription
        {
            get => this.stepDescription;
            private set => this.SetProperty(ref this.stepDescription, value);
        }

        public RelayCommand CancelCommand { get; }

        public SolvingProgressIndicatorViewModel(KnapsackSolver knapsackSolver, CancellationTokenSource cts)
        {
            this.knapsackSolver = knapsackSolver;
            this.cts = cts;

            this.knapsackSolver.ProgressReport += this.HandleProgressReport;

            this.CancelCommand = new RelayCommand(this.ExecuteCancelCommand);
        }

        public void Dispose()
        {
            this.knapsackSolver.ProgressReport -= this.HandleProgressReport;
        }

        private void HandleProgressReport(ProgressReport progressReport)
        {
            this.StepDescription = GetProgressDescription(progressReport);
        }

        private void ExecuteCancelCommand()
        {
            this.cts.Cancel();
        }

        private static string GetProgressDescription(ProgressReport report)
        {
            return report.Step switch
            {
                StepType.CreatingInitialPopulation => "Creating initial population...",
                StepType.ProcessingGeneration => $"Processing generation ({report.Progress.CurrentStep}/{report.Progress.TotalSteps})...",
                StepType.WorkCompleted => "Work completed.",
                _ => throw new ArgumentOutOfRangeException(nameof(report.Step))
            };
        }
    }
}
