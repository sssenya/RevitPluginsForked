﻿using System;
using System.Linq;

using Autodesk.Revit.DB;

using RevitClashDetective.Models.Value;

using RevitOpeningPlacement.Models.Configs;
using RevitOpeningPlacement.Models.Interfaces;
using RevitOpeningPlacement.Models.OpeningPlacement.Projectors;

namespace RevitOpeningPlacement.Models.OpeningPlacement.ParameterGetters {
    internal class InclinedWidthGetter : IParameterGetter<DoubleParamValue> {
        private readonly MepCurveWallClash _clash;
        private readonly IParameterGetter<DoubleParamValue> _sizeGetter;

        public InclinedWidthGetter(MepCurveWallClash clash, IParameterGetter<DoubleParamValue> sizeGetter) {
            _clash = clash;
            _sizeGetter = sizeGetter;
        }

        public ParameterValuePair<DoubleParamValue> GetParamValue() {
            var width = GetWidth();

            return new ParameterValuePair<DoubleParamValue>() {
                ParamName = RevitRepository.OpeningWidth,
                TValue = new DoubleParamValue(width)
            };
        }

        private double GetWidth() {
            var transformedMepLine = _clash.GetTransformedMepLine();

            //получение размера, на который будет смещена осевая линия инженерной системы
            var size = _sizeGetter.GetParamValue().TValue.TValue / 2;

            //алгоритм аналогичен тому, который описан в InclinedHeightGetter
            var angleToY = XYZ.BasisY.AngleOnPlaneTo(transformedMepLine.Direction, XYZ.BasisZ);
            var projector = new XoYProjector();

            if(Math.Abs(Math.Cos(angleToY)) < 0.0001) {
                return new MaxSizeGetter(_clash, projector).GetSize(XYZ.BasisY, size);
            } else if(Math.Abs(Math.Abs(Math.Cos(angleToY)) - 1) < 0.0001) {
                return new MaxSizeGetter(_clash, projector).GetSize(XYZ.BasisX, size);
            } else {
                var projectedDir = new XYZ(transformedMepLine.Direction.X, transformedMepLine.Direction.Y, 0);

                var vectorY = (projectedDir.GetLength() / Math.Cos(angleToY)) * XYZ.BasisY;
                var direction = (vectorY - projectedDir).Normalize();

                return new MaxSizeGetter(_clash, projector).GetSize(direction, size);
            }
        }
    }
}
