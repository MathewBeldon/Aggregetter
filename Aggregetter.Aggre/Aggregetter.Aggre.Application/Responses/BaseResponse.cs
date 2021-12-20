using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
            Message = string.Empty;
            ValidationErrors = null;
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public T Data { get; set; }

        public bool Success { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<string> ValidationErrors { get; set; }
    }
}
