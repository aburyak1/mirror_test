﻿using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Services.SbpNspk.Contracts
{
    /// <summary>
    /// Запрос на активацию Кассовой ссылки СБП для выполнения платежа.
    /// </summary>
    [DataContract]
    public class CreateParamsRequest
    {
        public CreateParamsRequest(string amount)
        {
            Amount = amount;
        }

        /// <summary>
        /// Сумма Операции СБП C2B в копейках. Целое, положительное число.
        /// </summary>
        [DataMember(IsRequired = true, Name = "amount")]
        public string Amount { get; }
    }
}