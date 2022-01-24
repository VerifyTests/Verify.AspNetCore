using Microsoft.AspNetCore.Mvc;

class SignInResultConverter :
    ResultConverter<SignInResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, SignInResult result)
    {
        writer.WriteProperty(result, result.AuthenticationScheme, "Scheme");
        //TODO: Claims
        //serializer.Serialize(writer, result.Principal.Claims);
        var properties = result.Properties;
        if (properties != null && properties.Items.Any())
        {
            writer.WriteProperty(result, properties.Items, "Properties");
        }
    }
}