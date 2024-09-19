using PB201MovieApp.MVC.ApiResponseMessages;
using PB201MovieApp.MVC.Services.Interfaces;
using PB201MovieApp.MVC.UIExceptions.Common;
using RestSharp;

namespace PB201MovieApp.MVC.Services.Implementations
{
    public class CrudService : ICrudService
    {
        private readonly RestClient _restClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CrudService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _restClient = new RestClient(_configuration.GetSection("API:Base_Url").Value);
            var token = _httpContextAccessor.HttpContext.Request.Cookies["token"];

            if(token != null)
            {
                _restClient.AddDefaultHeader("Authorization", "Bearer " + token);
            }
        }

        public async Task Create<T>(string endpoint, T entity) where T : class
        {
            var request = new RestRequest(endpoint, Method.Post);
            request.AddJsonBody(entity);

            var response = await _restClient.ExecuteAsync<ApiResponseMessage<T>>(request);
            if (!response.IsSuccessful) throw new Exception();
        }

        public async Task Delete<T>(string endpoint, int id)
        {
            var request = new RestRequest(endpoint,Method.Delete);

            var response = await _restClient.ExecuteAsync<ApiResponseMessage<T>>(request);
            if (!response.IsSuccessful) throw new Exception();
        }

        public async Task<T> GetAllAsync<T>(string endpoint)
        {
            var request = new RestRequest(endpoint,Method.Get);
            var response = await _restClient.ExecuteAsync<ApiResponseMessage<T>>(request);

            if (!response.IsSuccessful)
            {
                throw new Exception();
            }

            return response.Data.Data;
        }

        public async Task<T> GetByIdAsync<T>(string endpoint, int id)
        {
            if (id < 1) throw new Exception();
            var request = new RestRequest(endpoint, Method.Get);
            var response = await _restClient.ExecuteAsync<ApiResponseMessage<T>>(request);

            if (!response.IsSuccessful)
            {
                if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    throw new BadrequestException(response.Data.ErrorMessage);
                }
                else if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new ModelNotFoundException(response.Data.ErrorMessage);
                }
            }

            return response.Data.Data;
        }

        public async Task Update<T>(string endpoint, T entity) where T : class
        {
            var request = new RestRequest(endpoint, Method.Put);
            request.AddJsonBody(entity);

            var response = await _restClient.ExecuteAsync<ApiResponseMessage<T>>(request);

            if (!response.IsSuccessful)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest && response.Data.PropertyName is not null)
                {
                    throw new ModelStateException(response.Data.PropertyName, response.Data.ErrorMessage);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    throw new BadrequestException(response.Data.ErrorMessage);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new ModelNotFoundException(response.Data.ErrorMessage);
                }
            }
        }
    }
}
