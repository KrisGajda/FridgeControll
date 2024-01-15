namespace FridgeControll.Tests
{
    public class Tests
    {
        [Test]
        public void WhenThreeTemperaturesAdded_ThenReturnAverageTemperatureValue()
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
        public void WhenTemperaturesAdded_ThenCountPercentageOfBadMeasures()
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
    }
}