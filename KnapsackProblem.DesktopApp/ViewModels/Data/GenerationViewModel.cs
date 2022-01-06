namespace KnapsackProblem.DesktopApp.ViewModels.Data
{
    using System.Linq;
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using Avalonia.Collections;
    using KnapsackProblem.Solver.Model;

    internal class GenerationViewModel : ObservableObject
    {
        private int number;

        public int Number
        {
            get => this.number;
            set => this.SetProperty(ref this.number, value);
        }

        public AvaloniaList<KnapsackItemViewModel> Solution { get; } = new AvaloniaList<KnapsackItemViewModel>();

        public GenerationViewModel() { }

        public GenerationViewModel(Generation generation)
        {
            var solutionItems = generation.Solution.Select(item => new KnapsackItemViewModel(item));

            this.Number = generation.Number;
            this.Solution.AddRange(solutionItems);
        }
    }
}
