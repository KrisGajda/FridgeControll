namespace FridgeControll
{
    public abstract class FridgeBase : IFridge
    {
        public delegate void TemperatureAddedDelegate(object sender, EventArgs args);
        public abstract event TemperatureAddedDelegate TemperatureAdded;
        public FridgeBase(string producer, string id, float correctTemperature, float allowableDifference)
        {
            this.Producer = producer;
            this.Id = id;
            this.CorrectTemperature = correctTemperature;
            this.AllowableDifference = allowableDifference;
        }

        public FridgeBase(string producer, string id)
            : this(producer, id, 6, 2)
        {
        }

        public FridgeBase()
            : this("DefaultProducer", "Default ID", 6, 2)
        {
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
                if (value < 40 && value > 0)
                {
                    correctTemperature = value;
                }
                else
                {
                    throw new Exception("Wprowadzona temperatura wykracza poza wymaganą skalę (0 - 40)");
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
                    throw new Exception("Wprowadzona dopuszczalna odchyłka temperatury wykracza poza wymaganą skalę (+-5)");
                }
            }
        }
        public abstract void AddTemperature(float temperature);
        public abstract void AddTemperature(string temperature);
        public abstract void AddTemperature(double temperature);
        public abstract void AddTemperature(int temperature);
        public abstract void AddTemperature(long temperature);
        public abstract Statistics GetStatistics();
    }
}