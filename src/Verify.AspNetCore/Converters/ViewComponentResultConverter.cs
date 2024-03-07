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
            writer.WriteMember(result, result.TempData.ToDictionary(_ => _.Key, _ => _.Value), "TempData");
        }

        if (result.ViewData.Count != 0)
        {
            writer.WriteMember(result, result.ViewData.ToDictionary(_ => _.Key, _ => _.Value), "ViewData");
        }

        writer.WriteMember(result, result.Arguments, "Arguments");
        writer.WriteMember(result, result.Model, "Model");
    }
}