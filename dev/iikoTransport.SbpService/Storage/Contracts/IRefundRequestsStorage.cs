using System.Threading;
using System.Threading.Tasks;
using iikoTransport.SbpService.Storage.Contracts.Entities;

namespace iikoTransport.SbpService.Storage.Contracts
{
    public interface IRefundRequestsStorage
    {
        /// <summary>
        /// Get refundRequest info.
        /// </summary>
        Task<RefundRequest> Get(string opkcRefundRequestId, string trxId, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Save or insert refundRequest.
        /// </summary>
        Task Upsert(RefundRequest refundRequest, CancellationToken cancellationToken = default);
    }
}