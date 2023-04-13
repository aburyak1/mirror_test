using System.Threading;
using System.Threading.Tasks;
using iikoTransport.SbpService.Storage.Contracts.Entities;

namespace iikoTransport.SbpService.Storage.Contracts
{
    public interface IPaymentLinksStorage
    {
        /// <summary>
        /// Get PaymentLink.
        /// </summary>
        Task<PaymentLink> Get(string qrcId, string? paramsId = null, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Save or insert PaymentLink.
        /// </summary>
        Task Upsert(PaymentLink sbpSettings, CancellationToken cancellationToken = default);
    }
}