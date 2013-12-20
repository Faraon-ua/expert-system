using ExpertSystem.Phone.Helpers;
using ExpertSystem.Phone.Model;
using ExpertSystem.Phone.Resources;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace ExpertSystem.Phone.ViewModelNamespace
{
    public class ViewModel
    {
        private ViewModel() { }

        private static ViewModel _instance;

        public static ViewModel Instance
        {
            get { return _instance ?? (_instance = new ViewModel()); }
        }

        /// <summary>
        /// Get Json, hadle errors
        /// </summary>
        /// <param name="url">url to get data</param>
        /// <returns>json string</returns>
        private async Task<string> HandleGetJson(string url)
        {
            var json = await WebServiceHelper.Instance.GetJson(url);
            if (json == null)
            {
                var result = MessageBox.Show(AppResources.NoConnectionDescr, AppResources.NoConnectionTitle, MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                    return await HandleGetJson(url);
                else
                    Application.Current.Terminate();
            }
            return json;
        }

        /// <summary>
        /// Get Scenarios from web service
        /// </summary>
        /// <returns>Scenarions object</returns>
        public async Task<Scenarios> GetScenarios()
        {
            var scenariosJson = await HandleGetJson(ConfigurationHelper.WebService.ScenariosRoute);
            Scenarios scenarios = null;
            try
            {
                scenarios = JsonConvert.DeserializeObject<Scenarios>(scenariosJson);
            }
            catch
            {
                MessageBox.Show(AppResources.DeserializationError);
            }
            return scenarios;
        }

        /// <summary>
        /// Get Case from web service
        /// </summary>
        /// <param name="id">case id</param>
        /// <returns>Case object</returns>
        public async Task<CaseHolder> GetCase(string id)
        {
            var caseRoute = string.Format(ConfigurationHelper.WebService.CaseRoute, id);
            var caseJson = await HandleGetJson(caseRoute);
            CaseHolder esCase = null;
            try
            {
                esCase = JsonConvert.DeserializeObject<CaseHolder>(caseJson);
            }
            catch
            {
                MessageBox.Show(AppResources.DeserializationError);
            }
            return esCase;
        }
    }
}
