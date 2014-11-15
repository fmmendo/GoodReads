using System;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;

namespace GoodReads.API.Utilities
{
    public class GoodReadsAuthenticator : IGoodReadsAuthenticator
    {
        public async Task<WebAuthenticationResult> Authenticate(WebAuthenticationOptions webAuthenticationOptions, Uri url, Uri callback)
        {
            return await WebAuthenticationBroker.AuthenticateAsync(webAuthenticationOptions, url, callback);
        }
    }
}
