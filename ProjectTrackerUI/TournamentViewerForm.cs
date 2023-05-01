using System.ComponentModel;
using TrackerLibrary;
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
                            if (m.Winner == null || !(UnplayedOnlyCheckbox.Checked))
                            { 
                                selctedMatchups.Add(m);
                            }
                        }
                        //selctedMatchups = new BindingList<MatchupModel>(matchups);
                    }
                }
            }
            if(selctedMatchups.Count > 0)
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
            DisplayMatchupInfo();
        }

        private void DisplayMatchupInfo()
        {
            bool isVisible = selctedMatchups.Count > 0;
            TeamOneName.Visible = isVisible;
            TeamOneScoreLabel.Visible = isVisible;
            TeamOneScoreValue.Visible = isVisible;
            TeamTwoName.Visible = isVisible;
            TeamTwoScoreLabel.Visible= isVisible;
            TeamTwoScoreValue.Visible= isVisible;
            versusLabel.Visible = isVisible;
            scoreButton.Visible = isVisible;
            
        }
        private void MatchupListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMatchup((MatchupModel)MatchupListBox.SelectedItem);
        }

        private void UnplayedOnlyCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            LoadMatchups();
        }

        private string ValidateData()
        {
            string output = "";
            double teamOneScore;
            double teamTwoScore;

            bool scoreOneValid = double.TryParse(TeamOneScoreValue.Text, out teamOneScore);
            bool scoreTwoValid = double.TryParse(TeamTwoScoreValue.Text, out teamTwoScore);

            if(!scoreOneValid)
            {
                output ="The score one is not a valid number";
            }
            else if (!scoreTwoValid)
            {
                output = "The score two is not a valid number";
            }
            else if (teamOneScore == 0 && teamTwoScore == 0)
            {
                output="You did not enter a score for either team";
            }
            else if(teamOneScore == teamTwoScore)
            {
                output = "We do not allow ties in this application";
            }
            return output;
        }
        private void scoreButton_Click(object sender, EventArgs e)
        {
            string errorMessage = ValidateData();
            if (errorMessage.Length > 0)
            {
                MessageBox.Show($"Input error: {errorMessage}");
                return;
            }

            MatchupModel m = (MatchupModel)MatchupListBox.SelectedItem;
            double teamOneScore = 0;
            double teamTwoScore = 0;

            for (int i = 0; i < m.Entries.Count; i++)
            {
                if (i == 0)
                {
                    if (m.Entries[0].TeamCompeting != null)
                    {
                        teamOneScore = 0;
                        bool scoreValid = double.TryParse(TeamOneScoreValue.Text, out teamOneScore);
                        if (scoreValid)
                        {
                           m.Entries[0].Score = teamOneScore;
                        }
                        else
                        {
                            MessageBox.Show("Please enter valid score for team one");
                            return;
                        }
                         
                    }
                }

                if (i == 1)
                {
                    if (m.Entries[1].TeamCompeting != null)
                    {
                        teamTwoScore = 0;
                        bool scoreValid = double.TryParse(TeamTwoScoreValue.Text, out teamTwoScore);
                        if (scoreValid)
                        {
                            m.Entries[1].Score = teamTwoScore;
                        }
                        else
                        {
                            MessageBox.Show("Please enter valid score for team two");
                            return;
                        }
                    }
                }
            }
            try
            {
                TournamentLogic.UpdateTournamentResults(tournament);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The application had the following error: {ex.Message}");
                return;
            }

            LoadMatchups();

        }
    }
}