namespace iikoTransport.SbpService.Contracts.FrontPlugin.ToFront
{
    /// <summary>
    /// Sbp events message routes.
    /// </summary>
    public class RouteNames
    {
        /// <summary>
        /// Event on receiving final status ack method route.
        /// </summary>
        public const string SendFinalStatusAck = "Sbp_SendFinalStatusAckV2";
        
        /// <summary>
        /// Event on receiving final status b2c ack method route.
        /// </summary>
        public const string SendFinalStatusB2CAck = "Sbp_SendFinalStatusB2CAck";
        
        /// <summary>
        /// Event on receiving refund resolution method route.
        /// </summary>
        public const string RefundResolution = "Sbp_RefundResolution";
    }
}