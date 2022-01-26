using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Aggregetter.Aggre.Application.Models.Base
{
    public class BaseResponse<T>
    {
        [JsonConstructor]
        public BaseResponse()
        {
            Success = true;
        }

        [JsonConstructor]
        public BaseResponse(T data)
        {
            Success = true;
            Data = data;
            Message = null;
            ValidationErrors = null;
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public T Data { get; init; }

        public bool Success { get; init; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Message { get; init; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<string> ValidationErrors { get; init; }
    }
}
