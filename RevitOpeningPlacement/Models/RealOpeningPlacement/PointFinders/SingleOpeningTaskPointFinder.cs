﻿using System;

using Autodesk.Revit.DB;

using RevitOpeningPlacement.Models.Interfaces;
using RevitOpeningPlacement.Models.OpeningPlacement.ValueGetters;
using RevitOpeningPlacement.OpeningModels;

namespace RevitOpeningPlacement.Models.RealOpeningPlacement.PointFinders {
    /// <summary>
    /// Класс, предоставляющий точку вставки для чистового отверстия по заданию на отверстие
    /// </summary>
    internal class SingleOpeningTaskPointFinder : RoundValueGetter, IPointFinder {
        private readonly OpeningMepTaskIncoming _openingMepTasIncoming;

        /// <summary>
        /// Конструктор класса, предоставляющего точку вставки для чистового отверстия по заданию на отверстие
        /// </summary>
        /// <param name="incomingTask">Входящее задание на отверстие</param>
        /// <exception cref="ArgumentNullException"></exception>
        public SingleOpeningTaskPointFinder(OpeningMepTaskIncoming incomingTask) {
            if(incomingTask is null) { throw new ArgumentNullException(nameof(incomingTask)); }

            _openingMepTasIncoming = incomingTask;
        }


        public XYZ GetPoint() {
            switch(_openingMepTasIncoming.OpeningType) {
                case OpeningType.WallRound:
                case OpeningType.WallRectangle:
                var point = _openingMepTasIncoming.Location;
                return new XYZ(point.X, point.Y, RoundToFloorFeetToMillimeters(point.Z, 10));
                default:
                return _openingMepTasIncoming.Location;
            }
        }
    }
}
