public static class ModuleInitializer
{
    #region Enable

    [ModuleInitializer]
    public static void Initialize() =>
        VerifyAspNetCore.Enable();

    #endregion

    [ModuleInitializer]
    public static void InitializeOther() =>
        VerifyDiffPlex.Initialize();
}