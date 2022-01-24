using Microsoft.AspNetCore.Mvc;

class PartialViewResultConverter :
    ResultConverter<PartialViewResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, PartialViewResult result)
    {
        writer.WriteProperty(result, result.StatusCode, "StatusCode");
        writer.WriteProperty(result, result.ContentType, "ContentType");
        writer.WriteProperty(result, result.ViewName, "ViewName");
        writer.WriteProperty(result, result.Model, "Model");
    }
}