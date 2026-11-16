// Results<TResult1, TResult2> and its larger forms wrap the result that was returned.
// Write that result in place of the wrapper, the same way ActionResult<T> is unwrapped.
class NestedHttpResultConverter :
    WriteOnlyJsonConverter<INestedHttpResult>
{
    public override void Write(VerifyJsonWriter writer, INestedHttpResult result) =>
        writer.Serialize(result.Result);
}
