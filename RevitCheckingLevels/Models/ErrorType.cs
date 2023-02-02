﻿using System;

using Autodesk.Revit.DB;

namespace RevitCheckingLevels.Models {
    internal class ErrorType : IEquatable<ErrorType>, IComparable<ErrorType>, IComparable {
        public static readonly ErrorType NotStandard =
            new ErrorType(0) {
                Name = "Имена уровней не соответствуют стандарту",
                Description =
                    "Имена уровней должны соответствовать данному формату: \"[Префикс][Номер этажа] [пробел] [«этаж»][\"_\"][Название блока][\".\"][Номер уровня][\"_\"][Отметка уровня]\"."
            };

        public static readonly ErrorType NotElevation =
            new ErrorType(1) {
                Name = "Отметки уровня не соответствуют фактическим",
                Description =
                    $"Имена уровней должны оканчиваться значением параметра \"{LabelUtils.GetLabelFor(BuiltInParameter.LEVEL_ELEV)}\" в метрах с разделителем дробной части в виде точки."
            };

        public static readonly ErrorType NotMillimeterElevation =
            new ErrorType(2) {
                Name = "Отметка уровня не округлена",
                Description =
                    $"Значение параметра \"{LabelUtils.GetLabelFor(BuiltInParameter.LEVEL_ELEV)}\" (в миллиметрах) до 7 знака после запятой должно быть равно \"0\"."
            };

        public static readonly ErrorType NotRangeElevation =
            new ErrorType(3) {
                Name = "Уровни замоделированы не по стандарту",
                Description =
                    $"Значение параметра \"{LabelUtils.GetLabelFor(BuiltInParameter.LEVEL_ELEV)}\" должно отступать на 1500мм от предыдущего."
            };

        public ErrorType(int id) {
            Id = id;
        }

        public int Id { get; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        #region IEquatable<ErrorType>

        public bool Equals(ErrorType other) {
            if(ReferenceEquals(null, other)) {
                return false;
            }

            if(ReferenceEquals(this, other)) {
                return true;
            }

            return Id == other.Id;
        }

        public override bool Equals(object obj) {
            if(ReferenceEquals(null, obj)) {
                return false;
            }

            if(ReferenceEquals(this, obj)) {
                return true;
            }

            if(obj.GetType() != GetType()) {
                return false;
            }

            return Equals((ErrorType) obj);
        }

        public override int GetHashCode() {
            return Id;
        }

        public static bool operator ==(ErrorType left, ErrorType right) {
            return Equals(left, right);
        }

        public static bool operator !=(ErrorType left, ErrorType right) {
            return !Equals(left, right);
        }

        #endregion

        #region IComparable<ErrorType>

        public int CompareTo(ErrorType other) {
            if(ReferenceEquals(this, other)) {
                return 0;
            }

            if(ReferenceEquals(null, other)) {
                return 1;
            }

            return Id.CompareTo(other.Id);
        }

        public int CompareTo(object obj) {
            return CompareTo(obj as ErrorType);
        }

        #endregion

        public override string ToString() {
            return Name;
        }
    }
}