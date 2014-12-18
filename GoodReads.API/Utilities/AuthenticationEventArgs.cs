using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;

namespace GoodReads.API.Utilities
{
    public class AuthenticationEventArgs : EventArgs
    {
        public WebAuthenticationResult Result { get; private set; }

        public AuthenticationEventArgs(WebAuthenticationResult result)
        {
            Result = result;
        }
    }
}
