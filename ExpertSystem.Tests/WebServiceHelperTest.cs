using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpertSystem.Tests
{
    [TestClass]
    public class WebServiceHelperTest
    {
        [TestMethod]
        public async void GetJsonWithValidUrl()
        {
            var validUrl = "http://expert-system.internal.shinyshark.com/scenarios/";
            var result = await ExpertSystem.Phone.Helpers.WebServiceHelper.Instance.GetJson(validUrl);
            StringAssert.Contains(result, "{\"scenarios\": [{\"text\":");
        }

        [TestMethod]
        public async void GetJsonWithUnValidUrl()
        {
            var validUrl = "some unvalid url";
            var result = await ExpertSystem.Phone.Helpers.WebServiceHelper.Instance.GetJson(validUrl);
            Assert.IsNull(result);
        }


    }
}
