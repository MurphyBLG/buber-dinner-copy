﻿using BuberDinner.Domain.Common.Models;

namespace BuberDinner.Domain.Menu.ValueObjects;

public class MenuItemId : ValueObject
{
    public Guid Value { get; init; }

    private MenuItemId(Guid value)
        => Value = value;

    public static MenuItemId CreateUnique()
        => new(Guid.NewGuid());

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}