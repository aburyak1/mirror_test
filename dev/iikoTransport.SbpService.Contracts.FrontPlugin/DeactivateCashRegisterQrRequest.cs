using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin
{
    /// <summary>
    /// Deactivate cash register QR request.
    /// </summary>
    [DataContract]
    public class DeactivateCashRegisterQrRequest
    {
        public DeactivateCashRegisterQrRequest(string qrcId)
        {
            QrcId = qrcId ?? throw new ArgumentNullException(nameof(qrcId));
        }

        /// <summary>
        /// Идентификатор зарегистрированной Кассовой ссылки СБП.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string QrcId { get; }
    }
}