using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;

namespace WhatsPC
{
    public class MemoryUsageCheck : SystemCheck
    {
        private const int MaximumMemoryUsage = 4096;

        public override async Task RunCheck(Logger logger, Color color)
        {
            await Task.Run(() =>
            {
                // Get current memory usage
                int currentMemoryUsage = GetCurrentMemoryUsage(logger, color);

                if (currentMemoryUsage > MaximumMemoryUsage)
                {
                    logger.Log($"Current Memory Usage is high ({currentMemoryUsage} MB). Consider closing some Apps or restarting processes.", Color.Red);
                }
                else
                {
                    logger.Log($"Current Memory Usage is fine with {currentMemoryUsage} MB.", Color.Green);
                }
            });
        }

        private int GetCurrentMemoryUsage(Logger logger, Color color)
        {
            try
            {
                using (PerformanceCounter performanceCounter = new PerformanceCounter("Memory", "Available MBytes"))
                {
                    return (int)(performanceCounter.NextValue());
                }
            }
            catch (Exception ex)
            {
                logger.Log($"Error getting Memory Usage: {ex.Message}", Color.Red);
                Console.WriteLine($"Error getting Memory Usage: {ex.Message}");
                return -1; // error
            }
        }

        // Get used memory
        public double GetUsedMemory()
        {
            PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available MBytes");

            double usedMemoryInMB = ramCounter.RawValue;

            return usedMemoryInMB;
        }
    }
}