static class FileResultConverter
{
    public static void WriteFileData(VerifyJsonWriter writer, FileResult result)
    {
        writer.WriteMember(result, result.FileDownloadName, "FileDownloadName");
        writer.WriteMember(result, result.LastModified, "LastModified");
        writer.WriteMember(result, result.EntityTag, "EntityTag");
        writer.WriteMember(result, result.EnableRangeProcessing, "EnableRangeProcessing");
        writer.WriteMember(result, result.ContentType, "ContentType");
    }
}