using Microsoft.AspNetCore.Http.HttpResults;

class ChallengeHttpResultConverter :
    ResultConverter<ChallengeHttpResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, ChallengeHttpResult result)
    {
        AuthenticationWriter.WriteSchemes(writer, result, result.AuthenticationSchemes);
        AuthenticationWriter.WriteProperties(writer, result, result.Properties);
    }
}
