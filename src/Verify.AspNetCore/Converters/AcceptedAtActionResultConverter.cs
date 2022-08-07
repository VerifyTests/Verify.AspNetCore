using Microsoft.AspNetCore.Mvc;

class AcceptedAtActionResultConverter :
    ResultConverter<AcceptedAtActionResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, AcceptedAtActionResult result)
    {
        writer.WriteMember(result, result.ActionName, "ActionName");
        writer.WriteMember(result, result.ControllerName, "ControllerName");
        var values = result.RouteValues;
        if (values != null && values.Any())
        {
            writer.WriteMember(result, values.ToDictionary(x => x.Key, x => x.Value), "RouteValues");
        }
    }
}