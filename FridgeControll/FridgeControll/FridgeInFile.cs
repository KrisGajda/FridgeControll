namespace FridgeControll
{
    public class FridgeInFile : FridgeBase
    {
        public override event TemperatureAddedDelegate TemperatureAdded;

        public override void AddTemperature(float temperature)
        {
            throw new NotImplementedException();
        }

        public override void AddTemperature(string temperature)
        {
            throw new NotImplementedException();
        }

        public override void AddTemperature(double temperature)
        {
            throw new NotImplementedException();
        }

        public override void AddTemperature(int temperature)
        {
            throw new NotImplementedException();
        }

        public override void AddTemperature(long temperature)
        {
            throw new NotImplementedException();
        }

        public override Statistics GetStatistics()
        {
            throw new NotImplementedException();
        }
    }
}
