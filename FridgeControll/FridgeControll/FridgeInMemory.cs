namespace FridgeControll
{
    public class FridgeInMemory : FridgeBase
    {
        List<float> temperatures = new List<float>();
        List<float> badTemperatures = new List<float>();
        public override event TemperatureAddedDelegate TemperatureAdded;
        public FridgeInMemory()
            : base()
        {
        }

        public FridgeInMemory(string producer, string id)
            : base(producer, id)
        {
        }

        public FridgeInMemory(string producer, string id, float correctTemperature, float allowableDifference)
            : base(producer, id, correctTemperature, allowableDifference)
        {
        }

        public override void AddTemperature(float temperature)
        {
            if ((temperature >= -10 && temperature <= 40) && ((Math.Abs(temperature - CorrectTemperature)) > AllowableDifference))
            {
                temperatures.Add(temperature);
                badTemperatures.Add(temperature);
                if (TemperatureAdded != null)
                {
                    TemperatureAdded(this, new EventArgs());
                }
            }
            else if ((temperature >= -10 && temperature <= 40) && ((Math.Abs(temperature - CorrectTemperature)) <= AllowableDifference))
            {
                temperatures.Add(temperature);
                if (TemperatureAdded != null)
                {
                    TemperatureAdded(this, new EventArgs());
                }
            }
            else
            {
                throw new Exception("Invalid temperature value");
            }
        }
        public override Statistics GetStatistics()
        {
            var statistics = new Statistics();
            foreach (var temperature in temperatures)
            {
                statistics.AddTemperature(temperature);
            }
            foreach (var badTemperature in badTemperatures)
            {
                statistics.CountBadTemperature();
            }
            return statistics;
        }

    }
}
