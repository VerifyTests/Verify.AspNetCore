using Microsoft.AspNetCore.Mvc;

class FileContentResultConverter :
    ResultConverter<FileContentResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, FileContentResult result) =>
        FileResultConverter.WriteFileData(writer, result);
}