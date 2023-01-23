namespace iikoTransport.SbpService.Storage
{
    /// <summary>
    /// Настройки базы данных
    /// </summary>
    public interface IDbSettings
    {
        /// <summary>
        /// Строка подключения к базе данных
        /// </summary>
        string ConnectionString { get; }
    }
}