using Microsoft.AspNetCore.Mvc;

class SignInResultConverter :
    ResultConverter<SignInResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, SignInResult result)
    {
        writer.WriteMember(result, result.AuthenticationScheme, "Scheme");
        //TODO: Claims
        //serializer.Serialize(writer, result.Principal.Claims);
        var properties = result.Properties;
        if (properties != null && properties.Items.Any())
        {
            writer.WriteMember(result, properties.Items, "Properties");
        }
    }
}