class FileStreamResultConverter :
    ResultConverter<FileStreamResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, FileStreamResult result) =>
        //TODO: do stream
        FileResultConverter.WriteFileData(writer, result);
}