static class ResultTypeName
{
    // Generic results are named without their arity: Ok<TValue> is Ok rather than Ok`1.
    public static string Get(Type type)
    {
        var name = type.Name;
        var index = name.IndexOf('`');
        if (index == -1)
        {
            return name;
        }

        return name[..index];
    }
}
