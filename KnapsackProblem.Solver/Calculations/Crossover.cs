namespace KnapsackProblem.Solver.Calculations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using KnapsackProblem.Solver.Model;

    internal class Crossover
    {
        private readonly Random random;
        private readonly SolverOptions options;

        public Crossover(Random random, SolverOptions options)
        {
            this.random = random;
            this.options = options;
        }

        public List<Chromosome> GetResults(IList<Chromosome> parentsPool)
        {
            var childrens = new List<Chromosome>();

            for (var j = 0; j < this.options.InitialPopulationSize / 2; j++)
            {
                var firstParent = parentsPool.ElementAt(this.random.Next(parentsPool.Count));
                var otherParents = parentsPool.Where(parent => parent != firstParent).ToList();
                var secondParent = otherParents.ElementAt(this.random.Next(otherParents.Count));

                var crossoverResult = this.GetCrossoverResults(firstParent, secondParent);

                childrens.AddRange(crossoverResult);
            }

            return childrens;
        }

        private List<Chromosome> GetCrossoverResults(Chromosome parentA, Chromosome parentB)
        {
            if (parentA.EncodedValue.Length != parentB.EncodedValue.Length)
            {
                throw new InvalidOperationException("The length of chromosomes does not match");
            }

            var randomValue = this.random.NextDouble();

            if (randomValue > this.options.CrossoverProbability)
            {
                return new List<Chromosome>
                {
                    parentA.Clone(),
                    parentB.Clone()
                };
            }

            var crossoverPoint = this.random.Next(0, parentA.EncodedValue.Length - 1);
            var childA = PerformCrossover(parentA, parentB, crossoverPoint);
            var childB = PerformCrossover(parentB, parentA, crossoverPoint);

            return new List<Chromosome> { childA, childB };
        }

        private static Chromosome PerformCrossover(Chromosome firstHalf, Chromosome secondHalf, int crossoverPoint)
        {
            var head = firstHalf.EncodedValue.Take(crossoverPoint);
            var tail = secondHalf.EncodedValue.Skip(crossoverPoint);
            var encodedValue = head.Concat(tail).ToArray();

            return new Chromosome
            {
                EncodedValue = encodedValue
            };
        }
    }
}
