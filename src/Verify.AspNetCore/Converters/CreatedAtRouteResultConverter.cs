class CreatedAtRouteResultConverter :
    ResultConverter<CreatedAtRouteResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, CreatedAtRouteResult result)
    {
        writer.WriteMember(result, result.RouteName, "RouteName");
        var values = result.RouteValues;
        if (values != null && values.Count != 0)
        {
            writer.WriteMember(result, values.ToDictionary(_ => _.Key, _ => _.Value), "RouteValues");
        }

        ObjectResultConverter.WriteObjectResult(writer, result);
    }
}