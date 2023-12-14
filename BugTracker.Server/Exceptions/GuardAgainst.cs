using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace BugTracker.Server.Exceptions;

public static class GuardAgainst
{
    public static T ArgumentBeingNull<T>([NotNull] T? argumentValue, string? argumentName = null, string? exceptionMessage = null, IDictionary<object, object>? additionalData = null) where T : class
    {
        if (argumentValue != null)
        {
            return argumentValue;
        }

        ArgumentNullException ex = new ArgumentNullException(argumentName != null ? argumentName.ToNullIfWhitespace() : null, exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null);
        ex.AddData(additionalData);
        throw ex;
    }

    public static T ArgumentBeingNull<T>([NotNull] T? argumentValue, string? argumentName = null, string? exceptionMessage = null, IDictionary<object, object>? additionalData = null) where T : struct
    {
        if (argumentValue.HasValue)
        {
            return argumentValue.Value;
        }

        ArgumentNullException ex = new ArgumentNullException(argumentName != null ? argumentName.ToNullIfWhitespace() : null, exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null);
        ex.AddData(additionalData);
        throw ex;
    }

    public static string ArgumentBeingNullOrWhitespace([NotNull] string? argumentValue, string? argumentName = null, string? exceptionMessage = null, IDictionary<object, object>? additionalData = null)
    {
        if (!argumentValue.IsNullOrEmpty())
        {
            return argumentValue;
        }

        if (argumentValue == null)
        {
            ArgumentNullException ex = new ArgumentNullException(argumentName != null ? argumentName.ToNullIfWhitespace() : null, exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null);
            ex.AddData(additionalData);
            throw ex;
        }

        ArgumentException ex2 = new ArgumentException(exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null, argumentName != null ? argumentName.ToNullIfWhitespace() : null);
        ex2.AddData(additionalData);
        throw ex2;
    }

    public static string? ArgumentBeingWhitespace(string? argumentValue, string? argumentName = null, string? exceptionMessage = null, IDictionary<object, object>? additionalData = null)
    {
        if (argumentValue == null || !string.IsNullOrWhiteSpace(argumentValue))
        {
            return argumentValue;
        }

        ArgumentException ex = new ArgumentException(exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null, argumentName != null ? argumentName.ToNullIfWhitespace() : null);
        ex.AddData(additionalData);
        throw ex;
    }

    public static T ArgumentBeingNullOrLessThanMinimum<T>([NotNull] T? argumentValue, T minimumAllowedValue, string? argumentName = null, string? exceptionMessage = null, IDictionary<object, object>? additionalData = null) where T : class, IComparable<T>
    {
        if (argumentValue == null)
        {
            ArgumentNullException ex = new ArgumentNullException(argumentName != null ? argumentName.ToNullIfWhitespace() : null, exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null);
            ex.AddData(additionalData);
            throw ex;
        }

        if (argumentValue.IsLessThan(minimumAllowedValue))
        {
            ArgumentOutOfRangeException ex2 = new ArgumentOutOfRangeException(argumentName != null ? argumentName.ToNullIfWhitespace() : null, argumentValue, exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null);
            ex2.AddData(additionalData);
            throw ex2;
        }

        return argumentValue;
    }

    public static T ArgumentBeingNullOrLessThanOrEqualMinimum<T>([NotNull] T? argumentValue, T minimumAllowedValue, string? argumentName = null, string? exceptionMessage = null, IDictionary<object, object>? additionalData = null) where T : class, IComparable<T>
    {
        if (argumentValue == null)
        {
            ArgumentNullException ex = new ArgumentNullException(argumentName != null ? argumentName.ToNullIfWhitespace() : null, exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null);
            ex.AddData(additionalData);
            throw ex;
        }

        if (argumentValue.IsLessThanOrEqual(minimumAllowedValue))
        {
            ArgumentOutOfRangeException ex2 = new ArgumentOutOfRangeException(argumentName != null ? argumentName.ToNullIfWhitespace() : null, argumentValue, exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null);
            ex2.AddData(additionalData);
            throw ex2;
        }

        return argumentValue;
    }

    public static T ArgumentBeingNullOrLessThanMinimum<T>([NotNull] T? argumentValue, T minimumAllowedValue, string? argumentName = null, string? exceptionMessage = null, IDictionary<object, object>? additionalData = null) where T : struct, IComparable<T>
    {
        if (!argumentValue.HasValue)
        {
            ArgumentNullException ex = new ArgumentNullException(argumentName != null ? argumentName.ToNullIfWhitespace() : null, exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null);
            ex.AddData(additionalData);
            throw ex;
        }

        if (argumentValue.Value.IsLessThan(minimumAllowedValue))
        {
            ArgumentOutOfRangeException ex2 = new ArgumentOutOfRangeException(argumentName != null ? argumentName.ToNullIfWhitespace() : null, argumentValue, exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null);
            ex2.AddData(additionalData);
            throw ex2;
        }

        return argumentValue.Value;
    }

    public static T ArgumentBeingNullOrLessThanOrEqualMinimum<T>([NotNull] T? argumentValue, T minimumAllowedValue, string? argumentName = null, string? exceptionMessage = null, IDictionary<object, object>? additionalData = null) where T : struct, IComparable<T>
    {
        if (!argumentValue.HasValue)
        {
            ArgumentNullException ex = new ArgumentNullException(argumentName != null ? argumentName.ToNullIfWhitespace() : null, exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null);
            ex.AddData(additionalData);
            throw ex;
        }

        if (argumentValue.Value.IsLessThanOrEqual(minimumAllowedValue))
        {
            ArgumentOutOfRangeException ex2 = new ArgumentOutOfRangeException(argumentName != null ? argumentName.ToNullIfWhitespace() : null, argumentValue, exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null);
            ex2.AddData(additionalData);
            throw ex2;
        }

        return argumentValue.Value;
    }

    public static T? ArgumentBeingLessThanMinimum<T>(T? argumentValue, T minimumAllowedValue, string? argumentName = null, string? exceptionMessage = null, IDictionary<object, object>? additionalData = null) where T : IComparable<T>
    {
        if (argumentValue == null)
        {
            return argumentValue;
        }

        if (argumentValue.IsLessThan(minimumAllowedValue))
        {
            ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException(argumentName != null ? argumentName.ToNullIfWhitespace() : null, argumentValue, exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null);
            ex.AddData(additionalData);
            throw ex;
        }

        return argumentValue;
    }

    public static T? ArgumentBeingLessThanOrEqualMinimum<T>(T? argumentValue, T minimumAllowedValue, string? argumentName = null, string? exceptionMessage = null, IDictionary<object, object>? additionalData = null) where T : IComparable<T>
    {
        if (argumentValue == null)
        {
            return argumentValue;
        }

        if (argumentValue.IsLessThanOrEqual(minimumAllowedValue))
        {
            ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException(argumentName != null ? argumentName.ToNullIfWhitespace() : null, argumentValue, exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null);
            ex.AddData(additionalData);
            throw ex;
        }

        return argumentValue;
    }

    public static T ArgumentBeingNullOrGreaterThanMaximum<T>([NotNull] T? argumentValue, T maximumAllowedValue, string? argumentName = null, string? exceptionMessage = null, IDictionary<object, object>? additionalData = null) where T : class, IComparable<T>
    {
        if (argumentValue == null)
        {
            ArgumentNullException ex = new ArgumentNullException(argumentName != null ? argumentName.ToNullIfWhitespace() : null, exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null);
            ex.AddData(additionalData);
            throw ex;
        }

        if (argumentValue.IsMoreThan(maximumAllowedValue))
        {
            ArgumentOutOfRangeException ex2 = new ArgumentOutOfRangeException(argumentName != null ? argumentName.ToNullIfWhitespace() : null, argumentValue, exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null);
            ex2.AddData(additionalData);
            throw ex2;
        }

        return argumentValue;
    }

    public static T ArgumentBeingNullOrGreaterThanOrEqualMaximum<T>([NotNull] T? argumentValue, T maximumAllowedValue, string? argumentName = null, string? exceptionMessage = null, IDictionary<object, object>? additionalData = null) where T : class, IComparable<T>
    {
        if (argumentValue == null)
        {
            ArgumentNullException ex = new ArgumentNullException(argumentName != null ? argumentName.ToNullIfWhitespace() : null, exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null);
            ex.AddData(additionalData);
            throw ex;
        }

        if (argumentValue.IsMoreThanOrEqual(maximumAllowedValue))
        {
            ArgumentOutOfRangeException ex2 = new ArgumentOutOfRangeException(argumentName != null ? argumentName.ToNullIfWhitespace() : null, argumentValue, exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null);
            ex2.AddData(additionalData);
            throw ex2;
        }

        return argumentValue;
    }

    public static T ArgumentBeingNullOrGreaterThanMaximum<T>([NotNull] T? argumentValue, T maximumAllowedValue, string? argumentName = null, string? exceptionMessage = null, IDictionary<object, object>? additionalData = null) where T : struct, IComparable<T>
    {
        if (!argumentValue.HasValue)
        {
            ArgumentNullException ex = new ArgumentNullException(argumentName != null ? argumentName.ToNullIfWhitespace() : null, exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null);
            ex.AddData(additionalData);
            throw ex;
        }

        if (argumentValue.Value.IsMoreThan(maximumAllowedValue))
        {
            ArgumentOutOfRangeException ex2 = new ArgumentOutOfRangeException(argumentName != null ? argumentName.ToNullIfWhitespace() : null, argumentValue, exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null);
            ex2.AddData(additionalData);
            throw ex2;
        }

        return argumentValue.Value;
    }

    public static T ArgumentBeingNullOrGreaterThanOrEqualMaximum<T>([NotNull] T? argumentValue, T maximumAllowedValue, string? argumentName = null, string? exceptionMessage = null, IDictionary<object, object>? additionalData = null) where T : struct, IComparable<T>
    {
        if (!argumentValue.HasValue)
        {
            ArgumentNullException ex = new ArgumentNullException(argumentName != null ? argumentName.ToNullIfWhitespace() : null, exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null);
            ex.AddData(additionalData);
            throw ex;
        }

        if (argumentValue.Value.IsMoreThanOrEqual(maximumAllowedValue))
        {
            ArgumentOutOfRangeException ex2 = new ArgumentOutOfRangeException(argumentName != null ? argumentName.ToNullIfWhitespace() : null, argumentValue, exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null);
            ex2.AddData(additionalData);
            throw ex2;
        }

        return argumentValue.Value;
    }

    public static T? ArgumentBeingGreaterThanMaximum<T>(T? argumentValue, T maximumAllowedValue, string? argumentName = null, string? exceptionMessage = null, IDictionary<object, object>? additionalData = null) where T : IComparable<T>
    {
        if (argumentValue == null)
        {
            return argumentValue;
        }

        if (argumentValue.IsMoreThan(maximumAllowedValue))
        {
            ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException(argumentName != null ? argumentName.ToNullIfWhitespace() : null, argumentValue, exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null);
            ex.AddData(additionalData);
            throw ex;
        }

        return argumentValue;
    }

    public static T? ArgumentBeingGreaterThanOrEqualMaximum<T>(T? argumentValue, T maximumAllowedValue, string? argumentName = null, string? exceptionMessage = null, IDictionary<object, object>? additionalData = null) where T : IComparable<T>
    {
        if (argumentValue == null)
        {
            return argumentValue;
        }

        if (argumentValue.IsMoreThanOrEqual(maximumAllowedValue))
        {
            ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException(argumentName != null ? argumentName.ToNullIfWhitespace() : null, argumentValue, exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null);
            ex.AddData(additionalData);
            throw ex;
        }

        return argumentValue;
    }

    public static T ArgumentBeingNullOrOutOfRange<T>([NotNull] T? argumentValue, T minimumAllowedValue, T maximumAllowedValue, string? argumentName = null, string? exceptionMessage = null, IDictionary<object, object>? additionalData = null) where T : class, IComparable<T>
    {
        if (argumentValue == null)
        {
            ArgumentNullException ex = new ArgumentNullException(argumentName != null ? argumentName.ToNullIfWhitespace() : null, exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null);
            ex.AddData(additionalData);
            throw ex;
        }

        if (!argumentValue.IsInRange(minimumAllowedValue, maximumAllowedValue))
        {
            ArgumentOutOfRangeException ex2 = new ArgumentOutOfRangeException(argumentName != null ? argumentName.ToNullIfWhitespace() : null, argumentValue, exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null);
            ex2.AddData(additionalData);
            throw ex2;
        }

        return argumentValue;
    }

    public static T ArgumentBeingNullOrOutOfRange<T>([NotNull] T? argumentValue, T minimumAllowedValue, T maximumAllowedValue, string? argumentName = null, string? exceptionMessage = null, IDictionary<object, object>? additionalData = null) where T : struct, IComparable<T>
    {
        if (!argumentValue.HasValue)
        {
            ArgumentNullException ex = new ArgumentNullException(argumentName != null ? argumentName.ToNullIfWhitespace() : null, exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null);
            ex.AddData(additionalData);
            throw ex;
        }

        if (!argumentValue.Value.IsInRange(minimumAllowedValue, maximumAllowedValue))
        {
            ArgumentOutOfRangeException ex2 = new ArgumentOutOfRangeException(argumentName != null ? argumentName.ToNullIfWhitespace() : null, argumentValue, exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null);
            ex2.AddData(additionalData);
            throw ex2;
        }

        return argumentValue.Value;
    }

    public static T? ArgumentBeingOutOfRange<T>(T? argumentValue, T minimumAllowedValue, T maximumAllowedValue, string? argumentName = null, string? exceptionMessage = null, IDictionary<object, object>? additionalData = null) where T : IComparable<T>
    {
        if (argumentValue == null)
        {
            return argumentValue;
        }

        if (!argumentValue.IsInRange(minimumAllowedValue, maximumAllowedValue))
        {
            ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException(argumentName != null ? argumentName.ToNullIfWhitespace() : null, argumentValue, exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null);
            ex.AddData(additionalData);
            throw ex;
        }

        return argumentValue;
    }

    public static bool ArgumentBeingInvalid([DoesNotReturnIf(true)] bool argumentValueInvalid, string? argumentName = null, string? exceptionMessage = null, IDictionary<object, object>? additionalData = null)
    {
        if (!argumentValueInvalid)
        {
            return argumentValueInvalid;
        }

        ArgumentException ex = new ArgumentException(exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null, argumentName != null ? argumentName.ToNullIfWhitespace() : null);
        ex.AddData(additionalData);
        throw ex;
    }

    public static bool OperationBeingInvalid([DoesNotReturnIf(true)] bool operationInvalid, string? exceptionMessage = null, IDictionary<object, object>? additionalData = null)
    {
        if (!operationInvalid)
        {
            return operationInvalid;
        }

        InvalidOperationException ex = new InvalidOperationException(exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null);
        ex.AddData(additionalData);
        throw ex;
    }

    public static DateTime ArgumentBeingUnspecifiedDateTime(DateTime argumentValue, string? argumentName = null, string? exceptionMessage = null, IDictionary<object, object>? additionalData = null)
    {
        if (argumentValue.Kind != 0)
        {
            return argumentValue;
        }

        ArgumentException ex = new ArgumentException(exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null, argumentName != null ? argumentName.ToNullIfWhitespace() : null);
        ex.AddData(additionalData);
        throw ex;
    }

    public static string ArgumentBeingNullOrEmpty([NotNull] string? argumentValue, string? argumentName = null, string? exceptionMessage = null, IDictionary<object, object>? additionalData = null)
    {
        if (!argumentValue.IsNullOrEmpty())
        {
            return argumentValue;
        }

        if (argumentValue == null)
        {
            ArgumentNullException ex = new ArgumentNullException(argumentName != null ? argumentName.ToNullIfWhitespace() : null, exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null);
            ex.AddData(additionalData);
            throw ex;
        }

        ArgumentException ex2 = new ArgumentException(exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null, argumentName != null ? argumentName.ToNullIfWhitespace() : null);
        ex2.AddData(additionalData);
        throw ex2;
    }

    public static IEnumerable<T> ArgumentBeingNullOrEmpty<T>([NotNull] IEnumerable<T>? argumentValue, string? argumentName = null, string? exceptionMessage = null, IDictionary<object, object>? additionalData = null)
    {
        if (argumentValue == null)
        {
            ArgumentNullException ex = new ArgumentNullException(argumentName != null ? argumentName.ToNullIfWhitespace() : null, exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null);
            ex.AddData(additionalData);
            throw ex;
        }

        if (!argumentValue.Any())
        {
            ArgumentException ex2 = new ArgumentException(exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null, argumentName != null ? argumentName.ToNullIfWhitespace() : null);
            ex2.AddData(additionalData);
            throw ex2;
        }

        return argumentValue;
    }

    public static string? ArgumentBeingEmpty(string? argumentValue, string? argumentName = null, string? exceptionMessage = null, IDictionary<object, object>? additionalData = null)
    {
        if (argumentValue == null || !string.IsNullOrEmpty(argumentValue))
        {
            return argumentValue;
        }

        ArgumentException ex = new ArgumentException(exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null, argumentName != null ? argumentName.ToNullIfWhitespace() : null);
        ex.AddData(additionalData);
        throw ex;
    }

    public static IEnumerable<T>? ArgumentBeingEmpty<T>(IEnumerable<T>? argumentValue, string? argumentName = null, string? exceptionMessage = null, IDictionary<object, object>? additionalData = null)
    {
        if (argumentValue == null || argumentValue.Any())
        {
            return argumentValue;
        }

        ArgumentException ex = new ArgumentException(exceptionMessage != null ? exceptionMessage.ToNullIfWhitespace() : null, argumentName != null ? argumentName.ToNullIfWhitespace() : null);
        ex.AddData(additionalData);
        throw ex;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? ToNullIfWhitespace(this string @this)
    {
        if (!string.IsNullOrWhiteSpace(@this))
        {
            return @this;
        }

        return null;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void AddData(this Exception ex, IDictionary<object, object>? additionalData)
    {
        if (additionalData == null || !additionalData.Any())
        {
            return;
        }

        foreach (object key in additionalData.Keys)
        {
            ex.Data.Add(key, additionalData[key]);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsStringInRange(this string? @this, string? lowerBound, string? upperBound)
    {
        if (string.CompareOrdinal(@this, lowerBound) >= 0)
        {
            return string.CompareOrdinal(@this, upperBound) <= 0;
        }

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsInRange<T>(this T @this, T lowerBound, T upperBound) where T : IComparable<T>
    {
        if (typeof(T) == typeof(string))
        {
            return (@this as string).IsStringInRange(lowerBound as string, upperBound as string);
        }

        if (@this.CompareTo(lowerBound) >= 0)
        {
            return @this.CompareTo(upperBound) <= 0;
        }

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsStringLessThan(this string? @this, string? lowerBound)
    {
        return string.CompareOrdinal(@this, lowerBound) < 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsStringLessThanOrEqual(this string? @this, string? lowerBound)
    {
        return string.CompareOrdinal(@this, lowerBound) <= 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsLessThan<T>(this T @this, T lowerBound) where T : IComparable<T>
    {
        if (typeof(T) == typeof(string))
        {
            return (@this as string).IsStringLessThan(lowerBound as string);
        }

        return @this.CompareTo(lowerBound) < 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsLessThanOrEqual<T>(this T @this, T lowerBound) where T : IComparable<T>
    {
        if (typeof(T) == typeof(string))
        {
            return (@this as string).IsStringLessThanOrEqual(lowerBound as string);
        }

        return @this.CompareTo(lowerBound) <= 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsStringMoreThan(this string? @this, string? upperBound)
    {
        return string.CompareOrdinal(@this, upperBound) > 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsStringMoreThanOrEqual(this string? @this, string? upperBound)
    {
        return string.CompareOrdinal(@this, upperBound) >= 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsMoreThan<T>(this T @this, T upperBound) where T : IComparable<T>
    {
        if (typeof(T) == typeof(string))
        {
            return (@this as string).IsStringMoreThan(upperBound as string);
        }

        return @this.CompareTo(upperBound) > 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsMoreThanOrEqual<T>(this T @this, T upperBound) where T : IComparable<T>
    {
        if (typeof(T) == typeof(string))
        {
            return (@this as string).IsStringMoreThanOrEqual(upperBound as string);
        }

        return @this.CompareTo(upperBound) >= 0;
    }
}
