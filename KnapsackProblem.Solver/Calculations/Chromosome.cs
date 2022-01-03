namespace KnapsackProblem.Solver.Calculations
{
    using KnapsackProblem.Solver.Model;

    internal class Chromosome
    {
        public bool[] EncodedValue { get; set; }

        public List<KnapsackItem> Decode(Dictionary<int, KnapsackItem> itemsDatabase)
        {
            if (itemsDatabase.Count != this.EncodedValue.Length)
            {
                throw new InvalidOperationException("Encoded value does not match the specified database");
            }

            var selectedItems = new List<KnapsackItem>();

            for (var i = 0; i < this.EncodedValue.Length; i++)
            {
                var itemIsSelected = this.EncodedValue[i];

                if (!itemIsSelected)
                {
                    continue;
                }

                selectedItems.Add(itemsDatabase[i]);
            }

            return selectedItems;
        }

        public Chromosome Clone()
        {
            return new Chromosome
            {
                EncodedValue = this.EncodedValue.ToArray()
            };
        }
    }
}
