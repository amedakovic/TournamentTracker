﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class MatchupEntryModel
    {
        public int Id { get; set; }

        /// <summary>
        /// Represents one team in a matchup
        /// </summary>
        public TeamModel TeamCompeting { get; set; }
        /// <summary>
        /// Represents score for this particular team
        /// </summary>
        public double Score { get; set; }
        /// <summary>
        /// Represents matchup that this team came from as winner
        /// </summary>
        public MatchupModel ParentMatchup { get; set; }
        public int TeamCompetingId { get; set; }
        public int ParentMatchupId { get; set; }
    }
}
