using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ExpertSystem.Phone.Helpers;
using ExpertSystem.Phone.ViewModelNamespace;
using Microsoft.Phone.Controls;
using System.Windows.Media.Imaging;
using Telerik.Windows.Controls;
using System.Windows.Media;
using ExpertSystem.Phone.Model;

namespace ExpertSystem.Phone
{
    public partial class CasePage : PhoneApplicationPage
    {
        //default option to answers select list
        private const string AnswerHint = "Select Answer";

        //get case id from navigation uri
        private string _caseId;
        private string CaseId
        {
            get
            {
                NavigationContext.QueryString.TryGetValue(ConfigurationHelper.CaseIdKey, out _caseId);
                return _caseId;
            }
        }

        private Case Case { get; set; }

        public CasePage()
        {
            InitializeComponent();
            BackKeyPress += MainPage_BackKeyPress;
        }

        void MainPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UserControlsHelper.HideSplashPopup();
        }

        protected override async void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            // do not load data if navigated to the page with return button
            if (e.NavigationMode != System.Windows.Navigation.NavigationMode.Back)
            {
                UserControlsHelper.ShowSplashPopup();
                var esCaseHolder = await ViewModel.Instance.GetCase(CaseId);
                if (esCaseHolder != null)
                {
                    Case = esCaseHolder.@case;
                    SetCaseControls();
                }
            }
            else
            {
                answersList.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// set page controls values
        /// </summary>
        private void SetCaseControls()
        {
            answersList.PopupHeader = caseTitle.Text = Case.text;
            caseImage.Stretch = System.Windows.Media.Stretch.Uniform;
            answersList.Visibility = Case.answers == null ? Visibility.Collapsed : Visibility.Visible;
            if (answersList.Visibility == Visibility.Visible)
            {
                answersList.Items.Add(AnswerHint);
                foreach (var answer in Case.answers)
                {
                    answersList.Items.Add(answer.text);
                }
            }
            if (Case.image != null)
            {
                caseImage.Source = new BitmapImage(new Uri(Case.image));
                caseImage.ImageOpened += caseImage_ImageOpened;
                caseImage.ImageFailed += caseImage_ImageFailed;
            }
            else
            {
                UserControlsHelper.HideSplashPopup();
            }
        }

        void caseImage_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            UserControlsHelper.HideSplashPopup();
            caseImage.Source = (ImageSource)new ImageSourceConverter().ConvertFromString("Assets/noimage.png");
        }

        //When Image loaded - remove splash screen
        void caseImage_ImageOpened(object sender, RoutedEventArgs e)
        {
            UserControlsHelper.HideSplashPopup();
        }

        private void answersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedList = sender as RadListPicker;
            if (selectedList.SelectedItem != AnswerHint)
            {
                var answer = Case.answers.FirstOrDefault(entry => entry.text == selectedList.SelectedItem);
                if (answer.caseId != null)
                {
                    var navigationString = string.Format("/CasePage.xaml?{0}={1}", ConfigurationHelper.CaseIdKey, answer.caseId);
                    NavigationService.Navigate(new Uri(navigationString, UriKind.Relative));
                }
            }
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}