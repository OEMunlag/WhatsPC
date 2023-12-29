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
    // Check for storage type (SSD or HDD)
    public class StorageTypeCheck : SystemCheck
    {
        public override async Task RunCheck(Logger logger, Color color)
        {
            await Task.Run(() =>
            {
                DriveInfo driveC = new DriveInfo("C");
                string storageType = driveC.DriveType == DriveType.Fixed && driveC.DriveFormat.Equals("NTFS", StringComparison.OrdinalIgnoreCase)
                    ? "SSD"
                    : "HDD";

                logger.Log($"System Drive (C:) is a {storageType}.", Color.LightSeaGreen);
            });
        }
    }
}