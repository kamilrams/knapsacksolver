namespace KnapsackProblem.Solver.Calculations
{
    internal class RatedChromosome
    {
        public Chromosome Chromosome { get; }

        public double FitnessScore { get; }

        public RatedChromosome(Chromosome chromosome, double fitnessScore)
        {
            Chromosome = chromosome;
            FitnessScore = fitnessScore;
        }
    }
}
