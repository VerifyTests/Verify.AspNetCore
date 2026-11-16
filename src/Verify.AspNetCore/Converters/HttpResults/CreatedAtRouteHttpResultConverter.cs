using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;

class CreatedAtRouteHttpResultConverter() :
    HttpResultConverter(
        typeof(CreatedAtRoute),
        typeof(CreatedAtRoute<>),
        typeof(AcceptedAtRoute),
        typeof(AcceptedAtRoute<>))
{
    protected override void InnerWrite(VerifyJsonWriter writer, IResult result)
    {
        writer.WriteMember(result, GetProperty(result, "RouteName"), "RouteName");
        var values = (RouteValueDictionary) GetProperty(result, "RouteValues")!;
        if (values.Count != 0)
        {
            writer.WriteMember(result, values.ToDictionary(_ => _.Key, _ => _.Value), "RouteValues");
        }

        WriteValue(writer, result);
        WriteStatusCode(writer, result);
    }
}
