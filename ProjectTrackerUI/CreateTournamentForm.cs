using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace ProjectTrackerUI
{
    public partial class CreateTournamentForm : Form, IPrizeRequestor, ITeamRequestor
    {

        BindingList<TeamModel> availableTeams = GlobalConfig.Connection.GetTeam_All();
        BindingList<TeamModel> selectedTeams = new BindingList<TeamModel>();
        BindingList<PrizeModel> selectedPrizes = new BindingList<PrizeModel>();
        public CreateTournamentForm()
        {
            InitializeComponent();

            InitializeLists();
        }

        private void roundDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            TeamModel t = (TeamModel)tournametTeamsListBox.SelectedItem;

            if(t != null)
            {
                selectedTeams.Remove(t);
                availableTeams.Add(t);
            }
        }


        private void InitializeLists()
        {
            selectTeamDropDown.DataSource = availableTeams;
            selectTeamDropDown.DisplayMember = "TeamName";


            tournametTeamsListBox.DataSource = selectedTeams;
            tournametTeamsListBox.DisplayMember = "TeamName";

            prizesListBox.DataSource = selectedPrizes;
            prizesListBox.DisplayMember = "PlaceName";
        }

        private void addTeamButton_Click(object sender, EventArgs e)
        {
            TeamModel t = (TeamModel)selectTeamDropDown.SelectedItem;
            if(t != null)
            {
                availableTeams.Remove(t);
                selectedTeams.Add(t);
            }
        }

        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            CreatePrizeForm frm = new CreatePrizeForm(this);
            frm.Show();
        }

        public void PrizeComplete(PrizeModel model)
        {
            selectedPrizes.Add(model);
        }

        public void TeamComplete(TeamModel model)
        {
            selectedTeams.Add(model);
        }

        private void createNewTeamLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateTeamForm fmr = new CreateTeamForm(this);
            fmr.Show();
        }

        private void removeSelectedPrizeButton_Click(object sender, EventArgs e)
        {
            PrizeModel p = (PrizeModel)prizesListBox.SelectedItem;

            if(p != null)
            {
                selectedPrizes.Remove(p);
            }
        }

        private void createTournamentButton_Click(object sender, EventArgs e)
        {
            decimal fee = 0;
            bool feeAcceptable = decimal.TryParse(enteryFeeValue.Text, out fee);
            if(!feeAcceptable)
            {
                MessageBox.Show("You need to enter a valide entry fee", "Invalid fee", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TournamentModel tm = new TournamentModel();
            tm.TournamentName = tournamentNameValue.Text;
            tm.EntryFee = fee;
            foreach (PrizeModel prize in selectedPrizes)
            {
                tm.Prizes.Add(prize);
            }
            foreach (TeamModel team in selectedTeams)
            {
                tm.EnteredTeams.Add(team);
            }

            TournamentLogic.CreateRounds(tm);

            GlobalConfig.Connection.CreateTournament(tm);
        }
    }
}
