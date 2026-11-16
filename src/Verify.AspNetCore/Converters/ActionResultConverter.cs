class ActionResultConverter :
    WriteOnlyJsonConverter
{
    const BindingFlags flags = BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public;

    public override void Write(VerifyJsonWriter writer, object action)
    {
        var type = action.GetType();

        // An ActionResult<T> holds either a result (return NotFound()) or a value (return item).
        var result = type.GetProperty("Result", flags)!.GetValue(action);
        if (result != null)
        {
            writer.Serialize(result);
            return;
        }

        var value = type.GetProperty("Value", flags)!.GetValue(action)!;
        writer.Serialize(value);
    }

    public override bool CanConvert(Type type)
    {
        if (!type.IsGenericType)
        {
            return false;
        }

        var genericType = type.GetGenericTypeDefinition();
        return genericType == typeof(ActionResult<>);
    }
}
