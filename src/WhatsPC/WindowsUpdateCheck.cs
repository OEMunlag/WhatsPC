using System;
using System.Drawing;
using System.Threading.Tasks;
using WUApiLib;

namespace WhatsPC
{
    public class WindowsUpdateCheck : SystemCheck
    {
        public override async Task RunCheck(Logger logger, Color color)
        {
            await Task.Run(() =>
            {
                bool updatesAvailable = CheckForUpdates();

                if (updatesAvailable)
                {
                    logger.Log("Windows updates are available. Consider installing them for improved system security.", Color.PaleVioletRed);
                }
                else
                {
                    logger.Log("No pending Windows updates found.", Color.Green);
                }
            });
        }

        private bool CheckForUpdates()
        {
            try
            {
                UpdateSession updateSession = new UpdateSession();
                IUpdateSearcher updateSearcher = updateSession.CreateUpdateSearcher();

                // Search for pending updates
                ISearchResult searchResult = updateSearcher.Search("IsInstalled=0");

                return (searchResult.Updates.Count > 0);
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error checking for Windows updates: {ex.Message}");
                return false; 
            }
        }
    }
}
