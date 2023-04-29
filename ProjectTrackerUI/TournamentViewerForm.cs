using System.ComponentModel;
using TrackerLibrary.Models;

namespace ProjectTrackerUI
{
    public partial class TournamentViewerForm : Form
    {
        private TournamentModel tournament;
        List<int> rounds = new List<int>();
        BindingList<MatchupModel> selctedMatchups= new BindingList<MatchupModel>();

        public TournamentViewerForm(TournamentModel tournamentModel)
        {
            InitializeComponent();
            this.tournament = tournamentModel;
            LoadFormData();
            LoadRounds();
            WireUpMatchupsList();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void LoadFormData()
        {
            tournamentName.Text = tournament.TournamentName;

        }
        private void WireUpRoundsList()
        {
            roundDropDown.DataSource = null;
            roundDropDown.DataSource = rounds;
            
        }
        private void WireUpMatchupsList()
        {
            MatchupListBox.DataSource = null;
            MatchupListBox.DataSource = selctedMatchups;
            MatchupListBox.DisplayMember = "DisplayName";
        }
        private void LoadRounds()
        {
            rounds = new List<int>();
            rounds.Add(1);
            int currRound = 1;
            foreach (List<MatchupModel> matchups in tournament.Rounds)
            {
                if(matchups.First().MatchupRound > currRound)
                {
                    currRound++;
                    rounds.Add(currRound);
                }
            }
            WireUpRoundsList();
        }

        private void roundDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMatchups();
        }

        private void LoadMatchups()
        {
            if(roundDropDown.SelectedItem != null)
            {
                int round = (int)roundDropDown.SelectedItem;

                foreach (List<MatchupModel> matchups in tournament.Rounds)
                {
                    if (matchups.First().MatchupRound == round)
                    {
                        selctedMatchups.Clear();
                        foreach (MatchupModel m in matchups)
                        {
                            selctedMatchups.Add(m);
                        }
                        //selctedMatchups = new BindingList<MatchupModel>(matchups);
                    }
                }
            }
            LoadMatchup(selctedMatchups.First());
        }

        private void LoadMatchup(MatchupModel m)
        {
            if (MatchupListBox.SelectedItem != null)
            {

                for (int i = 0; i < m.Entries.Count; i++)
                {
                    if (i == 0)
                    {
                        if (m.Entries[0].TeamCompeting != null)
                        {
                            TeamOneName.Text = m.Entries[0].TeamCompeting.TeamName;
                            TeamOneScoreValue.Text = m.Entries[0].Score.ToString();

                            TeamTwoName.Text = "<bye>";
                            TeamTwoScoreValue.Text = "0";
                        }
                        else
                        {
                            TeamOneName.Text = "Not yet set";
                            TeamOneScoreValue.Text = "";
                        }
                    }

                    if (i == 1)
                    {
                        if (m.Entries[1].TeamCompeting != null)
                        {
                            TeamTwoName.Text = m.Entries[1].TeamCompeting.TeamName;
                            TeamTwoScoreValue.Text = m.Entries[1].Score.ToString();
                        }
                        else
                        {
                            TeamTwoName.Text = "Not yet set";
                            TeamTwoScoreValue.Text = "";
                        }
                    }
                } 
            }

        }
        private void MatchupListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMatchup((MatchupModel)MatchupListBox.SelectedItem);
        }
    }
}