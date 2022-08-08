using Microsoft.AspNetCore.Mvc;

class ViewResultConverter :
    ResultConverter<ViewResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, ViewResult result)
    {
        writer.WriteMember(result, result.StatusCode, "StatusCode");
        writer.WriteMember(result, result.ContentType, "ContentType");
        writer.WriteMember(result, result.Model, "Model");
        if (result.ViewData.Any())
        {
            writer.WriteMember(result, result.ViewData.ToDictionary(x => x.Key, x => x.Value), "ViewData");
        }

        if (result.TempData.Any())
        {
            writer.WriteMember(result, result.TempData.ToDictionary(x => x.Key, x => x.Value), "TempData");
        }
    }
}