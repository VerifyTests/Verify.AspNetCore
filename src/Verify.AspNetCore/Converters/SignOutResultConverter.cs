using Microsoft.AspNetCore.Mvc;

class SignOutResultConverter :
    ResultConverter<SignOutResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, SignOutResult result)
    {
        if (result.AuthenticationSchemes.Count == 1)
        {
            writer.WriteProperty(result, result.AuthenticationSchemes.Single(), "Scheme");
        }
        else
        {
            writer.WriteProperty(result, result.AuthenticationSchemes, "Schemes");
        }

        //TODO: Claims
        //serializer.Serialize(writer, result.Principal.Claims);
        var properties = result.Properties;
        if (properties != null && properties.Items.Any())
        {
            writer.WriteProperty(result, properties.Items, "Properties");
        }
    }
}