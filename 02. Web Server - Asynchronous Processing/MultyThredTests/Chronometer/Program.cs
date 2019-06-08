using System;
using System.Threading.Tasks;

namespace ChronometerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var chronometer = new Cronometer();

            string input = Console.ReadLine();

            while (input!="exit")
            {
                switch (input)
                {
                    case "start":
                        chronometer.Start();
                        break;
                    case "stop":
                        chronometer.Stop();
                        break;
                    case "lap":
                        Console.WriteLine(chronometer.Lap());
                        break;
                    case "laps":
                        Console.WriteLine("Laps\r\n" + (chronometer.Laps.Count == 0 ? "No laps.\r\n" : string.Join("\r\n", chronometer.Laps)));
                        break;
                    case "time":
                        Console.WriteLine(chronometer.GetTime);
                        break;
                    case "reset":
                        chronometer.Reset();
                        break;
                    default:
                        break;
                }
                input = Console.ReadLine();
            }
        }
    }
}
