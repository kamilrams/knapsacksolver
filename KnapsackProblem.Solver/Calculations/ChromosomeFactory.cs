namespace KnapsackProblem.Solver.Calculations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using KnapsackProblem.Solver.Model;

    internal class ChromosomeFactory
    {
        private readonly Dictionary<int, KnapsackItem> itemsDatabase;
        private readonly Random random;

        public ChromosomeFactory(Dictionary<int, KnapsackItem> itemsDatabase, Random random)
        {
            this.itemsDatabase = itemsDatabase;
            this.random = random;
        }

        public Chromosome CreateRandom() 
        {
            var encodedValue = this.itemsDatabase
                .Select(item => this.random.NextDouble() < 0.5 ? true : false)
                .ToArray();

            return new Chromosome
            {
                EncodedValue = encodedValue
            };
        }
    }
}
