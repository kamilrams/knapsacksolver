namespace KnapsackProblem.Solver.Calculations
{
    using System.Collections.Generic;
    using System.Linq;
    using KnapsackProblem.Solver.Model;

    internal class FitnessScoreCalculator
    {
        private readonly Dictionary<int, KnapsackItem> itemsDatabase;
        private readonly double maximumAllowedCapacity;

        public FitnessScoreCalculator(Dictionary<int, KnapsackItem> itemsDatabase, double maximumAllowedCapacity)
        {
            this.itemsDatabase = itemsDatabase;
            this.maximumAllowedCapacity = maximumAllowedCapacity;
        }

        public double CalculateFitnessScore(Chromosome chromosome)
        {
            var selectedItems = chromosome.Decode(this.itemsDatabase);
            var totalValue = selectedItems.Sum(item => item.Value);
            var totalWeight = selectedItems.Sum(item => item.Weight);

            if (totalWeight > maximumAllowedCapacity)
            {
                return 0;
            }

            return totalValue;
        }
    }
}
