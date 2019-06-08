using SIS.HTTP.Cookies;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses;
using SIS.HTTP.Responses.Contracts;
using SIS.WebServer.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Controlers
{
    public class HomeControler : BaseControler
    {
        public HomeControler(IHttpRequest httpRequest)
        {
            this.HttpRequest = httpRequest;
        }

        public IHttpResponse Index(IHttpRequest httpRequest)
        {
            //IHttpResponse newResponse = new HttpResponse();
            //HttpCookie cookie = new HttpCookie("lang", "en");
            //cookie.Delete();

            //newResponse.AddCookie(cookie);

            return this.View();
        }

       public IHttpResponse Home(IHttpRequest httpRequest)
        {
            if (!this.IsLogedIn())
            {
                return this.Redirect("/users/login");
            }

            this.ViewData["User"] = this.HttpRequest.Session.GetParameter("username");

            return this.View();
        }

       



    }
}
