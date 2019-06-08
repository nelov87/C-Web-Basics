using IRunes.Data;
using IRunes.Models;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace IRunes.App.Controllers
{
    public class UsersController : Controller
    {
        private string HashPassword(string password)
        {
            using (SHA256 sha256hash = SHA256.Create())
            {
                 return Encoding.UTF8.GetString(sha256hash.ComputeHash(Encoding.UTF8.GetBytes(password)));

            }
        }

        public IHttpResponse Login(IHttpRequest httpRequest)
        {
            return this.View();
        }

        public IHttpResponse LoginConfirm(IHttpRequest httpRequest)
        {
            using (var context = new RunesDbContext())
            {
                string username = httpRequest.FormData["username"].ToString();
                string password = httpRequest.FormData["password"].ToString();

                User userFromDb = context
                    .Users
                    .FirstOrDefault(user => (user.Username == username || user.Email == username) && user.Password == this.HashPassword(password));
                if (userFromDb == null)
                {
                    return this.Redirect("/Users/Login");
                }

                this.SignIn(httpRequest, userFromDb.Id, userFromDb.Username, userFromDb.Email);

            }

            return this.Redirect("/");
        }

        public IHttpResponse Register(IHttpRequest httpRequest)
        {
            return this.View();
        }

        public IHttpResponse RegisterConfirm(IHttpRequest httpRequest)
        {
            using (var context = new RunesDbContext())
            {
                string username = httpRequest.FormData["username"].ToString();
                string password = httpRequest.FormData["password"].ToString();
                string passwordConfirm = httpRequest.FormData["password"].ToString();
                string email = httpRequest.FormData["email"].ToString();

                if (password != passwordConfirm)
                {
                    return this.Redirect("/Users/Register");
                }

                User user = new User()
                {
                    Username = username,
                    Password = this.HashPassword(password),
                    Email = email
                };

                context.Users.Add(user);
                context.SaveChanges();

                return this.Redirect("/Users/Login");
                
            }
        }

        public IHttpResponse Logout(IHttpRequest httpRequest)
        {
            this.SignOut(httpRequest);

            return this.Redirect("/");
        }

    }
}
