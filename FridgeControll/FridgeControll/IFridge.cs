using static FridgeControll.FridgeBase;

namespace FridgeControll
{
    public interface IFridge
    {
        string Producer { get; }
        string Id { get; }
        float CorrectTemperature { get; }
        float AllowableDifference {  get; }
        void AddTemperature(float temperature);
        void AddTemperature(string temperature);
        void AddTemperature(double temperature);
        void AddTemperature(int temperature);
        void AddTemperature(long temperature);
        event TemperatureAddedDelegate TemperatureAdded;
        Statistics GetStatistics();

    }
}
