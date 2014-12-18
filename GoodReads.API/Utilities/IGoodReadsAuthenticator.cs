using System;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;

namespace GoodReads.API.Utilities
{
    public interface IGoodReadsAuthenticator
    {
        event EventHandler AuthenticationCompleted;

        Task<WebAuthenticationResult> Authenticate(WebAuthenticationOptions webAuthenticationOptions, Uri url, Uri callback);
    }
}
