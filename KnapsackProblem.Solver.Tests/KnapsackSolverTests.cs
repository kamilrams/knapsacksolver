namespace KnapsackProblem.Solver.Tests
{
    using System.Collections.Generic;
    using NUnit.Framework;
    using KnapsackProblem.Solver.Model;

    [TestFixture]
    internal class KnapsackSolverTests
    {
        [TestCase(5)]
        [TestCase(10)]
        [TestCase(20)]
        [TestCase(30)]
        [TestCase(50)]
        public void Solve_ReturnsCorrectResult(int numberOfGenerations)
        {
            var options = SolverOptions.Default;
            options.NumberOfGenerations = numberOfGenerations;

            var solver = new KnapsackSolver(options);

            var input = new SolverInput
            {
                KnapsackCapacity = 15,
                AvailableItems = new List<KnapsackItem>
                {
                    new KnapsackItem("Green", 12, 4),
                    new KnapsackItem("Grey", 1, 2),
                    new KnapsackItem("Blue", 2, 2),
                    new KnapsackItem("Orange", 1, 1),
                    new KnapsackItem("Gold", 4, 10)
                }
            };

            var result = solver.Solve(input);
            var bestResult = result.GetBestResult();

            Assert.IsTrue(bestResult.HasCorrectSolution);
            Assert.IsNotEmpty(bestResult.Solution);
            Assert.AreEqual(options.NumberOfGenerations, result.Generations.Count);
            Assert.IsTrue(bestResult.TotalWeight <= input.KnapsackCapacity);
            Assert.IsTrue(bestResult.TotalValue > 0);
        }
    }
}
