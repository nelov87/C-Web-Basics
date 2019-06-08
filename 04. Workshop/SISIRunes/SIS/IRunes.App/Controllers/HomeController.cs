﻿using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRunes.App.Controllers
{
    public class HomeController : BaseController
    {
        public IHttpResponse Index(IHttpRequest httpRequest)
        {
            if (this.IsLogedIn(httpRequest))
            {
                this.ViewData["Username"] = httpRequest.Session.GetParameter("username");
                return this.View("Home");
            }

            return this.View();
        }

    }
}
