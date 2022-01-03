namespace KnapsackProblem.Solver.Tests
{
    using System.Collections.Generic;
    using NUnit.Framework;
    using KnapsackProblem.Solver.Calculations;

    [TestFixture]
    internal class RouletteWheelTests
    {
        [Test]
        public void GetResult_ReturnsCorrectResult()
        {
            var rouletteItems = new List<RouletteWheelItem<int>>
            {
                new RouletteWheelItem<int>(1, 0.09),
                new RouletteWheelItem<int>(2, 0.33),
                new RouletteWheelItem<int>(3, 0.29),
                new RouletteWheelItem<int>(4, 0.29)
            };

            var rouletteWheel = new RouletteWheel<int>(rouletteItems);

            var randomNumbers = new[] { 0.12, 0.41, 0.56, 0.75 };
            var result = rouletteWheel.GetResult(randomNumbers);

            Assert.IsNotEmpty(result);
            CollectionAssert.AreEqual(new[] { 2, 2, 3, 4 }, result);
        }
    }
}
