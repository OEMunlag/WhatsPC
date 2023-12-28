using Microsoft.Web.WebView2.WinForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhatsPC
{
    public partial class EarthPageView : UserControl
    {
        private Logger logger;
        private WebView2 webView;

        public EarthPageView()
        {
            InitializeComponent();

            InitializeWebView();

            // Initialize logger with webView
            logger = new Logger(webView);

            SetStyle();
        }

        private async void InitializeWebView()
        {
            webView = new WebView2
            {
                Dock = DockStyle.None,  // Set DockStyle.None to use Anchor property
                Location = new Point(150, 0),
                Width = 700,            // Set an initial width
                Height = 700,           // Set an initial height
                Parent = this,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
            };

            webView.CoreWebView2InitializationCompleted += webView2_CoreWebView2InitializationCompleted;

            await webView.EnsureCoreWebView2Async(null);

            // Load HTML content into WebView2 control
            string pathToHtmlFile = Application.StartupPath + "\\whatsPC.html";

            if (File.Exists(pathToHtmlFile))
            {
                string htmlContent = File.ReadAllText(pathToHtmlFile);

                // Debugging output to check if HTML content is loaded
                Console.WriteLine("HTML Content:");
                Console.WriteLine(htmlContent);

                webView.NavigateToString(htmlContent); // Load HTML content
            }
            else
            {
                // Debugging output if the HTML file does not exist
                Console.WriteLine("HTML File does not exist!");
            }

            await webView.EnsureCoreWebView2Async();

            // Fire checks button
            webView.WebMessageReceived += (s, e) =>
            {
                string message = e.TryGetWebMessageAsString();
                if (message == "runChecks")
                {
                    // Run checks when the message is received
                    RunChecks();
                }
            };
        }

        private void SetStyle()
        {
            // Segoe MDL2 Assets
            btnHamburger.Text = "\uE700";
            // Some color styling
            pnlNav.BackColor = Color.White;
            BackColor =
               webView.DefaultBackgroundColor = Color.White;
        }

        private async void RunChecks()
        {
            var suggestionCheck = new SuggestionCheck(logger);

            var checks = new List<SystemCheck>
            {
                new SuggestionCheck(logger),
                new CPUCheck(),
                new SystemUptimeCheck(),
                new BIOSBootTimeCheck(),
                new DiskSpaceCheck(),
                new GraphicsCheck(),
                new StorageTypeCheck(),
                new WindowsVersionCheck(),
                new MemoryUsageCheck(),
                new NetworkSpeedCheck(),
                new ErrorLogsCheck(),
                new InstalledAppsCheck(logger),
                new LinuxCompatibilityCheck(),
                new RecommendationCheck(),
                      new WindowsUpdateCheck(),
            };

            foreach (var check in checks)
            {
                // Set different colors for checks
                Color color = GetColorForCheck(check);
                await check.RunCheck(logger, color);
            }

            // Run summary checks
            await RunSummaryChecks(logger);

            MessageBox.Show("All checks performed.");
        }

        private async Task RunSummaryChecks(Logger logger)
        {
            // Create instances of individual checks
            var errorLogsCheck = new ErrorLogsCheck();
            var diskSpaceCheck = new DiskSpaceCheck();

            // Run individual checks
            // await errorLogsCheck.RunCheck(logger, Color.Red); // use appropriate color
            // await diskSpaceCheck.RunCheck(logger, Color.Blue); // use appropriate color

            // Aggregate results and provide recommendations
            int errorCount = errorLogsCheck.GetErrorCount();
            double freeSpace = diskSpaceCheck.GetFreeSpace();
            // Flag to determine if any action is required
            bool actionsRequired = false;

            // Check overall system health and provide recommendations
            if (errorCount > 10)
            {
                logger.Log("High number of critical errors in event viewer detected. Consider investigating further.", Color.Red);
                actionsRequired = true;
            }

            if (freeSpace < 10) // GB
            {
                logger.Log("Low disk space detected. Consider freeing up space on your disk.", Color.YellowGreen);
                actionsRequired = true;
            }

            // Log "Actions required!" only if the flag is set
            if (actionsRequired)
            {
                logger.Log("Actions required!", Color.Black);
            }
        }

        private Color GetColorForCheck(SystemCheck check)
        {
            if (check is CPUCheck)
            {
                return Color.Pink;
            }
            else if (check is WindowsVersionCheck)
            {
                return Color.BlueViolet;
            }
            else if (check is SuggestionCheck)
            {
                return Color.Purple;
            }
            else if (check is DiskSpaceCheck)
            {
                return Color.Magenta;
            }
            else if (check is LinuxCompatibilityCheck)
            {
                return Color.Green;
            }
            else if (check is RecommendationCheck)
            {
                return Color.DarkSeaGreen;
            }
            else
            {
                return Color.Black; // Default color
            }
        }

        private void webView2_CoreWebView2InitializationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
            if (e.IsSuccess)
            {
            }
            else
            {
                MessageBox.Show("WebView2 initialization failed.");
            }
        }

        private void linkURLIcon_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(DataHelper.Uri.URL_ICONATTRIBUTION);
        }

        private void btnHamburger_Click(object sender, EventArgs e)
        {
            this.contextMenu.Show(Cursor.Position.X, Cursor.Position.Y);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string websiteUrl = "https://github.com/builtbybel/WhatsPC/releases";
            webView.CoreWebView2.Navigate(websiteUrl);
        }
    }
}