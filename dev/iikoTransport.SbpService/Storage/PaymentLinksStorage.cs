using System;
using System.Threading;
using System.Threading.Tasks;
using iikoTransport.Postgres;
using iikoTransport.SbpService.Storage.Contracts;
using iikoTransport.SbpService.Storage.Contracts.Entities;

namespace iikoTransport.SbpService.Storage
{
    public class PaymentLinksStorage : IPaymentLinksStorage
    {
        private readonly IDbContextFactory dbContextFactory;

        public PaymentLinksStorage(IDbContextFactory dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
        }

        public async Task<PaymentLink> Get(string qrcId, CancellationToken cancellationToken = default)
        {
            var query =
                $"select {PaymentLink.AllFieldsWithAliases} " +
                $"from {PaymentLink.TableName} " +
                $"where {PaymentLink.QrcIdCol} = @{nameof(qrcId)} ";

            using (var conn = await dbContextFactory.CreateAndOpenAsync())
            {
                return await conn.QuerySingleOrDefaultCancelable<PaymentLink>(query, new { qrcId }, cancellationToken: cancellationToken);
            }
        }

        public async Task Upsert(PaymentLink paymentLink, CancellationToken cancellationToken = default)
        {
            if (paymentLink == null) throw new ArgumentNullException(nameof(paymentLink));

            var query =
                $"insert into {PaymentLink.TableName} (" +
                $"  {PaymentLink.QrcIdCol}, " +
                $"  {PaymentLink.TerminalGroupUocIdCol}, " +
                $"  {PaymentLink.UpdatedAtCol} ) " +
                "values(" +
                $"  @{nameof(PaymentLink.QrcId)}, " +
                $"  @{nameof(PaymentLink.TerminalGroupUocId)}, " +
                $"  @{nameof(PaymentLink.UpdatedAt)} ) " +
                $"on conflict on constraint {PaymentLink.PrimaryKey} do update set " +
                $"  {PaymentLink.TerminalGroupUocIdCol} = @{nameof(PaymentLink.TerminalGroupUocId)}, " +
                $"  {PaymentLink.UpdatedAtCol} = @{nameof(PaymentLink.UpdatedAt)} ";

            using (var connection = await dbContextFactory.CreateAndOpenAsync())
            {
                await connection.ExecuteCancelable(query, paymentLink, cancellationToken: cancellationToken);
            }
        }
    }
}