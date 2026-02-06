namespace Offgrid.Framework.AspNetCore;

public static class ProblemTypeNames
{
    public const string Status400BadRequest = "https://tools.ietf.org/html/rfc9110#section-15.5.1";
    public const string Status401Unauthorized = "https://tools.ietf.org/html/rfc9110#section-15.5.2";
    public const string Status422UnprocessableContent = "https://tools.ietf.org/html/rfc9110#name-422-unprocessable-content";
    public const string Status500InternalServerError = "https://tools.ietf.org/html/rfc9110#section-15.6.1";
}