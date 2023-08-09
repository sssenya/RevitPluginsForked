﻿using System;
using System.Collections.Generic;
using System.Drawing.Imaging;

using Nuke.Common;
using Nuke.Common.IO;

using Serilog;

interface ICreateBundle : IClean, IHazTemplate {
    [Parameter]
    Uri IconUrl => TryGetValue(() => IconUrl)
                   ?? new Uri("https://icons8.com/icon/UgAl9mP8tniQ/example");

    [Parameter] string BundleName => TryGetValue(() => BundleName) ?? "BundleName";
    [Parameter] BundleType BundleType => TryGetValue(() => BundleType) ?? BundleType.InvokeButton;

    AbsolutePath TemplateBundleDirectory => TemplateDirectory + BundleType.ExtensionWithDot;
    AbsolutePath BundleDirectory => Output / BundleName + BundleType.ExtensionWithDot;


    IconSize IconSize => IconSize.Size96;
    string UriIconFormat => "https://img.icons8.com/?size={0}&id={1}&format=png";


    Target CreateBundle => _ => _
        //.Triggers(Clean)
        .Requires(() => BundleName)
        .Requires(() => BundleType)
        .Executes(async () => {
            Log.Debug("TemplateName: {TemplateName}", TemplateName);
            Log.Debug("TemplateDirectory: {TemplateDirectory}", TemplateDirectory);

            Log.Debug("IconUrl: {IconUrl}", IconUrl);
            Log.Debug("BundleName: {BundleName}", BundleName);
            Log.Debug("BundleType: {BundleType}", BundleType);
            Log.Debug("BundleDirectory: {BundleDirectory}", BundleDirectory);

            CopyDirectory(TemplateBundleDirectory, BundleDirectory,
                new Dictionary<string, string>() {
                    {"${{ gen.bundle_name }}", BundleName},
                    {"${{ gen.plugin_name }}", this.From<IHazPluginName>().PluginName},
                    {"${{ gen.plugin_command }}", this.From<IHazPluginName>().PluginName + "Command"},
                });

            BundleDirectory.CreateDirectory();
            await IconSize.CreateImages(GetImageUri(), BundleDirectory / "image.png");
        });

    Uri GetImageUri() {
        return new Uri(string.Format(UriIconFormat, IconSize.Size, IconUrl.AbsolutePath.Split('/')[^2]));
    }
}