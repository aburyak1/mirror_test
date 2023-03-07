using System;
using System.Threading.Tasks;

namespace iikoTransport.SbpService.Services.Interfaces
{
    public interface ITestService
    {
        /// <summary>
        /// Тестовый запрос к СБП на получение идентификаторов для многоразовых ссылок.
        /// </summary>
        Task<string> TestCreateQrcIdReservation();

        /// <summary>
        /// Тестовый запрос к СБП регистрация одноразовой Функциональной ссылки.
        /// </summary>
        Task<string> TestCreateAndGetOneTimePaymentLinkPayloadForB2B(Guid? tgId = null);

        /// <summary>
        /// Тестовый запрос к СБП на запрос содержимого Функциональной ссылки.
        /// </summary>
        Task<string> TestGetQRCPayload(string? qrcId = null);
    }
}