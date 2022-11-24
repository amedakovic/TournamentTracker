using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace ProjectTrackerUI
{
    public interface ITeamRequestor
    {
        void TeamComplete(TeamModel model);
    }
}
