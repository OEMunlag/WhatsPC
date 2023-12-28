namespace WhatsPC
{
    partial class EarthPageView
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlNav = new System.Windows.Forms.Panel();
            this.btnHamburger = new System.Windows.Forms.Button();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.textHeaderApp = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlNav.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlNav
            // 
            this.pnlNav.Controls.Add(this.btnHamburger);
            this.pnlNav.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlNav.Location = new System.Drawing.Point(0, 0);
            this.pnlNav.Name = "pnlNav";
            this.pnlNav.Size = new System.Drawing.Size(61, 607);
            this.pnlNav.TabIndex = 211;
            // 
            // btnHamburger
            // 
            this.btnHamburger.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHamburger.BackColor = System.Drawing.Color.Transparent;
            this.btnHamburger.FlatAppearance.BorderSize = 0;
            this.btnHamburger.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.btnHamburger.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHamburger.Font = new System.Drawing.Font("Segoe MDL2 Assets", 22.75F, System.Drawing.FontStyle.Bold);
            this.btnHamburger.ForeColor = System.Drawing.Color.Black;
            this.btnHamburger.Location = new System.Drawing.Point(13, 539);
            this.btnHamburger.Name = "btnHamburger";
            this.btnHamburger.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.btnHamburger.Size = new System.Drawing.Size(35, 43);
            this.btnHamburger.TabIndex = 225;
            this.btnHamburger.TabStop = false;
            this.btnHamburger.Text = "...";
            this.btnHamburger.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnHamburger.UseVisualStyleBackColor = false;
            this.btnHamburger.Click += new System.EventHandler(this.btnHamburger_Click);
            // 
            // contextMenu
            // 
            this.contextMenu.BackColor = System.Drawing.Color.WhiteSmoke;
            this.contextMenu.Font = new System.Drawing.Font("Segoe UI Variable Text Semiligh", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.textHeaderApp,
            this.toolStripMenuItem1});
            this.contextMenu.Name = "menuMain";
            this.contextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenu.Size = new System.Drawing.Size(222, 48);
            // 
            // textHeaderApp
            // 
            this.textHeaderApp.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textHeaderApp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textHeaderApp.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textHeaderApp.ForeColor = System.Drawing.Color.Gray;
            this.textHeaderApp.Margin = new System.Windows.Forms.Padding(1, 1, 1, 3);
            this.textHeaderApp.Name = "textHeaderApp";
            this.textHeaderApp.Size = new System.Drawing.Size(100, 16);
            this.textHeaderApp.Text = "Settings";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI Variable Display Semib", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(221, 24);
            this.toolStripMenuItem1.Text = "Check for updates...";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.BackColor = System.Drawing.Color.DarkGray;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(67, -2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(2, 607);
            this.label2.TabIndex = 244;
            // 
            // EarthPageView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pnlNav);
            this.Name = "EarthPageView";
            this.Size = new System.Drawing.Size(929, 607);
            this.pnlNav.ResumeLayout(false);
            this.contextMenu.ResumeLayout(false);
            this.contextMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlNav;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnHamburger;
        private System.Windows.Forms.ToolStripTextBox textHeaderApp;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}
