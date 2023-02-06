public static class ModuleInitializer
{
    #region Enable

    [ModuleInitializer]
    public static void Initialize() =>
        VerifyAspNetCore.Initialize();

    #endregion

    [ModuleInitializer]
    public static void InitializeOther()
    {
        VerifyDiffPlex.Initialize();
        VerifierSettings.InitializePlugins();
    }
}