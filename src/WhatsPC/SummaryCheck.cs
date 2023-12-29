using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using WhatsPC;

namespace WhatsPC
{
    public class SummaryCheck : SystemCheck
    {
        private readonly List<SystemCheck> individualChecks;

        public SummaryCheck(List<SystemCheck> checks)
        {
            individualChecks = checks;
        }

        public override async Task RunCheck(Logger logger, Color color)
        {
            await Task.Run(() =>
            {
                try
                {
                    logger.Log("Running summary check...\n", Color.Gray);

                    // Check individual params
                    CheckErrorLogs(logger);
                    CheckDiskSpace(logger);
                    CheckMemoryUsage(logger);

                    // Overall recommendations
                    ProvideRecommendations(logger, color);
                }
                catch (Exception ex)
                {
                    logger.Log("Error during summary check: " + ex.Message, Color.Red);
                    Console.WriteLine("Error during summary check: " + ex.Message);
                }
            });
        }

        private void CheckErrorLogs(Logger logger)
        {
            var errorLogsCheck = individualChecks.OfType<ErrorLogsCheck>().FirstOrDefault();
            if (errorLogsCheck != null)
            {
                var errorCount = errorLogsCheck.GetErrorCount();

                logger.Log($"Number of critical errors in logs: {errorCount}", Color.Red);
            }
        }

        private void CheckDiskSpace(Logger logger)
        {
            var diskSpaceCheck = individualChecks.OfType<DiskSpaceCheck>().FirstOrDefault();
            if (diskSpaceCheck != null)
            {
                var freeSpace = diskSpaceCheck.GetFreeSpace();

                logger.Log($"Free disk space: {freeSpace} GB", Color.Black);
            }
        }

        private void CheckMemoryUsage(Logger logger)
        {
            var memoryUsageCheck = individualChecks.OfType<MemoryUsageCheck>().FirstOrDefault();
            if (memoryUsageCheck != null)
            {
                var usedMemory = memoryUsageCheck.GetUsedMemory();

                logger.Log($"Used memory: {usedMemory} MB", Color.Blue);
            }
        }

        private void ProvideRecommendations(Logger logger, Color color)
        {
            // Critical errors or low disk space?
            if (CheckCriticalErrors() || CheckLowDiskSpace())
            {
                logger.Log("Consider taking action to improve system health.", Color.Red);
            }
            else
            {
                logger.Log("System health is good. No critical issues found.", Color.Green);
            }
        }

        // Check for critical errors in logs
        private bool CheckCriticalErrors()
        {
            var errorLogsCheck = individualChecks.OfType<ErrorLogsCheck>().FirstOrDefault();
            return errorLogsCheck?.GetErrorCount() > 0;
        }

        // Free disk space is below certain threshold?
        private bool CheckLowDiskSpace()
        {
            var diskSpaceCheck = individualChecks.OfType<DiskSpaceCheck>().FirstOrDefault();
            return diskSpaceCheck?.GetFreeSpace() < 10; // threshold of 10 GB
        }
    }
}