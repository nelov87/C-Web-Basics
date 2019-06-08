using System;
using System.Threading;
using System.Threading.Tasks;

namespace tes1
{
    public class Program
    {
        static void Main(string[] args)
        {

            

            //var start = new Class();

            Start();
        }

        public static string ReturnString()
        {
            return "It works.";
        }

        public static void Start()
        {
            Console.WriteLine("Start");

            Task.Run(() => {
                Console.WriteLine("Tread started!");
                //Task.Delay(10000);
                string a = ReturnString();
                Console.WriteLine(a);
                Console.WriteLine("Tread FiniShed");
            });
            Thread.Sleep(1000);
            Console.WriteLine("Stop");
            
        }


    }

    //public class Class
    //{
    //    public string ReturnString()
    //    {
    //        return "It works.";
    //    }

    //    public void Start()
    //    {
    //        Console.WriteLine("Start");

    //        Task.Run(() => {
    //            Console.WriteLine("Tread started!");
    //            //Task.Delay(10000);
    //            string a = ReturnString();
    //            Console.WriteLine(a);
    //            Console.WriteLine("Tread FiniShed");
    //        });

    //        Console.WriteLine("Stop");
    //    }
    //}


}
