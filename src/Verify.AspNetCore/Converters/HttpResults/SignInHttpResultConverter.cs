using Microsoft.AspNetCore.Http.HttpResults;

class SignInHttpResultConverter :
    ResultConverter<SignInHttpResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, SignInHttpResult result)
    {
        writer.WriteMember(result, result.AuthenticationScheme, "Scheme");
        writer.WriteMember(result, result.Principal, "Principal");
        AuthenticationWriter.WriteProperties(writer, result, result.Properties);
    }
}
