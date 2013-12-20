using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpertSystem.Phone.ViewModelNamespace;
using ExpertSystem.Phone.Model;

namespace ExpertSystem.Tests
{
    [TestClass]
    class ViewModelTest
    {
        [TestMethod]
        public async void GetScenariosTest()
        {
            var scenariosHolder = await ViewModel.Instance.GetScenarios();
            Assert.IsInstanceOfType(scenariosHolder, typeof(Scenarios));
            Assert.IsNotNull(scenariosHolder);
            Assert.AreNotEqual(0, scenariosHolder.scenarios.Count);
        }

        [TestMethod]
        public async void GetCaseTest()
        {
            var caseHolder = await ViewModel.Instance.GetCase("1");
            Assert.IsInstanceOfType(caseHolder, typeof(CaseHolder));
            Assert.IsNotNull(caseHolder);
        }
    }
}
