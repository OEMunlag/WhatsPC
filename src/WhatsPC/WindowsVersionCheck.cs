using System;
using System.Drawing;
using System.Management;
using System.Threading.Tasks;
using WhatsPC;

public class WindowsVersionCheck : SystemCheck
{
    public override async Task RunCheck(Logger logger, Color color)
    {
        await Task.Run(() =>
        {
            // Check if Windows 11
            bool isWin11 = GetWindowsVersionInfo(logger, out string versionInfo);

            // Log Windows version
            logger.Log($"Windows Version: {versionInfo}", isWin11 ? Color.LightSeaGreen : color);

            // Log additional sys infos
            LogSystemInfo(logger, color);
        });
    }

    // Log other sys infos
    private void LogSystemInfo(Logger logger, Color color)
    {
        string osVersionInfo = GetOSVersionInfo();

        logger.Log($"OS Platform: {Environment.OSVersion.Platform}", color);
        logger.Log($"OS Version: {osVersionInfo}", color);
        logger.Log($"64-bit OS: {Environment.Is64BitOperatingSystem}", color);
        logger.Log($"64-bit Process: {Environment.Is64BitProcess}", color);
        logger.Log($"Processor Count: {Environment.ProcessorCount}", color);
        logger.Log($"System Directory: {Environment.SystemDirectory}", color);
        logger.Log($"User Domain Name: {Environment.UserDomainName}", color);
        logger.Log($"User Name: {Environment.UserName}", color);
        logger.Log($"Machine Name: {Environment.MachineName}", color);
        logger.Log($"CLR Version: {Environment.Version}", color);
    }

    private string GetOSVersionInfo()
    {
        try
        {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem"))
            {
                ManagementObjectCollection information = searcher.Get();

                foreach (ManagementObject obj in information)
                {
                    string productName = obj["Caption"] as string;
                    string version = obj["Version"] as string;
                    return $"{productName} (Version {version})";
                }
            }
        }
        catch (Exception ex)
        {
            return $"Error retrieving OS version: {ex.Message}";
        }

        return "Unknown OS Version";
    }

    private bool GetWindowsVersionInfo(Logger logger, out string versionInfo)
    {
        try
        {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem"))
            {
                ManagementObjectCollection information = searcher.Get();

                foreach (ManagementObject obj in information)
                {
                    int osBuild = Convert.ToInt32(obj["BuildNumber"]);
                    versionInfo = $"Build {osBuild}";

                    if (osBuild >= 21996)
                    {
                        versionInfo += " Windows 11 (VERY NICE!)";
                        return true;
                    }
                    else
                    {
                        versionInfo += " Not Windows 11 (GOOD!)";
                        return false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            versionInfo = $"Error checking Windows version: {ex.Message}";
            logger.Log(versionInfo, Color.Red);
            return false;
        }

        versionInfo = "Unknown OS Version";
        return false;
    }
}