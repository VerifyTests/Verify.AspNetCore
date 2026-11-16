using Microsoft.AspNetCore.Authentication;

// Writes schemes and properties the same way as the MVC ChallengeResult, ForbidResult and SignOutResult converters.
static class AuthenticationWriter
{
    public static void WriteSchemes(VerifyJsonWriter writer, object result, IReadOnlyList<string> schemes)
    {
        if (schemes.Count == 1)
        {
            writer.WriteMember(result, schemes[0], "Scheme");
        }
        else
        {
            writer.WriteMember(result, schemes, "Schemes");
        }
    }

    public static void WriteProperties(VerifyJsonWriter writer, object result, AuthenticationProperties? properties)
    {
        if (properties != null &&
            properties.Items.Count != 0)
        {
            writer.WriteMember(result, properties.Items, "Properties");
        }
    }
}
