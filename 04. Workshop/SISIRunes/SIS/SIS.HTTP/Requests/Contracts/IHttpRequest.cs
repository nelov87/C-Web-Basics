using SIS.HTTP.Cookies;
using SIS.HTTP.Enums;
using SIS.HTTP.Headers.Contracts;
using SIS.HTTP.Sessions.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Requests.Contracts
{
    public interface IHttpRequest
    {
        string Path { get; }

        string Url { get; }

        Dictionary<string, object> FormData { get; }

        Dictionary<string, object> QueryData { get; }

        IHttpHeaderCollection Headers { get; }

        HttpRequestMethod RequestMethod { get; }

        IHttpCookieCollection CookieCollection { get; }

        IHttpSession Session { get; set; }
    }
}
