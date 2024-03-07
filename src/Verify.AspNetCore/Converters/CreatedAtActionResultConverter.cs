using Microsoft.AspNetCore.Mvc;

class CreatedAtActionResultConverter :
    ResultConverter<CreatedAtActionResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, CreatedAtActionResult result)
    {
        writer.WriteMember(result, result.ActionName, "ActionName");
        writer.WriteMember(result, result.ControllerName, "ControllerName");
        var values = result.RouteValues;
        if (values != null && values.Count != 0)
        {
            writer.WriteMember(result, values.ToDictionary(_ => _.Key, _ => _.Value), "RouteValues");
        }

        ObjectResultConverter.WriteObjectResult(writer, result);
    }
}