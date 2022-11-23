namespace ProjectTrackerUI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            //Initilize database connections
            TrackerLibrary.GlobalConfig.InitilizeConnections(TrackerLibrary.DataBaseType.TextFile);
            //Application.Run(new TournamentDashboardForm());
            Application.Run(new CreateTeamForm());
        }
    }
}