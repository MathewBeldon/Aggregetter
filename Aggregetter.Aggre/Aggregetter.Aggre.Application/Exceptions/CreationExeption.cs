using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Exceptions
{
    public sealed class CreationExeption : ApplicationException
    {
        public CreationExeption(string message) : base(message)
        {

        }
    }
}
