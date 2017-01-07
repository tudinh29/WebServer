using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceProcess;
using FromManageService;
using System.IO;
using runtime =  Microsoft.SqlServer.Dts.Runtime;

namespace FormManageService
{
    public partial class fmMain : Form
    {

        public const string TEXT_STOP = "Stop";
        public const string TEXT_START = "Start";
        public const string SERVICE_NAME = "DetectFile";

        public string[] argsService = new string[1];
        private ServiceController myService;
        public string time = string.Empty;
        public string server = string.Empty;
        public fmMain(string parameter)
        {
            string[] array = parameter.Split(';');
            string pathFolder = array[0];
            time = parameter.Split(';')[1];
            server = parameter.Split(';')[0];
            if (Directory.Exists(pathFolder))
            {
                InitializeComponent();
                argsService = array;
                SetService();
                myService = new ServiceController(SERVICE_NAME);
                System.Threading.Thread.Sleep(2000);
                setStatus();
            }

        }
        private void SetService()
        {
            if (processes.isExistService(SERVICE_NAME))
            {
                //unistall service
            }
            else
            {
                processes.InstallService();
            }
        }
        private void setTextComponent(string text)
        {
            if (text == TEXT_START)
            {
                btStart.Text = TEXT_START;
                lbStatus.Text = "Stopped.";

            }
            else if (text == TEXT_STOP)
            {
                btStart.Text = TEXT_STOP;
                lbStatus.Text = "Running.";

            }
        }
        public void setStatus()
        {
            if (myService.Status == ServiceControllerStatus.Running)
            {
                setTextComponent(TEXT_STOP);
            }
            else if (myService.Status == ServiceControllerStatus.Stopped)
            {
                setTextComponent(TEXT_START);
            }
        }
        private void StopService()
        {
            btStart.Enabled = false;
            myService.Stop();

            while (myService.Status == ServiceControllerStatus.Running)
            {
                System.Threading.Thread.Sleep(1000);
                myService.Refresh();
            }
            System.Threading.Thread.Sleep(2000);
            btStart.Enabled = true;
            setTextComponent(TEXT_START);
        }
        private void StartService()
        {
            btStart.Enabled = false;
            myService.Start(argsService);

            while (myService.Status == ServiceControllerStatus.Stopped)
            {
                System.Threading.Thread.Sleep(1000);
                myService.Refresh();
            }
            System.Threading.Thread.Sleep(2000);
            btStart.Enabled = true;
            setTextComponent(TEXT_STOP);
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            if (myService.Status == ServiceControllerStatus.Stopped)
                StartService();

            else if (myService.Status == ServiceControllerStatus.Running)
            {
                try
                {
                    StopService();
                }
                catch(Exception ex)
                { }
               
            }
        }

        private void fmMain_Load(object sender, EventArgs e)
        {
            lblTime.Text = time.Split(',')[0] + ":" + time.Split(',')[1];
            lblServer.Text = server;
        }



        private void fmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (myService.Status == ServiceControllerStatus.Running)
                StopService();
            Application.Exit();
        }

        class MyEventListener : runtime.DefaultEvents
        {
            public override bool OnError(runtime.DtsObject source, int errorCode, string subComponent,
              string description, string helpFile, int helpContext, string idofInterfaceWithError)
            {
                // Add application-specific diagnostics here.  
                Console.WriteLine("Error in {0}/{1} : {2}", source, subComponent, description);
                return false;
            }
        }  

        private void btnBuildDaily_Click(object sender, EventArgs e)
        {
            try
            {
                string pkgLocation;
                runtime.Package pkg;
                runtime.Application app;
                runtime.DTSExecResult pkgResults;
                MyEventListener eventListener = new MyEventListener();
                pkgLocation = @"D:\SSIS\SSIS_Bank\Integration Services Project1\DB_TransToDaily.dtsx";;
                app = new runtime.Application();
                pkg = app.LoadPackage(pkgLocation, eventListener);
                pkgResults = pkg.Execute(null, null, eventListener, null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnMonthly_Click(object sender, EventArgs e)
        {
            try
            {
                string pkgLocation;
                runtime.Package pkg;
                runtime.Application app;
                runtime.DTSExecResult pkgResults;
                MyEventListener eventListener = new MyEventListener();
                pkgLocation =  @"D:\SSIS\SSIS_Bank\Integration Services Project1\DB_DailyToMonthly.dtsx";
                app = new runtime.Application();
                pkg = app.LoadPackage(pkgLocation, eventListener);
             
                pkg.Variables["User::nam"].Value = DateTime.Today.Year;
                pkg.Variables["User::thang"].Value = DateTime.Today.Month;
                pkgResults = pkg.Execute(null, null, eventListener, null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnQuarterly_Click(object sender, EventArgs e)
        {
            try
            {
                string pkgLocation;
                runtime.Package pkg;
                runtime.Application app;
                runtime.DTSExecResult pkgResults;
                MyEventListener eventListener = new MyEventListener();
                pkgLocation = @"D:\SSIS\SSIS_Bank\Integration Services Project1\DB_MonthlyToQuarterly.dtsx";
                app = new runtime.Application();
                pkg = app.LoadPackage(pkgLocation, eventListener);
                int du = DateTime.Today.Month % 3 == 0 ? 0 : 1;
                int quy = DateTime.Today.Month / 3 + du;

                pkg.Variables["User::nam"].Value = DateTime.Today.Year;
                pkg.Variables["User::quy"].Value = quy ;
                pkgResults = pkg.Execute(null, null, eventListener, null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnYearly_Click(object sender, EventArgs e)
        {
            try
            {
                string pkgLocation;
                runtime.Package pkg;
                runtime.Application app;
                runtime.DTSExecResult pkgResults;
                MyEventListener eventListener = new MyEventListener();
                pkgLocation = @"D:\SSIS\SSIS_Bank\Integration Services Project1\DB_QuarterlyToYearly.dtsx";
                app = new runtime.Application();
                pkg = app.LoadPackage(pkgLocation, eventListener);
                pkg.Variables["User::nam"].Value = DateTime.Today.Year;
                pkgResults = pkg.Execute(null, null, eventListener, null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                runInsertDPtoServer(@"D:\SSIS\SSIS_Bank\Integration Services Project1\SERVER_DBTransToServer.dtsx");

                runInsertDPtoServer(@"D:\SSIS\SSIS_Bank\Integration Services Project1\SERVER_DBDailyToServer.dtsx");
                runInsertDPtoServer(@"D:\SSIS\SSIS_Bank\Integration Services Project1\SERVER_DBMonthLyToServer.dtsx");
                runInsertDPtoServer(@"D:\SSIS\SSIS_Bank\Integration Services Project1\SERVER_DBQuarterLyToServer.dtsx");
                runInsertDPtoServer(@"D:\SSIS\SSIS_Bank\Integration Services Project1\SERVER_DBYearlyToServer.dtsx");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void runInsertDPtoServer(string location)
        {
            try
            {
                string pkgLocation;
                runtime.Package pkg;
                runtime.Application app;
                runtime.DTSExecResult pkgResults;
                MyEventListener eventListener = new MyEventListener();
                pkgLocation = location;
                app = new runtime.Application();
                pkg = app.LoadPackage(pkgLocation, eventListener);
                pkgResults = pkg.Execute(null, null, eventListener, null, null);
                if (pkgResults.ToString() != "Success")
                    MessageBox.Show("Package run failed");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
    }
}
