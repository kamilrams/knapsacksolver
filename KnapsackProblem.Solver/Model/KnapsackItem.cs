namespace KnapsackProblem.Solver.Model
{
    public class KnapsackItem
    {
        public KnapsackItem(string name, double weight, double value)
        {
            Name = name;
            Weight = weight;
            Value = value;
        }

        public KnapsackItem() { }

        public string Name { get; set; }

        public double Weight { get; set; }

        public double Value { get; set; }


    }
}
