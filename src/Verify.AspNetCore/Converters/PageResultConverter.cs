using Microsoft.AspNetCore.Mvc.RazorPages;

class PageResultConverter :
    ResultConverter<PageResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, PageResult result)
    {
        writer.WriteMember(result, result.StatusCode, "StatusCode");
        writer.WriteMember(result, result.ContentType, "ContentType");
        writer.WriteMember(result, result.Model, "Model");

        if (result.ViewData.Count == 0)
        {
            return;
        }

        writer.WriteMember(result, result.ViewData.ToDictionary(_ => _.Key, _ => _.Value), "ViewData");
    }
}