

// Also covers RazorComponentResult<TComponent>, which derives from RazorComponentResult.
class RazorComponentResultConverter :
    ResultConverter<RazorComponentResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, RazorComponentResult result)
    {
        writer.WriteMember(result, result.ComponentType, "ComponentType");
        writer.WriteMember(result, result.StatusCode, "StatusCode");
        writer.WriteMember(result, result.ContentType, "ContentType");
        writer.WriteMember(result, result.PreventStreamingRendering, "PreventStreamingRendering");
        if (result.Parameters.Count != 0)
        {
            writer.WriteMember(result, result.Parameters, "Parameters");
        }
    }
}
