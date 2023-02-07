using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Services.SbpNspk.Contracts
{
    /// <summary>
    /// Ответ вызова методов api СБП.
    /// </summary>
    [DataContract]
    public class SbpNspkResponse<T>
    {
        public SbpNspkResponse(string code, string message, T? data)
        {
            Code = code;
            Message = message;
            Data = data;
        }

        /// <summary>
        /// Код ответа
        /// </summary>
        [DataMember(IsRequired = true, Name = "code")]
        public string Code { get; }

        /// <summary>
        /// Описание кода ответа
        /// </summary>
        [DataMember(IsRequired = true, Name = "message")]
        public string Message { get; }

        /// <summary>
        /// object
        /// </summary>
        [DataMember(IsRequired = false, Name = "data")]
        public T? Data { get; }
    }
}