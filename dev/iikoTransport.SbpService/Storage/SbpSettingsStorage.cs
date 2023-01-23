using System;
using System.Threading.Tasks;
using Dapper;
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

        public async Task Upsert(params SbpSetting[] sbpSettings)
        {
                if (sbpSettings == null) throw new ArgumentNullException(nameof(sbpSettings));
            
                using (var context = await dbContextFactory.CreateSingleTransactionDbContextAsync())
                {
                        try
                        {
                                await Upsert(context, sbpSettings);
                                await context.CommitAndCloseAsync();
                        }
                        catch (Exception)
                        {
                                await context.RollbackAndCloseAsync();
                                throw;
                        }
                }
        }

        private async Task Upsert(ISingleTransactionDbExecutionContext context, params SbpSetting[] sbpSetting)
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
                await context.Connection.ExecuteAsync(query, sbpSetting, context.Transaction);
        }
    }
}