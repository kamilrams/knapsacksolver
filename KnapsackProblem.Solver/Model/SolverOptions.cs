﻿namespace KnapsackProblem.Solver.Model
{
    using System;

    public class SolverOptions
    {
        public int NumberOfGenerations { get; set; }

        public int InitialPopulationSize { get; set; }

        public double InitialPopulationQuality { get; set; }

        public double CrossoverProbability { get; set; }

        public double MutationProbability { get; set; }

        public int RandomSeed { get; set; }

        public static SolverOptions Default { get; } = new SolverOptions
        {
            NumberOfGenerations = 10,
            InitialPopulationSize = 20,
            InitialPopulationQuality = 0.8,
            CrossoverProbability = 0.9,
            MutationProbability = 0.05,
            RandomSeed = DateTime.Now.Millisecond
        };
    }
}
