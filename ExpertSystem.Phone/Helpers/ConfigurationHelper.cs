using System;
using System.IO;
using System.Windows;

namespace ExpertSystem.Phone.Helpers
{
    /// <summary>
    /// Helper class to access Application Resources
    /// </summary>
    public static class ConfigurationHelper
    {
        //keys being used as parameters to navigation to the pages
        public const string CaseIdKey = "caseId";

        public static string ConnectionFailedMessage
        {
            get { return Application.Current.Resources["ConnectionFailedMessage"].ToString(); }
        }

        public static class WebService
        {
            private static string RootUrl
            {
                get
                {
                    return Application.Current.Resources["WebServiceRootUrl"].ToString();
                }
            }

            public static string ScenariosRoute
            {
                get
                {
                    return Path.Combine(RootUrl, Application.Current.Resources["WebServiceScenariosUrl"].ToString());
                }
            }

            public static string CaseRoute
            {
                get
                {
                    return Path.Combine(RootUrl, Application.Current.Resources["WebServiceCasesUrl"].ToString());
                }
            }
        }
    }
}
