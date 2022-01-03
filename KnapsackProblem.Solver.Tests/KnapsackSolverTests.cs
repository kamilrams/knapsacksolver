namespace KnapsackProblem.Solver.Tests
{
    using System.Collections.Generic;
    using System.Linq;
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
            var totalWeight = result.FinalSolution.Solution.Sum(item => item.Weight);
            var totalValue = result.FinalSolution.Solution.Sum(item => item.Value);

            Assert.IsNotEmpty(result.FinalSolution.Solution);
            Assert.AreEqual(options.NumberOfGenerations, result.AllGenerations.Count);
            Assert.IsTrue(totalWeight <= input.KnapsackCapacity);
        }
    }
}
