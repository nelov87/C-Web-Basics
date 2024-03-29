﻿using SIS.HTTP.Enums;
using SIS.HTTP.Headers;
using SIS.HTTP.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.WebServer.Results
{
    public class TextResult : HttpResponse
    {
        public TextResult(string content, HttpResponseStatusCode responseStatusCode, string contentType = "text/plain; charset=utf8")
            :base(responseStatusCode)
        {
            this.Headers.AddHeader(new HttpHeader("Content-Type", contentType));
            this.Content = Encoding.UTF8.GetBytes(content);
        }

        public TextResult(byte[] content, HttpResponseStatusCode responseStatusCode, string contentType = "text/plain; charset=utf8")
           : base(responseStatusCode)
        {
            this.Headers.AddHeader(new HttpHeader("Content-Type", contentType));
            this.Content = content;
        }
    }
}
