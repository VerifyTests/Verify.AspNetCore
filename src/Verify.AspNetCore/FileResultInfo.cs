using Microsoft.Net.Http.Headers;

record FileResultInfo(
    string FileDownloadName,
    DateTimeOffset? LastModified,
    EntityTagHeaderValue? EntityTag,
    bool EnableRangeProcessing,
    string ContentType);