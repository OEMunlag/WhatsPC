using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhatsPC
{
    // Check for available disk space
    public class DiskSpaceCheck : SystemCheck
    {
        public override async Task RunCheck(Logger logger, Color color)
        {
            await Task.Run(() =>
            {
                DriveInfo driveC = new DriveInfo("C");
                long availableSpace = driveC.AvailableFreeSpace;
                string message = $"Available Disk Space on C: Drive: {FormatBytes(availableSpace)}";
                logger.Log(message, color);
            });
        }

        private string FormatBytes(long bytes)
        {
            string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
            int suffixIndex = 0;
            double doubleBytes = bytes;

            while (doubleBytes >= 1024 && suffixIndex < suffixes.Length - 1)
            {
                doubleBytes /= 1024;
                suffixIndex++;
            }

            return $"{doubleBytes:0.##} {suffixes[suffixIndex]}";
        }

        // Free disk space
        public double GetFreeSpace()
        {
            DriveInfo driveInfo = new DriveInfo("C"); 

            double freeSpaceInGB = driveInfo.AvailableFreeSpace / (1024.0 * 1024.0 * 1024.0);

            return freeSpaceInGB;
        }
    }
}