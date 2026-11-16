class EmptyHttpResultConverter :
    ResultConverter<EmptyHttpResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, EmptyHttpResult result)
    {
    }
}
