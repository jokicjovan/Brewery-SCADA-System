using Brewery_SCADA_System.Models;
using System.Text.Json;

namespace Brewery_SCADA_System.Services
{
    public class StartupService : IHostedService
    {
        private readonly HttpClient _httpClient;
        public StartupService()
        {
            _httpClient = new HttpClient();
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            MyMethod();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async void MyMethod()
        {
            string jsonString = File.ReadAllText("scadaConfig.json");
            JsonDocument jsonDocument = JsonDocument.Parse(jsonString);
            foreach (JsonProperty property in jsonDocument.RootElement.EnumerateObject())
            {
                string propertyName = property.Name;
                JsonElement propertyValue = property.Value;

                switch (propertyName)
                {
                    case "frequency":
                        Global.Frequency =int.Parse(propertyValue.ToString());
                        break;
                    case "lowLimit":
                        Global.LowLimit =Double.Parse(propertyValue.ToString());
                        break;
                    case "highLimit":
                        Global.HighLimit =Double.Parse(propertyValue.ToString());
                        break;
                    case "simulation":
                        Global.Simulation = propertyValue.ToString();
                        break;
                }
            }
            Thread.Sleep(1000);
            HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:5041/api/Device/startSimulation");
            HttpResponseMessage responseStartup = await _httpClient.GetAsync("http://localhost:5041/api/Tag/startupCheck");
        }
    }
}
