using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin
{
    /// <summary>
    /// Запрос на создание Кассовой ссылки СБП, поддерживающей логику работы с Корзинами клиента.
    /// </summary>
    [DataContract]
    public class CreateCashRegisterQrRequest
    {
        public CreateCashRegisterQrRequest(string qrcId)
        {
            QrcId = qrcId;
        }

        /// <summary>
        /// Идентификатор многоразовой Платежной ссылки, предварительно полученный в запросе "Получение идентификаторов для многоразовых ссылок СБП".
        /// </summary>
        [DataMember(IsRequired = false, Name = "qrcId")]
        public string QrcId { get; }
    }
}