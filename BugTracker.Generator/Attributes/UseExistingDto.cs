using System;

namespace BugTracker.Generator.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class UseExistingDto : Attribute
{
    public UseExistingDto(params string[] classNames)
    {
        ClassNames = classNames;
    }

    public string[] ClassNames { get; set; }
}