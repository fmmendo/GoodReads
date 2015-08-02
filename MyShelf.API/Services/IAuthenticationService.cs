using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        Task<IRestResponse> RequestToken();

        Task<IRestResponse> RequestAccessToken();
    }
}
