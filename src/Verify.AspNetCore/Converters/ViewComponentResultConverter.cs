using Microsoft.AspNetCore.Mvc;

class ViewComponentResultConverter :
    ResultConverter<ViewComponentResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, ViewComponentResult result)
    {
        writer.WriteMember(result, result.StatusCode, "StatusCode");
        writer.WriteMember(result, result.ContentType, "ContentType");
        writer.WriteMember(result, result.ViewComponentName, "ViewComponentName");
        writer.WriteMember(result, result.ViewComponentType?.FullName, "ViewComponentType");

        if (result.TempData.Any())
        {
            writer.WriteMember(result, result.TempData.ToDictionary(x => x.Key, x => x.Value), "TempData");
        }

        if (result.ViewData.Any())
        {
            writer.WriteMember(result, result.ViewData.ToDictionary(x => x.Key, x => x.Value), "ViewData");
        }

        writer.WriteMember(result, result.Arguments, "Arguments");
        writer.WriteMember(result, result.Model, "Model");
    }
}