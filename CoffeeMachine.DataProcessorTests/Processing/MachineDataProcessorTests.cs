using CoffeeMachine.DataProcessor.Models;

namespace CoffeeMachine.DataProcessor.Processing
{
    public class MachineDataProcessorTests:IDisposable
    {
        private readonly MachineDataProcessor machineDataProcessor;
        private readonly FakeCoffeeCountStore coffeeCountStore;

        public MachineDataProcessorTests()
        {
            coffeeCountStore = new FakeCoffeeCountStore();
            machineDataProcessor = new MachineDataProcessor(coffeeCountStore);
        }
        [Fact]
        public void ShouldSaveCountPerCoffeeType()
        {
            //Arrange
            var items = new[]
            {
                new MachineDataItem("Cappuccino",new DateTime(2022,10,7,8,0,0)),
                new MachineDataItem("Cappuccino",new DateTime(2022,10,27,9,0,0)),
                new MachineDataItem("Espresso", new DateTime(2022, 10, 27, 9, 0, 0))
            };

            //Act
            machineDataProcessor.ProcessItems(items);

            //Assert
            Assert.Equal(2,coffeeCountStore.SavedItems.Count);

            var item = coffeeCountStore.SavedItems[0];
            Assert.Equal("Cappuccino", item.coffeeType);
            Assert.Equal(2, item.count);

            item = coffeeCountStore.SavedItems[1];
            Assert.Equal("Espresso", item.coffeeType);
            Assert.Equal(1, item.count);
        }
        [Fact]
        public void ShouldIgnoreItemsThatAreNotNewer()
        {
            //Arrange
            var items = new[]
            {
                new MachineDataItem("Cappuccino",new DateTime(2022,10,27,8,0,0)),
                new MachineDataItem("Cappuccino",new DateTime(2022,10,27,7,0,0)),
                new MachineDataItem("Cappuccino",new DateTime(2022,10,27,7,10,0)),
                new MachineDataItem("Cappuccino",new DateTime(2022,10,27,9,0,0)),
                new MachineDataItem("Espresso", new DateTime(2022, 10, 27, 10, 0, 0)),
                new MachineDataItem("Espresso", new DateTime(2022, 10, 27, 10, 0, 0))
            };

            //Act
            machineDataProcessor.ProcessItems(items);

            //Assert
            Assert.Equal(2, coffeeCountStore.SavedItems.Count);

            var item = coffeeCountStore.SavedItems[0];
            Assert.Equal("Cappuccino", item.coffeeType);
            Assert.Equal(2, item.count);

            item = coffeeCountStore.SavedItems[1];
            Assert.Equal("Espresso", item.coffeeType);
            Assert.Equal(1, item.count);
        }
        [Fact]
        public void ShouldSaveCountPerCoffeeCount()
        {
            //Arrange
            var items = new[]
            {
                new MachineDataItem("Cappuccino",new DateTime(2022,10,7,8,0,0)),
            };

            //Act
            machineDataProcessor.ProcessItems(items);
            machineDataProcessor.ProcessItems(items);

            //Assert
            Assert.Equal(2, coffeeCountStore.SavedItems.Count);
            foreach (var item in coffeeCountStore.SavedItems)
            {
                Assert.Equal("Cappuccino", item.coffeeType);
                Assert.Equal(1,item.count);
            }
        }

        public void Dispose()
        {
           //this runs after every test
        }
    }
}
