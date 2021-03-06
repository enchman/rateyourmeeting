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

namespace RateYourMeeting
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Main Initialization
    /// </summary>
    public partial class Screen : Window
    {
        public Screen()
        {
            InitializeComponent();

            PageSwitch.CurrentPage = this;
            PageSwitch.Forward(new LoginPage());
        }

        public void Navigate(UserControl page)
        {
            this.Content = page;
        }
    }
}
