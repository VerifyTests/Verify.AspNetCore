using Microsoft.AspNetCore.Http.HttpResults;

class EmptyHttpResultConverter :
    ResultConverter<EmptyHttpResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, EmptyHttpResult result)
    {
    }
}
