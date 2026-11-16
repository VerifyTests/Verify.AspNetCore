class ForbidResultConverter :
    ResultConverter<ForbidResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, ForbidResult result)
    {
        if (result.AuthenticationSchemes.Count == 1)
        {
            writer.WriteMember(result, result.AuthenticationSchemes.Single(), "Scheme");
        }
        else
        {
            writer.WriteMember(result, result.AuthenticationSchemes, "Schemes");
        }

        var properties = result.Properties;
        if (properties != null &&
            properties.Items.Count != 0)
        {
            writer.WriteMember(result, properties.Items, "Properties");
        }
    }
}
