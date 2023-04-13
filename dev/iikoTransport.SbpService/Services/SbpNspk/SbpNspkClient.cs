using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;
using iikoTransport.Logging;
using iikoTransport.Logging.Metrics;
using iikoTransport.SbpService.Services.SbpNspk.Contracts.Merchants;
using iikoTransport.SbpService.Services.SbpNspk.Contracts.PaymentLinksOperations;
using iikoTransport.Utils;

namespace iikoTransport.SbpService.Services.SbpNspk
{
    /// <summary>
    /// Клиент для вызова методов api СБП.
    /// </summary>
    public class SbpNspkClient
    {
        public readonly string AgentId;
        private readonly IMetrics metrics;
        private readonly HttpClient client;
        private readonly ILog log;
        private readonly string baseUri;
        private const string CreateAndGetOneTimePaymentLinkPayloadForB2BPath = "/payment/v1/b2b/payment-link/one-time-use";
        private const string CreateAndGetReusablePaymentLinkPayloadForB2BPath = "/payment/v1/b2b/payment-link/reusable";
        private const string GetQrcPayloadPath = "/payment/v1/qrc-data/{0}/payload";
        private const string GetStatusQrcOperationsPath = "/payment/v2/qrc-status";
        private const string CreateQrcIdReservationV1Path = "/payment/v1/qrc-id-reservation?quantity={0}";
        private const string CreateCashRegisterQrPath = "/payment/v1/cash-register-qrc";
        private const string CreateParamsPath = "/payment/v1/cash-register-qrc/{0}/params";
        private const string DeleteParamsPath = "/payment/v1/cash-register-qrc/{0}/params";
        private const string GetCashRegQrStatusPath = "/payment/v1/cash-register-qrc/{0}";
        private const string StatusCashRegQrPath = "/payment/v1/cash-register-qrc/{0}/{1}";
        private const string CreateRefundRequestPath = "/payment/v1/agent/refund/{0}";
        private const string GetRefundIdRequestPath = "/payment/v1/agent/refund/{0}?agentRefundRequestId={1}";
        private const string RefundRequestStatusV2Path = "/payment/v2/agent/refund/{0}/{1}";
        private const string SetNewAccountPath = "/payment/v1/cash-register-qrc/{0}";
        private const string SearchMerchantDataPath = "/merchant/v1/merchant/search?ogrn={0}&bic={1}";

        public SbpNspkClient(
            HttpClient httpClient,
            SbpNspkClientOptions options,
            ILog log,
            IMetrics metrics)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            httpClient.Timeout = options.Timeout;
            this.AgentId = options.AgentId;
            this.metrics = metrics ?? throw new ArgumentNullException(nameof(metrics));

            // Можно было бы использовать наш BaseServiceClient.ExecuteMethodByFullUriAsync,
            // но тот при коде ответа сервиса отличном от 200 падает в ошибку при EnsureSuccessStatusCode.
            // СБП в ряде случаев возвращает ответ 400 с документированными контрактами,
            // в которых описана ошибка - в текущей реализации она десериализуется и возвращается во фронт.  
            this.client = httpClient;
            this.baseUri = options.BaseUri;
            this.log = log ?? throw new ArgumentNullException(nameof(log));
        }

        /// <summary>
        /// Регистрация одноразовой Функциональной ссылки СБП для B2B.
        /// </summary>
        public async Task<SbpNspkResponse<QrcPayloadResponse>> CreateAndGetOneTimePaymentLinkPayloadForB2B(Guid correlationId,
            CreateAndGetOneTimePaymentLinkPayloadForB2BRequest request, string? mediaType, int? width, int? height)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var uriDetails = ConcatMediaType(CreateAndGetOneTimePaymentLinkPayloadForB2BPath, mediaType, width, height);
            return await CallSpbNspkMethod<SbpNspkResponse<QrcPayloadResponse>>(correlationId, uriDetails, request);
        }

        /// <summary>
        /// Регистрация многоразовой Функциональной ссылки СБП для B2B.
        /// </summary>
        public async Task<SbpNspkResponse<QrcPayloadResponse>> CreateAndGetReusablePaymentLinkPayloadForB2B(Guid correlationId,
            CreateAndGetReusablePaymentLinkPayloadForB2BRequest request, string? mediaType, int? width, int? height)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var uriDetails = ConcatMediaType(CreateAndGetReusablePaymentLinkPayloadForB2BPath, mediaType, width, height);
            return await CallSpbNspkMethod<SbpNspkResponse<QrcPayloadResponse>>(correlationId, uriDetails, request);
        }

        /// <summary>
        /// Запрос содержимого для ранее зарегистрированной Функциональной ссылки СБП.
        /// </summary>
        /// <param name="correlationId"></param>
        /// <param name="qrcId">Идентификатор зарегистрированной Функциональной ссылки СБП</param>
        /// <param name="mediaType">Опциональное получение QR-кода для Кассовой ссылки СБП</param>
        /// <param name="width">Ширина изображения</param>
        /// <param name="height">Высота изображения</param>
        public async Task<SbpNspkResponse<QrcPayloadResponse>> GetQRCPayload(Guid correlationId, string qrcId, string? mediaType, int? width,
            int? height)
        {
            if (qrcId == null) throw new ArgumentNullException(nameof(qrcId));

            var uriDetails = ConcatMediaType(string.Format(GetQrcPayloadPath, qrcId), mediaType, width, height);
            return await CallSpbNspkMethod<SbpNspkResponse<QrcPayloadResponse>>(correlationId, uriDetails);
        }

        /// <summary>
        /// Запрос статуса Операций СБП по идентификатору QR Dynamic (v2).
        /// </summary>
        public async Task<SbpNspkResponse<GetStatusQRCOperationsResponse[]>> GetStatusQRCOperations(Guid correlationId,
            GetStatusQRCOperationsRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            return await CallSpbNspkMethod<SbpNspkResponse<GetStatusQRCOperationsResponse[]>>(correlationId, GetStatusQrcOperationsPath, request,
                HttpMethod.Put);
        }

        /// <summary>
        /// Получение идентификаторов для многоразовых ссылок СБП.
        /// </summary>
        public async Task<SbpNspkResponse<CreateQrcIdReservationV1Response>> CreateQrcIdReservationV1(Guid correlationId, int quantity)
        {
            var uriDetails = string.Format(CreateQrcIdReservationV1Path, quantity);
            return await CallSpbNspkMethod<SbpNspkResponse<CreateQrcIdReservationV1Response>>(correlationId, uriDetails, null, HttpMethod.Post);
        }

        /// <summary>
        /// Регистрация Кассовой ссылки СБП.
        /// </summary>
        public async Task<SbpNspkResponse<QrcPayloadResponse>> СreateCashRegisterQr(Guid correlationId,
            CreateCashRegisterQrRequest request, string? mediaType, int? width, int? height)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var uriDetails = ConcatMediaType(CreateCashRegisterQrPath, mediaType, width, height);
            return await CallSpbNspkMethod<SbpNspkResponse<QrcPayloadResponse>>(correlationId, uriDetails, request);
        }

        /// <summary>
        /// Активация Кассовой ссылки СБП.
        /// </summary>
        /// <param name="correlationId"></param>
        /// <param name="qrcId">Идентификатор зарегистрированной Кассовой ссылки СБП</param>
        /// <param name="request">Запрос на активацию Кассовой ссылки СБП для выполнения платежа</param>
        public async Task<SbpNspkResponse<CreateParamsResponse>> CreateParams(Guid correlationId, string qrcId,
            CreateParamsRequest request)
        {
            if (qrcId == null) throw new ArgumentNullException(nameof(qrcId));
            if (request == null) throw new ArgumentNullException(nameof(request));

            var uriDetails = string.Format(CreateParamsPath, qrcId);
            return await CallSpbNspkMethod<SbpNspkResponse<CreateParamsResponse>>(correlationId, uriDetails, request);
        }

        /// <summary>
        /// Деактивация Кассовой ссылки СБП.
        /// </summary>
        /// <param name="correlationId"></param>
        /// <param name="qrcId">Идентификатор зарегистрированной Кассовой ссылки СБП</param>
        public async Task<SbpNspkResponse<object>> DeleteParams(Guid correlationId, string qrcId)
        {
            if (qrcId == null) throw new ArgumentNullException(nameof(qrcId));

            var uriDetails = string.Format(DeleteParamsPath, qrcId);
            return await CallSpbNspkMethod<SbpNspkResponse<object>>(correlationId, uriDetails, null, HttpMethod.Delete);
        }

        /// <summary>
        /// Запрос статуса Кассовой ссылки СБП.
        /// </summary>
        /// <param name="correlationId"></param>
        /// <param name="qrcId">Идентификатор зарегистрированной Кассовой ссылки СБП</param>
        public async Task<SbpNspkResponse<GetCashRegQrStatusResponse>> GetCashRegQrStatus(Guid correlationId, string qrcId)
        {
            if (qrcId == null) throw new ArgumentNullException(nameof(qrcId));

            var uriDetails = string.Format(GetCashRegQrStatusPath, qrcId);
            return await CallSpbNspkMethod<SbpNspkResponse<GetCashRegQrStatusResponse>>(correlationId, uriDetails);
        }

        /// <summary>
        /// Статус Операции по Кассовой ссылке СБП (v1).
        /// </summary>
        /// <param name="correlationId"></param>
        /// <param name="qrcId">Идентификатор Кассовой ссылки СБП</param>
        /// <param name="paramsId">Идентификатор активного набора параметров</param>
        public async Task<SbpNspkResponse<StatusCashRegQrResponse>> StatusCashRegQr(Guid correlationId, string qrcId, string paramsId)
        {
            if (qrcId == null) throw new ArgumentNullException(nameof(qrcId));

            var uriDetails = string.Format(StatusCashRegQrPath, qrcId, paramsId);
            return await CallSpbNspkMethod<SbpNspkResponse<StatusCashRegQrResponse>>(correlationId, uriDetails);
        }

        /// <summary>
        /// Запрос Агента ТСП на возврат по Операции СБП C2B.
        /// </summary>
        /// <param name="correlationId"></param>
        /// <param name="trxId">Идентификатор исходной Операции СБП C2B. Example: "A12930013057370100000546241820D7". </param>
        /// <param name="request">Запрос Агента ТСП на возврат по Операции СБП C2B. </param>
        public async Task<SbpNspkResponse<CreatedRefundResponse>> CreateRefundRequest(Guid correlationId, string trxId,
            CreateRefundRequest request)
        {
            if (trxId == null) throw new ArgumentNullException(nameof(trxId));
            if (request == null) throw new ArgumentNullException(nameof(request));

            var uriDetails = string.Format(CreateRefundRequestPath, trxId);
            return await CallSpbNspkMethod<SbpNspkResponse<CreatedRefundResponse>>(correlationId, uriDetails, request);
        }

        /// <summary>
        /// Получение идентификатора ОПКЦ СБП запроса на возврат.
        /// </summary>
        /// <param name="correlationId"></param>
        /// <param name="trxId">Идентификатор исходной Операции СБП C2B. Example: "A12930013057370100000546241820D7". </param>
        /// <param name="agentRefundRequestId">Уникальный идентификатор запроса на возврат, назначенный Агентом ТСП.
        /// Example: "3842ca282aea11e88feca860b60304d5". </param>
        public async Task<SbpNspkResponse<CreatedRefundResponse>> GetRefundIdRequest(Guid correlationId, string trxId, string agentRefundRequestId)
        {
            if (trxId == null) throw new ArgumentNullException(nameof(trxId));
            if (agentRefundRequestId == null) throw new ArgumentNullException(nameof(agentRefundRequestId));

            var uriDetails = string.Format(GetRefundIdRequestPath, trxId, agentRefundRequestId);
            return await CallSpbNspkMethod<SbpNspkResponse<CreatedRefundResponse>>(correlationId, uriDetails);
        }

        /// <summary>
        /// Статус запроса на возврат средств для Агента ТСП (v2).
        /// </summary>
        /// <param name="correlationId"></param>
        /// <param name="originalTrxId">Идентификатор исходной Операции СБП C2B. Example: "A12930013057370100000546241820D7"</param>
        /// <param name="opkcRefundRequestId">Уникальный идентификатор запроса на возврат, назначенный ОПКЦ СБП. Example: "AR10000GH635GB249P6PDNCJ6F7STTM5"</param>
        public async Task<SbpNspkResponse<RefundRequestStatusV2Response>> RefundRequestStatusV2(Guid correlationId, string originalTrxId,
            string opkcRefundRequestId)
        {
            if (originalTrxId == null) throw new ArgumentNullException(nameof(originalTrxId));
            if (opkcRefundRequestId == null) throw new ArgumentNullException(nameof(opkcRefundRequestId));

            var uriDetails = string.Format(RefundRequestStatusV2Path, originalTrxId, opkcRefundRequestId);
            return await CallSpbNspkMethod<SbpNspkResponse<RefundRequestStatusV2Response>>(correlationId, uriDetails);
        }

        /// <summary>
        /// Изменение счета ЮЛ, ИП или самозанятого для зарегистрированной Кассовой ссылки СБП.
        /// </summary>
        /// <param name="correlationId"></param>
        /// <param name="qrcId">Идентификатор зарегистрированной Кассовой ссылки СБП</param>
        /// <param name="account">Счет ЮЛ, ИП или самозанятого></param>
        public async Task<SbpNspkResponse<SetNewAccountResponse>> SetNewAccount(Guid correlationId, string qrcId, string account)
        {
            if (qrcId == null) throw new ArgumentNullException(nameof(qrcId));
            if (account == null) throw new ArgumentNullException(nameof(account));

            var uriDetails = string.Format(SetNewAccountPath, qrcId);
            return await CallSpbNspkMethod<SbpNspkResponse<SetNewAccountResponse>>(correlationId, uriDetails, new { account }, HttpMethod.Put);
        }

        /// <summary>
        /// Получение списка ТСП, зарегистрированных для ЮЛ или ИП.
        /// </summary>
        /// <param name="correlationId"></param>
        /// <param name="ogrn">ОГРН ЮЛ или ИП</param>
        /// <param name="bic">БИК банка-участника, в котором проводятся расчеты по операциям ТСП</param>
        public async Task<SbpNspkResponse<SearchMerchantDataResponse>> SearchMerchantData(Guid correlationId, string ogrn, string bic)
        {
            if (ogrn == null) throw new ArgumentNullException(nameof(ogrn));
            if (bic == null) throw new ArgumentNullException(nameof(bic));

            var uriDetails = string.Format(SearchMerchantDataPath, ogrn, bic);
            return await CallSpbNspkMethod<SbpNspkResponse<SearchMerchantDataResponse>>(correlationId, uriDetails);
        }

        /// <summary>
        /// Вызывать метод api sbp.nspk и вернуть десериализованный результат.
        /// </summary>
        /// <param name="correlationId">Correlation Id.</param>
        /// <param name="uriDetails">Метод.</param>
        /// <param name="body">Body для post-запроса.</param>
        /// <param name="httpMethod"></param>
        private async Task<T> CallSpbNspkMethod<T>(Guid correlationId, string uriDetails, object? body = null, HttpMethod? httpMethod = null)
        {
            if (string.IsNullOrWhiteSpace(uriDetails)) throw new ArgumentNullException(nameof(uriDetails));
            if (httpMethod == null) httpMethod = body == null ? HttpMethod.Get : HttpMethod.Post;

            var uri = baseUri.Trim().TrimEnd('/') + uriDetails;
            bool success = false;
            try
            {
                var requestMessage = new HttpRequestMessage(httpMethod, uri);
                if (httpMethod != HttpMethod.Get && body != null)
                {
                    requestMessage.Content = JsonContent.Create(body);
                    requestMessage.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json; charset=UTF-8");
                }
                requestMessage.Headers.Add("TrnCorrelationId", correlationId.ToString());
                requestMessage.Headers.Add("Accept", "application/json");
                requestMessage.Headers.Add("Accept-Charset", "UTF-8");
                
                var response = await client.SendAsync(requestMessage);
                var responseString = await response.Content.ReadAsStringAsync();
                var result = responseString.FromJson<T>();
                success = true;
                return result;
            }
            catch (Exception exc)
            {
                log.Error("Sbp.Nspk api call error", exc);
                throw;
            }
            finally
            {
                metrics.Count("sbpNspk_call",
                    new MetricLabel(MetricLabels.Method, TrimQueryParams(uriDetails)),
                    new MetricLabel(MetricLabels.Success, success));
            }
        }

        private static string TrimQueryParams(string uri)
        {
            var idx = uri.IndexOf("?", StringComparison.InvariantCulture);
            return idx == -1 ? uri : uri.Substring(0, idx);
        }

        private string ConcatMediaType(string basePath, string? mediaType, int? width, int? height)
        {
            if (string.IsNullOrWhiteSpace(mediaType))
            {
                return basePath;
            }

            return $"{basePath}?mediaType={HttpUtility.UrlEncode(mediaType)}" +
                   $"{(width.HasValue ? "&width=" : string.Empty)}{width}" +
                   $"{(height.HasValue ? "&height=" : string.Empty)}{height}";
        }
    }
}