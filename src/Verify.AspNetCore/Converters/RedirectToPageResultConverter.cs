using Microsoft.AspNetCore.Mvc;

class RedirectToPageResultConverter :
    ResultConverter<RedirectToPageResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, RedirectToPageResult result)
    {
        writer.WriteMember(result, result.Host, "Host");
        writer.WriteMember(result, result.Fragment, "Fragment");
        writer.WriteMember(result, result.Protocol, "Protocol");
        writer.WriteMember(result, result.PreserveMethod, "PreserveMethod");
        writer.WriteMember(result, result.PageHandler, "PageHandler");
        writer.WriteMember(result, result.PageName, "PageName");
        var values = result.RouteValues;
        if (values == null || values.Count == 0)
        {
            return;
        }

        writer.WriteMember(result, values.ToDictionary(_ => _.Key, _ => _.Value), "RouteValues");
    }
}