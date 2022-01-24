using Microsoft.AspNetCore.Mvc;

class RedirectToActionResultConverter :
    ResultConverter<RedirectToActionResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, RedirectToActionResult result)
    {
        writer.WriteProperty(result, result.ActionName, "ActionName");
        writer.WriteProperty(result, result.ControllerName, "ControllerName");
        writer.WriteProperty(result, result.Fragment, "Fragment");
        writer.WriteProperty(result, result.Permanent, "Permanent");
        writer.WriteProperty(result, result.PreserveMethod, "PreserveMethod");
        var values = result.RouteValues;
        if (values != null && values.Any())
        {
            writer.WriteProperty(result, values.ToDictionary(x => x.Value!, x => x.Value), "RouteValues");
        }
    }
}