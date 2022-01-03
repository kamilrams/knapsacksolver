namespace KnapsackProblem.Solver.Calculations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public record RouletteWheelItem<T>(T Item, double Score);

    public class RouletteWheel<T>
    {
        private readonly List<RouletteWheelItem<T>> rouletteItems;

        public RouletteWheel(List<RouletteWheelItem<T>> items)
        {
            this.rouletteItems = items;
        }

        public List<T> GetResult(IList<double> randomNumbers)
        {
            if (randomNumbers.Count != this.rouletteItems.Count)
            {
                throw new InvalidOperationException($"The specified random numbers count ({randomNumbers.Count} does not match the number of roulette items ({this.rouletteItems.Count})");
            }

            var rouletteWheel = CreateRouletteWheel(rouletteItems);
            var selectedItems = randomNumbers
                .OrderBy(x => x)
                .Select(x => rouletteWheel.First(sector => IsInsideSector(sector, x)))
                .Select(item => item.Item)
                .Distinct()
                .ToList();

            return selectedItems;
        }

        private static List<RouletteWheelSector> CreateRouletteWheel(IList<RouletteWheelItem<T>> rouletteItems)
        {
            var totalScore = rouletteItems.Sum(item => item.Score);
            var sectors = new List<RouletteWheelSector>();

            for (var i = 0; i < rouletteItems.Count; i++)
            {
                var currentItem = rouletteItems[i];
                var selectionProbability = currentItem.Score / totalScore;

                var lowerValue = sectors.ElementAtOrDefault(i - 1)?.Upper ?? 0;
                var upperValue = lowerValue + selectionProbability;

                sectors.Add(new RouletteWheelSector(lowerValue, upperValue, currentItem.Item));
            }

            return sectors;
        }

        private static bool IsInsideSector(RouletteWheelSector sector, double value)
        {
            return value >= sector.Lower && value <= sector.Upper;
        }

        private record RouletteWheelSector(double Lower, double Upper, T Item);
    }
}
