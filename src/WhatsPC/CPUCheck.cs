using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhatsPC
{
    // Check for CPU information
    public class CPUCheck : SystemCheck
    {

        public override async Task RunCheck(Logger logger, Color color)
        {
            await Task.Run(() =>
            {
                string cpuInfo = "";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");

            foreach (ManagementObject obj in searcher.Get())
            {
                cpuInfo += $"CPU: {obj["Name"]}\n";
            }

          logger.Log(cpuInfo, color);
            });
        }
    }
}
