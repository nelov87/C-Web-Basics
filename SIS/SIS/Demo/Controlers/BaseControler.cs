using SIS.HTTP.Cookies;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses;
using SIS.HTTP.Responses.Contracts;
using SIS.WebServer.Results;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Demo.Controlers
{
    public abstract class BaseControler
    {
        protected IHttpRequest HttpRequest { get; set; }

        protected Dictionary<string, object> ViewData = new Dictionary<string, object>();

        protected bool IsLogedIn()
        {
            return this.HttpRequest.Session.ContainsParameter("username");
        }

        private string ParseTemplate(string viewContent)
        {
            //this.ViewData.Add("HelloMassege", "Ivo");
            foreach (var param in this.ViewData)
            {
                viewContent = viewContent.Replace($"@Model.{param.Key}", param.Value.ToString());
            }

            return viewContent;
        }


        public IHttpResponse View([CallerMemberName] string view = "Home")
        {
            string controlerName = this.GetType().Name.Replace("Controler", "");
            string viewName = view;

            string viewContent = File.ReadAllText("Views/" +  controlerName + "/" + viewName + ".html");

            viewContent = this.ParseTemplate(viewContent);

            HtmlResult htmlResult = new HtmlResult(viewContent, SIS.HTTP.Enums.HttpResponseStatusCode.Ok);

            htmlResult.CookieCollection.AddCookie(new HttpCookie("lang", "en"));

            return htmlResult;
        }

        public IHttpResponse Redirect(string url)
        {
            return new RedirectResult(url);
        }

    }
}
