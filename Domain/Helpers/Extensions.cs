﻿using System;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Domain.Helpers;

public static class Extensions
{
    /// <summary>
    ///     A generic extension method that aids in reflecting 
    ///     and retrieving any attribute that is applied to an `Enum`.
    /// </summary>
    public static TAttribute? GetAttribute<TAttribute>(this Enum enumValue)
        where TAttribute : Attribute
    {
        return enumValue.GetType()
            .GetMember(enumValue.ToString())
            .First()
            .GetCustomAttribute<TAttribute>();
    }

    public static string? GetDisplayName(this Enum enumValue)
    {
        return enumValue.GetAttribute<DisplayAttribute>()?.Name;
    }
}