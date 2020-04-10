using System;
using System.Configuration;
using System.Threading;
using Nuts.ReportsGenerator.Entities;
using Nuts.ReportsGenerator.Simulator.Entities;

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
                var report =  PrepareSimulatedData();
                c.Send(report);

                Thread.Sleep(timeInterval);
            } while (true);
        }

        private static string GetConfigValue(string key, string defaultValue)
        {
            var result = ConfigurationManager.AppSettings[key];
            return result != null ? result.ToString() : defaultValue;
        }

        private static Report  PrepareSimulatedData()
        {
            var s = (uint)DateTime.Now.Second;
            return new Report
            {
                Var1 = s, StrVar1 = "Rep str1", StrVar2 = "Rep str2", SystemStatus = SystemStatusEnum.HF,
                Gd = new GdClass { Var1 = s+1, Var2 = s+1, Pr=new PrClass { Var1 = s+1, Var2 = s+2, Var3 = s+3, Var4 = s+4}},
                Hd = new HdClass { Var1 = s + 10, Var2 = s + 10, Ir = new IrClass { Var1 = s + 10, Var2 = s + 20, Var3 = s + 30, Var4 = s + 40 } }
            };
        }
    }
}