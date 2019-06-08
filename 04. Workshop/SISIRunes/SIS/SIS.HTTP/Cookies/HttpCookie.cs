using SIS.HTTP.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Cookies
{
    public class HttpCookie
    {
        private const int HttpCookieDefaulExpirationDate = 3;

        private const string HttpCookieDefaultPath = "/";

        public HttpCookie(string key, string value, int expires = HttpCookieDefaulExpirationDate, string path = HttpCookieDefaultPath)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));

            Key = key;
            Value = value;
            Expires = DateTime.UtcNow.AddDays(3);
            Path = path;
            IsNew = true;
        }
        public HttpCookie(string key, string value, bool isNew, int expires = HttpCookieDefaulExpirationDate, string path = HttpCookieDefaultPath)
            :this(key, value, expires, path)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));

            Key = key;
            Value = value;
            Expires = DateTime.UtcNow.AddDays(3);
            Path = path;
            IsNew = true;
        }

        public string Key { get; }

        public string Value { get; }

        public DateTime Expires { get; private set; }

        public string Path { get; set; }

        public bool IsNew { get; }

        public bool HttpOnly { get; } = true;

        public void Delete()
        {
            this.Expires = DateTime.UtcNow.AddDays(-1);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"{this.Key}={this.Value}; Expires={this.Expires:R}");

            if (this.HttpOnly)
            {
                sb.Append("; HttpOnly");
            }

            sb.Append($"; Path={this.Path}");

            return sb.ToString().TrimEnd();
        }
    }
}
