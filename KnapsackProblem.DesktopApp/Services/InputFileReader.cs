namespace KnapsackProblem.DesktopApp.Services
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualBasic.FileIO;
    using KnapsackProblem.Solver.Model;
    using System.Globalization;

    internal class InputFileReader
    {
        public List<KnapsackItem> ReadFromFile(string filePath)
        {
            try
            {
                return this.PerformItemReading(filePath);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Cannot read input from specified file", ex);
            }
        }

        private List<KnapsackItem> PerformItemReading(string filePath)
        {
            var result = new List<KnapsackItem>();

            var textFieldParser = new TextFieldParser(filePath)
            {
                TextFieldType = FieldType.Delimited,
                Delimiters = new[] { "," }
            };

            while (!textFieldParser.EndOfData)
            {
                var fields = textFieldParser.ReadFields();
                var canReadItem = this.TryReadKnapsackItem(fields, out var item);

                if (!canReadItem)
                {
                    throw new InvalidOperationException($"Cannot create an item from fields: {fields}");
                }

                result.Add(item);
            }

            return result;
        }

        private bool TryReadKnapsackItem(string[]? fields, out KnapsackItem item)
        {
            item = null;

            if (fields is null) return false;
            if (fields.Length != 3) return false;
            if (!double.TryParse(fields[1], NumberStyles.Float, CultureInfo.InvariantCulture, out var weight)) return false;
            if (!double.TryParse(fields[2], NumberStyles.Float, CultureInfo.InvariantCulture, out var value)) return false;

            item = new KnapsackItem(fields[0], weight, value);

            return true;
        }
    }
}
