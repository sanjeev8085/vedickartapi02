//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Entities.Models
//{
//    public class Settings
//    {
//        //System Constants
//        public const string DefaultSystemUser = "System";
//        public static readonly DateTime DefaultSeedDateTime = new(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
//        public const string DefaultWebUrl = "https://localhost";
//        public static string SecretKey = "TDhlQHpUIjlORiMydlIlc1gzdUNecEIka1lnSipRTQ==";
//        public const string SecretNotFoundMessage = "Secret not found for authentication encryption";

//    }
//}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Settings
    {
        //System Constants
        public const string DefaultSystemUser = "System";
        public static readonly DateTime DefaultSeedDateTime = new(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public const string DefaultWebUrl = "https://localhost";

        // JWT Settings
        public static string SecretKey = "TDhlQHpUIjlORiMydlIlc1gzdUNecEIka1lnSipRTQ==";
        public const string ValidIssuer = "YourAppName";
        public const string ValidAudience = "YourAppUsers";
        public const int ExpiryMinutes = 60; // 1 hour

        // Error Messages
        public const string SecretNotFoundMessage = "Secret not found for authentication encryption";
    }
}