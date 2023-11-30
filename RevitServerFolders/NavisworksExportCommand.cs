﻿using System;
using System.Windows;

using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;

using dosymep.Bim4Everyone;
using dosymep.Bim4Everyone.SimpleServices;
using dosymep.Xpf.Core.Ninject;

using Ninject;

using RevitServerFolders.Models;
using RevitServerFolders.Services;
using RevitServerFolders.ViewModels;
using RevitServerFolders.Views;

namespace RevitServerFolders {
    [Transaction(TransactionMode.Manual)]
    internal sealed class NavisworksExportCommand : BasePluginCommand {
        public NavisworksExportCommand() {
            PluginName = "Экспорт вида Navisworks";
        }

        protected override void Execute(UIApplication uiApplication) {
            using(IKernel kernel = uiApplication.CreatePlatformServices()) {
                kernel.Bind<RevitRepository>()
                    .ToSelf()
                    .InSingletonScope();

                kernel.Bind<PluginConfig>()
                    .ToMethod(c => PluginConfig.GetPluginConfig());
                
                kernel.Bind<IModelObjectService>()
                    .To<FileSystemModelObjectService>();

                kernel.UseXtraOpenFolderDialog<MainWindow>(
                    initialDirectory: Environment.GetFolderPath(Environment.SpecialFolder.Desktop));

                kernel.Bind<MainViewModel>().ToSelf();
                kernel.Bind<MainWindow>().ToSelf()
                    .WithPropertyValue(nameof(Window.Title), PluginName)
                    .WithPropertyValue(nameof(Window.DataContext),
                        c => c.Kernel.Get<MainViewModel>());

                Notification(kernel.Get<MainWindow>());
            }
        }
    }
}
