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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RumikApp.UI.Views
{
    /// <summary>
    /// Interaction logic for MainControlPanel.xaml
    /// </summary>
    public partial class MainControlPanel : UserControl
    {
        public MainControlPanel()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
