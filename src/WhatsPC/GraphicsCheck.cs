using System.Drawing;
using System.Management;
using System.Threading.Tasks;

namespace WhatsPC
{
    // Check for graphics adapter infos
    public class GraphicsCheck : SystemCheck
    {
        public override async Task RunCheck(Logger logger, Color color)
        {
            await Task.Run(() =>
            {
                string graphicsInfo = "";
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");

                foreach (ManagementObject obj in searcher.Get())
                {
                    graphicsInfo += $"Graphics Adapter: {obj["Caption"]}\n";
                }

                logger.Log(graphicsInfo, color);
            });
        }
    }
}