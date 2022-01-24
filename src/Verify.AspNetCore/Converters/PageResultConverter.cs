using Microsoft.AspNetCore.Mvc.RazorPages;

class PageResultConverter :
    ResultConverter<PageResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, PageResult result)
    {
        writer.WriteProperty(result, result.StatusCode, "StatusCode");
        writer.WriteProperty(result, result.ContentType, "ContentType");
        writer.WriteProperty(result, result.Model, "Model");

        if (result.ViewData.Any())
        {
            writer.WriteProperty(result, result.ViewData.ToDictionary(x => x.Key, x => x.Value), "ViewData");
        }
    }
}