namespace KnapsackProblem.Solver.Model
{
    using System.Collections.Generic;

    public class SolverInput
    {
        public double KnapsackCapacity { get; set; }

        public IList<KnapsackItem> AvailableItems { get; set; }
    }
}
