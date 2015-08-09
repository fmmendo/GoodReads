using RestSharp;
using System.Threading.Tasks;

namespace MyShelf.API.Services
{
    public enum AuthState
    {
        NotAuthenticated,
        Authenticating,
        Authenticated
    };

    public interface IAuthenticationService
    {
        bool IsTokenAvailable
        {
            get;
        }

        bool IsTokenSecretAvailable
        {
            get;
        }

        AuthState State
        {
            get;
        }

        Task<bool> Authenticate();

        Task<IRestResponse> RequestToken();

        Task<IRestResponse> RequestAccessToken();
    }
}
