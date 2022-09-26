using Common.Enum;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Common.Communication
{
    public class HttpCaller<T, S> : IDisposable
    {
        #region Delcarations
        private readonly HttpClient _httpClient;
        #endregion
        #region Constructors
        public HttpCaller(string BaseUrl)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            _httpClient = new HttpClient(clientHandler);
            _httpClient.BaseAddress = new Uri(BaseUrl);
        }
        #endregion
        #region Private Methods
        private void BindHeaders(Dictionary<string, string> RequiredHeaders)
        {
            foreach (var header in RequiredHeaders)
            {
                if (header.Key.Equals("Bearer"))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(header.Key, header.Value);
                    continue;
                }
                if (header.Key.Equals("Content-Type"))
                {
                    _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(header.Value));
                    continue;
                }
                _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }
        #endregion
        #region Core
        public string GenerateQueryString(string TargetEndpoint, KeyValuePair<string, string>[] _params)
        {
            string queryString = string.Format("{0}?", TargetEndpoint);
            for (int i = 0; i < _params.Length; i++)
            {
                queryString += string.Format("{0}={1}", _params[i].Key, _params[i].Value);
                if (i != _params.Length - 1)
                {
                    queryString += "&";
                }
            }
            return queryString;
        }

        public async Task<T> HttpCallAsync(string targetEndpoint, Dictionary<string, string> requiredHeaders, HttpVerbs callMethod, S payLoad)
        {
            try
            {
                var responseMessage = new HttpResponseMessage();
                string serializedPayload = JsonConvert.SerializeObject(payLoad);
                StringContent requestPayload = null;
                string response = string.Empty;
                dynamic responseObject = default;
                #region Staging
                this.BindHeaders(requiredHeaders);
                #endregion
                #region Processing Request
                switch (callMethod)
                {
                    case HttpVerbs.Get:
                        responseMessage = await _httpClient.GetAsync(targetEndpoint);
                        break;
                    case HttpVerbs.Post:
                        requestPayload = new StringContent(serializedPayload, Encoding.UTF8, "application/json");
                        responseMessage = await _httpClient.PostAsync(targetEndpoint, requestPayload);
                        break;
                    case HttpVerbs.Put:
                        requestPayload = new StringContent(serializedPayload, Encoding.UTF8, "application/json");
                        responseMessage = await _httpClient.PutAsync(targetEndpoint, requestPayload);
                        break;
                    case HttpVerbs.Delete:
                        responseMessage = await _httpClient.DeleteAsync(targetEndpoint);
                        break;
                    default:
                        break;
                }
                #endregion
                #region Parsing Response
                switch (responseMessage.StatusCode)
                {
                    case HttpStatusCode.OK:
                        response = await responseMessage.Content.ReadAsStringAsync();
                        if (!string.IsNullOrEmpty(response))
                        {
                            responseObject = JsonConvert.DeserializeObject<T>(response);
                        }
                        break;
                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.Unauthorized:
                    case HttpStatusCode.Forbidden:
                    case HttpStatusCode.NotFound:
                        responseObject = JsonConvert.DeserializeObject(response);
                        break;
                    default:
                        responseObject = null;
                        break;
                }
                #endregion

                return responseObject;
            }
            catch (WebException ex)
            {
                throw;
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                throw;
            }
            catch (TaskCanceledException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
        #region Implementation

        public void Dispose()
        {
            GC.Collect();
        }
        #endregion
    }
}
