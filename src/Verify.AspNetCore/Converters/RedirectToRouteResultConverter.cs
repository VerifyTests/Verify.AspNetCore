using Microsoft.AspNetCore.Mvc;

class RedirectToRouteResultConverter :
    ResultConverter<RedirectToRouteResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, RedirectToRouteResult result)
    {
        writer.WriteMember(result, result.Fragment, "Fragment");
        writer.WriteMember(result, result.Permanent, "Permanent");
        writer.WriteMember(result, result.PreserveMethod, "PreserveMethod");
        writer.WriteMember(result, result.RouteName, "RouteName");
        var values = result.RouteValues;
        if (values != null && values.Any())
        {
            writer.WriteMember(result, values.ToDictionary(_ => _.Key, _ => _.Value), "RouteValues");
        }
    }
}