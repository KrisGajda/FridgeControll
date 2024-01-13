namespace FridgeControll
{

    public class Statistics
    {
        public float Min { get; private set; }
        public float Max { get; private set; }
        public float Sum { get; private set; }
        public float Count { get; private set; }
        public float CountBad { get; private set; }
        public float Average
        {
            get
            {
                return this.Sum / this.Count;
            }
        }

        public float Condition
        {
            get
            {
                return 100*(CountBad / Count);
            }
        }

        public string TechnicalCondition
        {
            get
            {
                switch (this.Condition)
                {
                    case var percentageOfDeviations when percentageOfDeviations >= 50:
                        return "E - lodówka niesprawna! Przenieś próbki do innej lodówki!";
                    case var percentageOfDeviations when percentageOfDeviations >= 30:
                        return "D - lodówka w złej kondycji. Występują częste odchyłki od normy. Niezwłocznie wezwij serwis.";
                    case var percentageOfDeviations when percentageOfDeviations >= 20:
                        return "C - lodówka w średniej kondycji. Występują okresowe wahania temperatury. Wezwij serwis.";
                    case var percentageOfDeviations when percentageOfDeviations >= 10:
                        return "B - lodówka w dobrej kondycji, ale w razie ponownych problemów powiadom serwis.";
                    default:
                        return "A - lodówka w bardzo dobrym stanie. Przeprowadzaj konserwację zgodnie z harmonogramem.";
                }
            }
        }
        public Statistics()
        {
            this.Count = 0;
            this.CountBad = 0;
            this.Sum = 0;
            this.Max = float.MinValue;
            this.Min = float.MaxValue;
        }
        public void AddTemperature(float temperature)
        {
            this.Count++;
            this.Sum += temperature;
            this.Min = Math.Min(this.Min, temperature);
            this.Max = Math.Max(this.Max, temperature);
        }
        public void CountBadTemperature()
        {
            this.CountBad++;
        }
    }
}