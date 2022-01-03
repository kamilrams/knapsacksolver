namespace KnapsackProblem.Solver.Calculations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using KnapsackProblem.Solver.Model;

    internal class Mutation
    {
        private readonly Random random;
        private readonly SolverOptions options;

        public Mutation(Random random, SolverOptions options)
        {
            this.random = random;
            this.options = options;
        }

        public List<Chromosome> GetResult(IEnumerable<Chromosome> chromosomes)
        {
            return chromosomes
                .Select(chromosome => Mutate(chromosome, this.options, this.random))
                .ToList();
        }

        private static Chromosome Mutate(Chromosome original, SolverOptions options, Random random)
        {
            var encodedValue = original.EncodedValue
                .Select(bit => random.NextDouble() < options.MutationProbability ? !bit : bit)
                .ToArray();

            return new Chromosome
            {
                EncodedValue = encodedValue
            };
        }
    }
}
