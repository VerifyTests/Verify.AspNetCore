using Microsoft.AspNetCore.Mvc;

class ViewComponentResultConverter :
    ResultConverter<ViewComponentResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, ViewComponentResult result)
    {
        writer.WriteProperty(result, result.StatusCode, "StatusCode");
        writer.WriteProperty(result, result.ContentType, "ContentType");
        writer.WriteProperty(result, result.ViewComponentName, "ViewComponentName");
        writer.WriteProperty(result, result.ViewComponentType?.FullName, "ViewComponentType");

        if (result.TempData.Any())
        {
            writer.WriteProperty(result, result.TempData.ToDictionary(x => x.Key, x => x.Value), "TempData");
        }

        if (result.ViewData.Any())
        {
            writer.WriteProperty(result, result.ViewData.ToDictionary(x => x.Key, x => x.Value), "ViewData");
        }

        writer.WriteProperty(result, result.Arguments, "Arguments");
        writer.WriteProperty(result, result.Model, "Model");
    }
}