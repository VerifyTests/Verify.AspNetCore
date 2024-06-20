class AcceptedAtActionResultConverter :
    ResultConverter<AcceptedAtActionResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, AcceptedAtActionResult result)
    {
        writer.WriteMember(result, result.ActionName, "ActionName");
        writer.WriteMember(result, result.ControllerName, "ControllerName");
        var values = result.RouteValues;
        if (values == null || values.Count == 0)
        {
            return;
        }

        writer.WriteMember(result, values.ToDictionary(_ => _.Key, _ => _.Value), "RouteValues");
    }
}