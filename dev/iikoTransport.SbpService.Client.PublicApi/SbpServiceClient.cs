using System;
using System.Net.Http;
using System.Threading.Tasks;
using iikoTransport.Logging;
using iikoTransport.SbpService.Contracts.PublicApi;
using iikoTransport.ServiceClient;

namespace iikoTransport.SbpService.Client.PublicApi
{
    /// <summary>
    /// Класс для доступа к <see cref="ISbpServiceClient"/>.
    /// </summary>
    public class SbpServiceClient : BaseServiceClient, ISbpServiceClient
    {
        public SbpServiceClient(
            HttpClient httpClient,
            SbpServiceClientOptions options,
            IMethodCallSettingsFactory callSettingsFactory,
            ILog log) 
            : base(httpClient, options, callSettingsFactory, log)
        {
        }

        protected override string ControllerName
        {
            get => "PublicApiSbp";
            set => throw new InvalidOperationException();
        }
        
        public async Task<PaymentLinkPayloadResponse> CreateOneTimePaymentLink(CreateOneTimePaymentLinkRequest request, MethodCallSettings? callSettings)
        {
            return await ExecuteMethodAsync<PaymentLinkPayloadResponse>(nameof(CreateOneTimePaymentLink), request, callSettings);
        }
        
        public async Task<PaymentLinkPayloadResponse> GetQrcPayload(GetQrcPayloadRequest request, MethodCallSettings? callSettings)
        {
            return await ExecuteMethodAsync<PaymentLinkPayloadResponse>(nameof(GetQrcPayload), request, callSettings);
        }
        
        public async Task<GetStatusQrcOperationsResponse> GetStatusQrcOperations(GetStatusQrcOperationsRequest request, MethodCallSettings? callSettings)
        {
            return await ExecuteMethodAsync<GetStatusQrcOperationsResponse>(nameof(GetStatusQrcOperations), request, callSettings);
        }
        
        public async Task<CreatedRefundResponse> CreateRefundRequest(CreateRefundRequest request, MethodCallSettings? callSettings)
        {
            return await ExecuteMethodAsync<CreatedRefundResponse>(nameof(CreateRefundRequest), request, callSettings);
        }
        
        public async Task<CreatedRefundResponse> GetRefundIdRequest(GetRefundIdRequest request, MethodCallSettings? callSettings)
        {
            return await ExecuteMethodAsync<CreatedRefundResponse>(nameof(GetRefundIdRequest), request, callSettings);
        }
        
        public async Task<GetRefundStatusResponse> GetRefundStatus(GetRefundStatusRequest request, MethodCallSettings? callSettings)
        {
            return await ExecuteMethodAsync<GetRefundStatusResponse>(nameof(GetRefundStatus), request, callSettings);
        }
        
        public async Task<SetNewAccountResponse> SetNewAccount(SetNewAccountRequest request, MethodCallSettings? callSettings)
        {
            return await ExecuteMethodAsync<SetNewAccountResponse>(nameof(SetNewAccount), request, callSettings);
        }

        public async Task<SearchMerchantDataResponse> SearchMerchantData(SearchMerchantDataRequest request, MethodCallSettings? callSettings)
        {
            return await ExecuteMethodAsync<SearchMerchantDataResponse>(nameof(SearchMerchantData), request, callSettings);
        }
    }
}