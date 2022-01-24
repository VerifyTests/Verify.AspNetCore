using Microsoft.AspNetCore.Mvc;

class AcceptedAtActionResultConverter :
    ResultConverter<AcceptedAtActionResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, AcceptedAtActionResult result)
    {
        writer.WriteProperty(result, result.ActionName, "ActionName");
        writer.WriteProperty(result, result.ControllerName, "ControllerName");
        var values = result.RouteValues;
        if (values != null && values.Any())
        {
            writer.WriteProperty(result, values.ToDictionary(x => x.Key, x => x.Value), "RouteValues");
        }
    }
}