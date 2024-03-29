﻿using CoffeeMachine.DataProcessor.Models;

namespace CoffeeMachine.DataProcessor.Data
{
    public class ConsoleCoffeeCountStore : ICoffeeCountStore
    {
        private readonly TextWriter _textWriter;
        public ConsoleCoffeeCountStore() : this(Console.Out) { }
        public ConsoleCoffeeCountStore(TextWriter textWriter)
        {
            _textWriter = textWriter;
        }
        public void Save(CoffeeCountItem item)
        {
            var line = $"{item.coffeeType}:{item.count}";
            _textWriter.WriteLine(line);
        }
    }
}
