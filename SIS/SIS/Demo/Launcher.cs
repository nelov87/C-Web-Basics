using Demo.Controlers;
using Demo.Data;
using SIS.HTTP.Enums;
using SIS.WebServer;
using SIS.WebServer.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo
{
    public class Launcher
    {
        public static void Main(string[] args)
        {
            using (var context = new DemoDbContext())
            {
                context.Database.EnsureCreated();
            }

                IServerRoutingTable serverRoutingTable = new ServerRoutingTable();

            serverRoutingTable.Add(HttpRequestMethod.Get, "/", request => new HomeControler(request).Index(request));
            serverRoutingTable.Add(HttpRequestMethod.Get, "/users/login", request => new UsersControler().Login(request));
            serverRoutingTable.Add(HttpRequestMethod.Get, "/users/register", request => new UsersControler().Register(request));
            serverRoutingTable.Add(HttpRequestMethod.Get, "/users/logout", request => new UsersControler().Logout(request));

            serverRoutingTable.Add(HttpRequestMethod.Get, "/home", request => new HomeControler(request).Home(request));



            serverRoutingTable.Add(HttpRequestMethod.Post, "/users/login", request => new UsersControler().LoginConfirm(request));
            serverRoutingTable.Add(HttpRequestMethod.Post, "/users/register", request => new UsersControler().RegisterConfirm(request));

            Server server = new Server(8000, serverRoutingTable);

            server.Run();
        }
    }
}
