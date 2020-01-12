using System;
using System.Collections.Generic;
using System.Text;

namespace FootBallStats.Data.Competitions
{
    public class Links
    {
        public Self self { get; set; }
        public Teams teams { get; set; }
        public Fixtures fixtures { get; set; }
        public LeagueTable leagueTable { get; set; }
    }
}
