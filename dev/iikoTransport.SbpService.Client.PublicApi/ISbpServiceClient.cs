using System.Threading.Tasks;
using iikoTransport.SbpService.Contracts.PublicApi;
using iikoTransport.ServiceClient;

namespace iikoTransport.SbpService.Client.PublicApi
{
    /// <summary>
    /// Клиентский интерфейс для PublicApi. Предоставляет клиента для вызова методов SbpService.
    /// </summary>
    public interface ISbpServiceClient
    {
        /// <summary>
        /// Регистрация одноразовой Функциональной ссылки СБП для B2B.
        /// </summary>
        Task<PaymentLinkPayloadResponse> CreateOneTimePaymentLink(CreateOneTimePaymentLinkRequest request, MethodCallSettings? callSettings);

        /// <summary>
        /// Запрос содержимого для ранее зарегистрированной Функциональной ссылки СБП.
        /// </summary>
        Task<PaymentLinkPayloadResponse> GetQrcPayload(GetQrcPayloadRequest request, MethodCallSettings? callSettings);

        /// <summary>
        /// Запрос статуса Операций СБП по идентификаторам QR Dynamic. 
        /// </summary>
        Task<GetStatusQrcOperationsResponse> GetStatusQrcOperations(GetStatusQrcOperationsRequest request, MethodCallSettings? callSettings);

        /// <summary>
        /// Запрос Агента ТСП на возврат по Операции СБП.
        /// </summary>
        Task<CreatedRefundResponse> CreateRefundRequest(CreateRefundRequest request, MethodCallSettings? callSettings);

        /// <summary>
        /// Получение идентификатора ОПКЦ СБП запроса на возврат.
        /// </summary>
        Task<CreatedRefundResponse> GetRefundIdRequest(GetRefundIdRequest request, MethodCallSettings? callSettings);

        /// <summary>
        /// Статус запроса на возврат средств для Агента ТСП.
        /// </summary>
        Task<GetRefundStatusResponse> GetRefundStatus(GetRefundStatusRequest request, MethodCallSettings? callSettings);

        /// <summary>
        /// Изменение счета ЮЛ, ИП или самозанятого для зарегистрированной Кассовой ссылки СБП.
        /// </summary>
        Task<SetNewAccountResponse> SetNewAccount(SetNewAccountRequest request, MethodCallSettings? callSettings);

        /// <summary>
        /// Получение списка ТСП, зарегистрированных для ЮЛ или ИП. 
        /// </summary>
        Task<SearchMerchantDataResponse> SearchMerchantData(SearchMerchantDataRequest request, MethodCallSettings? callSettings);
    }
}