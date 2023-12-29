using System;
using System.Collections.Generic;
using System.Drawing;
using System.Management;
using System.Threading.Tasks;

namespace WhatsPC
{
    // Recommendation check
    public class RecommendationCheck : SystemCheck
    {
        private const int MinimumCPUSpeed = 2000; // min CPU speed
        private const int MinimumRAMSize = 4; // min RAM size
        private const long MinimumDiskSpace = 1000000000; // min disk space (1 GB in bytes)
        private const int MaximumComputerAge = 5; // max computer age in years

        public override async Task RunCheck(Logger logger, Color color)
        {
            await Task.Run(() =>
            {
                logger.Log("Running other checks we missed.", color);

                int cpuSpeed = GetCPUSpeed(logger, color);
                int ramSize = GetRAMSize(logger, color);
                long diskSpace = GetAvailableDiskSpace(logger, color);
                int computerAge = CalculateComputerAge(logger, color);

                if (cpuSpeed < MinimumCPUSpeed || ramSize < MinimumRAMSize || diskSpace < MinimumDiskSpace || computerAge > MaximumComputerAge)
                {
                    logger.Log("Your computer may not meet the recommended specifications for optimal performance. Consider upgrading or replacing it.", Color.Red);
                }
                else
                {
                    logger.Log("Your computer meets the recommended specifications. It should continue to perform well.", Color.DarkGreen);
                }
            });
        }

        private int GetCPUSpeed(Logger logger, Color color)
        {
            int cpuSpeed = 0;
            try
            {
                logger.Log("Getting CPU speed...", color);
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        cpuSpeed = Convert.ToInt32(obj["CurrentClockSpeed"]);
                    }
                }
                logger.Log($"CPU speed: {cpuSpeed} MHz", color);
            }
            catch (Exception ex)
            {
     
                logger.Log("Error getting CPU speed: " + ex.Message, Color.Red);
                Console.WriteLine("Error getting CPU speed: " + ex.Message);
            }

            return cpuSpeed;
        }

        private int GetRAMSize(Logger logger, Color color)
        {
            int ramSize = 0;
            try
            {
                logger.Log("Getting RAM size...", color);

                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        if (obj["TotalPhysicalMemory"] != null)
                        {
                            long totalPhysicalMemory = Convert.ToInt64(obj["TotalPhysicalMemory"]);
                            ramSize = (int)(totalPhysicalMemory / (1024 * 1024)); // Convert bytes to MB
                            logger.Log($"RAM size: {ramSize} MB", color);
                        }
                        else
                        {
                            logger.Log("TotalPhysicalMemory is null or not available.", Color.Red);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                logger.Log("Error getting RAM size: " + ex.Message, Color.Red);
                Console.WriteLine("Error getting RAM size: " + ex.Message);
            }

            return ramSize;
        }

        private long GetAvailableDiskSpace(Logger logger, Color color)
        {
            long availableDiskSpace = 0;
            try
            {
                logger.Log("Getting available disk space...", color);
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk WHERE DriveType=3"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        availableDiskSpace = Convert.ToInt64(obj["FreeSpace"]);
                    }
                }
                logger.Log($"Available disk space: {availableDiskSpace} bytes", color);
            }
            catch (Exception ex)
            {
              
                logger.Log("Error getting available disk space: " + ex.Message, Color.Red);
                Console.WriteLine("Error getting available disk space: " + ex.Message);
            }

            return availableDiskSpace;
        }

        private int CalculateComputerAge(Logger logger, Color color)
        {
            int computerAge = 0;
            try
            {
                logger.Log("Calculating Windows age (since last install)...", color);
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        DateTime installDate = ManagementDateTimeConverter.ToDateTime(obj["InstallDate"].ToString());
                        computerAge = (DateTime.Now - installDate).Days / 365; // Convert days to years
                    }
                }
                logger.Log($"Computer age: {computerAge} years", color);
            }
            catch (Exception ex)
            {
       
                logger.Log("Error calculating computer age: " + ex.Message, Color.Red);
                Console.WriteLine("Error calculating computer age: " + ex.Message);
            }

            return computerAge;
        }
    }
}