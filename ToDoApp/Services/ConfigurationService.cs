using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ToDoApp.Services
{
    public class Token
    {
        public string Key;
        public string Issuer;
        public string Audience;
    };

    public class ConfigurationService
    {
        private IConfiguration _configuration;
        public ConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Token Token
        {
            get
            {
                return new Token
                {
                    Key = _configuration["Token:Key"],
                    Issuer = _configuration["Token:Issuer"],
                    Audience = _configuration["Token:Audience"]
                };
            }
        }
    }
}
