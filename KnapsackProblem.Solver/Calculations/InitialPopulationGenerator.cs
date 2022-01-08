namespace KnapsackProblem.Solver.Calculations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using KnapsackProblem.Solver.Model;

    internal class InitialPopulationGenerator
    {
        private readonly Dictionary<int, KnapsackItem> itemsDatabase;
        private readonly SolverOptions options;
        private readonly Random random;

        public InitialPopulationGenerator(Dictionary<int, KnapsackItem> itemsDatabase, SolverOptions options, Random random)
        {
            this.itemsDatabase = itemsDatabase;
            this.options = options;
            this.random = random;
        }

        public List<Chromosome> CreateInitialPopulation()
        {
            var chromosomeFactory = new ChromosomeFactory(itemsDatabase, random);
            var initialPopulation = new List<Chromosome>();

            for (var i = 0; i < options.InitialPopulationSize; i++)
            {
                var chromosome = chromosomeFactory.CreateRandom();

                initialPopulation.Add(chromosome);
            }

            return initialPopulation;
        }
    }
}
