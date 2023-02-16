﻿using Autodesk.Revit.DB;

using dosymep.WPF.ViewModels;

using RevitCopingZones.Models;

namespace RevitCopingZones.ViewModels {
    internal class FloorPlanViewModel : BaseViewModel {
        private readonly FloorPlan _floorPlan;

        public FloorPlanViewModel(FloorPlan floorPlan) {
            _floorPlan = floorPlan;
        }

        public string FloorName => _floorPlan.Name;
        public string AreaPlanName => _floorPlan.AreaPlan?.Name;
    }
}