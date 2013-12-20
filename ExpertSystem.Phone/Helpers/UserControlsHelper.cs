using System;
using System.Linq;
using System.Windows.Controls.Primitives;
using ExpertSystem.Phone.UserControls;

namespace ExpertSystem.Phone.Helpers
{
    public static class UserControlsHelper
    {
        public static Popup Popup;

        /// <summary>
        /// Displays loading popup screen
        /// </summary>
        public static void ShowSplashPopup()
        {
            Popup = new Popup();
            Popup.Child = new PopupSplash();
            Popup.IsOpen = true;
        }

        /// <summary>
        /// Closes loading popup screen
        /// </summary>
        public static void HideSplashPopup()
        {
            if (Popup != null)
                if (Popup.IsOpen)
                    Popup.IsOpen = false;
        }
    }
}
