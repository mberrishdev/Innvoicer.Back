﻿using System.Linq;
using Innvoicer.Domain.Validators;

namespace Innvoicer.Domain.Primitives;

public class CommandBaseValidator : ICommandBase
{
    public void Validate()
    {
        if (this.GetType()
                .GetCustomAttributes(typeof(CommandValidationAttribute), true)
                .FirstOrDefault() is not CommandValidationAttribute commandValidationAttribute)
            return;

        var type = commandValidationAttribute.ValidatorType;
        CommandValidator.Validate(type, this);
    }
}