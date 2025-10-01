using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ProcessManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // hiển thị danh sách process khi mở form
            DisplayProcess();
        }

        // Hàm hiển thị danh sách process đang chạy
        void DisplayProcess()
        {
            lbOutput.Items.Clear();
            Process[] plist = Process.GetProcesses();
            foreach (Process process in plist)
            {
                lbOutput.Items.Add(process.ProcessName + ", " + process.Id);
            }
        }

        // Browse chọn file exe
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Executable files (*.exe)|*.exe|All files (*.*)|*.*";
            DialogResult ret = dlg.ShowDialog();

            if (ret == DialogResult.OK)
            {
                txtPath.Text = dlg.FileName;
            }
        }

        // Start process
        private void btnStart_Click(object sender, EventArgs e)
        {
            Process myProcess = new Process();
            try
            {
                myProcess.StartInfo.FileName = txtPath.Text;
                myProcess.Start();
                DisplayProcess();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Stop process
        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (lbOutput.SelectedItem != null)
                {
                    String[] param = lbOutput.SelectedItem.ToString().Split(',');
                    Process proc = Process.GetProcessById(Int32.Parse(param[1]));
                    proc.Kill();
                    DisplayProcess();
                }
                else
                {
                    MessageBox.Show("Hãy chọn một process trong danh sách để dừng.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Refresh process list
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DisplayProcess();
        }
    }
}
