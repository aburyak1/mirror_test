using System.Threading.Tasks;
using iikoTransport.SbpService.Contracts.PublicApi;
using iikoTransport.Service;

namespace iikoTransport.SbpService.Services.Interfaces
{
    /// <summary>
    /// Service interface for publicApi.
    /// </summary>
    public interface IPublicApiSbpService
    {
        /// <summary>
        /// Регистрация одноразовой Функциональной ссылки СБП для B2B.
        /// </summary>
        Task<PaymentLinkPayloadResponse> CreateOneTimePaymentLink(Call<CreateOneTimePaymentLinkRequest> call);

        /// <summary>
        /// Запрос содержимого для ранее зарегистрированной Функциональной ссылки СБП.
        /// </summary>
        Task<PaymentLinkPayloadResponse> GetQrcPayload(Call<GetQrcPayloadRequest> call);

        /// <summary>
        /// Запрос статуса Операций СБП по идентификаторам QR.
        /// </summary>
        Task<GetStatusQrcOperationsResponse> GetStatusQrcOperations(Call<GetStatusQrcOperationsRequest> call);

        /// <summary>
        /// Запрос Агента ТСП на возврат по Операции СБП C2B.
        /// </summary>
        Task<CreatedRefundResponse> CreateRefundRequest(Call<CreateRefundRequest> call);

        /// <summary>
        /// Получение идентификатора ОПКЦ СБП запроса на возврат.
        /// </summary>
        Task<CreatedRefundResponse> GetRefundIdRequest(Call<GetRefundIdRequest> call);

        /// <summary>
        /// Статус запроса на возврат средств для Агента ТСП (v2).
        /// </summary>
        Task<GetRefundStatusResponse> GetRefundStatus(Call<GetRefundStatusRequest> call);

        /// <summary>
        /// Изменение счета ЮЛ, ИП или самозанятого для зарегистрированной Кассовой ссылки СБП.
        /// </summary>
        Task<SetNewAccountResponse> SetNewAccount(Call<SetNewAccountRequest> call);

        /// <summary>
        /// Получение списка ТСП, зарегистрированных для ЮЛ или ИП. 
        /// </summary>
        Task<SearchMerchantDataResponse> SearchMerchantData(Call<SearchMerchantDataRequest> call);

        /// <summary>
        /// Запрос данных ТСП.
        /// </summary>
        Task<GetMerchantDataResponse> GetMerchantData(Call<GetMerchantDataRequest> call);
    }
}