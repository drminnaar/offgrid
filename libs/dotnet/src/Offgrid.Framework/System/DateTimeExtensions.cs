namespace Offgrid.Framework.System;

public static class DateTimeExtensions
{
    extension(DateTime source)
    {
        public long ToUnixTimeSeconds()
        {
            var utcDateTime = source.Kind == DateTimeKind.Utc
                ? source
                : source.ToUniversalTime();

            return new DateTimeOffset(utcDateTime).ToUnixTimeSeconds();
        }
    }
}
