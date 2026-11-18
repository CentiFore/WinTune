using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinTune
{
    public partial class Form1 : Form
    {
        private RichTextBox txtLog;
        private Label lblStatus;

        public Form1()
        {
            BuildModernUI();
        }

        private void BuildModernUI()
        {
            this.Text = "WinTune Optimizer Pro";
            this.Icon = new Icon("logo.ico");
            this.Size = new Size(850, 600);
            this.BackColor = Color.FromArgb(18, 18, 20);
            this.ForeColor = Color.White;
            this.Font = new Font("Segoe UI", 10F);
            this.StartPosition = FormStartPosition.CenterScreen;

            Panel pnlMain = new Panel();
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Padding = new Padding(40, 40, 40, 150);

            Panel pnlSidebar = new Panel();
            pnlSidebar.BackColor = Color.FromArgb(24, 24, 28);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Width = 220;

            this.Controls.Add(pnlMain);
            this.Controls.Add(pnlSidebar);

            Label lblLogo = new Label();
            lblLogo.Text = "⚡ WinTune";
            lblLogo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblLogo.ForeColor = Color.FromArgb(0, 230, 118);
            lblLogo.Location = new Point(20, 30);
            lblLogo.AutoSize = true;
            pnlSidebar.Controls.Add(lblLogo);

            // YENİ: GERİ AL BUTONU (Sol menüye eklendi)
            Button btnRestore = new Button();
            btnRestore.Text = "  ↩️ Restore Defaults";
            btnRestore.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRestore.ForeColor = Color.FromArgb(255, 100, 100); // Kırmızımsı renk
            btnRestore.BackColor = Color.Transparent;
            btnRestore.FlatStyle = FlatStyle.Flat;
            btnRestore.FlatAppearance.BorderSize = 0;
            btnRestore.TextAlign = ContentAlignment.MiddleLeft;
            btnRestore.Location = new Point(10, 450);
            btnRestore.Size = new Size(200, 40);
            btnRestore.Cursor = Cursors.Hand;
            btnRestore.Click += BtnRestore_Click;
            pnlSidebar.Controls.Add(btnRestore);

            Button btnHelp = new Button();
            btnHelp.Text = "  ❓ How to Use";
            btnHelp.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnHelp.ForeColor = Color.FromArgb(139, 92, 246);
            btnHelp.BackColor = Color.Transparent;
            btnHelp.FlatStyle = FlatStyle.Flat;
            btnHelp.Location = new Point(10, 500);
            btnHelp.Size = new Size(200, 40);
            btnHelp.Click += (s, e) => MessageBox.Show("1. Click any action card.\n2. Wait for the process to finish.\n3. Some changes require a PC restart.", "How to Use");
            pnlSidebar.Controls.Add(btnHelp);

            Label lblTitle = new Label();
            lblTitle.Text = "System Dashboard";
            lblTitle.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(40, 40);
            pnlMain.Controls.Add(lblTitle);

            Label lblSubtitle = new Label();
            lblSubtitle.Text = "One-click solutions to clean, optimize, and protect your Windows.";
            lblSubtitle.Font = new Font("Segoe UI", 11F);
            lblSubtitle.ForeColor = Color.FromArgb(160, 160, 170);
            lblSubtitle.AutoSize = true;
            lblSubtitle.Location = new Point(45, 90);
            pnlMain.Controls.Add(lblSubtitle);

            CreateActionButton(pnlMain, "🛡️ Privacy Protection", "Disable ads, telemetry, and Copilot.", 140, BtnPrivacy_Click);
            CreateActionButton(pnlMain, "🧹 Bloatware Cleaner", "Remove unnecessary built-in Windows apps.", 230, BtnBloatware_Click);
            CreateActionButton(pnlMain, "🚀 System Tweaks", "Classic context menu & left-aligned taskbar.", 320, BtnTweaks_Click);

            lblStatus = new Label();
            lblStatus.Text = "Ready.";
            lblStatus.ForeColor = Color.Gray;
            lblStatus.Location = new Point(45, 410);
            lblStatus.AutoSize = true;
            pnlMain.Controls.Add(lblStatus);

            txtLog = new RichTextBox();
            txtLog.Location = new Point(45, 440);
            txtLog.Size = new Size(480, 80);
            txtLog.BackColor = Color.FromArgb(10, 10, 12);
            txtLog.ForeColor = Color.FromArgb(0, 230, 118);
            txtLog.Font = new Font("Consolas", 9F);
            txtLog.ReadOnly = true;
            txtLog.Text = "> System ready...";
            pnlMain.Controls.Add(txtLog);
        }

        private void CreateActionButton(Panel parent, string title, string desc, int yPos, EventHandler onClick)
        {
            Button btn = new Button();
            btn.Location = new Point(45, yPos);
            btn.Size = new Size(480, 70);
            btn.FlatStyle = FlatStyle.Flat;
            btn.BackColor = Color.FromArgb(30, 30, 35);
            btn.ForeColor = Color.White;
            btn.FlatAppearance.BorderColor = Color.FromArgb(50, 50, 55);
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btn.Text = "  " + title + "\n  " + desc;
            btn.Click += onClick;
            parent.Controls.Add(btn);
        }

        // 1. GİZLİLİK BUTONU
        private void BtnPrivacy_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Disable Ads, Telemetry and Copilot?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.No) return;
            string script = "Set-ItemProperty -Path 'HKCU:\\Software\\Microsoft\\Windows\\CurrentVersion\\ContentDeliveryManager' -Name 'SubscribedContent-338388Enabled' -Value 0; Set-ItemProperty -Path 'HKLM:\\SOFTWARE\\Policies\\Microsoft\\Windows\\DataCollection' -Name 'AllowTelemetry' -Value 0 -Type DWord -Force; Set-ItemProperty -Path 'HKCU:\\Software\\Policies\\Microsoft\\Windows\\WindowsCopilot' -Name 'TurnOffWindowsCopilot' -Value 1 -Type DWord -Force";
            RunPowerShell(script, "Privacy settings updated!");
        }

        // 2. BLOATWARE SİLME BUTONU
        private void BtnBloatware_Click(object sender, EventArgs e)
        {
            string warning = "Removing: Xbox, Skype, Maps, Media Player, Office Hub.\nAre you sure?";
            if (MessageBox.Show(warning, "Confirm Removal", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;
            string script = "Get-AppxPackage *xbox* | Remove-AppxPackage; Get-AppxPackage *skype* | Remove-AppxPackage; Get-AppxPackage *officehub* | Remove-AppxPackage; Get-AppxPackage *zune* | Remove-AppxPackage; Get-AppxPackage *maps* | Remove-AppxPackage";
            RunPowerShell(script, "Bloatware removed!");
        }

        // 3. SİSTEM AYARLARI BUTONU
        private void BtnTweaks_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apply Taskbar and Context Menu tweaks?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.No) return;
            string script = "Set-ItemProperty -Path 'HKCU:\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced' -Name 'TaskbarAl' -Value 0; New-Item -Path 'HKCU:\\Software\\Classes\\CLSID\\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}\\InprocServer32' -Force | Out-Null; Set-ItemProperty -Path 'HKCU:\\Software\\Classes\\CLSID\\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}\\InprocServer32' -Name '(Default)' -Value ''";
            RunPowerShell(script, "System tweaks applied! (Restart PC)");
        }

        // YENİ: 4. GERİ ALMA BUTONU (Fabrika Ayarları)
        private void BtnRestore_Click(object sender, EventArgs e)
        {
            string warning = "This will revert ALL changes made by WinTune.\nAds, telemetry, taskbar and context menu will be restored to Windows defaults.\n\nContinue?";
            if (MessageBox.Show(warning, "Restore Defaults", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;

            // Reklamları, Telemetriyi, Copilot'u geri aç; Görev çubuğunu ortala, sağ tık menüsünü sıfırla
            string script = "Set-ItemProperty -Path 'HKCU:\\Software\\Microsoft\\Windows\\CurrentVersion\\ContentDeliveryManager' -Name 'SubscribedContent-338388Enabled' -Value 1; " +
                            "Set-ItemProperty -Path 'HKLM:\\SOFTWARE\\Policies\\Microsoft\\Windows\\DataCollection' -Name 'AllowTelemetry' -Value 3 -Type DWord -Force; " +
                            "Set-ItemProperty -Path 'HKCU:\\Software\\Policies\\Microsoft\\Windows\\WindowsCopilot' -Name 'TurnOffWindowsCopilot' -Value 0 -Type DWord -Force; " +
                            "Set-ItemProperty -Path 'HKCU:\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced' -Name 'TaskbarAl' -Value 1; " +
                            "Remove-Item -Path 'HKCU:\\Software\\Classes\\CLSID\\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}' -Recurse -Force -ErrorAction SilentlyContinue";

            RunPowerShell(script, "All settings restored to Windows defaults!");
        }

        // MOTOR (PowerShell Çalıştırıcı)
        private void RunPowerShell(string script, string successMsg)
        {
            try
            {
                txtLog.AppendText(Environment.NewLine + "> Processing...");
                lblStatus.Text = "Working...";

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = "-NoProfile -ExecutionPolicy Bypass -Command \"" + script + "\"",
                    Verb = "runas", // Yönetici izni ister
                    UseShellExecute = true,
                    CreateNoWindow = true
                };

                Process proc = Process.Start(psi);
                proc.WaitForExit();

                txtLog.AppendText(Environment.NewLine + "> SUCCESS: " + successMsg);
                lblStatus.Text = "Ready.";
                MessageBox.Show(successMsg, "Success");
            }
            catch (Exception ex)
            {
                txtLog.AppendText(Environment.NewLine + "> ERROR: " + ex.Message);
                lblStatus.Text = "Error.";
            }
        }
    }
}