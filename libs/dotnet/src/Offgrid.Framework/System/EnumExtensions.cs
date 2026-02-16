namespace Offgrid.Framework.System;

public static class EnumExtensions
{
    public static string ToCommaSeparatedList<TEnum>() where TEnum : Enum
    {
        return string.Join(", ", Enum.GetNames(typeof(TEnum)));
    }
}
