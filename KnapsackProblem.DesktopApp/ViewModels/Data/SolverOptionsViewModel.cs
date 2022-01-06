namespace KnapsackProblem.DesktopApp.ViewModels.Data
{
    using KnapsackProblem.Solver.Model;
    using Microsoft.Toolkit.Mvvm.ComponentModel;

    internal class SolverOptionsViewModel : ObservableObject
    {
        private int numberOfGenerations;
        private int populationSize;
        private double crossoverProbability;
        private double mutationProbability;
        private int randomSeed;

        public int NumberOfGenerations
        {
            get => this.numberOfGenerations;
            set => this.SetProperty(ref this.numberOfGenerations, value);
        }

        public int PopulationSize
        {
            get => this.populationSize;
            set => this.SetProperty(ref this.populationSize, value);
        }

        public double CrossoverProbability
        {
            get => this.crossoverProbability;
            set => this.SetProperty(ref this.crossoverProbability, value);
        }

        public double MutationProbability
        {
            get => this.mutationProbability;
            set => this.SetProperty(ref this.mutationProbability, value);
        }

        public int RandomSeed
        {
            get => this.randomSeed;
            set => SetProperty(ref this.randomSeed, value);
        }

        public SolverOptionsViewModel() { }

        public SolverOptionsViewModel(SolverOptions solverOptions)
        {
            this.NumberOfGenerations = solverOptions.NumberOfGenerations;
            this.PopulationSize = solverOptions.PopulationSize;
            this.CrossoverProbability = solverOptions.CrossoverProbability;
            this.MutationProbability = solverOptions.MutationProbability;
            this.RandomSeed = solverOptions.RandomSeed;
        }

        public SolverOptions ToModel()
        {
            return new SolverOptions
            {
                NumberOfGenerations = this.NumberOfGenerations,
                PopulationSize = this.PopulationSize,
                CrossoverProbability = this.CrossoverProbability,
                MutationProbability = this.MutationProbability,
                RandomSeed = this.RandomSeed
            };
        }
    }
}
