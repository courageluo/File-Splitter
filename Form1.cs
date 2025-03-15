using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

namespace FileSplitter
{
    public partial class Form1 : Form
    {
        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

        public Form1()
        {
            InitializeComponent();
            SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;
            
            this.Disposed += (s, e) => 
            {
                SystemEvents.UserPreferenceChanged -= SystemEvents_UserPreferenceChanged;
            };
            
            SetTheme();
            ApplyTitleBarTheme();
        }

        private void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            SetTheme();
            ApplyTitleBarTheme();
        }

        private void ApplyTitleBarTheme()
        {
            if (Environment.OSVersion.Version.Major < 10) return;
            
            int darkMode = IsDarkTheme() ? 1 : 0;
            DwmSetWindowAttribute(this.Handle, DWMWA_USE_IMMERSIVE_DARK_MODE, 
                ref darkMode, Marshal.SizeOf(darkMode));
        }

        private void SetTheme()
        {
            var isDark = IsDarkTheme();
            Color backColor = isDark ? Color.FromArgb(32, 32, 32) : SystemColors.Control;
            Color foreColor = isDark ? Color.White : SystemColors.ControlText;

            this.BackColor = backColor;
            this.ForeColor = foreColor;

            foreach (Control c in Controls)
            {
                SetControlTheme(c, isDark);
            }
        }

        private void SetControlTheme(Control control, bool isDark)
        {
            if (control is TextBox tb)
            {
                tb.BackColor = isDark ? Color.FromArgb(64, 64, 64) : SystemColors.Window;
                tb.ForeColor = isDark ? Color.White : SystemColors.WindowText;
            }
            else if (control is Button btn)
            {
                btn.BackColor = isDark ? Color.FromArgb(64, 64, 64) : SystemColors.Control;
                btn.ForeColor = isDark ? Color.White : SystemColors.ControlText;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderColor = isDark ? Color.Gray : SystemColors.ControlDark;
            }
            else if (control is NumericUpDown nud)
            {
                nud.BackColor = isDark ? Color.FromArgb(64, 64, 64) : SystemColors.Window;
                nud.ForeColor = isDark ? Color.White : SystemColors.WindowText;
            }
        }

        private bool IsDarkTheme()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(
                    @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize"))
                {
                    return key?.GetValue("AppsUseLightTheme") is int value && value == 0;
                }
            }
            catch
            {
                return false;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = ofd.FileName;
                }
            }
        }

        private void btnSplit_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = txtFilePath.Text;
                int parts = (int)numParts.Value;

                if (parts < 2)
                {
                    ShowThemedMessage("分割数量必须 ≥ 2");
                    return;
                }

                if (!File.Exists(filePath))
                {
                    ShowThemedMessage("请选择有效的文件");
                    return;
                }

                FileInfo fi = new FileInfo(filePath);
                long chunkSize = fi.Length / parts;

                if (chunkSize < 1)
                {
                    ShowThemedMessage($"文件太小({fi.Length}字节)，无法分割为{parts}份");
                    return;
                }

                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] buffer = new byte[4096];
                    for (int i = 1; i <= parts; i++)
                    {
                        string newPath = $"{fi.DirectoryName}\\{Path.GetFileNameWithoutExtension(fi.Name)}_{i}{fi.Extension}";
                        
                        using (FileStream output = File.Create(newPath))
                        {
                            long bytesWritten = 0;
                            while (bytesWritten < chunkSize && fs.Position < fs.Length)
                            {
                                int bytesToRead = (int)Math.Min(buffer.Length, chunkSize - bytesWritten);
                                int bytesRead = fs.Read(buffer, 0, bytesToRead);
                                output.Write(buffer, 0, bytesRead);
                                bytesWritten += bytesRead;
                            }
                        }
                    }
                }

                ShowThemedMessage($"文件已成功分割为 {parts} 个部分");
            }
            catch (Exception ex)
            {
                ShowThemedMessage($"错误: {ex.Message}");
            }
        }

        private void ShowThemedMessage(string message)
        {
            var dialog = new Form()
            {
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                Text = "提示",
                Size = new Size(300, 150),
                BackColor = IsDarkTheme() ? Color.FromArgb(32, 32, 32) : SystemColors.Control,
                ForeColor = IsDarkTheme() ? Color.White : SystemColors.ControlText
            };

            if (Environment.OSVersion.Version.Major >= 10)
            {
                int darkMode = IsDarkTheme() ? 1 : 0;
                DwmSetWindowAttribute(dialog.Handle, DWMWA_USE_IMMERSIVE_DARK_MODE, 
                    ref darkMode, Marshal.SizeOf(darkMode));
            }

            Label label = new Label()
            {
                Text = message,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            Button okButton = new Button()
            {
                Text = "确定",
                DialogResult = DialogResult.OK,
                Size = new Size(75, 30),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };
            okButton.Location = new Point(
                dialog.ClientSize.Width - okButton.Width - 10,
                dialog.ClientSize.Height - okButton.Height - 10);

            dialog.Controls.Add(label);
            dialog.Controls.Add(okButton);
            dialog.AcceptButton = okButton;

            this.BeginInvoke((MethodInvoker)delegate {
                dialog.ShowDialog(this);
            });
        }
    }
}