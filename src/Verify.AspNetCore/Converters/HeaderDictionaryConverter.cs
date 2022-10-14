class HeaderDictionaryConverter :
    WriteOnlyJsonConverter<IHeaderDictionary>
{
    public override void Write(VerifyJsonWriter writer, IHeaderDictionary value) =>
        writer.Serialize(value
            .ToDictionary(
                _ => _.Key,
                _ => _.Value.ToString()));
}