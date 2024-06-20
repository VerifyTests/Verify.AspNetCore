class RedirectToActionResultConverter :
    ResultConverter<RedirectToActionResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, RedirectToActionResult result)
    {
        writer.WriteMember(result, result.ActionName, "ActionName");
        writer.WriteMember(result, result.ControllerName, "ControllerName");
        writer.WriteMember(result, result.Fragment, "Fragment");
        writer.WriteMember(result, result.Permanent, "Permanent");
        writer.WriteMember(result, result.PreserveMethod, "PreserveMethod");
        var values = result.RouteValues;
        if (values == null || values.Count == 0)
        {
            return;
        }

        writer.WriteMember(result, values.ToDictionary(_ => _.Value!, _ => _.Value), "RouteValues");
    }
}