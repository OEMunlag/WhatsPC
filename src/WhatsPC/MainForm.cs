using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataHelper;

namespace WhatsPC
{
    public partial class MainForm : Form
    {
        private checksPageView checkPage;

        public MainForm()
        {
            InitializeComponent();
            // Initialize checksPageView
            checkPage = new checksPageView();
        }

        private void SetStyle()
        {
            // Some color styling
            BackColor = Color.FromArgb(243, 243, 243);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            pnlOverlay.Controls.Add(checkPage);
            checkPage.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom);
            checkPage.Dock = DockStyle.Fill;

            ViewHelper.SwitchView.INavPage = pnlForm.Controls[0];
            ViewHelper.SwitchView.mainForm = this;

            // Add styles
            this.SetStyle();
        }
    }
}