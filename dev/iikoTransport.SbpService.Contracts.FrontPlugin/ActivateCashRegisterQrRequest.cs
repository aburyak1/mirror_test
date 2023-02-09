using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin
{
    /// <summary>
    /// Activate cash register QR request.
    /// </summary>
    [DataContract]
    public class ActivateCashRegisterQrRequest
    {
        public ActivateCashRegisterQrRequest(string qrcId, string amount)
        {
            QrcId = qrcId ?? throw new ArgumentNullException(nameof(qrcId));
            Amount = amount ?? throw new ArgumentNullException(nameof(amount));
        }

        /// <summary>
        /// Идентификатор зарегистрированной Кассовой ссылки СБП.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string QrcId { get; }

        /// <summary>
        /// Сумма Операции СБП C2B в копейках. Целое, положительное число.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Amount { get; }
    }
}