namespace KnapsackProblem.InputGenerator
{
    using System.Collections.Generic;
    using Bogus;
    using KnapsackProblem.Solver.Model;

    public class KnapsackItemGenerator
    {
        private readonly InputGeneratorParameters parameters;

        public KnapsackItemGenerator(InputGeneratorParameters parameters)
        {
            this.parameters = parameters;
        }

        public List<KnapsackItem> Generate(int numberOfItems)
        {
            var faker = new Faker<KnapsackItem>()
                .StrictMode(true)
                .RuleFor(x => x.Name, f => f.Commerce.Product())
                .RuleFor(x => x.Weight, f => f.Random.Double(this.parameters.MinimumWeight, this.parameters.MaximumWeight))
                .RuleFor(x => x.Value, f => f.Random.Double(this.parameters.MinimumValue, this.parameters.MaximumValue));

            return faker.Generate(numberOfItems);
        }
    }
}
