namespace iikoTransport.SbpService.Contracts.FrontPlugin
{
    /// <summary>
    /// Message routes.
    /// </summary>
    public class RouteNames
    {
        /// <summary>
        /// Create one-time payment link method route.
        /// </summary>
        public const string CreateOneTimePaymentLink = "PluginsSbp/CreateOneTimePaymentLink";

        /// <summary>
        /// Create reusable payment link method route.
        /// </summary>
        public const string CreateReusablePaymentLink = "PluginsSbp/CreateReusablePaymentLink";

        /// <summary>
        /// Get QR-code payload for existing payment link method route.
        /// </summary>
        public const string GetQrcPayload = "PluginsSbp/GetQrcPayload";

        /// <summary>
        /// Reservate QR-code IDs for reusable payment links method route.
        /// </summary>
        public const string CreateQrcIdReservation = "PluginsSbp/CreateQrcIdReservation";

        /// <summary>
        /// Create cash register QR method route.
        /// </summary>
        public const string СreateCashRegisterQr = "PluginsSbp/СreateCashRegisterQr";

        /// <summary>
        /// Activate cash register QR method route.
        /// </summary>
        public const string ActivateCashRegisterQr = "PluginsSbp/ActivateCashRegisterQr";

        /// <summary>
        /// Deactivate cash register QR method route.
        /// </summary>
        public const string DeactivateCashRegisterQr = "PluginsSbp/DeactivateCashRegisterQr";

        /// <summary>
        /// Get status of QR-code operations method route. 
        /// </summary>
        public const string GetStatusQrcOperations = "PluginsSbp/GetStatusQrcOperations";

        /// <summary>
        /// Create payment petition method route. 
        /// </summary>
        public const string CreatePaymentPetition = "PluginsSbp/CreatePaymentPetition";

        /// <summary>
        /// Get refund status method route. 
        /// </summary>
        public const string GetRefundStatus = "PluginsSbp/GetRefundStatus";
    }
}