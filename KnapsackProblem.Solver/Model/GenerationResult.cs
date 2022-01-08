namespace KnapsackProblem.Solver.Model
{
    using System.Collections.Generic;

    public class GenerationResult
    {
        public int Number { get; }

        public bool HasCorrectSolution { get; }

        public IList<KnapsackItem> Solution { get; }

        public double TotalValue { get; }

        public double TotalWeight { get; }

        public GenerationResult(int generationNumber, bool hasCorrectSolution, IList<KnapsackItem> solution)
        {
            this.Number = generationNumber;
            this.HasCorrectSolution = hasCorrectSolution;
            this.Solution = solution;
            this.TotalValue = solution.Sum(item => item.Value);
            this.TotalWeight = solution.Sum(item => item.Weight);
        }
    }
}
