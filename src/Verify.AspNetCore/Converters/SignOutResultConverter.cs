using Microsoft.AspNetCore.Mvc;

class SignOutResultConverter :
    ResultConverter<SignOutResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, SignOutResult result)
    {
        if (result.AuthenticationSchemes.Count == 1)
        {
            writer.WriteMember(result, result.AuthenticationSchemes.Single(), "Scheme");
        }
        else
        {
            writer.WriteMember(result, result.AuthenticationSchemes, "Schemes");
        }

        //TODO: Claims
        //serializer.Serialize(writer, result.Principal.Claims);
        var properties = result.Properties;
        if (properties != null && properties.Items.Any())
        {
            writer.WriteMember(result, properties.Items, "Properties");
        }
    }
}