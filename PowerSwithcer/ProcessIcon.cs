using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace PowerSwitcher
{
    class ProcessIcon : IDisposable
    {

        private static readonly Process pro = new Process()
        {
            StartInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                //WorkingDirectory = "C:\\msr-utility"
            }
        };

        NotifyIcon ni = new NotifyIcon();
        ContextMenuStrip contextMenuStrip1 = new ContextMenuStrip();
        ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem("Balance");
        ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem("PowerSaving");
        ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem("Exit");


        public ProcessIcon()
        {
            pro.StartInfo.WorkingDirectory = System.IO.Directory.GetCurrentDirectory() + "\\msr-utility";
            toolStripMenuItem1.Click += item1_clicked;
            toolStripMenuItem2.Click += item2_clicked;
            toolStripMenuItem3.Click += item3_clicked;
            contextMenuStrip1.Items.Add(toolStripMenuItem1);
            contextMenuStrip1.Items.Add(toolStripMenuItem2);
            contextMenuStrip1.Items.Add(toolStripMenuItem3);

            ni.ContextMenuStrip = contextMenuStrip1;
            ni.Visible = true;
            OnPowerChanged(this, new PowerModeChangedEventArgs(PowerModes.Resume));
        }

        public void Dispose()
        {
            Disposed?.Invoke(this, null);
            ni.Dispose();
        }

        public void item1_clicked(object sender, EventArgs e)
        {
            ChangeToBalance();
        }

        public void item2_clicked(object sender, EventArgs e)
        {
            ChangeToPowerSaving();
        }

        public void item3_clicked(object sender, EventArgs e)
        {
            Dispose();
        }

        public void OnPowerChanged(object sender, PowerModeChangedEventArgs e)
        {
            try
            {
                switch (e.Mode)
                {

                    case PowerModes.StatusChange:
                        switch (SystemInformation.PowerStatus.PowerLineStatus)
                        {
                            case PowerLineStatus.Offline:
                                ChangeToPowerSaving();
                                break;

                            case PowerLineStatus.Online:
                            case PowerLineStatus.Unknown:
                            default:
                                ChangeToBalance();
                                break;
                        }
                        break;

                    case PowerModes.Resume:
                    default:
                        pro.StartInfo.FileName = "Undervolting.bat";
                        pro.Start();
                        pro.WaitForExit();
                        switch (SystemInformation.PowerStatus.PowerLineStatus)
                        {
                            case PowerLineStatus.Offline:
                                ChangeToPowerSaving();
                                break;

                            case PowerLineStatus.Online:
                            case PowerLineStatus.Unknown:
                            default:
                                ChangeToBalance();
                                break;
                        }
                        break;

                    case PowerModes.Suspend:
                        toolStripMenuItem1.Checked = false;
                        toolStripMenuItem2.Checked = false;
                        break;
                }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void ChangeToBalance()
        {
            if (!toolStripMenuItem1.Checked)
            {
                toolStripMenuItem1.Checked = true;
                toolStripMenuItem2.Checked = false;
                ni.Icon = Properties.Resources.Balance;
                pro.StartInfo.FileName = "ModeBalance.bat";
                pro.Start();
                pro.WaitForExit();
                ni.ShowBalloonTip(2, "Power plan", "Balance Mode", ToolTipIcon.Info);
            }
        }

        private void ChangeToPowerSaving()
        {
            if (!toolStripMenuItem2.Checked)
            {
                toolStripMenuItem1.Checked = false;
                toolStripMenuItem2.Checked = true;
                ni.Icon = Properties.Resources.BatterySaving;
                pro.StartInfo.FileName = "ModePowerSaving.bat";
                pro.Start();
                pro.WaitForExit();
                ni.ShowBalloonTip(2, "Power plan", "PowerSaving Mode", ToolTipIcon.Info);
            }
        }

        public event EventHandler Disposed;

    }
}
