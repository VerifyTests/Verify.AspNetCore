using Microsoft.AspNetCore.Mvc;

static class FileResultConverter
{
    public static void WriteFileData(VerifyJsonWriter writer, FileResult result)
    {
        writer.WriteProperty(result, result.FileDownloadName, "FileDownloadName");
        writer.WriteProperty(result, result.LastModified, "LastModified");
        writer.WriteProperty(result, result.EntityTag, "EntityTag");
        writer.WriteProperty(result, result.EnableRangeProcessing, "EnableRangeProcessing");
        writer.WriteProperty(result, result.ContentType, "ContentType");
    }
}