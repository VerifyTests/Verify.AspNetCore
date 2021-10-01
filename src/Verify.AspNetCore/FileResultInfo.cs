using Microsoft.Net.Http.Headers;

class FileResultInfo
{
    public string FileDownloadName { get; set; } = null!;
    public DateTimeOffset? LastModified { get; set; }
    public EntityTagHeaderValue? EntityTag { get; set; }
    public bool EnableRangeProcessing { get; set; }
    public string ContentType { get; set; } = null!;
}