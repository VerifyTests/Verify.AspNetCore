using System.Diagnostics.CodeAnalysis;
using EmptyFiles;
using Microsoft.AspNetCore.Http.HttpResults;

namespace VerifyTests;

public static partial class VerifyAspNetCore
{
    public static bool Initialized { get; private set; }

    public static void Initialize()
    {
        if (Initialized)
        {
            throw new("Already Initialized");
        }

        Initialized = true;

        InnerVerifier.ThrowIfVerifyHasBeenRun();
        VerifierSettings.RegisterFileConverter<FileStreamResult>(ConvertFileResult);
        VerifierSettings.RegisterFileConverter<FileContentResult>(ConvertFileResult);
        VerifierSettings.RegisterFileConverter<PhysicalFileResult>(ConvertFileResult);
        VerifierSettings.RegisterFileConverter<VirtualFileResult>(ConvertFileResult);
        VerifierSettings.RegisterFileConverter<FileStreamHttpResult>(ConvertFileResult);
        VerifierSettings.RegisterFileConverter<FileContentHttpResult>(ConvertFileResult);
        VerifierSettings.RegisterFileConverter<PhysicalFileHttpResult>(ConvertFileResult);
        VerifierSettings.RegisterFileConverter<VirtualFileHttpResult>(ConvertFileResult);
        VerifierSettings.AddExtraSettings(serializer =>
        {
            var converters = serializer.Converters;
            converters.Add(new HttpContextConverter());
            converters.Add(new HttpResponseConverter());
            converters.Add(new HeaderDictionaryConverter());
            converters.Add(new HttpRequestConverter());
            converters.Add(new ControllerContextConverter());
            converters.Add(new ActionResultConverter());

            // MVC results. A converter also matches subclasses of its type, and the first match wins,
            // so the converter for a subclass has to be added before the converter for its base type.

            // ObjectResult subclasses, then ObjectResult
            converters.Add(new AcceptedAtActionResultConverter());
            converters.Add(new AcceptedAtRouteResultConverter());
            converters.Add(new AcceptedResultConverter());
            converters.Add(new BadRequestObjectResultConverter());
            converters.Add(new ConflictObjectResultConverter());
            converters.Add(new CreatedAtActionResultConverter());
            converters.Add(new CreatedAtRouteResultConverter());
            converters.Add(new CreatedResultConverter());
            converters.Add(new NotFoundObjectResultConverter());
            converters.Add(new OkObjectResultConverter());
            converters.Add(new UnauthorizedObjectResultConverter());
            converters.Add(new UnprocessableEntityObjectResultConverter());
            converters.Add(new ObjectResultConverter());

            // StatusCodeResult subclasses, then StatusCodeResult
            converters.Add(new BadRequestResultConverter());
            converters.Add(new ConflictResultConverter());
            converters.Add(new NoContentResultConverter());
            converters.Add(new NotFoundResultConverter());
            converters.Add(new OkResultConverter());
            converters.Add(new UnauthorizedResultConverter());
            converters.Add(new UnprocessableEntityResultConverter());
            converters.Add(new UnsupportedMediaTypeResultConverter());
            converters.Add(new StatusCodeResultConverter());

            converters.Add(new ChallengeResultConverter());
            converters.Add(new ContentResultConverter());
            converters.Add(new EmptyResultConverter());
            converters.Add(new FileContentResultConverter());
            converters.Add(new FileStreamResultConverter());
            converters.Add(new PhysicalFileResultConverter());
            converters.Add(new VirtualFileResultConverter());
            converters.Add(new ForbidResultConverter());
            converters.Add(new JsonResultConverter());
            converters.Add(new LocalRedirectResultConverter());
            converters.Add(new PartialViewResultConverter());
            converters.Add(new RedirectResultConverter());
            converters.Add(new RedirectToActionResultConverter());
            converters.Add(new RedirectToPageResultConverter());
            converters.Add(new RedirectToRouteResultConverter());
            converters.Add(new SignInResultConverter());
            converters.Add(new SignOutResultConverter());
            converters.Add(new ViewComponentResultConverter());
            converters.Add(new ViewResultConverter());
            converters.Add(new PageResultConverter());

            // Minimal API results (Microsoft.AspNetCore.Http.HttpResults)
            converters.Add(new NestedHttpResultConverter());
            converters.Add(new StatusCodeHttpResultConverter());
            converters.Add(new CreatedHttpResultConverter());
            converters.Add(new CreatedAtRouteHttpResultConverter());
            converters.Add(new JsonHttpResultConverter());
            converters.Add(new ProblemHttpResultConverter());
            converters.Add(new ContentHttpResultConverter());
            converters.Add(new Utf8ContentHttpResultConverter());
            converters.Add(new ChallengeHttpResultConverter());
            converters.Add(new ForbidHttpResultConverter());
            converters.Add(new SignInHttpResultConverter());
            converters.Add(new SignOutHttpResultConverter());
            converters.Add(new RedirectHttpResultConverter());
            converters.Add(new RedirectToRouteHttpResultConverter());
            converters.Add(new EmptyHttpResultConverter());
            converters.Add(new FileHttpResultConverter());
            converters.Add(new RazorComponentResultConverter());
        });
    }

    static ConversionResult ConvertFileResult(FileContentResult target, IReadOnlyDictionary<string, object> context) =>
        ConvertContents(GetFileResultInfo(target), target.FileContents);

    static ConversionResult ConvertFileResult(FileContentHttpResult target, IReadOnlyDictionary<string, object> context) =>
        ConvertContents(GetFileResultInfo(target), target.FileContents);

    static Task<ConversionResult> ConvertFileResult(VirtualFileResult target, IReadOnlyDictionary<string, object> context) =>
        ConvertFile(GetFileResultInfo(target), target.FileName);

    static Task<ConversionResult> ConvertFileResult(VirtualFileHttpResult target, IReadOnlyDictionary<string, object> context) =>
        ConvertFile(GetFileResultInfo(target), target.FileName);

    static Task<ConversionResult> ConvertFileResult(PhysicalFileResult target, IReadOnlyDictionary<string, object> context) =>
        ConvertFile(GetFileResultInfo(target), target.FileName);

    static Task<ConversionResult> ConvertFileResult(PhysicalFileHttpResult target, IReadOnlyDictionary<string, object> context) =>
        ConvertFile(GetFileResultInfo(target), target.FileName);

    static Task<ConversionResult> ConvertFileResult(FileStreamResult target, IReadOnlyDictionary<string, object> context) =>
        ConvertStream(GetFileResultInfo(target), target.FileStream);

    static Task<ConversionResult> ConvertFileResult(FileStreamHttpResult target, IReadOnlyDictionary<string, object> context) =>
        ConvertStream(GetFileResultInfo(target), target.FileStream);

    static ConversionResult ConvertContents(FileResultInfo info, ReadOnlyMemory<byte> contents)
    {
        if (!TryGetExtension(info, out var extension))
        {
            return new(info, []);
        }

        if (FileExtensions.IsTextExtension(extension))
        {
            return new(info, extension, Encoding.UTF8.GetString(contents.Span));
        }

        return new(info, extension, new MemoryStream(contents.ToArray()));
    }

    static async Task<ConversionResult> ConvertFile(FileResultInfo info, string path)
    {
        if (!TryGetExtension(info, out var extension))
        {
            return new(info, []);
        }

        if (FileExtensions.IsTextExtension(extension))
        {
            return new(info, extension, await File.ReadAllTextAsync(path));
        }

        return new(info, extension, File.OpenRead(path));
    }

    static async Task<ConversionResult> ConvertStream(FileResultInfo info, Stream stream)
    {
        if (!TryGetExtension(info, out var extension))
        {
            return new(info, []);
        }

        if (FileExtensions.IsTextExtension(extension))
        {
            return new(info, extension, await stream.ReadAsStringAsync());
        }

        return new(info, extension, stream);
    }

    static bool TryGetExtension(FileResultInfo info, [NotNullWhen(true)] out string? extension)
    {
        if (info.ContentType == null)
        {
            extension = null;
            return false;
        }

        return HttpExtensions.TryGetMediaTypeExtension(info.ContentType, out extension);
    }

    static FileResultInfo GetFileResultInfo(FileResult target) =>
        new(
            target.FileDownloadName,
            target.LastModified,
            target.EntityTag,
            target.EnableRangeProcessing,
            target.ContentType
        );

    static FileResultInfo GetFileResultInfo(IFileHttpResult target) =>
        FileHttpResultConverter.GetInfo(target);

    public static void ScrubAspTextResponse(
        this VerifySettings settings,
        Func<string, string> scrub) =>
        settings.Context["ScrubHttpResponseKey"] = scrub;

    public static SettingsTask ScrubAspTextResponse(
        this SettingsTask settings,
        Func<string, string> scrub)
    {
        settings.CurrentSettings.ScrubAspTextResponse(scrub);
        return settings;
    }

    internal static bool HasHttpTextResponseScrubber(this VerifyJsonWriter writer) =>
        writer.Context.ContainsKey("ScrubHttpResponseKey");

    internal static bool TryGetHttpTextResponseScrubber(
        this VerifyJsonWriter writer,
        [NotNullWhen(true)] out Func<string, string>? scrubber)
    {
        if (writer.Context.TryGetValue("ScrubHttpResponseKey", out var scrubberValue))
        {
            scrubber = (Func<string, string>) scrubberValue;
            return true;
        }

        scrubber = null;
        return false;
    }
}