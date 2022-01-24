using Microsoft.AspNetCore.Mvc;

class ChallengeResultConverter :
    ResultConverter<ChallengeResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, ChallengeResult result)
    {
        if (result.AuthenticationSchemes.Count == 1)
        {
            writer.WriteProperty(result, result.AuthenticationSchemes.Single(), "Scheme");
        }
        else
        {
            writer.WriteProperty(result, result.AuthenticationSchemes, "Schemes");
        }

        var properties = result.Properties;
        if (properties != null && properties.Items.Any())
        {
            writer.WriteProperty(result, properties.Items, "Properties");
        }
    }
}