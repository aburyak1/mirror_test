using System;
using System.Threading;
using System.Threading.Tasks;
using iikoTransport.Postgres;
using iikoTransport.SbpService.Storage.Contracts;
using iikoTransport.SbpService.Storage.Contracts.Entities;

namespace iikoTransport.SbpService.Storage
{
    public class RefundRequestsStorage : IRefundRequestsStorage
    {
        private readonly IDbContextFactory dbContextFactory;

        public RefundRequestsStorage(IDbContextFactory dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
        }

        public async Task<RefundRequest> Get(string opkcRefundRequestId, string trxId, CancellationToken cancellationToken = default)
        {
            var query =
                $"select {RefundRequest.AllFieldsWithAliases} " +
                $"from {RefundRequest.TableName} " +
                $"where {RefundRequest.OpkcRefundRequestIdCol} = @{nameof(opkcRefundRequestId)} " +
                $"  and {RefundRequest.TrxIdCol} = @{nameof(trxId)} ";

            using (var conn = await dbContextFactory.CreateAndOpenAsync())
            {
                return await conn.QuerySingleOrDefaultCancelable<RefundRequest>(query, new { opkcRefundRequestId, trxId },
                    cancellationToken: cancellationToken);
            }
        }

        public async Task Upsert(RefundRequest refundRequest, CancellationToken cancellationToken = default)
        {
            if (refundRequest == null) throw new ArgumentNullException(nameof(refundRequest));

            var query =
                $"insert into {RefundRequest.TableName} (" +
                $"  {RefundRequest.OpkcRefundRequestIdCol}, " +
                $"  {RefundRequest.TrxIdCol}, " +
                $"  {RefundRequest.TerminalGroupUocIdCol} ) " +
                "values(" +
                $"  @{nameof(RefundRequest.OpkcRefundRequestId)}, " +
                $"  @{nameof(RefundRequest.TrxId)}, " +
                $"  @{nameof(RefundRequest.TerminalGroupUocId)} ) " +
                $"on conflict on constraint {RefundRequest.PrimaryKey} do update set " +
                $"  {RefundRequest.TrxIdCol} = @{nameof(RefundRequest.TrxId)}, " +
                $"  {RefundRequest.TerminalGroupUocIdCol} = @{nameof(RefundRequest.TerminalGroupUocId)} ";

            using (var connection = await dbContextFactory.CreateAndOpenAsync())
            {
                await connection.ExecuteCancelable(query, refundRequest, cancellationToken: cancellationToken);
            }
        }
    }
}