﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

using dosymep;
using dosymep.Bim4Everyone;
using dosymep.Bim4Everyone.ProjectParams;
using dosymep.Revit;

using RevitCopyViews.ViewModels;
using RevitCopyViews.Views;

namespace RevitCopyViews {
    [Transaction(TransactionMode.Manual)]
    public class CopyViewCommand : IExternalCommand {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements) {
            AppDomain.CurrentDomain.AssemblyResolve += AppDomainExtensions.CurrentDomain_AssemblyResolve;
            try {
                Excecute(commandData);
            } catch(Exception ex) {
#if DEBUG
                System.Windows.MessageBox.Show(ex.ToString(), "Ошибка", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
#else
                System.Windows.MessageBox.Show(ex.Message, "Ошибка", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
#endif
            } finally {
                AppDomain.CurrentDomain.AssemblyResolve -= AppDomainExtensions.CurrentDomain_AssemblyResolve;
            }

            return Result.Succeeded;
        }

        private void Excecute(ExternalCommandData commandData) {
            var uiApplication = commandData.Application;
            var application = uiApplication.Application;

            var uiDocument = uiApplication.ActiveUIDocument;
            var document = uiDocument.Document;

            var selectedViews = uiDocument.Selection.GetElementIds().Select(item => document.GetElement(item)).OfType<View>().ToList();
            if(selectedViews.Count == 0) {
                TaskDialog.Show("Предупреждение!", "Выберите виды, которые требуется копировать.");
                return;
            }

            var views = new FilteredElementCollector(document).OfClass(typeof(View)).ToElements();
            var groupViews = views
                 .Select(item => (string) item.GetParamValueOrDefault(ProjectParamsConfig.Instance.ViewGroup))
                 .Where(item => !string.IsNullOrEmpty(item))
                 .OrderBy(item => item)
                 .Distinct();

            var window = new CopyViewWindow() {
                DataContext = new CopyViewViewModel(selectedViews) {
                    Document = document,
                    UIDocument = uiDocument,
                    Application = application,

                    GroupViews = new ObservableCollection<string>(groupViews),
                    RestrictedViewNames = views.Select(item => item.Name).ToList()
                }
            };

            new WindowInteropHelper(window) { Owner = uiApplication.MainWindowHandle };
            window.ShowDialog();
        }
    }
}