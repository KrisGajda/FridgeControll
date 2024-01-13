namespace FridgeControll.Tests
{
    public class Tests
    {
        [Test]
        public void WhenThreeTemperaturesAdded_ThenReturnAverageValue()
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
    }
}