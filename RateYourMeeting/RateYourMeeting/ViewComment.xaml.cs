using System;
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
    /// Interaction logic for ViewComment.xaml
    /// </summary>
    public partial class ViewComment : Window
    {
        public ViewComment()
        {
            InitializeComponent();
            LoadItems();
        }

        private void Panel_MouseEnter(object sender, MouseEventArgs e)
        {
            StackPanel panel = sender as StackPanel;
            panel.Background = Brushes.AliceBlue;
        }
        private void Panel_MouseLeave(object sender, MouseEventArgs e)
        {
            StackPanel panel = sender as StackPanel;
            panel.Background = Brushes.Transparent;
        }

        private void LoadItems()
        {
            Comment post = new Comment();
            post.Id = 1;
            post.Post = "Hello world";
            post.PostDate = DateTime.Now;
            post.Poster = MainControl.Session;

            Border bord = new Border();
            bord.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF1986D1"));
            bord.BorderThickness = new Thickness(0,0,0,1);

            // MinHeight="50" Orientation="Horizontal" MouseEnter="Panel_MouseEnter" MouseLeave="Panel_MouseLeave"

            StackPanel panel = new StackPanel();
            panel.Uid = post.Id.ToString();
            panel.MinHeight = 50;
            panel.Orientation = Orientation.Horizontal;
            panel.MouseEnter += Panel_MouseEnter;
            panel.MouseLeave += Panel_MouseLeave;

            // Width="100" MinHeight="50" HorizontalAlignment="Left" Orientation="Vertical"
            StackPanel p2 = new StackPanel();
            p2.Width = 120;
            p2.MinHeight = 50;
            p2.HorizontalAlignment = HorizontalAlignment.Left;
            p2.Orientation = Orientation.Vertical;

            // FontSize="8" Foreground="#FF2C71D4"
            Label labelFullname = new Label();
            labelFullname.Content = post.Poster.Firstname + " " + post.Poster.Lastname;
            labelFullname.FontSize = 9;
            labelFullname.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2C71D4"));

            Label labelDate = new Label();
            labelDate.Content = post.PostDate.ToString("dd-MM-yyyy HH:mm:ss");
            labelDate.FontSize = 8;
            labelDate.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF1C8A95"));

            // Width="272" Margin="0,5"
            StackPanel p3 = new StackPanel();
            p3.Width = 352;
            p3.Margin = new Thickness(0,5,0,5);

            TextBlock txt = new TextBlock();
            txt.FontSize = 9;
            txt.TextWrapping = TextWrapping.WrapWithOverflow;
            txt.Text = post.Post;

            // Assembly items
            // Lowest child
            p3.Children.Add(txt);
            // Lowest child
            p2.Children.Add(labelFullname);
            p2.Children.Add(labelDate);
            // 2nd child
            panel.Children.Add(p2);
            panel.Children.Add(p3);

            // 1st child
            bord.Child = panel;

            // Comment list
            panelComment.Children.Add(bord);
        }
    }
}
