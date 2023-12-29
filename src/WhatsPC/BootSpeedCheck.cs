using WhatsPC;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;

namespace WhatsPC
{
	// Check BIOS/UEFI time and provide recommendations
	public class BIOSBootTimeCheck : SystemCheck
	{
		private const int FastBiosTimeThresholdMS = 2000; // some threshold agaiN!

		public override async Task RunCheck(Logger logger, Color color)
		{
			await Task.Run(() =>
			{
				try
				{
					logger.Log("Checking BIOS/UEFI time...\n", Color.Fuchsia);

					// Measure BIOS/UEFI time
					long biosTimeMS = GetBiosTime();

					// Log BIOS/UEFI time
					logger.Log($"BIOS/UEFI Time: {biosTimeMS} milliseconds", color);

					// Add recommendations based on BIOS/UEFI time
					string recommendation = GetBiosTimeRecommendation(biosTimeMS);
					logger.Log(recommendation, color);
				}
				catch (Exception ex)
				{
					logger.Log("Error checking BIOS/UEFI time: " + ex.Message, Color.Red);
				
				}
			});
		}

		private long GetBiosTime()
		{
			try
			{
				using (var uptime = new PerformanceCounter("System", "System Up Time"))
				{
					uptime.NextValue(); // get first value
					long systemUptimeMS = (long)(uptime.NextValue() * 1000); // Convert to milliseconds
					return systemUptimeMS;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error getting BIOS/UEFI time: " + ex.Message);
			}

			return -1;
		}

		private string GetBiosTimeRecommendation(long biosTimeMS)
		{
			if (biosTimeMS <= FastBiosTimeThresholdMS)
			{
				return "Your BIOS/UEFI time is fast! The system boots quickly.";
			}
			else
			{
				return "Your BIOS/UEFI time is within a reasonable range.";
			}
		}
	}
}
