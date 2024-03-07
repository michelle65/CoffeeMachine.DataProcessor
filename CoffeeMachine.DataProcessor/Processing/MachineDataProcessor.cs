using CoffeeMachine.DataProcessor.Data;
using CoffeeMachine.DataProcessor.Models;

namespace CoffeeMachine.DataProcessor.Processing
{
    public class MachineDataProcessor
    {
        private readonly Dictionary<string, int> _countPerCoffeeType = new();
        private readonly ICoffeeCountStore _coffeeCountStore;
        private MachineDataItem? _previousItem;

        public MachineDataProcessor(ICoffeeCountStore coffeeCountStore)
        {
            _coffeeCountStore = coffeeCountStore;
        }

        public void ProcessItems(MachineDataItem[] dataItems)
        {
            _previousItem = null;
            _countPerCoffeeType.Clear();
            foreach (var dataItem in dataItems)
            {
                ProcessItem(dataItem);
            }
            SaveCountPerCoffeeType();
        }
        private void ProcessItem(MachineDataItem dataItem)
        {
            if (!IsNewerThanPreviousMethod(dataItem))
            {
                return;
            }
            if (!_countPerCoffeeType.ContainsKey(dataItem.coffeeType))
            {
                _countPerCoffeeType.Add(dataItem.
                    coffeeType, 1);
            }
            else
            {
                _countPerCoffeeType[dataItem.coffeeType]++;
            }
            _previousItem = dataItem;

        }

        private bool IsNewerThanPreviousMethod(MachineDataItem dataItem)
        {
            return _previousItem == null || _previousItem.createdAt < dataItem.createdAt;
        }

        private void SaveCountPerCoffeeType()
        {
            foreach (var entry in _countPerCoffeeType)
            {
                _coffeeCountStore.Save(new CoffeeCountItem(entry.Key, entry.Value));
            }
        }
    }
}
