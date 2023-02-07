using System;
using System.Threading;
using System.Threading.Tasks;
using iikoTransport.Postgres;
using iikoTransport.SbpService.Storage.Contracts;
using iikoTransport.SbpService.Storage.Contracts.Entities;

namespace iikoTransport.SbpService.Storage
{
    public class SbpSettingsStorage : ISbpSettingsStorage
    {
        private readonly IDbContextFactory dbContextFactory;

        public SbpSettingsStorage(IDbContextFactory dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
        }

        public async Task<SbpSetting> Get(Guid terminalGroupUocId, CancellationToken cancellationToken = default)
        {
            #region Query

            var query =
                $"select {SbpSetting.AllFieldsWithAliases} " +
                $"from {SbpSetting.TableName} " +
                $"where {SbpSetting.TerminalGroupUocIdCol} = @{nameof(terminalGroupUocId)} ";

            #endregion

            using (var conn = await dbContextFactory.CreateAndOpenAsync())
            {
                return await conn.QuerySingleOrDefaultCancelable<SbpSetting>(query, new { terminalGroupUocId }, cancellationToken: cancellationToken);
            }
        }

        public async Task Upsert(SbpSetting[] sbpSettings, CancellationToken cancellationToken = default)
        {
            if (sbpSettings == null) throw new ArgumentNullException(nameof(sbpSettings));

            using (var context = await dbContextFactory.CreateSingleTransactionDbContextAsync())
            {
                try
                {
                    await Upsert(context, sbpSettings, cancellationToken);
                    await context.CommitAndCloseAsync();
                }
                catch (Exception)
                {
                    await context.RollbackAndCloseAsync();
                    throw;
                }
            }
        }

        private async Task Upsert(ISingleTransactionDbExecutionContext context, SbpSetting[] sbpSetting,
            CancellationToken cancellationToken = default)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (sbpSetting == null) throw new ArgumentNullException(nameof(sbpSetting));

            var query =
                $"insert into {SbpSetting.TableName} (" +
                $"  {SbpSetting.IdCol}, " +
                $"  {SbpSetting.TerminalGroupUocIdCol}, " +
                $"  {SbpSetting.MerchantIdCol}, " +
                $"  {SbpSetting.AccountCol}, " +
                $"  {SbpSetting.MemberIdCol}) " +
                "values(" +
                $"  @{nameof(SbpSetting.Id)}, " +
                $"  @{nameof(SbpSetting.TerminalGroupUocId)}, " +
                $"  @{nameof(SbpSetting.MerchantId)}, " +
                $"  @{nameof(SbpSetting.Account)}, " +
                $"  @{nameof(SbpSetting.MemberId)} ) " +
                $"on conflict on constraint {SbpSetting.PrimaryKey} do update set " +
                $"  {SbpSetting.TerminalGroupUocIdCol} = @{nameof(SbpSetting.TerminalGroupUocId)}, " +
                $"  {SbpSetting.MerchantIdCol} = @{nameof(SbpSetting.MerchantIdCol)}, " +
                $"  {SbpSetting.AccountCol} = @{nameof(SbpSetting.Account)}, " +
                $"  {SbpSetting.MemberIdCol} = @{nameof(SbpSetting.MemberId)} ";
            await context.Connection.ExecuteCancelable(query, sbpSetting, context.Transaction, cancellationToken: cancellationToken);
        }
    }
}