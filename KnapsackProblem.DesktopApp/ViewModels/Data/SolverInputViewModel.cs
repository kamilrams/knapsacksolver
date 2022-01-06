namespace KnapsackProblem.DesktopApp.ViewModels.Data
{
    using System.Linq;
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using Avalonia.Collections;
    using KnapsackProblem.Solver.Model;

    internal class SolverInputViewModel : ObservableObject
    {
        private double knapsackCapacity;

        public double KnapsackCapacity
        {
            get => this.knapsackCapacity;
            set => this.SetProperty(ref this.knapsackCapacity, value);
        }

        public AvaloniaList<KnapsackItemViewModel> AvailableItems { get; } = new AvaloniaList<KnapsackItemViewModel>();

        public SolverInputViewModel() { }

        public SolverInputViewModel(SolverInput solverInput)
        {
            var availableItems = solverInput.AvailableItems.Select(item => new KnapsackItemViewModel(item));
            
            this.KnapsackCapacity = solverInput.KnapsackCapacity;
            this.AvailableItems.AddRange(availableItems);
        }
    }
}
