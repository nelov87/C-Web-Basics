using System;
using System.Net;

namespace P01_URL_Decode
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = Console.ReadLine();
            bool isRunning = true;

            while (isRunning)
            {
               // var isValid = Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute);

                string result = WebUtility.UrlDecode(url);



                Console.WriteLine(result);

                url = Console.ReadLine();
            }


            
        }
    }
}
