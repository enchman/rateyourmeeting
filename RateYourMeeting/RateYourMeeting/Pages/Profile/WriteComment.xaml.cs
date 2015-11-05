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

namespace RateYourMeeting
{
    /// <summary>
    /// Interaction logic for WriteComment.xaml
    /// </summary>
    public partial class WriteComment : Window
    {
        public WriteComment()
        {
            InitializeComponent();
            boxComment.Focus();
        }

        public WriteComment(string uid)
        {
            InitializeComponent();
            boxComment.Focus();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
