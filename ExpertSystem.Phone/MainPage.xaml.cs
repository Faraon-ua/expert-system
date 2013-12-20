using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ExpertSystem.Phone.Resources;
using ExpertSystem.Phone.Helpers;
using System.Threading.Tasks;
using ExpertSystem.Phone.ViewModelNamespace;
using ExpertSystem.Phone.Model;
using Telerik.Windows.Controls;

namespace ExpertSystem.Phone
{
    public partial class MainPage : PhoneApplicationPage
    {
        private Scenarios scenariosHolder;
        private List<Scenario> dataSource;

        private const string DataSourceKey = "dataSource";

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Search event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            var currentText = txtSearch.Text;
            //set up timeout after last symbol imput
            var delayedTimer = new DispatcherTimer{Interval = TimeSpan.FromSeconds(1.5)};
            delayedTimer.Tick += (s, args) =>
            {
                //compare text in textbox after timeout
                if (txtSearch.Text == currentText)
                {
                    //filter data
                    var newDataSource = dataSource.Where(entry => entry.text.ToLower().Contains(currentText.ToLower())).ToList();
                    listBox.ItemsSource = newDataSource;
                }
                delayedTimer.Stop();
            };
            delayedTimer.Start();
        }

        /// <summary>
        /// Scenario selected event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void scenario_tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var caseId = (int)(sender as StackPanel).Tag;

            var navigationString = string.Format("/CasePage.xaml?{0}={1}", ConfigurationHelper.CaseIdKey, caseId);

            //put some delay to see animation
            DispatcherTimer timer = new DispatcherTimer();
            int counter = 0;

            timer.Tick +=
                delegate(object s, EventArgs args)
                {
                    timer.Stop();
                    NavigationService.Navigate(new Uri(navigationString, UriKind.Relative));
                };

            timer.Interval = new TimeSpan(0, 0, 0, 0, 300); // one second
            timer.Start();
        }


        protected override async void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //if navigated to the page with back button - do not load data again
            if (e.NavigationMode != System.Windows.Navigation.NavigationMode.Back)
            {
                //display loading popup
                UserControlsHelper.ShowSplashPopup();
                //set tap animation
                InteractionEffectManager.AllowedTypes.Add(typeof(RadDataBoundListBoxItem));

                //get scenarios from web service
                scenariosHolder = await ViewModel.Instance.GetScenarios();
                if (scenariosHolder != null)
                {
                    dataSource = scenariosHolder.scenarios;
                }

                //bound listbox
                if (dataSource != null)
                    listBox.ItemsSource = dataSource;
                //cloase loading popup
                UserControlsHelper.HideSplashPopup();
            }
        }
    }

}