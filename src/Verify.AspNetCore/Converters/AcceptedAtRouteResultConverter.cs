﻿class AcceptedAtRouteResultConverter :
    ResultConverter<AcceptedAtRouteResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, AcceptedAtRouteResult result)
    {
        writer.WriteMember(result, result.RouteName, "RouteName");
        var values = result.RouteValues;
        if (values == null || values.Count == 0)
        {
            return;
        }

        writer.WriteMember(result, values.ToDictionary(_ => _.Key, _ => _.Value), "RouteValues");
    }
}