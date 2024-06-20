class VirtualFileResultConverter :
    ResultConverter<VirtualFileResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, VirtualFileResult result)
    {
        FileResultConverter.WriteFileData(writer, result);
        writer.WriteMember(result, result.FileName, "FileName");
    }
}