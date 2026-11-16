using Microsoft.AspNetCore.Http.HttpResults;

// Covers FileContentHttpResult, FileStreamHttpResult, PhysicalFileHttpResult, VirtualFileHttpResult and
// PushStreamHttpResult. When one of the first four is verified directly, its content is written to its
// own file by the file converters registered in VerifyAspNetCore.Initialize.
class FileHttpResultConverter :
    ResultConverter<IFileHttpResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, IFileHttpResult result)
    {
        var info = GetInfo(result);
        writer.WriteMember(result, info.FileDownloadName, "FileDownloadName");
        writer.WriteMember(result, info.LastModified, "LastModified");
        writer.WriteMember(result, info.EntityTag, "EntityTag");
        writer.WriteMember(result, info.EnableRangeProcessing, "EnableRangeProcessing");
        writer.WriteMember(result, info.ContentType, "ContentType");
        writer.WriteMember(result, info.FileLength, "FileLength");
        if (result is PhysicalFileHttpResult physical)
        {
            writer.WriteMember(result, physical.FileName, "FileName");
        }
        else if (result is VirtualFileHttpResult virtualFile)
        {
            writer.WriteMember(result, virtualFile.FileName, "FileName");
        }
    }

    // IFileHttpResult only declares FileDownloadName and ContentType, so read the rest from each type.
    public static FileResultInfo GetInfo(IFileHttpResult result) =>
        result switch
        {
            FileContentHttpResult file => new(file.FileDownloadName, file.LastModified, file.EntityTag, file.EnableRangeProcessing, file.ContentType, file.FileLength),
            FileStreamHttpResult file => new(file.FileDownloadName, file.LastModified, file.EntityTag, file.EnableRangeProcessing, file.ContentType, file.FileLength),
            PhysicalFileHttpResult file => new(file.FileDownloadName, file.LastModified, file.EntityTag, file.EnableRangeProcessing, file.ContentType, file.FileLength),
            VirtualFileHttpResult file => new(file.FileDownloadName, file.LastModified, file.EntityTag, file.EnableRangeProcessing, file.ContentType, file.FileLength),
            PushStreamHttpResult file => new(file.FileDownloadName, file.LastModified, file.EntityTag, file.EnableRangeProcessing, file.ContentType, file.FileLength),
            _ => new(result.FileDownloadName, null, null, false, result.ContentType)
        };
}
