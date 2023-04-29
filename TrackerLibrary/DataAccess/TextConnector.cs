using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;
using TrackerLibrary.DataAccess.TextHelpers;
using System.ComponentModel;

namespace TrackerLibrary.DataAccess
{
    public class TextConnector : IDataConnection
    {
        //TODO - Implement the CreatePrize method

        private const string PrizesFile = "PrizesModels.csv";
        private const string PeopleFile = "PeopleModels.csv";
        private const string TeamFile = "TeamModels.csv";
        private const string TournamentFile = "TournamentModels.csv";
        private const string MatchupFile = "MatchupModels.csv";
        private const string MatchupEntriesFile = "MatchupEntryModels.csv";
        public PrizeModel CreatePrize(PrizeModel model)
        {
            List<PrizeModel> prizes =  PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();

            int currentId = 1;

            if (prizes.Count > 0)
            {
                currentId = prizes.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;

            prizes.Add(model);

            prizes.SaveToPrizeFile(PrizesFile);

            return model;
        }

        public PersonModel CreatePerson(PersonModel model)
        {
            List<PersonModel> people = PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();

            int currentId = 1;

            if (people.Count > 0)
            {
                currentId = people.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;

            people.Add(model);

            people.SaveToPeopleFile(PeopleFile);

            return model;
        }

        public BindingList<PersonModel> GetPerson_All()
        {
            List<PersonModel> p = PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();
            return new BindingList<PersonModel>(p);
        }

        public TeamModel CreateTeam(TeamModel model)
        {
            List<TeamModel> teams = TeamFile.FullFilePath().LoadFile().ConvertToTeamModels(PeopleFile);
            int currentId = 1;

            if (teams.Count > 0)
            {
                currentId = teams.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;

            teams.Add(model);

            teams.SaveToTeamFile(TeamFile);

            return model;
        }
        public void CreateTournament(TournamentModel model)
        {
            

            List<TournamentModel> tournaments = TournamentFile.FullFilePath().LoadFile().ConvertToTournamentModels(TeamFile, PeopleFile, PrizesFile);

            int currentId = 1;

            if(tournaments.Count > 0)
            {
                currentId = tournaments.OrderBy(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;

            model.SaveRoundsToFile(MatchupFile, MatchupEntriesFile);

            tournaments.Add(model);

            tournaments.SaveToTournamentFile(TournamentFile);

            

        }

        public BindingList<TeamModel> GetTeam_All()
        {
            List<TeamModel> t = TeamFile.FullFilePath().LoadFile().ConvertToTeamModels(PeopleFile);
            return new BindingList<TeamModel>(t);
        }

      

        BindingList<TournamentModel> IDataConnection.GetTournament_All()
        {
            var output = TournamentFile.FullFilePath().LoadFile().ConvertToTournamentModels(TeamFile, PeopleFile, PrizesFile);
            return new BindingList<TournamentModel>(output);
        }

        public void UpdataMatchup(MatchupModel model)
        {
            throw new NotImplementedException();
        }
    }
}
