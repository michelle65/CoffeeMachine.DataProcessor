using CoffeeMachine.DataProcessor.Models;

namespace CoffeeMachine.DataProcessor.Data
{
    public class ConsoleCoffeeCountStoreTests
    {
        [Fact]
        public void ShouldWriteOutputToConsole()
        {
            //Arrange
            var item = new CoffeeCountItem("Espresso", 10);
            var stringWriter = new StringWriter();
            var consoleCoffeeCountStore = new ConsoleCoffeeCountStore(stringWriter);

            //Act
            consoleCoffeeCountStore.Save(item);

            //Assert
            var result = stringWriter.ToString();
            Assert.Equal($"{item.coffeeType}:{item.count}{Environment.NewLine}",result);

        }
    }
}
