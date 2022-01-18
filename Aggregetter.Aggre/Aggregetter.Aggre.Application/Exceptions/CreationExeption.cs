using System;

namespace Aggregetter.Aggre.Application.Exceptions
{
    public sealed class CreationExeption : ApplicationException
    {
        public CreationExeption(string message) : base(message)
        {

        }
    }
}
