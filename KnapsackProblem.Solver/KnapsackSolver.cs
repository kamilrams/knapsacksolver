namespace KnapsackProblem.Solver
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using KnapsackProblem.Solver.Calculations;
    using KnapsackProblem.Solver.Model;

    public class KnapsackSolver
    {
        public delegate void ProgressReportHandler(ProgressReport report);
        public event ProgressReportHandler ProgressReport;

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

        public SolverResult Solve(SolverInput input, CancellationToken cancellationToken = default)
        {
            this.ValidateInput(input);

            this.ReportProgress(StepType.CreatingInitialPopulation);

            this.itemsDatabase = this.CreateItemsDatabase(input);
            this.scoreCalculator = new FitnessScoreCalculator(this.itemsDatabase, input.KnapsackCapacity);
            this.initialPopulation = this.CreateInitialPopulation(cancellationToken);

            var generationResults = new List<GenerationResult>();
            var currentPopulation = this.initialPopulation;

            for (var i = 0; i < this.options.NumberOfGenerations; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                this.ReportProgress(StepType.ProcessingGeneration, i + 1, this.options.NumberOfGenerations);

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
                    .First();

                var generationResult = new GenerationResult(i,
                    bestChromosome.FitnessScore > 0,
                    bestChromosome.Chromosome.Decode(this.itemsDatabase));

                generationResults.Add(generationResult);

                currentPopulation = mutated;
            }

            cancellationToken.ThrowIfCancellationRequested();

            this.ReportProgress(StepType.WorkCompleted);

            return new SolverResult
            {
                Generations = generationResults
            };
        }

        private void ValidateInput(SolverInput input)
        {
            var minimumAllowedCapacity = input.AvailableItems
                .Min(item => item.Weight);

            if (input.KnapsackCapacity >= minimumAllowedCapacity)
            {
                return;
            }

            throw new InvalidOperationException("The provided knapsack capacity is too small");
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

        private List<Chromosome> CreateInitialPopulation(CancellationToken cancellationToken)
        {
            var chromosomeFactory = new ChromosomeFactory(this.itemsDatabase, this.random);
            var populationGenerator = new InitialPopulationGenerator(chromosomeFactory, this.scoreCalculator, this.options);

            return populationGenerator.CreateInitialPopulation(cancellationToken);
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

        private void ReportProgress(StepType stepType, int currentStep, int totalSteps)
        {
            var progressReport = new ProgressReport(stepType, new Progress(currentStep, totalSteps));

            this.ProgressReport?.Invoke(progressReport);
        }

        private void ReportProgress(StepType stepType)
        {
            this.ReportProgress(stepType, 1, 1);
        }
    }
}
