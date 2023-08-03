﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.UI;

using dosymep.WPF.ViewModels;

using RevitPylonDocumentation.ViewModels;

namespace RevitPylonDocumentation.Models.UserSettings {
    class UserSchedulesSettings : BaseViewModel {
        public UserSchedulesSettings(MainViewModel mainViewModel) {

            ViewModel = mainViewModel;
        }

        public MainViewModel ViewModel { get; set; }


        private string _rebarSchedulePrefixTemp = "Пилон ";
        private string _rebarScheduleSuffixTemp = "";
        private string _materialSchedulePrefixTemp = "!СМ_Пилон ";
        private string _materialScheduleSuffixTemp = "";
        private string _systemPartsSchedulePrefixTemp = "!ВД_СИС_";
        private string _systemPartsScheduleSuffixTemp = "";
        private string _IFCPartsSchedulePrefixTemp = "!ВД_IFC_";
        private string _IFCPartsScheduleSuffixTemp = "";

        private string _rebarScheduleNameTemp = "!СА_Базовая";
        private string _materialScheduleNameTemp = "!СМ";
        private string _systemPartsScheduleNameTemp = "!ВД_СИС";
        private string _IFCPartsScheduleNameTemp = "!ВД_IFC";

        private string _rebarScheduleDisp1Temp = "обр_ФОП_Раздел проекта";
        private string _rebarScheduleDisp2Temp = "!СА_Пилоны";
        private string _materialScheduleDisp1Temp = "обр_ФОП_Раздел проекта";
        private string _materialScheduleDisp2Temp = "СМ_Пилоны";
        private string _systemPartsScheduleDisp1Temp = "обр_ФОП_Раздел проекта";
        private string _systemPartsScheduleDisp2Temp = "ВД_СИС_Пилоны";
        private string _IFCPartsScheduleDisp1Temp = "обр_ФОП_Раздел проекта";
        private string _IFCPartsScheduleDisp2Temp = "ВД_IFC_Пилоны";

        private ObservableCollection<ScheduleFilterParamHelper> _paramsForScheduleFilters = new ObservableCollection<ScheduleFilterParamHelper>() {
            new ScheduleFilterParamHelper("обр_ФОП_Форма_номер", ""),
            new ScheduleFilterParamHelper("обр_ФОП_Раздел проекта", "обр_ФОП_Раздел проекта"),
            new ScheduleFilterParamHelper("обр_Метка основы_универсальная", "Марка"),
            new ScheduleFilterParamHelper("обр_ФОП_Орг. уровень", "обр_ФОП_Орг. уровень")
        };


        public string REBAR_SCHEDULE_PREFIX { get; set; }
        public string REBAR_SCHEDULE_PREFIX_TEMP {
            get => _rebarSchedulePrefixTemp;
            set {
                RaiseAndSetIfChanged(ref _rebarSchedulePrefixTemp, value);
                ViewModel.SettingsEdited = true;
            }
        }

        public string REBAR_SCHEDULE_SUFFIX { get; set; }
        public string REBAR_SCHEDULE_SUFFIX_TEMP {
            get => _rebarScheduleSuffixTemp;
            set {
                RaiseAndSetIfChanged(ref _rebarScheduleSuffixTemp, value);
                ViewModel.SettingsEdited = true;
            }
        }


        public string MATERIAL_SCHEDULE_PREFIX { get; set; }
        public string MATERIAL_SCHEDULE_PREFIX_TEMP {
            get => _materialSchedulePrefixTemp;
            set {
                RaiseAndSetIfChanged(ref _materialSchedulePrefixTemp, value);
                ViewModel.SettingsEdited = true;
            }
        }

        public string MATERIAL_SCHEDULE_SUFFIX { get; set; }
        public string MATERIAL_SCHEDULE_SUFFIX_TEMP {
            get => _materialScheduleSuffixTemp;
            set {
                RaiseAndSetIfChanged(ref _materialScheduleSuffixTemp, value);
                ViewModel.SettingsEdited = true;
            }
        }

        public string SYSTEM_PARTS_SCHEDULE_PREFIX { get; set; }
        public string SYSTEM_PARTS_SCHEDULE_PREFIX_TEMP {
            get => _systemPartsSchedulePrefixTemp;
            set {
                RaiseAndSetIfChanged(ref _systemPartsSchedulePrefixTemp, value);
                ViewModel.SettingsEdited = true;
            }
        }

        public string SYSTEM_PARTS_SCHEDULE_SUFFIX { get; set; }
        public string SYSTEM_PARTS_SCHEDULE_SUFFIX_TEMP {
            get => _systemPartsScheduleSuffixTemp;
            set {
                RaiseAndSetIfChanged(ref _systemPartsScheduleSuffixTemp, value);
                ViewModel.SettingsEdited = true;
            }
        }


        public string IFC_PARTS_SCHEDULE_PREFIX { get; set; }
        public string IFC_PARTS_SCHEDULE_PREFIX_TEMP {
            get => _IFCPartsSchedulePrefixTemp;
            set {
                RaiseAndSetIfChanged(ref _IFCPartsSchedulePrefixTemp, value);
                ViewModel.SettingsEdited = true;
            }
        }

        public string IFC_PARTS_SCHEDULE_SUFFIX { get; set; }
        public string IFC_PARTS_SCHEDULE_SUFFIX_TEMP {
            get => _IFCPartsScheduleSuffixTemp;
            set {
                RaiseAndSetIfChanged(ref _IFCPartsScheduleSuffixTemp, value);
                ViewModel.SettingsEdited = true;
            }
        }

        public string REBAR_SCHEDULE_NAME { get; set; }
        public string REBAR_SCHEDULE_NAME_TEMP {
            get => _rebarScheduleNameTemp;
            set {
                RaiseAndSetIfChanged(ref _rebarScheduleNameTemp, value);
                ViewModel.SettingsEdited = true;
            }
        }

        public string MATERIAL_SCHEDULE_NAME { get; set; }
        public string MATERIAL_SCHEDULE_NAME_TEMP {
            get => _materialScheduleNameTemp;
            set {
                RaiseAndSetIfChanged(ref _materialScheduleNameTemp, value);
                ViewModel.SettingsEdited = true;
            }
        }

        public string SYSTEM_PARTS_SCHEDULE_NAME { get; set; }
        public string SYSTEM_PARTS_SCHEDULE_NAME_TEMP {
            get => _systemPartsScheduleNameTemp;
            set {
                RaiseAndSetIfChanged(ref _systemPartsScheduleNameTemp, value);
                ViewModel.SettingsEdited = true;
            }
        }

        public string IFC_PARTS_SCHEDULE_NAME { get; set; }
        public string IFC_PARTS_SCHEDULE_NAME_TEMP {
            get => _IFCPartsScheduleNameTemp;
            set {
                RaiseAndSetIfChanged(ref _IFCPartsScheduleNameTemp, value);
                ViewModel.SettingsEdited = true;
            }
        }

        public string REBAR_SCHEDULE_DISP1 { get; set; }
        public string REBAR_SCHEDULE_DISP1_TEMP {
            get => _rebarScheduleDisp1Temp;
            set {
                RaiseAndSetIfChanged(ref _rebarScheduleDisp1Temp, value);
                ViewModel.SettingsEdited = true;
            }
        }
        public string MATERIAL_SCHEDULE_DISP1 { get; set; }
        public string MATERIAL_SCHEDULE_DISP1_TEMP {
            get => _materialScheduleDisp1Temp;
            set {
                RaiseAndSetIfChanged(ref _materialScheduleDisp1Temp, value);
                ViewModel.SettingsEdited = true;
            }
        }
        public string SYSTEM_PARTS_SCHEDULE_DISP1 { get; set; }
        public string SYSTEM_PARTS_SCHEDULE_DISP1_TEMP {
            get => _systemPartsScheduleDisp1Temp;
            set {
                RaiseAndSetIfChanged(ref _systemPartsScheduleDisp1Temp, value);
                ViewModel.SettingsEdited = true;
            }
        }
        public string IFC_PARTS_SCHEDULE_DISP1 { get; set; }
        public string IFC_PARTS_SCHEDULE_DISP1_TEMP {
            get => _IFCPartsScheduleDisp1Temp;
            set {
                RaiseAndSetIfChanged(ref _IFCPartsScheduleDisp1Temp, value);
                ViewModel.SettingsEdited = true;
            }
        }

        public string REBAR_SCHEDULE_DISP2 { get; set; }
        public string REBAR_SCHEDULE_DISP2_TEMP {
            get => _rebarScheduleDisp2Temp;
            set {
                RaiseAndSetIfChanged(ref _rebarScheduleDisp2Temp, value);
                ViewModel.SettingsEdited = true;
            }
        }
        public string MATERIAL_SCHEDULE_DISP2 { get; set; }
        public string MATERIAL_SCHEDULE_DISP2_TEMP {
            get => _materialScheduleDisp2Temp;
            set {
                RaiseAndSetIfChanged(ref _materialScheduleDisp2Temp, value);
                ViewModel.SettingsEdited = true;
            }
        }
        public string SYSTEM_PARTS_SCHEDULE_DISP2 { get; set; }
        public string SYSTEM_PARTS_SCHEDULE_DISP2_TEMP {
            get => _systemPartsScheduleDisp2Temp;
            set {
                RaiseAndSetIfChanged(ref _systemPartsScheduleDisp2Temp, value);
                ViewModel.SettingsEdited = true;
            }
        }

        public string IFC_PARTS_SCHEDULE_DISP2 { get; set; }
        public string IFC_PARTS_SCHEDULE_DISP2_TEMP {
            get => _IFCPartsScheduleDisp2Temp;
            set {
                RaiseAndSetIfChanged(ref _IFCPartsScheduleDisp2Temp, value);
                ViewModel.SettingsEdited = true;
            }
        }

        public ObservableCollection<ScheduleFilterParamHelper> ParamsForScheduleFilters {
            get => _paramsForScheduleFilters;
            set {
                RaiseAndSetIfChanged(ref _paramsForScheduleFilters, value);
                ViewModel.SettingsEdited = true;
            }
        }



        //Сделать явно вызов команды изменения настроек



        public void ApplySchedulesSettings() {
            
            REBAR_SCHEDULE_PREFIX = REBAR_SCHEDULE_PREFIX_TEMP;
            REBAR_SCHEDULE_SUFFIX = REBAR_SCHEDULE_SUFFIX_TEMP;

            MATERIAL_SCHEDULE_PREFIX = MATERIAL_SCHEDULE_PREFIX_TEMP;
            MATERIAL_SCHEDULE_SUFFIX = MATERIAL_SCHEDULE_SUFFIX_TEMP;

            SYSTEM_PARTS_SCHEDULE_PREFIX = SYSTEM_PARTS_SCHEDULE_PREFIX_TEMP;
            SYSTEM_PARTS_SCHEDULE_SUFFIX = SYSTEM_PARTS_SCHEDULE_SUFFIX_TEMP;

            IFC_PARTS_SCHEDULE_PREFIX = IFC_PARTS_SCHEDULE_PREFIX_TEMP;
            IFC_PARTS_SCHEDULE_SUFFIX = IFC_PARTS_SCHEDULE_SUFFIX_TEMP;

            REBAR_SCHEDULE_NAME = REBAR_SCHEDULE_NAME_TEMP;
            MATERIAL_SCHEDULE_NAME = MATERIAL_SCHEDULE_NAME_TEMP;
            SYSTEM_PARTS_SCHEDULE_NAME = SYSTEM_PARTS_SCHEDULE_NAME_TEMP;
            IFC_PARTS_SCHEDULE_NAME = IFC_PARTS_SCHEDULE_NAME_TEMP;

            REBAR_SCHEDULE_DISP1 = REBAR_SCHEDULE_DISP1_TEMP;
            MATERIAL_SCHEDULE_DISP1 = MATERIAL_SCHEDULE_DISP1_TEMP;
            SYSTEM_PARTS_SCHEDULE_DISP1 = SYSTEM_PARTS_SCHEDULE_DISP1_TEMP;
            IFC_PARTS_SCHEDULE_DISP1 = IFC_PARTS_SCHEDULE_DISP1_TEMP;

            REBAR_SCHEDULE_DISP2 = REBAR_SCHEDULE_DISP2_TEMP;
            MATERIAL_SCHEDULE_DISP2 = MATERIAL_SCHEDULE_DISP2_TEMP;
            SYSTEM_PARTS_SCHEDULE_DISP2 = SYSTEM_PARTS_SCHEDULE_DISP2_TEMP;
            IFC_PARTS_SCHEDULE_DISP2 = IFC_PARTS_SCHEDULE_DISP2_TEMP;
        }
    }
}
