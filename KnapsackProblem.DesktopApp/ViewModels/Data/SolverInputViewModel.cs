namespace KnapsackProblem.DesktopApp.ViewModels.Data
{
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using Avalonia.Collections;

    internal class SolverInputViewModel : ObservableObject
    {
        private double knapsackCapacity;

        public double KnapsackCapacity
        {
            get => this.knapsackCapacity;
            set => this.SetProperty(ref this.knapsackCapacity, value);
        }

        public AvaloniaList<KnapsackItemViewModel> AvailableItems { get; } = new AvaloniaList<KnapsackItemViewModel>();
    }
}
