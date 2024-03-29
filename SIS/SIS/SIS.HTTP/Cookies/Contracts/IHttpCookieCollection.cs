﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Cookies
{
    public interface IHttpCookieCollection : IEnumerable<HttpCookie>
    {
        void AddCookie(HttpCookie cookie);

        bool ContainsKey(string key);

        HttpCookie GetCookie(string key);

        bool HasCookies();
    }
}
