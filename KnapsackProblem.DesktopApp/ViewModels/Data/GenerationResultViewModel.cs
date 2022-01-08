namespace KnapsackProblem.DesktopApp.ViewModels.Data
{
    using System.Linq;
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using Avalonia.Collections;
    using KnapsackProblem.Solver.Model;

    internal class GenerationResultViewModel : ObservableObject
    {
        private int number;
        private bool hasCorrectSolution;
        private double totalValue;
        private double totalWeight;

        public int Number
        {
            get => this.number;
            set => this.SetProperty(ref this.number, value);
        }

        public bool HasCorrectSolution
        {
            get => this.hasCorrectSolution;
            set => this.SetProperty(ref this.hasCorrectSolution, value);
        }

        public double TotalValue
        {
            get => this.totalValue;
            set => this.SetProperty(ref this.totalValue, value);
        }

        public double TotalWeight
        {
            get => this.totalWeight;
            set => this.SetProperty(ref this.totalWeight, value);
        }  

        public AvaloniaList<KnapsackItemViewModel> Solution { get; } = new AvaloniaList<KnapsackItemViewModel>();

        public GenerationResultViewModel() { }

        public GenerationResultViewModel(GenerationResult generation)
        {
            this.Number = generation.Number;
            this.HasCorrectSolution = generation.HasCorrectSolution;
            this.TotalValue = generation.TotalValue;
            this.TotalWeight = generation.TotalWeight;

            var solutionItems = generation.Solution.Select(item => new KnapsackItemViewModel(item));

            this.Solution.AddRange(solutionItems);
        }
    }
}
