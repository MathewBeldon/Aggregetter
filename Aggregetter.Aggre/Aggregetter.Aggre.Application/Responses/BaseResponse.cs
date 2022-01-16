using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Aggregetter.Aggre.Application.Responses
{
    public class BaseResponse<T>
    {
        public BaseResponse()
        {
            Success = true;
        }

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
