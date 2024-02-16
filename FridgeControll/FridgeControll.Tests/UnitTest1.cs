namespace FridgeControll.Tests
{
    public class Tests
    {
        [Test]
        public void WhenThreeTemperaturesAdded_ThenReturnAverageTemperatureValueInMemory()
        {
            // arrange
            var fridge = new FridgeInMemory();
            fridge.AddTemperature(3);
            fridge.AddTemperature(3.25);
            fridge.AddTemperature(3.5);

            // act
            var statistics = fridge.GetStatistics();

            // assert
            Assert.AreEqual(3.25, statistics.Average);

        }

        [Test]
        public void WhenTemperaturesAdded_ThenCountPercentageOfBadMeasuresInMemory()
        {
            // arrange
            // correct temperature is 6
            // allowable difference is 2
            var fridge = new FridgeInMemory("Producer", "Model", 6, 2);
            fridge.AddTemperature(3);
            fridge.AddTemperature(9);
            fridge.AddTemperature(5);
            fridge.AddTemperature(6.5);

            // act
            var statistics = fridge.GetStatistics();

            // assert
            Assert.AreEqual(50, statistics.Condition);

        }

        [Test]
        public void WhenTemperaturesAdded_ThenCountPercentageOfBadMeasuresFromFilesData()
        {
            // arrange
            // correct temperature is 6
            // allowable difference is 2
            var fridge = new FridgeInFile("Producer", "Model", 6, 2);
            fridge.AddTemperature(3);
            fridge.AddTemperature(9);
            fridge.AddTemperature(5);
            fridge.AddTemperature(6.5);

            // act
            var statistics = fridge.GetStatistics();

            // assert
            Assert.AreEqual(50, statistics.Condition);

        }
    }
}