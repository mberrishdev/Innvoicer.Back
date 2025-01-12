using System;
using System.Collections.Generic;

namespace Innvoicer.Domain.Exceptions;

public class DomainException : Exception
{
    protected DomainException(IEnumerable<string> messages) : base(string.Join("," + Environment.NewLine + " ",
        messages))
    {
    }
}