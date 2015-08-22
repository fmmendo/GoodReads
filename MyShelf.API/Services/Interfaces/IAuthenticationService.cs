using RestSharp;
using System;
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
        event EventHandler<AuthState> AuthStateChanged;

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
