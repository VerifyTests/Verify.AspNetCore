using Microsoft.AspNetCore.Mvc;

class RedirectToPageResultConverter :
    ResultConverter<RedirectToPageResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, RedirectToPageResult result)
    {
        writer.WriteProperty(result, result.Host, "Host");
        writer.WriteProperty(result, result.Fragment, "Fragment");
        writer.WriteProperty(result, result.Protocol, "Protocol");
        writer.WriteProperty(result, result.PreserveMethod, "PreserveMethod");
        writer.WriteProperty(result, result.PageHandler, "PageHandler");
        writer.WriteProperty(result, result.PageName, "PageName");
        var values = result.RouteValues;
        if (values != null && values.Any())
        {
            writer.WriteProperty(result, values.ToDictionary(x => x.Key, x => x.Value), "RouteValues");
        }
    }
}