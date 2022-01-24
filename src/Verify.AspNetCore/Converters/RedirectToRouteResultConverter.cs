using Microsoft.AspNetCore.Mvc;

class RedirectToRouteResultConverter :
    ResultConverter<RedirectToRouteResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, RedirectToRouteResult result)
    {
        writer.WriteProperty(result, result.Fragment, "Fragment");
        writer.WriteProperty(result, result.Permanent, "Permanent");
        writer.WriteProperty(result, result.PreserveMethod, "PreserveMethod");
        writer.WriteProperty(result, result.RouteName, "RouteName");
        var values = result.RouteValues;
        if (values != null && values.Any())
        {
            writer.WriteProperty(result, values.ToDictionary(x => x.Key, x => x.Value), "RouteValues");
        }
    }
}