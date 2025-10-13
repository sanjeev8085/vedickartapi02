using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class JwtConfiguration
    {
        public string Section { get; set; } = "AuthenticationSettings:Jwt";
        public string? ValidIssuer { get; set; }
        public string? ValidAudience { get; set; }
        public string? TokenExpiresMinutes { get; set; }
        public string? TokenRememberMeExpiresDays { get; set; }
    }
}
