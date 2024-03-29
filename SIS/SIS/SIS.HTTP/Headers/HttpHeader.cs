﻿using SIS.HTTP.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Headers
{
    public class HttpHeader
    {
        public string Key { get;}

        public string Value { get;}

        public const string Location = "Location";

        public const string ContentType = "Content-Type";

        public const string Cookie = "Cookie";

        public HttpHeader(string key, string value)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));

            this.Key = key;
            this.Value = value;
        }

        public override string ToString()
        {
            return $"{this.Key}: {this.Value}";
        }
    }
}
