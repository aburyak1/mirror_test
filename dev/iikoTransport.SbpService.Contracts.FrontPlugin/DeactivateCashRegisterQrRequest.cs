using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin
{
    /// <summary>
    /// Запрос деактивации Кассовой ссылки СБП.
    /// </summary>
    [DataContract]
    public class DeactivateCashRegisterQrRequest
    {
        public DeactivateCashRegisterQrRequest(string qrcId)
        {
            QrcId = qrcId;
        }

        /// <summary>
        /// Идентификатор зарегистрированной Кассовой ссылки СБП.
        /// </summary>
        [DataMember(IsRequired = true, Name = "qrcId")]
        public string QrcId { get; }
    }
}