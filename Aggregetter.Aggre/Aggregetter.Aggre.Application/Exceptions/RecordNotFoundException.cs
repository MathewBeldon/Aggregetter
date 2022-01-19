using System;

namespace Aggregetter.Aggre.Application.Exceptions
{
    public sealed class RecordNotFoundException : ApplicationException
    {
        public RecordNotFoundException(string message) : base(message) { }
    }
}
