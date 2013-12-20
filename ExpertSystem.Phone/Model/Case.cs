using System.Windows.Media;
using ExpertSystem.Phone.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpertSystem.Phone.Model
{
    public class CaseHolder
    {
        public Case @case { get; set; }
    }

    public class Case
    {
        public string text { get; set; }
        public string image { get; set; }

        public int id { get; set; }
        public List<Answer> answers { get; set; }
    }
}
