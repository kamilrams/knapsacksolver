namespace KnapsackProblem.Solver.Model
{
    using System.Collections.Generic;

    public class Generation
    {
        public int Number { get; set; }

        public IList<KnapsackItem> Solution { get; set; }
    }
}
