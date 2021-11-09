using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

class ChallengeResultConverter :
    ResultConverter<ChallengeResult>
{
    protected override void InnerWrite(JsonWriter writer, ChallengeResult result, JsonSerializer serializer)
    {
        if (result.AuthenticationSchemes.Count == 1)
        {
            writer.WritePropertyName("Scheme");
            serializer.Serialize(writer, result.AuthenticationSchemes.Single());
        }
        else
        {
            writer.WritePropertyName("Schemes");
            serializer.Serialize(writer, result.AuthenticationSchemes);
        }

        var properties = result.Properties;
        if (properties != null && properties.Items.Any())
        {
            writer.WritePropertyName("Properties");
            serializer.Serialize(writer, properties.Items);
        }
    }
}