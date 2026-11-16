using Microsoft.AspNetCore.Http.HttpResults;

class ForbidHttpResultConverter :
    ResultConverter<ForbidHttpResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, ForbidHttpResult result)
    {
        AuthenticationWriter.WriteSchemes(writer, result, result.AuthenticationSchemes);
        AuthenticationWriter.WriteProperties(writer, result, result.Properties);
    }
}
