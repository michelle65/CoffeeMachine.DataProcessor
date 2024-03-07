using CoffeeMachine.DataProcessor.Data;
using CoffeeMachine.DataProcessor.Models;
using CoffeeMachine.DataProcessor.Parsing;
using CoffeeMachine.DataProcessor.Processing;

namespace CoffeeMachine.DataProcessor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("   Coffee Machine - Data Processor");
            Console.WriteLine("---------------------------------------------");

            const string filename = "CoffeeMachineData.csv";
            string[] csvLines=File.ReadAllLines(filename);

            MachineDataItem[] machineDataItems = CsvLineParser.Parse(csvLines);

            var machineDataProcessor = new MachineDataProcessor(new ConsoleCoffeeCountStore());
            machineDataProcessor.ProcessItems(machineDataItems);

            Console.WriteLine();
            Console.WriteLine($"File {filename} was successfully processed!");

            Console.ReadLine();
        }
    }
}
