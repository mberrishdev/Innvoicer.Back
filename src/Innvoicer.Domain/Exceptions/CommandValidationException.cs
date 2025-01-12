using System.Collections.Generic;
using System.Linq;
using Innvoicer.Domain.Validators;

namespace Innvoicer.Domain.Exceptions;

public class CommandValidationException : DomainException
{
    public CommandValidationException(IEnumerable<CommandValidationError> messages) : base(
        messages.Select(x => x.ErrorMessage))
    {
    }
}