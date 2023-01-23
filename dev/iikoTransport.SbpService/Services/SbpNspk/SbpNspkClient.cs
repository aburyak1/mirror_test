using System;
using System.Net.Http;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using iikoTransport.Logging;
using iikoTransport.Logging.Metrics;
using iikoTransport.SbpService.Services.SbpNspk.Contracts;
using iikoTransport.ServiceClient;
using iikoTransport.Utils;

namespace iikoTransport.SbpService.Services
{
    /// <summary>
    /// Клиент для вызова методов api СБП.
    /// </summary>
    public class SbpNspkClient : BaseServiceClient
    {
        private readonly SbpNspkClientOptions options;
        private readonly IMetrics metrics;
        private const string createAndGetOneTimePaymentLinkPayloadForB2B = "/payment/v1/b2b/payment-link/one-time-use";

        public SbpNspkClient(
            HttpClient httpClient,
            SbpNspkClientOptions options,
            IMethodCallSettingsFactory callSettingsFactory,
            ILog log,
            IMetrics metrics)
            : base(httpClient, options, callSettingsFactory, log)
        {
            httpClient.Timeout = TimeSpan.FromMilliseconds(int.MaxValue);
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.metrics = metrics ?? throw new ArgumentNullException(nameof(metrics));
        }

        protected override string ControllerName
        {
            get => throw new InvalidOperationException();
            set => throw new InvalidOperationException();
        }

        protected override bool IsExternalClient => true;

        /// <summary>
        /// Тестовый метод для проверки работоспособности api СБП.
        /// </summary>
        public async Task<string> TestMethod(CreateAndGetOneTimePaymentLinkPayloadForB2BRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                var handler = new HttpClientHandler();
                handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                handler.SslProtocols = SslProtocols.Tls12;
                var cert = new X509Certificate2("Services\\SbpNspk\\myreq_2022.cer");
                handler.ClientCertificates.Add(cert);
                handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                handler.ServerCertificateCustomValidationCallback =
                    (httpRequestMessage, certificate, certChain, policyErrors) => { return true; };
                var client = new HttpClient(handler);
                var response = await client.PostAsync(
                    "https://sbp-gate4.nspk.ru/payment/v1/b2b/payment-link/one-time-use",
                    new StringContent(request.ToJson()));
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
            catch (Exception exc)
            {
                Log.Error("Error in TestMethod", exc);
                throw;
            }
        }

        /// <summary>
        /// Регистрация одноразовой Функциональной ссылки СБП для B2B.
        /// </summary>
        public async Task<string> СreateAndGetOneTimePaymentLinkPayloadForB2B(Guid correlationId,
            CreateAndGetOneTimePaymentLinkPayloadForB2BRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            return await CallSpbNspkMethod<string>(correlationId, createAndGetOneTimePaymentLinkPayloadForB2B, request);
        }

        /// <summary>
        /// Вызывать метод api ВКС Яндекс и вернуть десериализованный результат.
        /// </summary>
        /// <param name="correlationId">Correlation Id.</param>
        /// <param name="uriDetails">Метод.</param>
        /// <param name="body">Body для post-запроса.</param>
        private async Task<T> CallSpbNspkMethod<T>(Guid correlationId, string uriDetails, object body)
        {
            if (string.IsNullOrWhiteSpace(uriDetails)) throw new ArgumentNullException(nameof(uriDetails));
            if (body == null) throw new ArgumentNullException(nameof(body));

            var callSettings = CallSettingsFactory.CreateWithTimeout(correlationId, options.Timeout);
            // Пример добавления хедеров, искать в документации апи по слову mediaType
            //callSettings.Headers.Add("mediaType", mediaTypePng);  

            var uri = BaseUri.Trim().TrimEnd('/') + uriDetails;
            bool success = false;
            try
            {
                var result = await ExecuteMethodByFullUriAsync<T>(uri, body, callSettings, HttpMethod.Post);
                success = true;
                return result;
            }
            catch (Exception exc)
            {
                Log.Error("Sbp.nspk api call error", exc);
                throw;
            }
            finally
            {
                metrics.Count("sbp.nspk_call",
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