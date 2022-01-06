namespace KnapsackProblem.DesktopApp.ViewModels.Data
{
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using KnapsackProblem.Solver.Model;

    internal class KnapsackItemViewModel : ObservableObject
    {
        private string name = string.Empty;
        private double weight;
        private double value;

        public string Name
        {
            get => this.name;
            set => this.SetProperty(ref this.name, value);
        }

        public double Weight
        {
            get => this.weight;
            set => this.SetProperty(ref this.weight, value);
        }

        public double Value
        {
            get => this.value;
            set => this.SetProperty(ref this.value, value);
        }

        public KnapsackItemViewModel(KnapsackItem knapsackItem)
        {
            this.Name = knapsackItem.Name;
            this.Weight = knapsackItem.Weight;
            this.Value = knapsackItem.Value;
        }

        public KnapsackItemViewModel(string name, double weight, double value)
        {
            this.Name = name;
            this.Weight = weight;
            this.Value = value;
        }

        public KnapsackItemViewModel()
        {
        }
    }
}
