namespace KnapsackProblem.Solver.Calculations
{
    using System.Collections.Generic;
    using System.Linq;

    internal class ParentSelector
    {
        public List<Chromosome> SelectParents(IList<RatedChromosome> ratedChromosomes, int maximumParentsCount)
        {
            var orderedChromosomes = ratedChromosomes
                .OrderByDescending(x => x.FitnessScore)
                .Select(x => x.Chromosome)
                .Take(maximumParentsCount)
                .ToList();

            return orderedChromosomes;
        }
    }
}
