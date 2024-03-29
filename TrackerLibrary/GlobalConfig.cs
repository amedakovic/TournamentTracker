﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.DataAccess;

namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        public const string PrizesFile = "PrizesModels.csv";
        public const string PeopleFile = "PeopleModels.csv";
        public const string TeamFile = "TeamModels.csv";
        public const string TournamentFile = "TournamentModels.csv";
        public const string MatchupFile = "MatchupModels.csv";
        public const string MatchupEntriesFile = "MatchupEntryModels.csv";
        public static IDataConnection Connection { get; private set; }
        public static void InitilizeConnections(DataBaseType connectionType)
        {
            if (connectionType == DataBaseType.Sql)
            {
                //TODO - Create SQL Connection properly
                SQLConnector sql = new SQLConnector();
                Connection = sql;
            }
            else if (connectionType == DataBaseType.TextFile)
            {
                //TODO - Create Text Connection
                TextConnector text = new TextConnector();
                Connection = text;
            }
        }
        public static string CnnString(string name)
        {
            return ConfigurationManager.ConnectionStrings["Tournaments"].ConnectionString;
        }
    }
}
