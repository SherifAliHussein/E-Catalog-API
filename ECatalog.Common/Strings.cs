using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECatalog.Common
{
    public class Strings
    {

        public const string JWT = "JWT";
        public const string userID = "UserID";
        public const string userName = "Name";
        public const string userRole = "Role";


        public static readonly List<string> SupportedLanguages = new List<string> { "ar-eg",  "en-US" };
        public const string DefaultLanguage = "en-US";

    }
}
