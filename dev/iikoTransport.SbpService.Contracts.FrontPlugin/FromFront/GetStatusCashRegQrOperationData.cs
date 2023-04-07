using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin.FromFront
{
    /// <summary>
    /// Get status of cash register QR operation response data.
    /// </summary>
    [DataContract]
    public class GetStatusCashRegQrOperationData
    {
        public GetStatusCashRegQrOperationData(string cashRegisterQrcStatus, string trxStatus, string? trxId, string amount, string? timestamp,
            string? payerId, string? kzo)
        {
            CashRegisterQrcStatus = cashRegisterQrcStatus ?? throw new ArgumentNullException(nameof(cashRegisterQrcStatus));
            TrxStatus = trxStatus ?? throw new ArgumentNullException(nameof(trxStatus));
            TrxId = trxId;
            Amount = amount ?? throw new ArgumentNullException(nameof(amount));
            Timestamp = timestamp;
            PayerId = payerId;
            Kzo = kzo;
        }

        /// <summary>
        /// Статус Кассовой ссылки СБП:
        /// "WAITING_PAYMENT" - Кассовая ссылка СБП активирована и готова к оплате;
        /// "IN_PROGRESS" - Операция по Кассовой ссылке СБП в процессе выполнения;
        /// "DEACTIVATED" - Кассовая ссылка СБП деактивирована.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string CashRegisterQrcStatus { get; }

        /// <summary>
        /// Статус операции по Кассовой ссылке.
        /// <remarks>Enum: "ACWP" "RJCT" "RCVD" "NTST".</remarks>
        /// </summary>
        [DataMember(IsRequired = true)]
        public string TrxStatus { get; }

        /// <summary>
        /// Идентификатор Операции СБП C2B.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? TrxId { get; }

        /// <summary>
        /// Сумма Операции СБП в копейках. Целое, положительное число. Валюта операции – рубли РФ.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Amount { get; }

        /// <summary>
        /// Дата и время выполнения Операции СБП C2B.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? Timestamp { get; }

        /// <summary>
        /// Маскированный номер телефона Клиента-Плательщика.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? PayerId { get; }

        /// <summary>
        /// Контрольное Значение Операции СБП C2B.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? Kzo { get; }
    }
}