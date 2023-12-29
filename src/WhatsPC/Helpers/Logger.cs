using Microsoft.Web.WebView2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace WhatsPC
{
    public class Logger
    {
        private WebView2 webView;

        public Logger(WebView2 webView)
        {
            this.webView = webView;
        }

  

        // Log method for a list of strings
        public void Log(List<string> messages, Color color)
        {
            if (webView.InvokeRequired)
            {
                webView.Invoke(new Action(() => Log(messages, color)));
                return;
            }

            foreach (var message in messages)
            {
                LogToWebView(message, color);
            }
        }

        // Log method for a single string
        public void Log(string message, Color color)
        {
            if (webView.InvokeRequired)
            {
                webView.Invoke(new Action(() => Log(message, color)));
                return;
            }

            LogToWebView(message, color);
        }

        // Helper method to add log entry to WebView2
        private void LogToWebView(string message, Color color)
        {
            string formattedMessage = $"<div style='color:{ColorToHex(color)}'>{message}</div>";
            webView.CoreWebView2.PostWebMessageAsString(formattedMessage);

            // Some JS to append formatted message to logContainer
            webView.ExecuteScriptAsync($"document.getElementById('logContainer').innerHTML += '{formattedMessage}';");

            // Scroll down after adding new content
            webView.ExecuteScriptAsync("window.scrollTo(0, document.body.scrollHeight);");
        }

        // Some Helpers to convert Color to hexadecimal
        private string ColorToHex(Color color)
        {
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        }
    }
}
