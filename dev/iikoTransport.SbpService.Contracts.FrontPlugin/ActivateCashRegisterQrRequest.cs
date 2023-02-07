using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin
{
    /// <summary>
    /// Запрос на активацию Кассовой ссылки СБП для выполнения платежа.
    /// </summary>
    [DataContract]
    public class ActivateCashRegisterQrRequest
    {
        public ActivateCashRegisterQrRequest(string qrcId, string amount)
        {
            QrcId = qrcId;
            Amount = amount;
        }

        /// <summary>
        /// Идентификатор зарегистрированной Кассовой ссылки СБП.
        /// </summary>
        [DataMember(IsRequired = true, Name = "qrcId")]
        public string QrcId { get; }

        /// <summary>
        /// Сумма Операции СБП C2B в копейках. Целое, положительное число.
        /// </summary>
        [DataMember(IsRequired = true, Name = "amount")]
        public string Amount { get; }
    }
}