using Microsoft.AspNetCore.Http.HttpResults;

class SignOutHttpResultConverter :
    ResultConverter<SignOutHttpResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, SignOutHttpResult result)
    {
        AuthenticationWriter.WriteSchemes(writer, result, result.AuthenticationSchemes);
        AuthenticationWriter.WriteProperties(writer, result, result.Properties);
    }
}
