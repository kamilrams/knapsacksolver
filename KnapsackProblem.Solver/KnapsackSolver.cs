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
        private FitnessScoreCalculator scoreCalculator;
        private List<Chromosome> initialPopulation;

        public KnapsackSolver(SolverOptions options)
        {
            this.options = options;
            this.random = new Random(options.RandomSeed);
        }

        public SolverResult Solve(SolverInput input)
        {
            this.itemsDatabase = this.CreateItemsDatabase(input);
            this.scoreCalculator = new FitnessScoreCalculator(this.itemsDatabase, input.KnapsackCapacity);
            this.initialPopulation = this.CreateInitialPopulation();

            var generations = new List<Generation>();
            var currentPopulation = this.initialPopulation;

            for (var i = 0; i < this.options.NumberOfGenerations; i++)
            {
                var ratedChromosomes = this.CalculateFitnessScore(currentPopulation);

                var maximumParentsCount = this.options.InitialPopulationSize / 2;
                var parentSelector = new ParentSelector();
                var parentsPool = parentSelector.SelectParents(ratedChromosomes, maximumParentsCount);

                var crossover = new Crossover(this.random, this.options);
                var childrens = crossover.GetResults(parentsPool);

                var mutation = new Mutation(this.random, this.options);
                var mutated = mutation.GetResult(childrens);

                var bestChromosome = this.CalculateFitnessScore(currentPopulation)
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

        private Dictionary<int, KnapsackItem> CreateItemsDatabase(SolverInput input)
        {
            var itemsDatabase = new Dictionary<int,KnapsackItem>();

            for (var i = 0; i < input.AvailableItems.Count; i++)
            {
                itemsDatabase.Add(i, input.AvailableItems[i]);
            }

            return itemsDatabase;
        }

        private List<Chromosome> CreateInitialPopulation()
        {
            var chromosomeFactory = new ChromosomeFactory(this.itemsDatabase, this.random);
            var populationGenerator = new InitialPopulationGenerator(chromosomeFactory, this.scoreCalculator, this.options);

            return populationGenerator.CreateInitialPopulation();
        }

        private List<RatedChromosome> CalculateFitnessScore(List<Chromosome> population)
        {
            var result = new List<RatedChromosome>();

            foreach (var item in population)
            {
                var fitnessScore = this.scoreCalculator.CalculateFitnessScore(item);
                result.Add(new RatedChromosome(item, fitnessScore));
            }

            return result;
        }
    }
}
