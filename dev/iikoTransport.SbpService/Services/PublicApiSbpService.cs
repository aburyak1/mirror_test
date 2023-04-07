using System;
using System.Threading.Tasks;
using iikoTransport.Common.Contracts;
using iikoTransport.DictionariesService.Client.Transport;
using iikoTransport.DictionariesService.Contracts.Transport;
using iikoTransport.Logging;
using iikoTransport.PublicApi.Contracts.Internal.Exceptions;
using iikoTransport.SbpService.Contracts.PublicApi;
using iikoTransport.SbpService.Converters.PublicApi;
using iikoTransport.SbpService.Services.Interfaces;
using iikoTransport.SbpService.Services.SbpNspk;
using iikoTransport.SbpService.Storage.Contracts;
using iikoTransport.Service;
using iikoTransport.ServiceClient;
using Nspk = iikoTransport.SbpService.Services.SbpNspk.Contracts.PaymentLinksOperations;

namespace iikoTransport.SbpService.Services
{
    /// <summary>
    /// Service for public api.
    /// </summary>
    public class PublicApiSbpService : IPublicApiSbpService
    {
        private readonly ISbpSettingsStorage settingsStorage;
        private readonly SbpNspkClient sbpClient;
        private readonly IDictionariesServiceClient dictionariesClient;
        private readonly IMethodCallSettingsFactory callSettingsFactory;
        private readonly ILog log;

        public PublicApiSbpService(ISbpSettingsStorage settingsStorage, SbpNspkClient sbpClient, IDictionariesServiceClient dictionariesClient,
            IMethodCallSettingsFactory callSettingsFactory, ILog log)
        {
            this.settingsStorage = settingsStorage ?? throw new ArgumentNullException(nameof(settingsStorage));
            this.sbpClient = sbpClient ?? throw new ArgumentNullException(nameof(sbpClient));
            this.dictionariesClient = dictionariesClient ?? throw new ArgumentNullException(nameof(dictionariesClient));
            this.callSettingsFactory = callSettingsFactory ?? throw new ArgumentNullException(nameof(callSettingsFactory));
            this.log = log ?? throw new ArgumentNullException(nameof(log));
        }

        /// <inheritdoc />
        public async Task<PaymentLinkPayloadResponse> CreateOneTimePaymentLink(Call<CreateOneTimePaymentLinkRequest> call)
        {
            if (call == null) throw new ArgumentNullException(nameof(call));
            var request = call.Payload;

            var tgId = await GetTerminalGroupUocId(request.OrganizationId, request.TerminalGroupId, call.Context);
            var setting = await settingsStorage.Get(tgId, call.Context.CancellationToken);
            
            var requestToSbp = request.Convert(setting, sbpClient.AgentId);
            var response = await sbpClient.CreateAndGetOneTimePaymentLinkPayloadForB2B(call.Context.CorrelationId, requestToSbp, request.MediaType,
                request.Width, request.Height);
            
            return response.Convert();
        }

        /// <inheritdoc />
        public async Task<PaymentLinkPayloadResponse> GetQrcPayload(Call<GetQrcPayloadRequest> call)
        {
            if (call == null) throw new ArgumentNullException(nameof(call));

            var request = call.Payload;
            var response = await sbpClient.GetQRCPayload(call.Context.CorrelationId, request.QrcId, request.MediaType, request.Width, request.Height);
            return response.Convert();
        }

        /// <inheritdoc />
        public async Task<GetStatusQrcOperationsResponse> GetStatusQrcOperations(Call<GetStatusQrcOperationsRequest> call)
        {
            if (call == null) throw new ArgumentNullException(nameof(call));

            var response = await sbpClient.GetStatusQRCOperations(call.Context.CorrelationId,
                new Nspk.GetStatusQRCOperationsRequest(call.Payload.QrcIds));
            return response.Convert();
        }

        /// <inheritdoc />
        public async Task<CreatedRefundResponse> CreateRefundRequest(Call<CreateRefundRequest> call)
        {
            if (call == null) throw new ArgumentNullException(nameof(call));
            var request = call.Payload;

            var tgId = await GetTerminalGroupUocId(request.OrganizationId, request.TerminalGroupId, call.Context);
            var setting = await settingsStorage.Get(tgId, call.Context.CancellationToken);
            
            var requestToSbp = call.Payload.Convert(setting);
            var response = await sbpClient.CreateRefundRequest(call.Context.CorrelationId, request.TrxId, requestToSbp);
            return response.Convert();
        }

        /// <inheritdoc />
        public async Task<CreatedRefundResponse> GetRefundIdRequest(Call<GetRefundIdRequest> call)
        {
            if (call == null) throw new ArgumentNullException(nameof(call));

            var response = await sbpClient.GetRefundIdRequest(call.Context.CorrelationId, call.Payload.TrxId, call.Payload.AgentRefundRequestId);
            return response.Convert();
        }

        /// <inheritdoc />
        public async Task<GetRefundStatusResponse> GetRefundStatus(Call<GetRefundStatusRequest> call)
        {
            if (call == null) throw new ArgumentNullException(nameof(call));

            var response = await sbpClient.RefundRequestStatusV2(call.Context.CorrelationId, call.Payload.OriginalTrxId,
                call.Payload.OpkcRefundRequestId);
            return response.Convert();
        }

        /// <inheritdoc />
        public async Task<SetNewAccountResponse> SetNewAccount(Call<SetNewAccountRequest> call)
        {
            if (call == null) throw new ArgumentNullException(nameof(call));

            var response = await sbpClient.SetNewAccount(call.Context.CorrelationId, call.Payload.QrcId, call.Payload.Account);
            return response.Convert();
        }

        /// <inheritdoc />
        public async Task<SearchMerchantDataResponse> SearchMerchantData(Call<SearchMerchantDataRequest> call)
        {
            if (call == null) throw new ArgumentNullException(nameof(call));

            var response = await sbpClient.SearchMerchantData(call.Context.CorrelationId, call.Payload.Ogrn, call.Payload.Bic);
            return response.Convert();
        }

        private async Task<Guid> GetTerminalGroupUocId(Guid organizationId, Guid terminalGroupId, ICallContext context)
        {
            var requestForTerminalGroupUocId = new GetTerminalGroupByExternalIdRequest(organizationId, terminalGroupId);
            var terminalGroupUocIdResponse = await dictionariesClient.GetTerminalGroupUocIdByExternalId(requestForTerminalGroupUocId,
                callSettingsFactory.CreateFromContext(context, 0.3));
            if (!terminalGroupUocIdResponse.TerminalGroupUocId.HasValue)
                throw new TerminalGroupNotFoundException(context.CorrelationId, terminalGroupId, organizationId);
            return terminalGroupUocIdResponse.TerminalGroupUocId.Value;
        }
    }
}