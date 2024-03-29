﻿using System.Collections.Generic;
using System.Net;
using System.Text.Json.Serialization;

namespace Aggregetter.Aggre.Application.Models.Base
{
    public class BaseResponse
    {
        public BaseResponse() { }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Message { get; init; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Dictionary<string, string> ValidationErrors { get; init; }
    }
}
