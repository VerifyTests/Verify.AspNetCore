class StatusCodeHttpResultConverter() :
    HttpResultConverter(
        typeof(Ok),
        typeof(Ok<>),
        typeof(BadRequest),
        typeof(BadRequest<>),
        typeof(Conflict),
        typeof(Conflict<>),
        typeof(NotFound),
        typeof(NotFound<>),
        typeof(UnprocessableEntity),
        typeof(UnprocessableEntity<>),
        typeof(InternalServerError),
        typeof(InternalServerError<>),
        typeof(NoContent),
        typeof(UnauthorizedHttpResult),
        typeof(StatusCodeHttpResult),
        // The events are not exposed, so only the status code can be shown.
        typeof(ServerSentEventsResult<>))
{
    protected override void InnerWrite(VerifyJsonWriter writer, IResult result)
    {
        WriteValue(writer, result);
        WriteStatusCode(writer, result);
    }
}
