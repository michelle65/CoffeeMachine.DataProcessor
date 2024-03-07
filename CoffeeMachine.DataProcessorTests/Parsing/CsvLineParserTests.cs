namespace CoffeeMachine.DataProcessor.Parsing
{
    public class CsvLineParserTests
    {
        [Fact]
        public void ShouldParseValidLine()
        {
            // Arrange
            string[] csvLines = new[] { "Cappuccino;1/1/2000 10:10:10 AM" };

            // Act
            var machineDataItems = CsvLineParser.Parse(csvLines);

            // Assert
            Assert.NotNull(machineDataItems);
            Assert.Single(machineDataItems);
            Assert.Equal("Cappuccino", machineDataItems[0].coffeeType);
            Assert.Equal(new DateTime(2000, 1, 1, 10, 10, 10), machineDataItems[0].createdAt);
        }

        [Fact]
        public void ShouldSkipEmptyLines()
        {
            //Arange
            string[] csvLines = new[] { "", " " };

            //Act
            var machineDataItems = CsvLineParser.Parse(csvLines);

            //Assert
            Assert.NotNull(machineDataItems);
            Assert.Empty(machineDataItems);
        }

        //[Fact]
        //public void ShouldThrowExcptionForInvalidLine()
        //{
        //    //Arrange
        //    var csvLine = "Espresso";
        //    string[] csvLines = new[] { csvLine };

        //    //Act
        //    var exception = Assert.Throws<Exception>(() => CsvLineParser.Parse(csvLines));
        //    Assert.Equal($"Invalid csv line: {csvLine}", exception.Message);

        //}

        [Fact]
        public void ShouldThrowExcptionForInvalidLine2()
        {
            //Arrange
            var csvLine = "Espresso;InvalidDateTime";
            string[] csvLines = new[] { csvLine };

            //Act
            var exception = Assert.Throws<Exception>(() => CsvLineParser.Parse(csvLines));
            Assert.Equal($"Invalid datetime in csv line: {csvLine}", exception.Message);

        }

        [InlineData("Espresso", "Invalid csv line")]
        [InlineData("Espresso;InvalidDateTime", "Invalid datetime in csv line")]
        [Theory]
        public void ShouldThrowExcptionForInvalidLine(string csvLine, string expectedMessagePrefix)
        {
            //Arrange
            string[] csvLines = new[] { csvLine };

            //Act
            var exception = Assert.Throws<Exception>(() => CsvLineParser.Parse(csvLines));
            Assert.Equal($"{expectedMessagePrefix}: {csvLine}", exception.Message);

        }
    }
}
