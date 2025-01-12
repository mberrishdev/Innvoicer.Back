using System;

namespace Innvoicer.Domain.Validators;

[AttributeUsage(AttributeTargets.Class)]
public class CommandValidationAttribute : Attribute
{
    public Type ValidatorType { get; set; }
    public string ValidatorName { get; set; }

    public CommandValidationAttribute(Type validatorType)
    {
        ValidatorType = validatorType;
        ValidatorName = validatorType.Name;
    }
}