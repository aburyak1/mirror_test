using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using iikoTransport.Logging;
using iikoTransport.Logging.Metrics;
using iikoTransport.SbpService.Services.SbpNspk.Contracts;
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
        private const string CreateQrcIdReservationV1Path = "/payment/v1/qrc-id-reservation";
        private const string CreateCashRegisterQrPath = "/payment/v1/cash-register-qrc";
        private const string CreateParamsPath = "/payment/v1/cash-register-qrc/{0}/params";
        private const string DeleteParamsPath = "/payment/v1/cash-register-qrc/{0}/params";
        private const string CreatePaymentPetitionPath = "/payment/v1/agent/refund/{0}";
        private const string RefundRequestStatusV2Path = "/payment/v2/agent/refund/{0}/{1}";
        private const string GetStatusQrcOperationsPath = "/payment/v2/qrc-status";

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
            CreateAndGetOneTimePaymentLinkPayloadForB2BRequest request, X509Certificate2? cert = null)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            return await CallSpbNspkMethod<SbpNspkResponse<QrcPayloadResponse>>(
                correlationId, CreateAndGetOneTimePaymentLinkPayloadForB2BPath, request);
        }

        /// <summary>
        /// Регистрация многоразовой Функциональной ссылки СБП для B2B.
        /// </summary>
        public async Task<SbpNspkResponse<QrcPayloadResponse>> CreateAndGetReusablePaymentLinkPayloadForB2B(Guid correlationId,
            CreateAndGetReusablePaymentLinkPayloadForB2BRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            return await CallSpbNspkMethod<SbpNspkResponse<QrcPayloadResponse>>(correlationId,
                CreateAndGetReusablePaymentLinkPayloadForB2BPath, request);
        }

        /// <summary>
        /// Запрос содержимого для ранее зарегистрированной Функциональной ссылки СБП.
        /// </summary>
        /// <param name="correlationId"></param>
        /// <param name="qrcId">Идентификатор зарегистрированной Функциональной ссылки СБП</param>
        public async Task<SbpNspkResponse<QrcPayloadResponse>> GetQRCPayload(Guid correlationId, string qrcId)
        {
            if (qrcId == null) throw new ArgumentNullException(nameof(qrcId));

            var uriDetails = string.Format(GetQrcPayloadPath, qrcId);
            return await CallSpbNspkMethod<SbpNspkResponse<QrcPayloadResponse>>(correlationId, uriDetails, null);
        }

        /// <summary>
        /// Получение идентификаторов для многоразовых ссылок СБП.
        /// </summary>
        public async Task<SbpNspkResponse<CreateQrcIdReservationV1Response>> CreateQrcIdReservationV1(Guid correlationId,
            CreateQrcIdReservationV1Request request, X509Certificate2? cert = null)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            return await CallSpbNspkMethod<SbpNspkResponse<CreateQrcIdReservationV1Response>>(correlationId, CreateQrcIdReservationV1Path, request);
        }

        /// <summary>
        /// Регистрация Кассовой ссылки СБП.
        /// </summary>
        public async Task<SbpNspkResponse<QrcPayloadResponse>> СreateCashRegisterQr(Guid correlationId,
            CreateCashRegisterQrRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            return await CallSpbNspkMethod<SbpNspkResponse<QrcPayloadResponse>>(correlationId,
                CreateCashRegisterQrPath, request);
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
        /// Запрос статуса Операций СБП по идентификатору QR Dynamic (v2)
        /// </summary>
        public async Task<SbpNspkResponse<GetStatusQRCOperationsResponse[]>> GetStatusQRCOperations(Guid correlationId,
            GetStatusQRCOperationsRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var uriDetails = string.Format(GetStatusQrcOperationsPath);
            return await CallSpbNspkMethod<SbpNspkResponse<GetStatusQRCOperationsResponse[]>>(correlationId, uriDetails, request, HttpMethod.Put);
        }

        /// <summary>
        /// Запрос Агента ТСП на возврат по Операции СБП C2B.
        /// </summary>
        /// <param name="correlationId"></param>
        /// <param name="trxId">Идентификатор исходной Операции СБП C2B. Example: "A12930013057370100000546241820D7"</param>
        /// <param name="request">Запрос Агента ТСП на возврат по Операции СБП C2B</param>
        public async Task<SbpNspkResponse<CreatePaymentPetitionResponse>> CreatePaymentPetition(Guid correlationId, string trxId,
            CreatePaymentPetitionRequest request)
        {
            if (trxId == null) throw new ArgumentNullException(nameof(trxId));
            if (request == null) throw new ArgumentNullException(nameof(request));

            var uriDetails = string.Format(CreatePaymentPetitionPath, trxId);
            return await CallSpbNspkMethod<SbpNspkResponse<CreatePaymentPetitionResponse>>(correlationId, uriDetails, request);
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
            return await CallSpbNspkMethod<SbpNspkResponse<RefundRequestStatusV2Response>>(correlationId, uriDetails, null);
        }

        /// <summary>
        /// Вызывать метод api sbp.nspk и вернуть десериализованный результат.
        /// </summary>
        /// <param name="correlationId">Correlation Id.</param>
        /// <param name="uriDetails">Метод.</param>
        /// <param name="body">Body для post-запроса.</param>
        /// <param name="httpMethod"></param>
        private async Task<T> CallSpbNspkMethod<T>(Guid correlationId, string uriDetails, object? body, HttpMethod? httpMethod = null)
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
    }
}