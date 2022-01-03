namespace KnapsackProblem.Solver.Model
{
    using System.Collections.Generic;

    public class SolverResult
    {
        public Generation FinalSolution { get; set; }

        public IList<Generation> AllGenerations { get; set; }
    }
}
