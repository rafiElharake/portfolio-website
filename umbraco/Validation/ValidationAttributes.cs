﻿using System.ComponentModel.DataAnnotations;

namespace umbraco.Validation;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
public class MustBeTrue : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        return value != null && value is bool v && v;
    }
}