namespace Brewery_SCADA_System.Models
{
    public static class Global
    {
        public static int Frequency = 2500;
        public static Double LowLimit=0;
        public static Double HighLimit=100;
        public static String Simulation = "SIN";
        public static SemaphoreSlim _semaphore = new SemaphoreSlim(1);

    }

}
