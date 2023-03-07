﻿using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin.FromFront
{
    /// <summary>
    /// Create cash register QR request.
    /// </summary>
    [DataContract]
    public class CreateCashRegisterQrRequest
    {
        public CreateCashRegisterQrRequest(string qrcId)
        {
            QrcId = qrcId ?? throw new ArgumentNullException(nameof(qrcId));
        }

        /// <summary>
        /// Идентификатор многоразовой Платежной ссылки, предварительно полученный в запросе "Получение идентификаторов для многоразовых ссылок СБП".
        /// </summary>
        [DataMember(IsRequired = true)]
        public string QrcId { get; }
    }
}