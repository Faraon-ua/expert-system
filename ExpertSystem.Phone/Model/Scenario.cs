using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpertSystem.Phone.Model
{
    public class Scenarios
    {
        private List<Scenario> _scenarios;
        public List<Scenario> scenarios
        {
            get
            {
                if(_scenarios!=null)
                    return _scenarios.OrderBy(entry => entry.text).ToList();
                return null;
            }

            set
            {
                _scenarios = value; 
            }
        }
    }

    public class Scenario
    {
        public string text { get; set; }
        public int id { get; set; }
        public int caseId { get; set; }
    }
}
