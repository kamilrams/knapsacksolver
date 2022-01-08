namespace KnapsackProblem.Solver.Calculations
{
    using System.Collections.Generic;
    using KnapsackProblem.Solver.Model;

    internal class InitialPopulationGenerator
    {
        private readonly ChromosomeFactory chromosomeFactory;
        private readonly FitnessScoreCalculator scoreCalculator;
        private readonly SolverOptions options;

        public InitialPopulationGenerator(ChromosomeFactory chromosomeFactory, FitnessScoreCalculator scoreCalculator,
            SolverOptions options)
        {
            this.chromosomeFactory = chromosomeFactory;
            this.scoreCalculator = scoreCalculator;
            this.options = options;
        }

        public List<Chromosome> CreateInitialPopulation()
        {
            var initialPopulation = new List<Chromosome>();

            var minimumNumberOfCorrectSolutions = (int) (this.options.InitialPopulationSize * this.options.InitialPopulationQuality);

            while (initialPopulation.Count != this.options.InitialPopulationSize)
            {
                var chromosome = this.chromosomeFactory.CreateRandom();

                if (initialPopulation.Count >= minimumNumberOfCorrectSolutions)
                {
                    initialPopulation.Add(chromosome);
                    continue;
                }

                if (this.IsRepresentingCorrectSolution(chromosome))
                {
                    initialPopulation.Add(chromosome);
                }
            }

            return initialPopulation;
        }

        private bool IsRepresentingCorrectSolution(Chromosome chromosome)
        {
            var score = this.scoreCalculator.CalculateFitnessScore(chromosome);

            return score > 0;
        }
    }
}
