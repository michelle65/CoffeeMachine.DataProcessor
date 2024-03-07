using CoffeeMachine.DataProcessor.Models;

namespace CoffeeMachine.DataProcessor.Data
{
    public interface ICoffeeCountStore
    {
        void Save(CoffeeCountItem  item);
    }
}
