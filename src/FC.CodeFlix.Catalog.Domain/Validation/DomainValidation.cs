﻿using FC.CodeFlix.Catalog.Domain.Exceptions;

namespace FC.CodeFlix.Catalog.Domain.Validation;
public class DomainValidation
{
    public static void NotNull(object? target, string fieldName)
    {
        if (target is null)
            throw new EntityValidationException($"{fieldName} should not be null");
    }

    public static void NotNullOrEmpty(string? target, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(target))
            throw new EntityValidationException($"{fieldName} should not be empty or null");
    }

    public static void MinLenght(string target, int minLenght, string fieldName)
    {
        if (target.Length < minLenght)
            throw new EntityValidationException($"{fieldName} should be at least {minLenght} characters long");
    }

    public static void MaxLenght(string target, int maxLenght, string fieldName)
    {
        if (target.Length > maxLenght)
            throw new EntityValidationException($"{fieldName} should be less or equal {maxLenght} characters long");
    }
}
