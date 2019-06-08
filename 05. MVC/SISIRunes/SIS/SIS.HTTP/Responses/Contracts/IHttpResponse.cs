using SIS.HTTP.Cookies;
using SIS.HTTP.Enums;
using SIS.HTTP.Headers;
using SIS.HTTP.Headers.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Responses.Contracts
{
    public interface IHttpResponse
    {
        HttpResponseStatusCode StatusCode { get; set; }

        IHttpHeaderCollection Headers { get;}

        IHttpCookieCollection CookieCollection { get; }

        void AddCookie(HttpCookie cookie);

        byte[] Content { get; set; }

        void AddHeader(HttpHeader header);

        byte[] GetBytes();
    }
}
