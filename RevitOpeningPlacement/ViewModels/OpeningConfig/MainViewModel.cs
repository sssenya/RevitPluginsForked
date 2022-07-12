﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

using Autodesk.Revit.UI;

using dosymep.WPF.Commands;
using dosymep.WPF.ViewModels;

using RevitOpeningPlacement.Models;
using RevitOpeningPlacement.Models.Configs;
using RevitOpeningPlacement.ViewModels.Interfaces;
using RevitOpeningPlacement.ViewModels.OpeningConfig.MepCategories;
using RevitOpeningPlacement.ViewModels.OpeningConfig.OffsetViewModels;
using RevitOpeningPlacement.ViewModels.OpeningConfig.SizeViewModels;

namespace RevitOpeningPlacement.ViewModels.OpeningConfig {
    internal class MainViewModel : BaseViewModel {
        private readonly RevitRepository _revitRepository;
        private string _errorText;
        private ObservableCollection<IMepCategoryViewModel> _mepCategories;

        public MainViewModel(UIApplication uiApplication, Models.Configs.OpeningConfig openingConfig) {
            _revitRepository = new RevitRepository(uiApplication);
            if(openingConfig.Categories.Any()) {
                MepCategories = new ObservableCollection<IMepCategoryViewModel>(openingConfig.Categories.Select(item => new MepCategoryViewModel(item)));
            } else {
                InitializeCategories();
            }

            SaveConfigCommand = new RelayCommand(SaveConfig);
        }

        public ObservableCollection<IMepCategoryViewModel> MepCategories {
            get => _mepCategories;
            set => this.RaiseAndSetIfChanged(ref _mepCategories, value);
        }

        public string ErrorText {
            get => _errorText;
            set => this.RaiseAndSetIfChanged(ref _errorText, value);
        }

        public ICommand SaveConfigCommand { get; }

        private void InitializeCategories() {
            MepCategories = new ObservableCollection<IMepCategoryViewModel>() {
                GetPipeCategory(),
                GetRectangleDuct(),
                GetRoundDuct(),
                GetCableTray()
            };
        }

        private MepCategoryViewModel GetPipeCategory() => new MepCategoryViewModel {
            Name = "Трубы",
            MinSizes = new ObservableCollection<ISizeViewModel>() {
                new SizeViewModel(){Name ="Диаметр"}
            },
            Offsets = new ObservableCollection<IOffsetViewModel>() {
                new OffsetViewModel()
            },
            ImageSource = "../Resources/pipe.png"
        };

        private MepCategoryViewModel GetRectangleDuct() => new MepCategoryViewModel {
            Name = "Воздуховоды (прямоугольное сечение)",
            MinSizes = new ObservableCollection<ISizeViewModel>() {
                new SizeViewModel(){Name ="Ширина"},
                new SizeViewModel(){Name ="Высота"}
            },
            Offsets = new ObservableCollection<IOffsetViewModel>() {
                new OffsetViewModel()
            },
            ImageSource = "../Resources/rectangleDuct.png"
        };

        private MepCategoryViewModel GetRoundDuct() => new MepCategoryViewModel {
            Name = "Воздуховоды (круглое сечение)",
            MinSizes = new ObservableCollection<ISizeViewModel>() {
                new SizeViewModel(){Name ="Диаметр"}
            },
            Offsets = new ObservableCollection<IOffsetViewModel>() {
                new OffsetViewModel()
            },
            ImageSource = "../Resources/roundDuct.png"
        };

        private MepCategoryViewModel GetCableTray() => new MepCategoryViewModel {
            Name = "Лотки",
            MinSizes = new ObservableCollection<ISizeViewModel>() {
                new SizeViewModel(){Name ="Ширина"},
                new SizeViewModel(){Name ="Высота"}
            },
            Offsets = new ObservableCollection<IOffsetViewModel>() {
                new OffsetViewModel()
            },
            ImageSource = "../Resources/tray.png"
        };

        private void SaveConfig(object p) {
            var categories = MepCategories.Select(item => item.GetMepCategory()).ToList();
            var config = Models.Configs.OpeningConfig.GetOpeningConfig();
            config.Categories = categories;
            config.SaveProjectConfig();
        }
    }
}