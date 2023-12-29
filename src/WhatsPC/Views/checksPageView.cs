using Microsoft.Web.WebView2.WinForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace WhatsPC
{
    public partial class checksPageView : UserControl
    {
        private Logger logger;
        private WebView2 webView;

        public checksPageView()
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
                Dock = DockStyle.None,
                Width = this.Width,
                Height = this.Height,
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

                webView.NavigateToString(htmlContent); // Load HTML content
            }
            else   // whatsPC.html does not exist
            {
            }

            await webView.EnsureCoreWebView2Async();

            // Fire checks button
            webView.WebMessageReceived += (s, e) =>
            {
                string message = e.TryGetWebMessageAsString();
                if (message == "runChecks")
                {
                    // Run checks when message is received
                    RunChecks();
                }
            };
        }

        private void SetStyle()
        {
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

            webView.CoreWebView2.PostWebMessageAsString("Checks completed.");
        }

        private async Task RunSummaryChecks(Logger logger)
        {
            // Create instances of individual checks
            var errorLogsCheck = new ErrorLogsCheck();
            var diskSpaceCheck = new DiskSpaceCheck();

            // Run individual checks
            // await errorLogsCheck.RunCheck(logger, Color.Red);
            // await diskSpaceCheck.RunCheck(logger, Color.Blue);

            // Aggregate results and provide recommendations
            int errorCount = errorLogsCheck.GetErrorCount();
            double freeSpace = diskSpaceCheck.GetFreeSpace();
            // Flag to determine if any action is required
            bool actionsRequired = false;

            // Check overall system health
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
                return Color.Gray;
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
                return Color.Black; // default color
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