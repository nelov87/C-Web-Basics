using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRunes.App.Controllers
{
    public class InfoController : Controller
    {
        public IHttpResponse About(IHttpRequest httpRequest)
        {
            return this.View();
        }
    }
    
}
