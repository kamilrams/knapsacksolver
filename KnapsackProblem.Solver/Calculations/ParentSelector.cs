namespace KnapsackProblem.Solver.Calculations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class ParentSelector
    {
        private readonly Random random;

        public ParentSelector(Random random)
        {
            this.random = random;
        }

        public List<Chromosome> SelectParents(IList<RatedChromosome> ratedChromosomes)
        {
            var rouletteWheelItems = ratedChromosomes
                .Select(item => new RouletteWheelItem<Chromosome>(item.Chromosome, item.FitnessScore))
                .ToList();

            var rouletteWheel = new RouletteWheel<Chromosome>(rouletteWheelItems);
            var randomNumbers = rouletteWheelItems.Select(_ => random.NextDouble()).ToList();

            return rouletteWheel.GetResult(randomNumbers);
        }
    }
}
