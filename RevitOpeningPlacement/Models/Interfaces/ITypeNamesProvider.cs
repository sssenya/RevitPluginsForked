﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitOpeningPlacement.Models.Interfaces {
    interface ITypeNamesProvider {
        IEnumerable<string> GetTypeNames();
    }
}
