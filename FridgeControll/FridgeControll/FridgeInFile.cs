namespace FridgeControll
{
    public class FridgeInFile : FridgeBase
    {
        public override event TemperatureAddedDelegate TemperatureAdded;
        public FridgeInFile()
            : base()
        {
            FilesCreating();
        }

       public FridgeInFile(string producer, string id)
            : base(producer, id)
        {
            FilesCreating();
        }

        public FridgeInFile(string producer, string id, float correctTemperature, float allowableDifference)
            : base(producer, id, correctTemperature, allowableDifference)
        {
            FilesCreating();
        }

        private void FilesCreating()
        {
            string allTempsfileName = CreateFileName(Producer, Id) + "_All.txt";
            string badTempsfileName = CreateFileName(Producer, Id) + "_Bad.txt";
            AllTempsFileName = allTempsfileName;
            BadTempsFileName = badTempsfileName;
        }
        private string AllTempsFileName { get; set; }
        private string BadTempsFileName { get; set; }
        
        public override void AddTemperature(float temperature)
        {
            if ((temperature >= -10 && temperature <= 40) && ((Math.Abs(temperature - CorrectTemperature)) 
                > AllowableDifference))
            {
                using (var writerAll = File.AppendText(AllTempsFileName))
                {
                    writerAll.WriteLine(temperature);
                }
                using (var writerBad = File.AppendText(BadTempsFileName))
                {
                    writerBad.WriteLine(temperature);
                }
                if (TemperatureAdded != null)
                {
                    TemperatureAdded(this, new EventArgs());
                }
            }
            else if ((temperature >= -10 && temperature <= 40) && ((Math.Abs(temperature - CorrectTemperature)) 
                <= AllowableDifference))
            {
                using (var writerAll = File.AppendText(AllTempsFileName))
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
        public override Statistics GetStatistics()
        {
            var statistics = new Statistics();
            var allTemperaturesFromFile = ReadAllTemperaturesFromFile();
            var badTemperaturesFromFile = ReadBadTemperaturesFromFile();
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
            if (File.Exists(AllTempsFileName))
            {
                using (var reader = File.OpenText(AllTempsFileName))
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
            if (File.Exists(BadTempsFileName))
            {
                using (var reader = File.OpenText(BadTempsFileName))
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

        private string CreateFileName(string producer, string id)
        {
            return ($"{producer}_{id}");
        }

    }
}
