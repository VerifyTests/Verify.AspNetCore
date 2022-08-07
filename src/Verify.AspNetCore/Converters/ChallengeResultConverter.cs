using Microsoft.AspNetCore.Mvc;

class ChallengeResultConverter :
    ResultConverter<ChallengeResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, ChallengeResult result)
    {
        if (result.AuthenticationSchemes.Count == 1)
        {
            writer.WriteMember(result, result.AuthenticationSchemes.Single(), "Scheme");
        }
        else
        {
            writer.WriteMember(result, result.AuthenticationSchemes, "Schemes");
        }

        var properties = result.Properties;
        if (properties != null && properties.Items.Any())
        {
            writer.WriteMember(result, properties.Items, "Properties");
        }
    }
}