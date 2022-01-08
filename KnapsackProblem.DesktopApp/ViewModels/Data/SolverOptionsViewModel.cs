namespace KnapsackProblem.DesktopApp.ViewModels.Data
{
    using KnapsackProblem.Solver.Model;
    using Microsoft.Toolkit.Mvvm.ComponentModel;

    internal class SolverOptionsViewModel : ObservableObject
    {
        private int numberOfGenerations;
        private int initialPopulationSize;
        private double crossoverProbability;
        private double mutationProbability;
        private int randomSeed;

        public int NumberOfGenerations
        {
            get => this.numberOfGenerations;
            set => this.SetProperty(ref this.numberOfGenerations, value);
        }

        public int InitialPopulationSize
        {
            get => this.initialPopulationSize;
            set => this.SetProperty(ref this.initialPopulationSize, value);
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
            this.InitialPopulationSize = solverOptions.InitialPopulationSize;
            this.CrossoverProbability = solverOptions.CrossoverProbability;
            this.MutationProbability = solverOptions.MutationProbability;
            this.RandomSeed = solverOptions.RandomSeed;
        }

        public SolverOptions ToModel()
        {
            return new SolverOptions
            {
                NumberOfGenerations = this.NumberOfGenerations,
                InitialPopulationSize = this.InitialPopulationSize,
                CrossoverProbability = this.CrossoverProbability,
                MutationProbability = this.MutationProbability,
                RandomSeed = this.RandomSeed
            };
        }
    }
}
