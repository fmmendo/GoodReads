using GoodReads.Utilities;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;

namespace GoodReads.API.Utilities
{
    public class GoodReadsAuthenticator : IGoodReadsAuthenticator
    {
        public event EventHandler AuthenticationCompleted;

        public async Task<WebAuthenticationResult> Authenticate(WebAuthenticationOptions webAuthenticationOptions, Uri url, Uri callback)
        {
            WebAuthenticationBroker.AuthenticateAndContinue(url, callback, null, webAuthenticationOptions);

            return null;
        }

    }
}
