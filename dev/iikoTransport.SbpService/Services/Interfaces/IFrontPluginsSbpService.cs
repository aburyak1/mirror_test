using System.Threading.Tasks;
using iikoTransport.SbpService.Contracts.FrontPlugin.FromFront;
using iikoTransport.Service;

namespace iikoTransport.SbpService.Services.Interfaces
{
    /// <summary>
    /// Service interface for iikoFront plugins.
    /// </summary>
    public interface IFrontPluginsSbpService
    {
        /// <summary>
        /// Регистрация одноразовой Функциональной ссылки СБП для B2B.
        /// </summary>
        Task<PaymentLinkPayloadResponse> CreateOneTimePaymentLink(Call<CreateOneTimePaymentLinkRequest> call);

        /// <summary>
        /// Регистрация многоразовой Функциональной ссылки СБП для B2B.
        /// </summary>
        Task<PaymentLinkPayloadResponse> CreateReusablePaymentLink(Call<CreateReusablePaymentLinkRequest> call);

        /// <summary>
        /// Запрос содержимого для ранее зарегистрированной Функциональной ссылки СБП.
        /// </summary>
        Task<PaymentLinkPayloadResponse> GetQrcPayload(Call<GetQrcPayloadRequest> call);

        /// <summary>
        /// Запрос статуса Операций СБП по идентификаторам QR.
        /// </summary>
        Task<GetStatusQrcOperationsResponse> GetStatusQrcOperations(Call<GetStatusQrcOperationsRequest> call);

        /// <summary>
        /// Получение идентификаторов для многоразовых ссылок СБП.
        /// </summary>
        Task<CreateQrcIdReservationResponse> CreateQrcIdReservation(Call<CreateQrcIdReservationRequest> call);

        /// <summary>
        /// Регистрация Кассовой ссылки СБП.
        /// </summary>
        Task<PaymentLinkPayloadResponse> СreateCashRegisterQr(Call<CreateCashRegisterQrRequest> call);

        /// <summary>
        /// Активация Кассовой ссылки СБП.
        /// </summary>
        Task<ActivateCashRegisterQrResponse> ActivateCashRegisterQr(Call<ActivateCashRegisterQrRequest> call);

        /// <summary>
        /// Деактивация Кассовой ссылки СБП.
        /// </summary>
        Task<DeactivateCashRegisterQrResponse> DeactivateCashRegisterQr(Call<DeactivateCashRegisterQrRequest> call);

        /// <summary>
        /// Запрос статуса Кассовой ссылки СБП.
        /// </summary>
        Task<GetCashRegQrStatusResponse> GetCashRegQrStatus(Call<GetCashRegQrStatusRequest> call);

        /// <summary>
        /// Статус Операции по Кассовой ссылке СБП.
        /// </summary>
        Task<GetStatusCashRegQrOperationResponse> GetStatusCashRegQrOperation(Call<GetStatusCashRegQrOperationRequest> call);

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
    }
}