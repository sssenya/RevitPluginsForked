﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Autodesk.Revit.DB;

using dosymep.WPF.Commands;
using dosymep.WPF.ViewModels;

using RevitRooms.Models;

namespace RevitRooms.ViewModels {
    internal abstract class ElementViewModel<TElement> : BaseViewModel, IElementViewModel<TElement>, IComparable<ElementViewModel<TElement>>, IEquatable<ElementViewModel<TElement>>
        where TElement : Element {

        public ElementViewModel(TElement element, RevitRepository revitRepository) {
            Element = element ?? throw new ArgumentNullException(nameof(element));
            RevitRepository = revitRepository ?? throw new ArgumentNullException(nameof(revitRepository));

            ShowElementCommand = new RelayCommand(ShowElement, CanShowElement);
            SelectElementCommand = new RelayCommand(SelectElement, CanSelectElement);
        }

        public TElement Element { get; }
        public RevitRepository RevitRepository { get; }

        public ElementId ElementId => Element.Id;

        public string Name => Element.Name;
        public virtual string PhaseName { get; }
        public virtual string LevelName { get; }

        public string CategoryName => Element.Category.Name;

        public ICommand ShowElementCommand { get; }
        public ICommand SelectElementCommand { get; }

        #region Commands

        private void ShowElement(object p) {
            RevitRepository.ShowElement(Element);
        }

        private bool CanShowElement(object p) {
            return true;
        }

        private void SelectElement(object p) {
            RevitRepository.SelectElement(Element);
        }

        private bool CanSelectElement(object p) {
            return true;
        }

        #endregion

        #region SystemOverrides

        public int CompareTo(ElementViewModel<TElement> other) {
            return Element.Name.CompareTo(other.Element.Name);
        }

        public override bool Equals(object obj) {
            return Equals(obj as ElementViewModel<TElement>);
        }

        public bool Equals(ElementViewModel<TElement> other) {
            return other != null && Element.Id.Equals(other.Element.Id);
        }

        public override int GetHashCode() {
            return -2121273300 + Element.Id.GetHashCode();
        }

        public static bool operator <(ElementViewModel<TElement> left, ElementViewModel<TElement> right) {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(ElementViewModel<TElement> left, ElementViewModel<TElement> right) {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(ElementViewModel<TElement> left, ElementViewModel<TElement> right) {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(ElementViewModel<TElement> left, ElementViewModel<TElement> right) {
            return left.CompareTo(right) >= 0;
        }

        public static bool operator ==(ElementViewModel<TElement> left, ElementViewModel<TElement> right) {
            return ReferenceEquals(left, right) || left.Equals(right);
        }

        public static bool operator !=(ElementViewModel<TElement> left, ElementViewModel<TElement> right) {
            return !ReferenceEquals(left, right) && !left.Equals(right);
        }

        #endregion
    }
}
