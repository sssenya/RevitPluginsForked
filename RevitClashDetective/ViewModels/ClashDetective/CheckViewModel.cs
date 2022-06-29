﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Autodesk.Revit.UI;

using dosymep.WPF.Commands;
using dosymep.WPF.ViewModels;

using RevitClashDetective.Models;
using RevitClashDetective.Models.Clashes;
using RevitClashDetective.Models.FilterModel;
using RevitClashDetective.ViewModels.ClashDetective;
using RevitClashDetective.ViewModels.Interfaces;
using RevitClashDetective.ViewModels.Navigator;
using RevitClashDetective.Views;

namespace RevitClashDetective.ViewModels {
    internal class CheckViewModel : BaseViewModel, INamedViewModel {
        private readonly RevitRepository _revitRepository;
        private readonly FiltersConfig _filtersConfig;
        private string _name;
        private string errorText;
        private bool _hasReport;
        private bool _isSelected;
        private SelectionViewModel _firstSelection;
        private SelectionViewModel _secondSelection;

        public CheckViewModel(RevitRepository revitRepository, FiltersConfig filtersConfig, Check check = null) {
            _revitRepository = revitRepository;
            _filtersConfig = filtersConfig;

            Name = check?.Name ?? "Без имени";


            if(check == null) {
                InitializeSelections();
                HasReport = false;
            } else {
                InitializeFilterProviders(check);
            }

            ShowClashesCommand = new RelayCommand(ShowClashes, CanShowClashes);
        }

        public ICommand SelectMainProviderCommand { get; }
        public ICommand ShowClashesCommand { get; }
        public bool IsFilterSelected => FirstSelection.SelectedProviders.Any() && SecondSelection.SelectedProviders.Any();
        public bool IsFilesSelected => FirstSelection.SelectedFiles.Any() && SecondSelection.SelectedFiles.Any();

        public bool IsSelected {
            get => _isSelected;
            set => this.RaiseAndSetIfChanged(ref _isSelected, value);
        }

        public bool HasReport {
            get => _hasReport;
            set => this.RaiseAndSetIfChanged(ref _hasReport, value);
        }

        public string Name {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        public string ErrorText {
            get => errorText;
            set => this.RaiseAndSetIfChanged(ref errorText, value);
        }

        public SelectionViewModel FirstSelection {
            get => _firstSelection;
            set => this.RaiseAndSetIfChanged(ref _firstSelection, value);
        }

        public SelectionViewModel SecondSelection {
            get => _secondSelection;
            set => this.RaiseAndSetIfChanged(ref _secondSelection, value);
        }

        private string ReportName => $"{_revitRepository.GetDocumentName()}_{Name}";

        public List<ClashModel> GetClashes() {
            var mainProviders = FirstSelection
                .GetProviders()
                .ToList();
            var otherProviders = SecondSelection
                .GetProviders()
                .ToList();
            var clashDetector = new ClashDetector(_revitRepository, mainProviders, otherProviders);
            return clashDetector.FindClashes();
        }

        public void SaveClashes() {
            var config = ClashesConfig.GetClashesConfig(_revitRepository.GetObjectName(), ReportName);
            config.Clashes = GetClashes();
            config.SaveProjectConfig();
            HasReport = true;
        }


        private void InitializeSelections(Check check = null) {
            FirstSelection = new SelectionViewModel(_revitRepository, _filtersConfig, check?.FirstSelection);
            SecondSelection = new SelectionViewModel(_revitRepository, _filtersConfig, check?.SecondSelection);
        }

        private void InitializeFilterProviders(Check check) {
            InitializeSelections(check);

            var firstFiles = FirstSelection.GetMissedFiles();
            if(!string.IsNullOrEmpty(firstFiles)) {
                ErrorText = $"Не найдены файлы первой выборки: {firstFiles}" + Environment.NewLine;
            }
            var firstFilters = FirstSelection.GetMissedFilters();
            if(!string.IsNullOrEmpty(firstFilters)) {
                ErrorText += $"Не найдены поисковые наборы первой выборки: {firstFilters}" + Environment.NewLine;
            }

            var secondFiles = SecondSelection.GetMissedFiles();
            if(!string.IsNullOrEmpty(secondFiles)) {
                ErrorText += $"Не найдены файлы второй выборки: {secondFiles}" + Environment.NewLine;
            }
            var secondFilters = SecondSelection.GetMissedFilters();
            if(!string.IsNullOrEmpty(secondFilters)) {
                ErrorText = $"Не найдены поисковые наборы второй выборки: {secondFilters}" + Environment.NewLine;
            }

            if(ClashesConfig.GetClashesConfig(_revitRepository.GetObjectName(), ReportName).Clashes.Count > 0) {
                HasReport = true;
            }
        }

        private void ShowClashes(object p) {
            var view = new NavigatorView() { DataContext = new ClashesViewModel(_revitRepository, ReportName) { OpenFromClashDetector = true } };
            view.Show();
        }

        private bool CanShowClashes(object p) {
            return HasReport;
        }
    }
}