class RedirectToRouteHttpResultConverter :
    ResultConverter<RedirectToRouteHttpResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, RedirectToRouteHttpResult result)
    {
        writer.WriteMember(result, result.Fragment, "Fragment");
        writer.WriteMember(result, result.Permanent, "Permanent");
        writer.WriteMember(result, result.PreserveMethod, "PreserveMethod");
        writer.WriteMember(result, result.RouteName, "RouteName");
        var values = result.RouteValues;
        if (values.Count == 0)
        {
            return;
        }

        writer.WriteMember(result, values.ToDictionary(_ => _.Key, _ => _.Value), "RouteValues");
    }
}
