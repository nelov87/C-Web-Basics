using Demo.Data;
using Demo.Models;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.Controlers
{
    public class UsersControler : BaseControler
    {
        public IHttpResponse Login(IHttpRequest request)
        {
            return this.View();
        }

        public IHttpResponse LoginConfirm(IHttpRequest httpRequest)
        {
            using (var context = new DemoDbContext())
            {
                string username = httpRequest.FormData["username"].ToString();
                string password = httpRequest.FormData["password"].ToString();

                User userFromDb = context.Users.SingleOrDefault(user => user.Username == username && user.Password == password);

                if (userFromDb == null)
                {
                    return this.Redirect("/users/login");
                }

                httpRequest.Session.AddParameter("username", userFromDb.Username);
                
            }

            return this.Redirect("/home");
        }

        public IHttpResponse Register(IHttpRequest request)
        {
            return this.View();
        }

        public IHttpResponse RegisterConfirm(IHttpRequest httpRequest)
        {
            using (var context = new DemoDbContext())
            {
                string username = httpRequest.FormData["username"].ToString();
                string password = httpRequest.FormData["password"].ToString();
                string confurmPassword = httpRequest.FormData["confirmPassword"].ToString();

                if (password != confurmPassword)
                {
                    return this.Redirect("/registet");
                }

                User user = new User
                {
                    Username = username,
                    Password = password
                };

                context.Add(user);
                context.SaveChanges();
            }

            return this.Redirect("/users/login");
        }

        public IHttpResponse Logout(IHttpRequest request)
        {
            request.Session.ClearParameters();

            return this.Redirect("/");

        }
    }
}
