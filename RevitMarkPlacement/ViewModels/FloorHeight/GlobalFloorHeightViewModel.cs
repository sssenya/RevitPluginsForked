﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;

using dosymep.WPF.ViewModels;

using RevitMarkPlacement.Models;

namespace RevitMarkPlacement.ViewModels {

    internal class GlobalFloorHeightViewModel : BaseViewModel, IFloorHeightProvider {
        private readonly RevitRepository _revitRepository;
        private GlobalParameterViewModel _selectedGlobalParameter;

        public GlobalFloorHeightViewModel(RevitRepository revitRepository, string description) {
            _revitRepository = revitRepository;

            Description = description;

            GlobalParameters = _revitRepository.GetDoubleGlobalParameters()
                .Select(item => new GlobalParameterViewModel(item.Name, GetValue(item)))
                .ToList();

            SelectedGlobalParameter = GlobalParameters[0];
        }

        public string Description { get; }
        public List<GlobalParameterViewModel> GlobalParameters { get; }

        public GlobalParameterViewModel SelectedGlobalParameter {
            get => _selectedGlobalParameter;
            set => this.RaiseAndSetIfChanged(ref _selectedGlobalParameter, value);
        }

        public double GetFloorHeight() {
            return SelectedGlobalParameter.Value;
        }

        private double GetValue(GlobalParameter parameter) {
#if D2020 || R2020
            return UnitUtils.ConvertFromInternalUnits((parameter.GetValue() as DoubleParameterValue).Value, DisplayUnitType.DUT_MILLIMETERS);
#else
            return UnitUtils.ConvertFromInternalUnits((parameter.GetValue() as DoubleParameterValue).Value, UnitTypeId.Millimeters);
#endif
        }
    }
}
