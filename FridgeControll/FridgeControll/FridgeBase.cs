namespace FridgeControll
{
    public abstract class FridgeBase : IFridge
    {
        public delegate void TemperatureAddedDelegate(object sender, EventArgs args);
        public abstract event TemperatureAddedDelegate TemperatureAdded;

        public FridgeBase()
            : this("DefaultProducer", "Default ID", 6, 2)
        {
        }

        public FridgeBase(string producer, string id)
            : this(producer, id, 6, 2)
        {
            Producer = producer;
            Id = id;
        }

        public FridgeBase(string producer, string id, float correctTemperature, float allowableDifference)
        {
            Producer = producer;
            Id = id;
            CorrectTemperature = correctTemperature;
            AllowableDifference = allowableDifference;
        }

        public string Producer { get; private set; }
        public string Id { get; private set; }
        private float correctTemperature;
        private float allowableDifference;
        public float CorrectTemperature
        {
            get
            {
                return correctTemperature;
            }
            set
            {
                if (value <= 40 && value >= -10)
                {
                    correctTemperature = value;
                }
                else
                {
                    throw new Exception("Wprowadzona temperatura wykracza poza wymaganą skalę (-10 do +40°C)");
                }
            }
        }
        public float AllowableDifference
        {
            get
            {
                return allowableDifference;
            }
            set
            {
                if (value < 5 && value > 0)
                {
                    allowableDifference = value;
                }
                else
                {
                    throw new Exception("Wprowadzona dopuszczalna odchyłka temperatury wykracza poza wymaganą skalę (+-5°C)");
                }
            }
        }
        public abstract void AddTemperature(float temperature);
        public void AddTemperature(string temperature)
        {
            if (float.TryParse(temperature, out float result))
            {
                AddTemperature(result);
            }
            else
            {
                throw new Exception("Input value is not a number value!");
            }
        }
        public void AddTemperature(double temperature)
        {
            float temperatureValue = (float)temperature;
            AddTemperature(temperatureValue);
        }
        public void AddTemperature(int temperature)
        {
            float temperatureValue = temperature;
            AddTemperature(temperatureValue);
        }

        public void AddTemperature(long temperature)
        {
            float temperatureValue = temperature;
            AddTemperature(temperatureValue);
        }

        public abstract Statistics GetStatistics();
    }
}