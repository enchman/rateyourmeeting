using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RateYourMeeting
{
    class PageSwitch
    {
        public static Screen CurrentPage;
        private static UserControl currentControl;
        private static UserControl prevControl;

        public static void Forward(UserControl page)
        {
            if(currentControl == null)
            {
                currentControl = page;
                CurrentPage.Navigate(currentControl);
            }
            else
            {
                prevControl = currentControl;
                currentControl = page;
                CurrentPage.Navigate(currentControl);
            }
        }

        public static void Backward()
        {
            if(prevControl != null)
            {
                UserControl temp = currentControl;
                currentControl = prevControl;
                prevControl = temp;
                CurrentPage.Navigate(currentControl);
            }
        }
    }
}
