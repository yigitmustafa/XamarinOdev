using System;
using System.Collections.Generic;
using System.Text;

namespace FootBallStats.Data.CompetitionTeams
{
    public class CompetitionTeams
    {
        public Links _links { get; set; }
        public int count { get; set; }
        public List<Team> teams { get; set; }
    }


    public class Self
    {
        public string href { get; set; }
    }

    public class Competition
    {
        public string href { get; set; }
    }

    public class Links
    {
        public Self self { get; set; }
        public Competition competition { get; set; }
    }

    public class Self2
    {
        public string href { get; set; }
    }

    public class Fixtures
    {
        public string href { get; set; }
    }

    public class Players
    {
        public string href { get; set; }
    }

    public class Links2
    {
        public Self2 self { get; set; }
        public Fixtures fixtures { get; set; }
        public Players players { get; set; }
    }

    public class Team
    {
        public Links2 _links { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string shortName { get; set; }
        public object squadMarketValue { get; set; }
        public string crestUrl { get; set; }
    }
}
