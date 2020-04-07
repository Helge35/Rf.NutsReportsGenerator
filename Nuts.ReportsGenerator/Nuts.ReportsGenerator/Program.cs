using System;

namespace Nuts.ReportsGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            UdpSocket s = new UdpSocket("127.0.0.1", 27000);
            s.Init();

            Console.ReadKey();
        }
    }
}
