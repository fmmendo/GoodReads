using GoodReads.Utilities;
using System;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;

namespace GoodReads.API.Utilities
{
    public class GoodReadsAuthenticator : IGoodReadsAuthenticator, IWebAuthenticationContinuable
    {
        public async Task<WebAuthenticationResult> Authenticate(WebAuthenticationOptions webAuthenticationOptions, Uri url, Uri callback)
        {
            WebAuthenticationBroker.AuthenticateAndContinue(url, callback, null, webAuthenticationOptions);

            return null;
        }

        public void ContinueWebAuthentication(Windows.ApplicationModel.Activation.WebAuthenticationBrokerContinuationEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
