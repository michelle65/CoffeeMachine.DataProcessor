using CoffeeMachine.DataProcessor.Data;
using CoffeeMachine.DataProcessor.Models;

public class FakeCoffeeCountStore : ICoffeeCountStore
{
    public List<CoffeeCountItem> SavedItems { get; } = new();

    public void Save(CoffeeCountItem item)
    {
        SavedItems.Add(item);
    }
}