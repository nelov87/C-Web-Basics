using System;
using System.Linq;
using System.Net;
using System.Text;

namespace P_02_Validate_URL
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = Console.ReadLine();
            bool isRunning = true;
            

            while (isRunning)
            {

                var isValid = Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute);

                if (isValid)
                {
                    StringBuilder sb = new StringBuilder();
                    var uri = new Uri(url);
                    var protocol = uri.Scheme;
                    var host = uri.Host;
                    var port = uri.Port;
                    var path = uri.AbsolutePath;
                    var query = uri.Query;
                    var fragment = uri.Fragment;

                    if ((protocol == "http" && port == 80) || (protocol == "https" && port == 443) && path.Length >= 1)
                    {
                        sb.AppendLine($"Protocol: {protocol}");
                        sb.AppendLine($"Host: {host}");
                        sb.AppendLine($"Port: {port}");
                        sb.AppendLine($"Path: {path}");
                        if (!String.IsNullOrEmpty(query))
                        {
                            sb.AppendLine($"Query: {query.Substring(1)}");
                        }
                        if (!String.IsNullOrEmpty(fragment))
                        {
                            sb.AppendLine($"Fragment: {fragment.Substring(1)}");
                        }

                    }
                    else
                    {
                        Console.WriteLine("Invalid URL");
                    }

                    Console.WriteLine(sb.ToString().TrimEnd());
                    
                }
                else
                {
                    Console.WriteLine("Invalid URL");
                }

               


                

                url = Console.ReadLine();
            }
        }
    }
}
