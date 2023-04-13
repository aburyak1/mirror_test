using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.PublicApi
{
    /// <summary>
    /// Sbp.nspk base response.
    /// </summary>
    [DataContract]
    public class SbpNspkBaseResponse<T>
    {
        public SbpNspkBaseResponse(string code, string message, T? data)
        {
            Code = code ?? throw new ArgumentNullException(nameof(code));
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Data = data;
        }

        /// <summary>
        /// Код ответа.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Code { get; }

        /// <summary>
        /// Описание кода ответа.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Message { get; }

        /// <summary>
        /// Data object. 
        /// </summary>
        [DataMember(IsRequired = false)]
        public T? Data { get; }
    }
}