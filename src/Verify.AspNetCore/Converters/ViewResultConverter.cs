using Microsoft.AspNetCore.Mvc;

class ViewResultConverter :
    ResultConverter<ViewResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, ViewResult result)
    {
        writer.WriteProperty(result, result.StatusCode, "StatusCode");
        writer.WriteProperty(result, result.ContentType, "ContentType");
        writer.WriteProperty(result, result.Model, "Model");
        if (result.ViewData.Any())
        {
            writer.WriteProperty(result, result.ViewData.ToDictionary(x => x.Key, x => x.Value), "ViewData");
        }

        if (result.TempData.Any())
        {
            writer.WriteProperty(result, result.TempData.ToDictionary(x => x.Key, x => x.Value), "TempData");
        }
    }
}