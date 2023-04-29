using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class MatchupModel
    {
        public int Id { get; set; }
        public List<MatchupEntryModel> Entries { get; set; } = new List<MatchupEntryModel>();
        public TeamModel Winner { get; set; }
        public int MatchupRound { get; set; }
        public int WinnerId { get; set; }
        public string DisplayName 
        { 
            get 
            {
                string output = "";
                foreach (MatchupEntryModel matchupEntry in Entries)
                {
                    if (matchupEntry.TeamCompeting != null)
                    {
                        if (output.Length == 0)
                        {
                            output = matchupEntry.TeamCompeting.TeamName;
                        }
                        else
                        {
                            output += $" vs {matchupEntry.TeamCompeting.TeamName}";
                        } 
                    }
                    else
                    {
                        output = "Matchup not yet determined";
                        break;
                    }
                }
                return output;
            } 
        }
    }
}
