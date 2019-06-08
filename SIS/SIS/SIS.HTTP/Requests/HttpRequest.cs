using SIS.HTTP.Common;
using SIS.HTTP.Enums;
using SIS.HTTP.Headers;
using SIS.HTTP.Headers.Contracts;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SIS.HTTP.Cookies;
using SIS.HTTP.Sessions;
using SIS.HTTP.Sessions.Contracts;

namespace SIS.HTTP.Requests
{
    public class HttpRequest : IHttpRequest
    {
        public string Path { get; private set;}

        public string Url { get; private set; }

        public Dictionary<string, object> FormData { get; }

        public Dictionary<string, object> QueryData { get; }

        public IHttpHeaderCollection Headers { get; }

        public HttpRequestMethod RequestMethod { get; private set; }

        public IHttpCookieCollection CookieCollection { get; }

        public IHttpSession Session { get; set; }

        public HttpRequest(string requestString)
        {
            CoreValidator.ThrowIfNullOrEmpty(requestString, nameof(requestString));

            this.FormData = new Dictionary<string, object>();
            this.QueryData = new Dictionary<string, object>();
            this.Headers = new HttpHeaderCollection();
            this.CookieCollection = new HttpCookieCollection();

            this.ParseRequest(requestString);
        }

        private bool IsValidRequestLine(string[] requestLine)
        {
            if (requestLine.Length == 3)
            {
                if (requestLine[2] == GlobalConstants.HttpOneProtocolFragment)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        private bool IsValidRequestQueryString(string queryString, string[] queryParameters)
        {
            return String.IsNullOrEmpty(queryString) && queryParameters.Length >= 1;
            
        }

        private void ParseRequestMethod(string[] requestLine)
        {
            HttpRequestMethod method;

            var parsResult = Enum.TryParse(requestLine[0], true, out method);

            if (!parsResult)
            {
                throw new BadRequestException(string.Format(GlobalConstants.UnsuportedHttpMethodExceptionMessage, requestLine[0]));

            }

            this.RequestMethod = method;

        }

        private void ParseRequestUrl(string[] requestLine)
        {
            this.Url = requestLine[1];
        }

        private void ParseRequestPath()
        {
            this.Path = this.Url.Split('?')[0];
        }

        private IEnumerable<string> ParsPlainRerquestHeaders(string[] requestLines)
        {
            for (int i = 1; i < requestLines.Length - 1; i++)
            {
                if (!string.IsNullOrEmpty(requestLines[i]))
                {
                    yield return requestLines[i];
                }
            }
        }

        private void ParseRequestHeaders(string[] requestContent)
        {
            foreach (var item in requestContent)
            {
                var keyValuPair = item.Split(": ", StringSplitOptions.RemoveEmptyEntries).ToArray();
                HttpHeader header = new HttpHeader(keyValuPair[0], keyValuPair[1]);
                this.Headers.AddHeader(header);
            }
        }

        private void ParseCookies()
        {
            if (this.Headers.ContainsHeader(HttpHeader.Cookie))
            {
                string value = this.Headers.GetHeader(HttpHeader.Cookie).Value;
                string[] unparsedCookies = value.Split("; ", StringSplitOptions.RemoveEmptyEntries);

                foreach (var unparsedCookie in unparsedCookies)
                {
                    string[] cookieKeyValue = unparsedCookie.Split("=").ToArray();

                    HttpCookie httpCookie = new HttpCookie(cookieKeyValue[0], cookieKeyValue[1]);

                    this.CookieCollection.AddCookie(httpCookie);
                }

            }
        }

        private bool HasQueryString()
        {
            return this.Url.Split('?').Length > 1;
        }

        private void ParseRequestQueryParameters()
        {
            if (this.HasQueryString())
            {
                string[] parameters = this.Url.Split('?')[1].Split('&').ToArray();

                foreach (var parameter in parameters)
                {
                    var keyValuPair = parameter.Split(new char[] { '=', '#' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                    this.QueryData.Add(keyValuPair[0], keyValuPair[1]);
                }
            }
        }

        private bool HasFormDataParameters(string requestBody)
        {
            return requestBody.Split('&').Length > 1;
        }

        private void ParseRequestFormDataParameters(string requestBody)
        {
            if (this.HasFormDataParameters(requestBody))
            {
                string[] parameters = requestBody.Split('&').ToArray();

                foreach (var parameter in parameters)
                {
                    var keyValuPair = parameter.Split('=').ToArray();
                    this.FormData.Add(keyValuPair[0], keyValuPair[1]);
                }
            }
        }

        

        private void ParseRequestParameters(string requestBody)
        {
            this.ParseRequestQueryParameters();
            this.ParseRequestFormDataParameters(requestBody);
        }

        private void ParseRequest(string requestString)
        {
            string[] splitRequestContent = requestString
                .Split(new[] { GlobalConstants.HttpNewLine }, StringSplitOptions.None);

            string[] requestLine = splitRequestContent[0]
                .Trim()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (!this.IsValidRequestLine(requestLine))
            {
                throw new BadRequestException();
            }

            this.ParseRequestMethod(requestLine);
            this.ParseRequestUrl(requestLine);
            this.ParseRequestPath();

            this.ParseRequestHeaders(this.ParsPlainRerquestHeaders(splitRequestContent).ToArray());
            this.ParseCookies();
            this.ParseRequestParameters(splitRequestContent[splitRequestContent.Length - 1]);
        }
    }
}
