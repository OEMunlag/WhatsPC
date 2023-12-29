using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhatsPC
{
    public class NetworkSpeedCheck : SystemCheck
    {

        public override async Task RunCheck(Logger logger, Color color)
        {
            await Task.Run(() =>
            {
                // Get current network speed
                int currentNetworkSpeed = GetCurrentNetworkSpeed();

            if (currentNetworkSpeed < MinimumNetworkSpeed)
            {
                logger.Log($"Current Network Speed is low ({currentNetworkSpeed} Mbps). Consider upgrading your network hardware.", Color.Red);

            } else
                logger.Log($"Current Network Speed is fine with {currentNetworkSpeed} Mbps.", Color.Green);
        });
        }

        private int GetCurrentNetworkSpeed()
        {
            int maxSpeed = 0;

            // Iterate over network interfaces to find maximum speed
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                // Now non-loopback interfaces
                if (nic.OperationalStatus == OperationalStatus.Up && !nic.Description.ToLowerInvariant().Contains("loopback"))
                {
                    // Get max speed in bits per second
                    long speed = nic.Speed;

                    // Convert speed to megabits per second
                    int speedMbps = (int)(speed / 1_000_000);

                    // Update maxSpeed if current interface has a higher speed
                    if (speedMbps > maxSpeed)
                    {
                        maxSpeed = speedMbps;
                    }
                }
            }

            return maxSpeed;
        }

        private const int MinimumNetworkSpeed = 100; // Hmm, minimum Speed in Mbps! Improve!!!
    }
}