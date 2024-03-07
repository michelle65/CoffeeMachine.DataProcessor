using CoffeeMachine.DataProcessor.Models;

namespace CoffeeMachine.DataProcessor.Parsing
{
    public class CsvLineParser
    {
        private const string DateTimeFormat = "M/d/yyyy hh:mm:ss tt";

        public static MachineDataItem[] Parse(string[] csvLines)
        {
            var machineDataItems = new List<MachineDataItem>();

            foreach (var csvLine in csvLines)
            {
                if (string.IsNullOrWhiteSpace(csvLine))
                {
                    continue;
                }

                try
                {
                    var machineDataItem = ParseLine(csvLine);
                    machineDataItems.Add(machineDataItem);
                }
                catch (FormatException ex)
                {
                    throw new Exception($"Invalid datetime in csv line: {csvLine}", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Invalid csv line: {csvLine}", ex);
                }
            }

            return machineDataItems.ToArray();
        }

        private static MachineDataItem ParseLine(string csvLine)
        {
            var lineItems = csvLine.Split(';');

            if (lineItems.Length != 2)
            {
                throw new Exception($"Invalid csv line: {csvLine}");
            }

            string coffeeType = lineItems[0];
            string dateTimeString = lineItems[1];

            if (DateTime.TryParseExact(dateTimeString, DateTimeFormat, null, System.Globalization.DateTimeStyles.None, out DateTime createdAt))
            {
                return new MachineDataItem(coffeeType, createdAt);
            }
            else
            {
                // Handle invalid datetime format
                throw new FormatException($"Invalid datetime format in csv line: {csvLine}");
            }
        }
    }
}
