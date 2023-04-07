using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin.FromFront
{
    /// <summary>
    /// Get cash register QR status request.
    /// </summary>
    [DataContract]
    public class GetCashRegQrStatusRequest
    {
        public GetCashRegQrStatusRequest(string qrcId)
        {
            QrcId = qrcId ?? throw new ArgumentNullException(nameof(qrcId));
        }

        /// <summary>
        /// Идентификатор Кассовой ссылки СБП.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string QrcId { get; }
    }
}