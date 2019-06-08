using SIS.HTTP.Responses.Contracts;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace IRunes.App.Controllers
{
    public abstract class BaseController
    {
        protected IHttpResponse View([CallerMemberName] string viewName = null)
        {

        }
    }
}
