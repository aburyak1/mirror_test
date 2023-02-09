using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Services.SbpNspk.Contracts
{
    [DataContract]
    public class GetStatusQRCOperationsResponse
    {
        public GetStatusQRCOperationsResponse(string qrcId, string code, string message, string? status, string? trxId, string? kzo)
        {
            QrcId = qrcId ?? throw new ArgumentNullException(nameof(qrcId));
            Code = code ?? throw new ArgumentNullException(nameof(code));
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Status = status;
            TrxId = trxId;
            Kzo = kzo;
        }

        /// <summary>
        /// Идентификатор Платежной ссылки СБП, запрос по которой направлялся.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string QrcId { get; }

        /// <summary>
        /// Код ответа на запрос статусов Операций СБП C2B.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Code { get; }

        /// <summary>
        /// Описание кода ответа на запрос.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Message { get; }

        /// <summary>
        /// Статус Операции СБП:
        /// ACWP - ACCEPTED, Операция завершена успешно;
        /// RCVD - RECEIVED, Операция в обработке;
        /// RJCT - REJECTED, Операция отклонена;
        /// NTST - NOT_STARTED, Операция по Платежной ссылке СБП не найдена.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? Status { get; }

        /// <summary>
        /// Идентификатор Операции СБП. 
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? TrxId { get; }

        /// <summary>
        /// Контрольное Значение Операции СБП. Формируется ОПКЦ СБП на основании собственных
        /// алгоритмов. Присутствует при условии, что Операция СБП C2B по запрашиваемому qrcId
        /// завершена успешно (параметр "status" ="ACWP").
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? Kzo { get; }
    }
}