namespace KnapsackProblem.Solver.Model
{
    using System.Collections.Generic;
    using System.Linq;

    public class SolverResult
    {
        public IList<GenerationResult> Generations { get; set; }

        public GenerationResult GetBestResult()
        {
            return this.Generations
                .Where(generation => generation.HasCorrectSolution)
                .OrderByDescending(generation => generation.TotalValue)
                .ThenByDescending(generation => generation.Number)
                .First();
        }
    }
}
