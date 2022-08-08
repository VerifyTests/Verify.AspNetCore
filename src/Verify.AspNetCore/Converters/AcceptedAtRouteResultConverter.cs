using Microsoft.AspNetCore.Mvc;

class AcceptedAtRouteResultConverter :
    ResultConverter<AcceptedAtRouteResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, AcceptedAtRouteResult result)
    {
        writer.WriteMember(result, result.RouteName, "RouteName");
        var values = result.RouteValues;
        if (values != null && values.Any())
        {
            writer.WriteMember(result, values.ToDictionary(x => x.Key, x => x.Value), "RouteValues");
        }
    }
}