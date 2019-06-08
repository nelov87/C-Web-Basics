using IRunes.Models;
using SIS.HTTP.Cookies;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;
using SIS.WebServer.Results;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace IRunes.App.Controllers
{
    public abstract class BaseController
    {
        //protected IHttpRequest HttpRequest { get; set; }
        protected BaseController()
        {
            this.ViewData = new Dictionary<string, object>();
        }

        protected Dictionary<string, object> ViewData;


        private string ParseTemplate(string viewContent)
        {
            //this.ViewData.Add("HelloMassege", "Ivo");
            foreach (var param in this.ViewData)
            {
                viewContent = viewContent.Replace($"@Model.{param.Key}", param.Value.ToString());
            }

            return viewContent;
        }

        protected void SignIn(IHttpRequest httpRequest, User user)
        {
            httpRequest.Session.AddParameter("id", user.Id);
            httpRequest.Session.AddParameter("username", user.Username);
            httpRequest.Session.AddParameter("email", user.Email);
        }

        protected void SignOut(IHttpRequest httpRequest)
        {
            httpRequest.Session.ClearParameters();
        }


        protected bool IsLogedIn(IHttpRequest httpRequest)
        {
            return httpRequest.Session.ContainsParameter("username");
        }

        protected IHttpResponse View([CallerMemberName] string view = "Home")
        {
            string controlerName = this.GetType().Name.Replace("Controler", "");
            string viewName = view;

            string viewContent = File.ReadAllText("Views/" + controlerName.Replace("Controller", "") + "/" + viewName + ".html");

            viewContent = this.ParseTemplate(viewContent);

            HtmlResult htmlResult = new HtmlResult(viewContent, SIS.HTTP.Enums.HttpResponseStatusCode.Ok);

            htmlResult.CookieCollection.AddCookie(new HttpCookie("lang", "en"));

            return htmlResult;
        }

        protected IHttpResponse Redirect(string url)
        {
            return new RedirectResult(url);
        }

        //protected IHttpResponse View([CallerMemberName] string viewName = null)
        //{

        //}
    }
}
