class PhysicalFileResultConverter :
    ResultConverter<PhysicalFileResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, PhysicalFileResult result)
    {
        FileResultConverter.WriteFileData(writer, result);
        writer.WriteMember(result, result.FileName, "FileName");
    }
}