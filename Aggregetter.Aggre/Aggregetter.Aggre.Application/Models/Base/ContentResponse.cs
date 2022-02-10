using System;
using System.Text.Json.Serialization;

namespace Aggregetter.Aggre.Application.Models.Base
{
    public class ContentResponse <T> : BaseResponse
    {
        public ContentResponse() { }

        public ContentResponse(T data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public T Data { get; init; }
    }
}
