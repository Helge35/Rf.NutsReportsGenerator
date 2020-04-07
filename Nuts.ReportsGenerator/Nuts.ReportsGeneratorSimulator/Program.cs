using System;
using System.Configuration;
using System.Threading;

namespace Nuts.ReportsGenerator.Simulator
{
    class Program
    {
        static void Main(string[] args)
        {
            var address = GetConfigValue("address", "127.0.0.1");
            int.TryParse(GetConfigValue("port", "27000"), out var port);
            int.TryParse(GetConfigValue("timeIntervalInMilSec", "2000"), out var timeInterval);

            UdpSocketSender c = new UdpSocketSender(address, port);

            do
            {
                var status = PrepareSimulatedData();
                c.Send(status);

                Thread.Sleep(timeInterval);
            } while (true);
        }

        private static string GetConfigValue(string key, string defaultValue)
        {
            var result = ConfigurationManager.AppSettings[key];
            return result != null ? result.ToString() : defaultValue;
        }

        private static NutStatus PrepareSimulatedData()
        {
            UInt32 s = (UInt32)DateTime.Now.Second;
            return new NutStatus() { PData1 = s, PData2 = "Test", PData3 = s + 2, PData4 = s + 3 };
        }
    }
}