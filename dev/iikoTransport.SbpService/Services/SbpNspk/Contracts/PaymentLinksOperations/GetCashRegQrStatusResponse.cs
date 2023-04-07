using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Services.SbpNspk.Contracts.PaymentLinksOperations
{
    /// <summary>
    /// Статус Кассовой Ссылки.
    /// </summary>
    [DataContract]
    public class GetCashRegQrStatusResponse
    {
        public GetCashRegQrStatusResponse(string status, string? paramsId)
        {
            Status = status ?? throw new ArgumentNullException(nameof(status));
            ParamsId = paramsId ?? throw new ArgumentNullException(nameof(paramsId));
        }

        /// <summary>
        /// Статус Кассовой ссылки СБП:
        /// "INACTIVATED" - Ссылка не активирована;
        /// "WAITING_PAYMENT" - Кассовая ссылка СБП активирована и готова к оплате;
        /// "IN_PROGRESS" - Операция по Кассовой ссылке СБП в процессе выполнения.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Status { get; }

        /// <summary>
        /// Идентификатор активного набора параметров.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? ParamsId { get; }
    }
}