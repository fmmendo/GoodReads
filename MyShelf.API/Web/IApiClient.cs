using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShelf.API.Web
{
    public interface IApiClient
    {
        IAuthenticator Authenticator { get; set; }

        Task<IRestResponse> ExecuteAsync(string url, Method method);
        //OAuth1Authenticator GetRequestTokenAuthenticator(string consumerKey, string consumerSecret);
    }
}
