using PinkWorld.Common.Models;
using PinkWorld.Common.Request;
using PinkWorld.Common.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PinkWorld.Common.Services
{
    public interface IApiService
    {
        Task<Response> GetListAsync<T>(string urlBase, string servicePrefix, string controller);
        Task<Response> GetTokenAsync(string urlBase, string servicePrefix, string controller, TokenRequest request);

        Task<Response> RecoverPasswordAsync(string urlBase, string servicePrefix, string controller, EmailRequest emailRequest);

        Task<Response> RegisterUserAsync(string urlBase, string servicePrefix, string controller, UserRequest userRequest);


        Task<Response> ModifyUserAsync(string urlBase, string servicePrefix, string controller, UserRequest userRequest, string token);


        Task<Response> ChangePasswordAsync(string urlBase, string servicePrefix, string controller, ChangePasswordRequest changePasswordRequest, string token);

        Task<Response> GetTokenAsync(string urlBase, string servicePrefix, string controller, FacebookProfile request);


        Task<Response> GetListAsync<T>(string urlBase, string servicePrefix, string controller, string token);

        Task<Response> GetListAsync(string urlBase, string servicePrefix, string controller,string token, List<QuestionnaireResponse> Request);
    }
}
