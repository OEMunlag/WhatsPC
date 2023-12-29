using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace WhatsPC
{
    // Some Intro, can be removed maybe!
    public class SuggestionCheck : SystemCheck
    {
        private Logger logger;

        public SuggestionCheck(Logger logger)
        {
            this.logger = logger;
        }

        public override async Task RunCheck(Logger logger, Color color)
        {
            List<string> suggestions = new List<string>();

            if (ShouldUseAsFileServer())
            {
                suggestions.Add($"Hi, {Environment.UserName}");
                suggestions.Add("!(This is for informational purposes only) If WhatsPC app can't help you with your decision to throw away your PC, consider the following.");
                suggestions.Add("- Consider using your computer as a small file server.");
            }

            if (ShouldKeepAsBackup())
            {
                suggestions.Add("- Keep your computer as a backup.");
            }

            if (ShouldInstallLinuxLite())
            {
                suggestions.Add("- Consider installing a lightweight OS like Linux Lite.");
            }

            if (ShouldSellOrGiveAway())
            {
                suggestions.Add("- Explore the option of selling or giving away your computer.");
            }

            // Log suggestions with specified color
            await Task.Run(() => logger.Log(suggestions, color));
        }


        private bool ShouldUseAsFileServer()
        {
            return true;
        }

        private bool ShouldKeepAsBackup()
        {
            return true;
        }

        private bool ShouldInstallLinuxLite()
        {
            return true;
        }

        private bool ShouldSellOrGiveAway()
        {
            return true;
        }
    }
}
