using SIS.HTTP.Common;
using SIS.HTTP.Cookies;
using SIS.HTTP.Enums;
using SIS.HTTP.Extensions;
using SIS.HTTP.Headers;
using SIS.HTTP.Headers.Contracts;
using SIS.HTTP.Responses.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Responses
{
    public class HttpResponse : IHttpResponse
    {
        public HttpResponse()
        {
            this.Headers = new HttpHeaderCollection();
            this.Content = new byte[0];
            this.CookieCollection = new HttpCookieCollection();
        }

        public HttpResponse(HttpResponseStatusCode statusCode) : this()
        {
            CoreValidator.ThrowIfNull(statusCode, nameof(statusCode));
            this.StatusCode = statusCode;
        }

        public HttpResponseStatusCode StatusCode { get; set; }

        public IHttpHeaderCollection Headers {get;}

        public byte[] Content { get; set; }

        public IHttpCookieCollection CookieCollection { get; }

        public void AddHeader(HttpHeader header)
        {
            this.Headers.AddHeader(header);
        }

        public void AddCookie(HttpCookie cookie)
        {
            CoreValidator.ThrowIfNull(cookie, nameof(cookie));

            this.CookieCollection.AddCookie(cookie);
        }

        public byte[] GetBytes()
        {
            byte[] httpResponseWithoutBody = Encoding.UTF8.GetBytes(this.ToString());

            byte[] httpResponseWithBody = new byte[httpResponseWithoutBody.Length + this.Content.Length];

            int curentIndex = 0;

            for (; curentIndex < httpResponseWithoutBody.Length; curentIndex++)
            {
                httpResponseWithBody[curentIndex] = httpResponseWithoutBody[curentIndex];
            }

            int i = 0;

            for (; curentIndex < httpResponseWithBody.Length; curentIndex++)
            {
                httpResponseWithBody[curentIndex] = this.Content[i];
                i++;
            }

            return httpResponseWithBody;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append($"{GlobalConstants.HttpOneProtocolFragment} {this.StatusCode.GetStatusLine()}")
                .Append(GlobalConstants.HttpNewLine)
                .Append($"{this.Headers} ")
                .Append(GlobalConstants.HttpNewLine);
            

            if (this.CookieCollection.HasCookies())
            {
                
                result.Append($"{this.CookieCollection}").Append(GlobalConstants.HttpNewLine);
            }

            result.Append(GlobalConstants.HttpNewLine);
                

            result.Append(GlobalConstants.HttpNewLine);

            return result.ToString();
        }
    }
}
