using System;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;

namespace GoodReads.API.Utilities
{
    public interface IGoodReadsAuthenticator
    {
        Task<WebAuthenticationResult> Authenticate(WebAuthenticationOptions webAuthenticationOptions, Uri url, Uri callback);
    }

}
