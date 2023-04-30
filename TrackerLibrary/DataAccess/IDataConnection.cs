using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess
{
    public interface IDataConnection
    {
        void CreatePrize(PrizeModel model);
        void CreatePerson(PersonModel model);
        BindingList<PersonModel> GetPerson_All();
        void CreateTeam(TeamModel model);
        BindingList<TeamModel> GetTeam_All();
        void CreateTournament(TournamentModel model);
        BindingList<TournamentModel> GetTournament_All();
        void UpdateMatchup(MatchupModel model);
    }
}
