namespace KnapsackProblem.Solver.Model
{
    public enum StepType
    {
        CreatingInitialPopulation,
        ProcessingGeneration,
        WorkCompleted
    }

    public record Progress(int CurrentStep, int TotalSteps);

    public class ProgressReport
    {
        public StepType Step { get; }

        public Progress Progress { get; }

        public ProgressReport(StepType step, Progress progress)
        {
            this.Step = step;
            this.Progress = progress;
        }
    }
}
