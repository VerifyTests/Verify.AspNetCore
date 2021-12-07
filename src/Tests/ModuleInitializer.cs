public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Initialize()
    {
        #region Enable
        VerifyAspNetCore.Enable();
        #endregion
    }
}