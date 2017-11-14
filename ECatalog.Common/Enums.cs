using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JScript;

namespace ECatalog.Common
{
    public class Enums
    {
        public enum RoleType
        {
            GlobalAdmin,
            RestaurantAdmin,
            Waiter
        }

        public enum SupportedLanguage
        {
            areg,
            enus,
            engb
        }
    }
}
