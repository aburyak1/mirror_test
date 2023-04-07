using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin.FromFront
{
    /// <summary>
    /// Get status of cash register QR operation request.
    /// </summary>
    [DataContract]
    public class GetStatusCashRegQrOperationRequest
    {
        public GetStatusCashRegQrOperationRequest(string qrcId, string paramsId)
        {
            QrcId = qrcId ?? throw new ArgumentNullException(nameof(qrcId));
            ParamsId = paramsId ?? throw new ArgumentNullException(nameof(paramsId));
        }

        /// <summary>
        /// Идентификатор Кассовой ссылки СБП.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string QrcId { get; }

        /// <summary>
        /// Идентификатор активного набора параметров.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string ParamsId { get; }
    }
}