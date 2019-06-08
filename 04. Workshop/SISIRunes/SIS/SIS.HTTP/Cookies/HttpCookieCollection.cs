using SIS.HTTP.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Cookies
{
    public class HttpCookieCollection : IHttpCookieCollection
    {
        private Dictionary<string, HttpCookie> CookieColection;

        public HttpCookieCollection()
        {
            this.CookieColection = new Dictionary<string, HttpCookie>();
        }

        public void AddCookie(HttpCookie cookie)
        {
            CoreValidator.ThrowIfNull(cookie, nameof(cookie));

            if (!this.ContainsCookie(cookie.Key))
            {
                this.CookieColection.Add(cookie.Key, cookie);
            }

        }

        public bool ContainsCookie(string key)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));

            return this.CookieColection.ContainsKey(key);
        }

        public HttpCookie GetCookie(string key)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));

            return this.CookieColection[key];
        }

        public IEnumerator<HttpCookie> GetEnumerator()
        {
            return this.CookieColection.Values.GetEnumerator();
        }

        public bool HasCookies()
        {
            return this.CookieColection.Count != 0;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var cookie in this.CookieColection.Values)
            {
                sb.Append($"Set-Cookie: {cookie}" + GlobalConstants.HttpNewLine);

            }

            return sb.ToString();

            //return string.Join($"{GlobalConstants.HttpNewLine}",
            //    this.httpHeaders
            //    .Values
            //    .Select(x => x.ToString())
            //    );

            //return string.Join(";", CookieColection.Values);
        }

    }
}
