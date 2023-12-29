using WhatsPC;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;

namespace WhatsPC
{
    // Check Windows system uptime
    public class SystemUptimeCheck : SystemCheck
    {
        private const int RebootThresholdHours = 24;

        public override async Task RunCheck(Logger logger, Color color)
        {
            await Task.Run(() =>
            {
                try
                {
                    logger.Log("Checking Windows system uptime...\n", Color.Fuchsia);

                    // Measure Windows system uptime
                    TimeSpan systemUptime = GetSystemUptime();

                    // Log system uptime
                    logger.Log($"System Uptime: {systemUptime.TotalHours} hours", color);

                    // Provide recommendations
                    string recommendation = GetRebootRecommendation(systemUptime);
                    logger.Log(recommendation, color);
                }
                catch (Exception ex)
                {
                    logger.Log("Error checking system uptime: " + ex.Message, Color.Red);
                    Console.WriteLine("Error checking system uptime: " + ex.Message);
                }
            });
        }

        private TimeSpan GetSystemUptime()
        {
            using (var uptime = new PerformanceCounter("System", "System Up Time"))
            {
                uptime.NextValue();
                return TimeSpan.FromSeconds(uptime.NextValue());
            }
        }

        private string GetRebootRecommendation(TimeSpan systemUptime)
        {
            if (systemUptime.TotalHours >= RebootThresholdHours)
            {
                return "Your system has been running for an extended period. Consider rebooting your PC to refresh system resources.";
            }
            else
            {
                return "Your system uptime is within a reasonable range. No immediate need for a reboot.";
            }
        }
    }
}