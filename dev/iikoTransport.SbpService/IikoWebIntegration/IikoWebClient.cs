using System;
using System.Net.Http;
using System.Threading.Tasks;
using iikoTransport.EmployeesService.IikoWebIntegration;
using iikoTransport.Logging;
using iikoTransport.SbpService.IikoWebIntegration.Contracts;
using iikoTransport.ServiceClient;

namespace iikoTransport.SbpService.IikoWebIntegration
{
    /// <summary>
    /// IikoWeb internal api client.
    /// </summary>
    public class IikoWebClient : BaseServiceClient, IIikoWebClient
    {
        public IikoWebClient(
            HttpClient httpClient,
            IikoWebClientOptions options,
            IMethodCallSettingsFactory callSettingsFactory,
            ILog log) 
            : base(httpClient, options, callSettingsFactory, log)
        {
        }

        protected override string ControllerName
        {
            get => string.Empty;
            set => throw new InvalidOperationException();
        }
        
        protected override bool IsExternalClient => true;
        
        public async Task<SbpSettingsChanges> GetSbpSettingsChanges(GetSbpSettingsChangesRequest request, MethodCallSettings? callSettings)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            
            var methodUri = $"{BaseUri}/api/sbp/settings";
            // todo: Метод на стороне веба ещё не реализован
            var response = await ExecuteMethodByFullUriAsync<GetSbpSettingsChangesResponse>(methodUri, request, callSettings, HttpMethod.Post);
            if (response == null)
                throw new InvalidOperationException("IikoWeb response is null (may be it is empty or contract mismatch?). " +
                                                    "See previous logs for details.");

            return response.Data;
        }
    }
}