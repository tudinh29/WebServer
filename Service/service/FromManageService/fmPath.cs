using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FormManageService;

namespace FromManageService
{
    public partial class fmPath : Form
    {
        public fmPath()
        {
            InitializeComponent();
        }

        private void btOpen_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            DialogResult result = fbd.ShowDialog();

            if (!string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                tbPathFolder.Text = fbd.SelectedPath;
            }
        }
        private void fmPath_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now.AddMinutes(10);
            tbHour.Text = now.Hour.ToString();
            tbMinute.Text = now.Minute.ToString();
        }

        private void btOkay_Click(object sender, EventArgs e)
        {
            try
            {
                string Hour = tbHour.Text;
                string Minute = tbMinute.Text;

                if (Hour.Length < 1 || Minute.Length < 1)
                {
                    MessageBox.Show("Error! Hour or Minute is not fill out.");
                    return;
                }
                int iHour = int.Parse(Hour);
                int iMinute = int.Parse(Minute);
                
                if (iHour > 23 || iMinute > 59)
                {
                    MessageBox.Show("Error! Date Time is not invalid.");
                    return;
                }
                if (Directory.Exists(tbPathFolder.Text))
                {
                    string parameter = tbPathFolder.Text + ";" + Hour + "," + Minute;
                    fmMain formMain = new fmMain(parameter);
                    this.Hide();
                    formMain.Visible = true;
                }
                else
                {
                    MessageBox.Show("Error! Monitor folder is not exist.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }



    }
}
