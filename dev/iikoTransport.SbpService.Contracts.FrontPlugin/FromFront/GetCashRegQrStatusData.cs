using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin.FromFront
{
    /// <summary>
    /// Get cash register QR status response data.
    /// </summary>
    [DataContract]
    public class GetCashRegQrStatusData
    {
        public GetCashRegQrStatusData(string status, string? paramsId)
        {
            Status = status ?? throw new ArgumentNullException(nameof(status));
            ParamsId = paramsId;
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
        [DataMember(IsRequired = true)]
        public string? ParamsId { get; }
    }
}