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
                $"  {RefundRequest.IdCol}, " +
                $"  {RefundRequest.OpkcRefundRequestIdCol}, " +
                $"  {RefundRequest.TrxIdCol}, " +
                $"  {RefundRequest.TerminalGroupUocIdCol}, " +
                $"  {RefundRequest.TerminalIdCol}, " +
                $"  {RefundRequest.UpdatedAtCol} ) " +
                " values(" +
                $"  @{nameof(RefundRequest.Id)}, " +
                $"  @{nameof(RefundRequest.OpkcRefundRequestId)}, " +
                $"  @{nameof(RefundRequest.TrxId)}, " +
                $"  @{nameof(RefundRequest.TerminalGroupUocId)}, " +
                $"  @{nameof(RefundRequest.TerminalId)}, " +
                $"  @{nameof(RefundRequest.UpdatedAt)} ) " +
                $"on conflict on constraint {RefundRequest.PrimaryKey} do update set " +
                $"  {RefundRequest.OpkcRefundRequestIdCol} = @{nameof(RefundRequest.OpkcRefundRequestId)}, " +
                $"  {RefundRequest.TrxIdCol} = @{nameof(RefundRequest.TrxId)}, " +
                $"  {RefundRequest.TerminalGroupUocIdCol} = @{nameof(RefundRequest.TerminalGroupUocId)}, " +
                $"  {RefundRequest.TerminalIdCol} = @{nameof(RefundRequest.TerminalId)}, " +
                $"  {RefundRequest.UpdatedAtCol} = @{nameof(RefundRequest.UpdatedAt)} ";

            using (var connection = await dbContextFactory.CreateAndOpenAsync())
            {
                await connection.ExecuteCancelable(query, refundRequest, cancellationToken: cancellationToken);
            }
        }
    }
}