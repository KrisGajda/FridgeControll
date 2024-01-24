using System.Diagnostics;

namespace FridgeControll
{
    public class FridgeInFile : FridgeBase
    {
        List<float> temperatures = new List<float>();
        List<float> badTemperatures = new List<float>();
        public override event TemperatureAddedDelegate TemperatureAdded;
        public FridgeInFile()
            : base()
        {
            string allTempsfileName = CreateFileName(this.Producer, this.Id) + "All.txt";
            string badTempsfileName = CreateFileName(this.Producer, this.Id) + "Bad.txt";
            this.AllTempsFileName = allTempsfileName;
            this.BadTempsFileName = badTempsfileName;
        }

        public FridgeInFile(string producer, string id)
            : base(producer, id)
        {
            string allTempsfileName = CreateFileName(producer, id) + "All.txt";
            string badTempsfileName = CreateFileName(producer, id) + "Bad.txt";
            this.AllTempsFileName = allTempsfileName;
            this.BadTempsFileName = badTempsfileName;
        }

        public FridgeInFile(string producer, string id, float correctTemperature, float allowableDifference)
            : base(producer, id, correctTemperature, allowableDifference)
        {
            string allTempsfileName = CreateFileName(producer, id) + "All.txt";
            string badTempsfileName = CreateFileName(producer, id) + "Bad.txt";
            this.AllTempsFileName = allTempsfileName;
            this.BadTempsFileName = badTempsfileName;
        }

        public string AllTempsFileName { get; private set; }
        public string BadTempsFileName { get; private set; }
        public override void AddTemperature(float temperature)
        {
            if ((temperature >= -10 && temperature <= 40) && ((Math.Abs(temperature - CorrectTemperature)) > AllowableDifference))
            {
                using (var writerAll = File.AppendText(this.AllTempsFileName))
                {
                    writerAll.WriteLine(temperature);
                }
                using (var writerBad = File.AppendText(this.BadTempsFileName))
                {
                    writerBad.WriteLine(temperature);
                }
                if (TemperatureAdded != null)
                {
                    TemperatureAdded(this, new EventArgs());
                }
            }
            else if ((temperature >= -10 && temperature <= 40) && ((Math.Abs(temperature - CorrectTemperature)) <= AllowableDifference))
            {
                using (var writerAll = File.AppendText(this.AllTempsFileName))
                {
                    writerAll.WriteLine(temperature);
                }
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
        public override void AddTemperature(string temperature)
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
        public override void AddTemperature(double temperature)
        {
            float temperatureValue = (float)temperature;
            AddTemperature(temperatureValue);
        }
        public override void AddTemperature(int temperature)
        {
            float temperatureValue = (float)temperature;
            AddTemperature(temperatureValue);
        }

        public override void AddTemperature(long temperature)
        {
            float temperatureValue = (float)temperature;
            AddTemperature(temperatureValue);
        }

        public override Statistics GetStatistics()
        {
            var statistics = new Statistics();
            var allTemperaturesFromFile = this.ReadAllTemperaturesFromFile();
            var badTemperaturesFromFile = this.ReadBadTemperaturesFromFile();
            foreach (var everyTemperature in allTemperaturesFromFile)
            {
                statistics.AddTemperature(everyTemperature);
            }
            foreach (var badTemperature in badTemperaturesFromFile)
            {
                statistics.CountBadTemperature();
            }
            return statistics;
        }
        private List<float> ReadAllTemperaturesFromFile()
        {
            var allTemperatures = new List<float>();
            if (File.Exists(this.AllTempsFileName))
            {
                using (var reader = File.OpenText(this.AllTempsFileName))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        allTemperatures.Add(float.Parse(line));
                        line = reader.ReadLine();
                    }
                }
            }
            return allTemperatures;
        }
        private List<float> ReadBadTemperaturesFromFile()
        {
            var badTemperatures = new List<float>();
            if (File.Exists(this.BadTempsFileName))
            {
                using (var reader = File.OpenText(this.BadTempsFileName))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        badTemperatures.Add(float.Parse(line));
                        line = reader.ReadLine();
                    }
                }
            }
            return badTemperatures;
        }

        public string CreateFileName(string producer, string id)
        {
            return producer + id;
        }

    }
}
