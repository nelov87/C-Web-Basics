using SIS.HTTP.Common;
using SIS.HTTP.Sessions.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Sessions
{
    public class HttpSession : IHttpSession
    {
        public string Id { get; }

        private Dictionary<string, object> HttpSesionParameters;

        public HttpSession(string id)
        {
            this.Id = id;
            this.HttpSesionParameters = new Dictionary<string, object>();
        }
        public void AddParameter(string name, object parameter)
        {
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));
            CoreValidator.ThrowIfNull(parameter, nameof(parameter));

            this.HttpSesionParameters.Add(name, parameter);
        }

        public void ClearParameters()
        {
            this.HttpSesionParameters.Clear();
        }

        public bool ContainsParameter(string name)
        {
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));

            return this.HttpSesionParameters.ContainsKey(name);
        }

        public object GetParameter(string name)
        {
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));

            if (this.HttpSesionParameters.ContainsKey(name))
            {
                return this.HttpSesionParameters[name];
            }

            return null;
        }
    }
}
