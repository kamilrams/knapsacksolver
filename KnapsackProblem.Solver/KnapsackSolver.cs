namespace KnapsackProblem.Solver
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using KnapsackProblem.Solver.Calculations;
    using KnapsackProblem.Solver.Model;

    public class KnapsackSolver
    {
        private readonly SolverOptions options;
        private readonly Random random;

        private Dictionary<int, KnapsackItem> itemsDatabase;
        private List<Chromosome> initialPopulation;

        public KnapsackSolver(SolverOptions options)
        {
            this.options = options;
            this.random = new Random(options.RandomSeed);
        }

        public SolverResult Solve(SolverInput input)
        {
            this.itemsDatabase = CreateItemsDatabase(input);
            this.initialPopulation = CreateInitialPopulation(this.itemsDatabase, this.options, this.random);

            var generations = new List<Generation>();
            var currentPopulation = this.initialPopulation;

            for (var i = 0; i < this.options.NumberOfGenerations; i++)
            {
                var ratedChromosomes = CalculateFitnessScore(this.itemsDatabase, currentPopulation, input);
                
                var parentSelector = new ParentSelector(this.random);
                var parentsPool = parentSelector.SelectParents(ratedChromosomes);

                var crossover = new Crossover(this.random, this.options);
                var childrens = crossover.GetResults(parentsPool);

                var mutation = new Mutation(this.random, this.options);
                var mutated = mutation.GetResult(childrens);

                var bestChromosome = CalculateFitnessScore(this.itemsDatabase, currentPopulation, input)
                    .OrderByDescending(item => item.FitnessScore)
                    .Select(item => item.Chromosome)
                    .First();

                var generation = new Generation
                {
                    Number = i,
                    Solution = bestChromosome.Decode(this.itemsDatabase)
                };

                generations.Add(generation);

                currentPopulation = mutated;
            }

            return new SolverResult
            {
                FinalSolution = generations.Last(),
                AllGenerations = generations
            };
        }

        private static Dictionary<int, KnapsackItem> CreateItemsDatabase(SolverInput input)
        {
            var itemsDatabase = new Dictionary<int,KnapsackItem>();

            for (var i = 0; i < input.AvailableItems.Count; i++)
            {
                itemsDatabase.Add(i, input.AvailableItems[i]);
            }

            return itemsDatabase;
        }

        private static List<Chromosome> CreateInitialPopulation(Dictionary<int, KnapsackItem> itemsDatabase, SolverOptions options, Random random)
        {
            var chromosomeFactory = new ChromosomeFactory(itemsDatabase, random);
            var initialPopulation = new List<Chromosome>();

            for (var i = 0; i < options.PopulationSize; i++)
            {
                var chromosome = chromosomeFactory.CreateRandom();

                initialPopulation.Add(chromosome);
            }

            return initialPopulation;
        }

        private static List<RatedChromosome> CalculateFitnessScore(Dictionary<int, KnapsackItem> itemsDatabase, List<Chromosome> population, SolverInput input)
        {
            var fitnessScoreCalculator = new FitnessScoreCalculator(itemsDatabase, input.KnapsackCapacity);
            var result = new List<RatedChromosome>();

            foreach (var item in population)
            {
                var fitnessScore = fitnessScoreCalculator.CalculateFitnessScore(item);
                result.Add(new RatedChromosome(item, fitnessScore));
            }

            return result;
        }
    }
}
