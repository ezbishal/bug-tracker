using System;

namespace BugTracker.Shared.Generator.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class ExcludeProperty : Attribute
{
    public ExcludeProperty(params string[] classNames)
    {
        ClassNames = classNames;
    }

    public string[] ClassNames { get; set; }
}