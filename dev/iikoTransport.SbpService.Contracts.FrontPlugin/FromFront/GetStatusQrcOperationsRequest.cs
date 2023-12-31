﻿using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin.FromFront
{
    /// <summary>
    /// Get status of QR-code operations request.
    /// </summary>
    [DataContract]
    public class GetStatusQrcOperationsRequest
    {
        public GetStatusQrcOperationsRequest(string[] qrcIds)
        {
            QrcIds = qrcIds ?? throw new ArgumentNullException(nameof(qrcIds));
        }

        /// <summary>
        /// Массив qrcId, по которым нужно получить статус Операций СБП C2B.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string[] QrcIds { get; }
    }
}