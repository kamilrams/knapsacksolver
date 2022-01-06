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
    using KnapsackProblem.Solver.Model;

    internal class MainWindowViewModel : ObservableObject
    {
        public SolverOptionsViewModel SolverOptions { get; }

        public SolverInputViewModel SolverInput { get; }

        public AvaloniaList<GenerationViewModel> SolverResults { get; } = new AvaloniaList<GenerationViewModel>();

        public MainWindowViewModel()
        {
            var solverOptions = KnapsackProblem.Solver.Model.SolverOptions.Default;
            var solverInput = new KnapsackProblem.Solver.Model.SolverInput
            {
                KnapsackCapacity = 20.0,
                AvailableItems = new List<KnapsackItem>
                {
                    new KnapsackItem("First item", 10.0, 10.0),
                    new KnapsackItem("Second item", 50.0, 42.0),
                    new KnapsackItem("Third item", 34.0, 60.0),
                    new KnapsackItem("Fourth item", 23, 7.0),
                    new KnapsackItem("Fiveth item", 4.0, 15.0),
                }
            };

            this.SolverOptions = new SolverOptionsViewModel(solverOptions);
            this.SolverInput = new SolverInputViewModel(solverInput);
        }
    }
}
