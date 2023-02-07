namespace iikoTransport.SbpService.Contracts.FrontPlugin;

/// <summary>
/// Message routes.
/// </summary>
public class RouteNames
{
    /// <summary>
    /// Регистрация одноразовой Функциональной ссылки СБП для B2B.
    /// </summary>
    public const string CreateOneTimePaymentLink = "PluginsSbp/CreateOneTimePaymentLink";

    /// <summary>
    /// Регистрация многоразовой Функциональной ссылки СБП для B2B.
    /// </summary>
    public const string CreateReusablePaymentLink = "PluginsSbp/CreateReusablePaymentLink";

    /// <summary>
    /// Запрос содержимого для ранее зарегистрированной Функциональной ссылки СБП.
    /// </summary>
    public const string GetQrcPayload = "PluginsSbp/GetQrcPayload";

    /// <summary>
    /// Получение идентификаторов для многоразовых ссылок СБП.
    /// </summary>
    public const string CreateQrcIdReservation = "PluginsSbp/CreateQrcIdReservation";

    /// <summary>
    /// Регистрация Кассовой ссылки СБП.
    /// </summary>
    public const string СreateCashRegisterQr = "PluginsSbp/СreateCashRegisterQr";

    /// <summary>
    /// Активация Кассовой ссылки СБП.
    /// </summary>
    public const string ActivateCashRegisterQr = "PluginsSbp/ActivateCashRegisterQr";

    /// <summary>
    /// Деактивация Кассовой ссылки СБП.
    /// </summary>
    public const string DeactivateCashRegisterQr = "PluginsSbp/DeactivateCashRegisterQr";

    /// <summary>
    /// Запрос статуса Операций СБП по идентификаторам QR. 
    /// </summary>
    public const string GetStatusQrcOperations = "PluginsSbp/GetStatusQrcOperations";

    /// <summary>
    /// Запрос Агента ТСП на возврат по Операции СБП C2B.
    /// </summary>
    public const string CreatePaymentPetition = "PluginsSbp/CreatePaymentPetition";

    /// <summary>
    /// Статус запроса на возврат средств для Агента ТСП.
    /// </summary>
    public const string GetRefundStatus = "PluginsSbp/GetRefundStatus";
}