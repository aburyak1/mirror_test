using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin.FromFront
{
    /// <summary>
    /// Get refund id request.
    /// </summary>
    [DataContract]
    public class GetRefundIdRequest
    {
        public GetRefundIdRequest(string trxId, string agentRefundRequestId)
        {
            TrxId = trxId ?? throw new ArgumentNullException(nameof(trxId));
            AgentRefundRequestId = agentRefundRequestId ?? throw new ArgumentNullException(nameof(agentRefundRequestId));
        }

        /// <summary>
        /// Идентификатор исходной Операции СБП C2B. 
        /// </summary>
        [DataMember(IsRequired = true)]
        public string TrxId { get; }

        /// <summary>
        /// Уникальный идентификатор запроса на возврат, назначенный Агентом ТСП. 
        /// </summary>
        [DataMember(IsRequired = true)]
        public string AgentRefundRequestId { get; }
    }
}