using System;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Windows;
using ExpertSystem.Phone.Resources;

namespace ExpertSystem.Phone.Helpers
{
    public class WebServiceHelper
    {
        private WebServiceHelper() { }

        private static WebServiceHelper _instance;

        public static WebServiceHelper Instance
        {
            get { return _instance ?? (_instance = new WebServiceHelper()); }
        }

        /// <summary>
        /// Get json string from provided url
        /// </summary>
        /// <param name="url">url of the web service</param>
        /// <returns>json string</returns>
        public async Task<string> GetJson(string url)
        {
            // using HttpClient to get json string.
            HttpClient client = new HttpClient();
            string responseBody = null;
            // Call asynchronous network methods in a try/catch block to handle exceptions 
            try
            {
                responseBody = await client.GetStringAsync(url);
            }
            catch (HttpRequestException e)
            {
                return null;
            }

            // Need to call dispose on the HttpClient object 
            // when done using it, so the app doesn't leak resources
            client.Dispose();
            return responseBody;
        }
    }
}
