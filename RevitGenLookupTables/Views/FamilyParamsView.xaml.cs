﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using dosymep.WPF.Views;

namespace RevitGenLookupTables.Views {
    /// <summary>
    /// Interaction logic for FamilyParamsView.xaml
    /// </summary>
    public partial class FamilyParamsView {
        public FamilyParamsView() {
            InitializeComponent();
        }

        public override string PluginName => nameof(RevitGenLookupTables);
        public override string ProjectConfigName => nameof(FamilyParamsView);

        private void ButtonOK_Click(object sender, RoutedEventArgs e) {
            DialogResult = true;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e) {
            DialogResult = false;
        }
    }
}
