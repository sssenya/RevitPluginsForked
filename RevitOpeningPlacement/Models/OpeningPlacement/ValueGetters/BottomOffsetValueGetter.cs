﻿using System;

using Autodesk.Revit.DB;

using RevitClashDetective.Models.Value;

using RevitOpeningPlacement.Models.Interfaces;

namespace RevitOpeningPlacement.Models.OpeningPlacement.ValueGetters {
    internal class BottomOffsetValueGetter : IValueGetter<DoubleParamValue> {
        private readonly IPointFinder _pointFinder;
        private readonly IValueGetter<DoubleParamValue> _heightInFeetParamGetter;

        /// <summary>
        /// Конструктор класса, предоставляющего значение высотной отметки низа отверстия, кроме прямоугольных отверстий в стенах и кроме отверстий в перекрытиях
        /// Для прямоугольных отверстий в стенах использовать <see cref="BottomOffsetOfRectangleOpeningInWallValueGetter"/>
        /// Для отверстий в перекрытиях использовать <see cref="BottomOffsetOfOpeningInFloorValueGetter"/>
        /// </summary>
        /// <param name="pointFinder">Объект, предоставляющий координату центра отверстия в футах</param>
        /// <param name="heightInFeetParamGetter">Объект, предоставляющий высоту отверстия в футах</param>
        /// <exception cref="ArgumentNullException"></exception>
        public BottomOffsetValueGetter(IPointFinder pointFinder, IValueGetter<DoubleParamValue> heightInFeetParamGetter) {
            if(pointFinder is null) {
                throw new ArgumentNullException(nameof(pointFinder));
            }
            if(heightInFeetParamGetter is null) {
                throw new ArgumentNullException(nameof(heightInFeetParamGetter));
            }
            _pointFinder = pointFinder;
            _heightInFeetParamGetter = heightInFeetParamGetter;
        }

        public DoubleParamValue GetValue() {
            double offsetInFeet = _pointFinder.GetPoint().Z - (_heightInFeetParamGetter.GetValue().TValue / 2);
            double offsetInMm = 0;
#if REVIT_2020_OR_LESS
            offsetInMm = UnitUtils.ConvertFromInternalUnits(offsetInFeet, DisplayUnitType.DUT_MILLIMETERS);
#else
            offsetInMm = UnitUtils.ConvertFromInternalUnits(offsetInFeet, UnitTypeId.Millimeters);
#endif
            return new DoubleParamValue(Math.Round(offsetInMm));
        }
    }
}
