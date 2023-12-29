using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsPC
{
    // Check for system error logs in Event viewer
    public class ErrorLogsCheck : SystemCheck
    {
        private const int MaxLogsToShow = 100;

        public override async Task RunCheck(Logger logger, Color color)
        {
            await Task.Run(() =>
            {
                try
                {
                    logger.Log("Checking system error logs (ONLY critical errors will be listed)...\nNo panic! Not all of this is critical, and you shouldn't throw away your PC immediately. Take your time to look at the reports", Color.Fuchsia);

                    // Log name 
                    string logName = "System";

                    // Event type 
                    string eventType = "Error";

                    // Get event logs based on specified log name
                    EventLog eventLog = new EventLog(logName);

                    // Filter events based on specified event type
                    IEnumerable<EventLogEntry> errorEntries = eventLog.Entries
                        .Cast<EventLogEntry>()
                        .Where(entry => entry.EntryType.ToString() == eventType)
                        .Take(MaxLogsToShow); // Limit number of logs to show!

                    // Log error entries
                    if (errorEntries.Any())
                    {
                        foreach (EventLogEntry entry in errorEntries)
                        {
                            string logMessage = $"Instance ID: {entry.InstanceId}, Source: {entry.Source}, Message: {entry.Message}";
                            logger.Log(logMessage, Color.Red);
                        }

                        // Limitation! show a message..
                        if (errorEntries.Count() == MaxLogsToShow)
                        {
                            logger.Log($"Showing the latest {MaxLogsToShow} {eventType} logs. The full list may be viewed in the Event Viewer.", Color.Red);
                        }
                    }
                    else
                    {
                        logger.Log($"No {eventType} logs found in the {logName} log.", color);
                    }
                }
                catch (Exception ex)
                {
                    logger.Log("Error checking system error logs: " + ex.Message, Color.Red);
                    Console.WriteLine("Error checking system error logs: " + ex.Message);
                }
            });


        }
        // Get count of critical errors
        public int GetErrorCount()
        {
            EventLog eventLog = new EventLog("System");

            int criticalErrorCount = eventLog.Entries
                .Cast<EventLogEntry>()
                .Count(entry => entry.EntryType == EventLogEntryType.Error);

            return criticalErrorCount;
        }
    }
}
