using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhatsPC
{
    //IMPROVE THIS!
    // Enum to critical apps
    public enum ImportantApps
    {
        CCleaner,
    }

    public class InstalledAppsCheck : SystemCheck
    {
        private Logger logger;

        public InstalledAppsCheck(Logger logger)
        {
            this.logger = logger;
        }

        public override async Task RunCheck(Logger logger, Color color)
        {
            await Task.Run(() =>
            {
                List<string> installedApps = GetInstalledApps();
                if (installedApps.Any())
                {
                    logger.Log("Let us check your apps:", Color.HotPink);
                    foreach (string app in installedApps)
                    {
                        logger.Log(app, Color.Black);
                    }

                    if (IsAppInstalled(installedApps, ImportantApps.CCleaner))
                    {
                        logger.Log("CCleaner is installed. We do not recommend this app!", Color.Red);
                    }
                    else
                    {
                        logger.Log("No critical apps found. Ensure you have a backup or can reinstall apps before making a decision to throw away the PC.", Color.Green);
                    }
                }
                else
                {
                    logger.Log("No installed apps found.", Color.Green);
                }
            });
        }

        private List<string> GetInstalledApps()
        {
            List<string> installedApps = new List<string>();

            // Check x64bit reg
            GetInstalledAppsFromRegistry(Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"), installedApps);

            // Check 32-bit reg on x64bit Windows
            if (Environment.Is64BitOperatingSystem)
            {
                GetInstalledAppsFromRegistry(Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall"), installedApps);
            }

            // Check current user reg
            GetInstalledAppsFromRegistry(Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"), installedApps);

            return installedApps;
        }

        private void GetInstalledAppsFromRegistry(RegistryKey registryKey, List<string> installedApps)
        {
            if (registryKey != null)
            {
                foreach (string subKeyName in registryKey.GetSubKeyNames())
                {
                    using (RegistryKey subKey = registryKey.OpenSubKey(subKeyName))
                    {
                        object displayName = subKey?.GetValue("DisplayName");
                        if (displayName != null)
                        {
                            installedApps.Add(displayName.ToString());
                        }
                    }
                }
            }
        }

        private bool IsAppInstalled(List<string> installedApps, ImportantApps app)
        {
            string appNameSubstring = Enum.GetName(typeof(ImportantApps), app);
            return installedApps.Any(installedApp => installedApp.Contains(appNameSubstring));
        }
    }
}