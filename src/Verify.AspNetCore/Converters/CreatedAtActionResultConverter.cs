using Microsoft.AspNetCore.Mvc;

class CreatedAtActionResultConverter :
    ResultConverter<CreatedAtActionResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, CreatedAtActionResult result)
    {
        writer.WriteProperty(result, result.ActionName, "ActionName");
        writer.WriteProperty(result, result.ControllerName, "ControllerName");
        var values = result.RouteValues;
        if (values != null && values.Any())
        {
            writer.WriteProperty(result, values.ToDictionary(x => x.Key, x => x.Value), "RouteValues");
        }

        ObjectResultConverter.WriteObjectResult(writer, result);
    }
}