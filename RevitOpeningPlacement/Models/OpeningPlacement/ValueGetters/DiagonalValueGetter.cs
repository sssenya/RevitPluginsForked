﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;

using RevitClashDetective.Models.Value;

using RevitOpeningPlacement.Models.Configs;
using RevitOpeningPlacement.Models.Extensions;
using RevitOpeningPlacement.Models.Interfaces;

namespace RevitOpeningPlacement.Models.OpeningPlacement.ValueGetters {
    internal class DiagonalValueGetter : IValueGetter<DoubleParamValue> {
        private readonly MepCurveWallClash _clash;
        private readonly Plane _plane;
        private readonly MepCategory _categoryOptions;

        public DiagonalValueGetter(MepCurveWallClash clash, Plane plane, MepCategory categoryOptions) {
            _clash = clash;
            _plane = plane;
            _categoryOptions = categoryOptions;
        }

        public DoubleParamValue GetValue() {
            var height = _clash.Curve.GetHeight();
            var width = _clash.Curve.GetWidth();
            var coordinateSystem = _clash.Curve.GetConnectorCoordinateSystem();
            var dirX = coordinateSystem.BasisX;
            var dirY = coordinateSystem.BasisY;

            height += _categoryOptions.GetOffset(height);
            width += _categoryOptions.GetOffset(width);

            //получение длин проекций диагоналей коннектора инженерной системы на плоскость
            var diagonals = new[] { _plane.ProjectVector(dirX * width + dirY * height).GetLength(),
                                    _plane.ProjectVector(dirX * width - dirY * height).GetLength() };

            return new DoubleParamValue(diagonals.Max());
        }
    }
}