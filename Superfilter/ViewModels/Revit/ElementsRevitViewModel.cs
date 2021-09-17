﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;

using Superfilter.Models;

namespace Superfilter.ViewModels.Revit {
    internal class ElementsRevitViewModel : RevitViewModel {
        public ElementsRevitViewModel(Application application, Document document)
            : base(application, document) {
        }

        protected override IEnumerable<CategoryViewModel> GetCategoryViewModels() {
            return _revitRepository.GetAllElements()
                .Where(item => item.Category != null && item.Category.Parent == null)
                .GroupBy(item => item.Category, new CategoryComparer())
                .Select(item => new CategoryViewModel(item.Key, item))
                .OrderBy(item => item.Name);
        }
    }
}