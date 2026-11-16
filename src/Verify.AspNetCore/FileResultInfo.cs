using Microsoft.Net.Http.Headers;

record FileResultInfo(
    string? FileDownloadName,
    DateTimeOffset? LastModified,
    EntityTagHeaderValue? EntityTag,
    bool EnableRangeProcessing,
    string? ContentType,
    // Only the minimal API file results have a FileLength.
    long? FileLength = null);
