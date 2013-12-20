using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ExpertSystem.Phone.UserControls
{
    public partial class PopupSplash : UserControl
    {
        public PopupSplash()
        {
            InitializeComponent();
            Loaded += PopupSplash_Loaded;
            progressBar1.IsIndeterminate = true;
        }

        void PopupSplash_Loaded(object sender, RoutedEventArgs e)
        {
            Height = Application.Current.Host.Content.ActualHeight;
            Width = Application.Current.Host.Content.ActualWidth;
            Background = new SolidColorBrush(Colors.Green);
        }
    }
}
