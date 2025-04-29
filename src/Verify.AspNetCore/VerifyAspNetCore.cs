﻿using System.Diagnostics.CodeAnalysis;
using EmptyFiles;

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
        VerifierSettings.AddExtraSettings(serializer =>
        {
            var converters = serializer.Converters;
            converters.Add(new HttpContextConverter());
            converters.Add(new HttpResponseConverter());
            converters.Add(new HeaderDictionaryConverter());
            converters.Add(new HttpRequestConverter());
            converters.Add(new ChallengeResultConverter());
            converters.Add(new ActionResultConverter());
            converters.Add(new ContentResultConverter());
            converters.Add(new ControllerContextConverter());
            converters.Add(new EmptyResultConverter());
            converters.Add(new FileContentResultConverter());
            converters.Add(new FileStreamResultConverter());
            converters.Add(new PhysicalFileResultConverter());
            converters.Add(new VirtualFileResultConverter());
            converters.Add(new ForbidResultConverter());
            converters.Add(new JsonResultConverter());
            converters.Add(new LocalRedirectResultConverter());
            converters.Add(new ObjectResultConverter());
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
            converters.Add(new PartialViewResultConverter());
            converters.Add(new RedirectResultConverter());
            converters.Add(new RedirectToActionResultConverter());
            converters.Add(new RedirectToPageResultConverter());
            converters.Add(new RedirectToRouteResultConverter());
            converters.Add(new SignInResultConverter());
            converters.Add(new SignOutResultConverter());
            converters.Add(new StatusCodeResultConverter());
            converters.Add(new BadRequestResultConverter());
            converters.Add(new ConflictResultConverter());
            converters.Add(new NoContentResultConverter());
            converters.Add(new NotFoundResultConverter());
            converters.Add(new OkResultConverter());
            converters.Add(new UnauthorizedResultConverter());
            converters.Add(new UnprocessableEntityResultConverter());
            converters.Add(new UnsupportedMediaTypeResultConverter());
            converters.Add(new ConflictResultConverter());
            converters.Add(new ViewComponentResultConverter());
            converters.Add(new ViewResultConverter());
            converters.Add(new PageResultConverter());
        });
    }

    static ConversionResult ConvertFileResult(FileContentResult target, IReadOnlyDictionary<string, object> context)
    {
        var info = GetFileResultInfo(target);

        if (!HttpExtensions.TryGetMediaTypeExtension(target.ContentType, out var extension))
        {
            return new(info, []);
        }

        if (FileExtensions.IsTextExtension(extension))
        {
            return new(info, extension, Encoding.UTF8.GetString(target.FileContents));
        }

        return new(info, extension, new MemoryStream(target.FileContents));
    }

    static async Task<ConversionResult> ConvertFileResult(VirtualFileResult target, IReadOnlyDictionary<string, object> context)
    {
        var info = GetFileResultInfo(target);

        if (!HttpExtensions.TryGetMediaTypeExtension(target.ContentType, out var extension))
        {
            return new(info, []);
        }

        if (FileExtensions.IsTextExtension(extension))
        {
            return new(info, extension, await File.ReadAllTextAsync(target.FileName));
        }

        return new(info, extension, File.OpenRead(target.FileName));
    }

    static async Task<ConversionResult> ConvertFileResult(PhysicalFileResult target, IReadOnlyDictionary<string, object> context)
    {
        var info = GetFileResultInfo(target);

        if (!HttpExtensions.TryGetMediaTypeExtension(target.ContentType, out var extension))
        {
            return new(info, []);
        }

        if (FileExtensions.IsTextExtension(extension))
        {
            return new(info, extension, await File.ReadAllTextAsync(target.FileName));
        }

        return new(info, extension, File.OpenRead(target.FileName));
    }

    static async Task<ConversionResult> ConvertFileResult(FileStreamResult target, IReadOnlyDictionary<string, object> context)
    {
        var info = GetFileResultInfo(target);

        if (!HttpExtensions.TryGetMediaTypeExtension(target.ContentType, out var extension))
        {
            return new(info, []);
        }

        if (FileExtensions.IsTextExtension(extension))
        {
            return new(info, extension, await target.FileStream.ReadAsStringAsync());
        }

        return new(info, extension, target.FileStream);
    }

    static FileResultInfo GetFileResultInfo(FileResult target) =>
        new(
            target.FileDownloadName,
            target.LastModified,
            target.EntityTag,
            target.EnableRangeProcessing,
            target.ContentType
        );

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