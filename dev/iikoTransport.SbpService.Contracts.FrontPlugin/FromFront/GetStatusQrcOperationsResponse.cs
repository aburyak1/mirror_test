﻿using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin.FromFront
{
    /// <summary>
    /// Get status of QR-code operations response.
    /// </summary>
    [DataContract]
    public class GetStatusQrcOperationsResponse : SbpNspkBaseResponse<GetStatusQrcOperationsData[]>
    {
        public GetStatusQrcOperationsResponse(string code, string message, GetStatusQrcOperationsData[]? data)
            : base(code, message, data)
        {
        }
    }
}