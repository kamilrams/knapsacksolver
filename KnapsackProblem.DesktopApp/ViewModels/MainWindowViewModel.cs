namespace KnapsackProblem.DesktopApp.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using Avalonia.Collections;
    using KnapsackProblem.DesktopApp.ViewModels.Data;

    internal class MainWindowViewModel : ObservableObject
    {
        public SolverInputViewModel SolverInput { get; } = new SolverInputViewModel(); 

        public SolverOptionsViewModel SolverOptions { get; } = new SolverOptionsViewModel();

        public AvaloniaList<GenerationViewModel> SolverResults { get; } = new AvaloniaList<GenerationViewModel>();

        public MainWindowViewModel()
        {
            this.SolverInput.AvailableItems.Add(new KnapsackItemViewModel("First item", 10.0, 10.0));
            this.SolverInput.AvailableItems.Add(new KnapsackItemViewModel("Second item", 50.0, 42.0));
            this.SolverInput.AvailableItems.Add(new KnapsackItemViewModel("Third item", 34.0, 60.0));
            this.SolverInput.AvailableItems.Add(new KnapsackItemViewModel("Fourth item", 23, 7.0));
            this.SolverInput.AvailableItems.Add(new KnapsackItemViewModel("Fiveth item", 4.0, 15.0));
        }
    }
}
