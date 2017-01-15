using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Dts.Runtime;
using System.Threading;
using runtime = Microsoft.SqlServer.Dts.Runtime;
using Excel = Microsoft.Office.Interop.Excel;
using System.Security.Cryptography;

namespace DetectFile
{
    class MyEventListener : DefaultEvents
    {
        public override bool OnError(DtsObject source, int errorCode, string subComponent,
          string description, string helpFile, int helpContext, string idofInterfaceWithError)
        {
            // Add application-specific diagnostics here.  
            Console.WriteLine("Error in {0}/{1} : {2}", source, subComponent, description);
            return false;
        }
    }

    public partial class Service1 : ServiceBase
    {
        Thread thread = new Thread(Alert);
        private FileSystemWatcher watcher;
        private const string PATH_FOLDER_MONITOR = @"D:\Zalo\";
        private const string PATH_LOG_SERVICE = "D:\\LogService.txt";


        public Service1()
        {
            InitializeComponent();
            watcher = new FileSystemWatcher();
        }

        //ham ghi file
        public void WriteToFile(string text, DateTime time)
        {
            string path = PATH_LOG_SERVICE;
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(time + ": " + text);
                writer.Close();
            }
        }
        // Define the event handlers.
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
 
            string path = PATH_LOG_SERVICE;
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(DateTime.Now + " File: " + e.FullPath + " " + e.ChangeType);
                if (File.Exists(e.FullPath))
                {
                    try
                    {
                        var fileName = System.IO.Path.GetFileNameWithoutExtension(e.FullPath);
                        if (fileName.Contains("Transaction") )
                        {
                            string[] spilit = fileName.Split('_');
                            var fileExtension = System.IO.Path.GetExtension(e.FullPath);
                            var newName = new StringBuilder();
                            if (spilit.Length < 2)
                            {
                                newName.Append(@"D:\Backup\Transaction\" + fileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + fileExtension);
                                MaHoaFileExcel(e.FullPath, "TRANSACTION_DETAIL", "AccountNumber");
                                System.IO.File.Move(e.FullPath, newName.ToString());
                                throw new System.InvalidOperationException("Format invalid");
                            }
                            DateTime ReportDate;
                            if (!DateTime.TryParse(spilit[1], out ReportDate))
                            {
                                newName.Append(@"D:\Backup\Transaction\" + fileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + fileExtension);
                                MaHoaFileExcel(e.FullPath, "TRANSACTION_DETAIL", "AccountNumber");
                                System.IO.File.Move(e.FullPath, newName.ToString());
                                throw new System.InvalidOperationException("Cannot parse Report Date");
                            }
                            MaHoaFileExcel(e.FullPath, "TRANSACTION_DETAIL", "AccountNumber");
                            string pkgLocation;
                            Package pkg;
                            Application app;
                            DTSExecResult pkgResults;
                            MyEventListener eventListener = new MyEventListener();
                            pkgLocation =
                              @"D:\SSIS\SSIS_Bank\Integration Services Project1\DB_SourceExcelToTrans.dtsx";
                            app = new Application();
                            pkg = app.LoadPackage(pkgLocation, eventListener);
                            pkg.Variables["User::FileFullPath"].Value = e.FullPath.ToString();
                            pkg.Variables["User::FileName"].Value = System.IO.Path.GetFileName(e.FullPath);
                            pkg.Variables["User::ReportDate"].Value = ReportDate;
                            pkgResults = pkg.Execute(null, null, eventListener, null, null);
//                            MaHoaFileExcel(e.FullPath);
                            var serverBackupFolder = string.Empty;
                            if (pkgResults.ToString() == "Success")
                            {
                                serverBackupFolder = @"D:\Backup\Transaction";
                                writer.WriteLine(DateTime.Now + ": Import " + System.IO.Path.GetFileName(e.FullPath) + " successfully");
                            }
                            else
                            {
                                serverBackupFolder = @"D:\Backup\Failure";
                                writer.WriteLine(DateTime.Now + ": Import " + System.IO.Path.GetFileName(e.FullPath) + " failed");
                            }

                            
                            //writer.WriteLine(DateTime.Now + ":cb ma hoa ");
                            
                            //writer.WriteLine(DateTime.Now + ": ma hoa thanh cong ");    

                            newName.Append(serverBackupFolder + @"\" + fileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + fileExtension);
                            
                            System.IO.File.Move(e.FullPath, newName.ToString());
                        }
                        else if (fileName.Contains("Retrival"))
                        {
                            string[] spilit = fileName.Split('_');
                            var fileExtension = System.IO.Path.GetExtension(e.FullPath);
                            var newName = new StringBuilder();
                            if (spilit.Length < 2)
                            {
                                newName.Append(@"D:\Backup\Transaction\" + fileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + fileExtension);
                                MaHoaFileExcel(e.FullPath, "TRANSACTION_DETAIL", "AccountNumber");
                                System.IO.File.Move(e.FullPath, newName.ToString());
                                throw new System.InvalidOperationException("Format invalid");
                            }
                            DateTime ReportDate;
                            if (!DateTime.TryParse(spilit[1], out ReportDate))
                            {
                                newName.Append(@"D:\Backup\Transaction\" + fileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + fileExtension);
                                MaHoaFileExcel(e.FullPath, "TRANSACTION_DETAIL", "AccountNumber");
                                System.IO.File.Move(e.FullPath, newName.ToString());
                                throw new System.InvalidOperationException("Cannot parse Report Date");
                            }
                            MaHoaFileExcel(e.FullPath, "TRANSACTION_DETAIL", "AccountNumber");

                            string pkgLocation;
                            Package pkg;
                            Application app;
                            DTSExecResult pkgResults;
                            MyEventListener eventListener = new MyEventListener();
                            pkgLocation =
                              @"D:\SSIS\SSIS_Bank\Integration Services Project1\DB_SourceExcelToRetrival.dtsx";
                            app = new Application();
                            pkg = app.LoadPackage(pkgLocation, eventListener);
                            pkg.Variables["User::FileFullPath"].Value = e.FullPath.ToString();
                            pkg.Variables["User::FileName"].Value = System.IO.Path.GetFileName(e.FullPath);
                            pkg.Variables["User::ReportDate"].Value = ReportDate;
                            pkgResults = pkg.Execute(null, null, eventListener, null, null);
                            

                            var serverBackupFolder = string.Empty;
                            if (pkgResults.ToString() == "Success")
                            {
                                serverBackupFolder = @"D:\Backup\Transaction";
                                writer.WriteLine(DateTime.Now + ": Import " + System.IO.Path.GetFileName(e.FullPath) + " successfully");
                            }
                            else
                            {
                                serverBackupFolder = @"D:\Backup\Failure";
                                writer.WriteLine(DateTime.Now + ": Import " + System.IO.Path.GetFileName(e.FullPath) + " failed");
                            }

                            
                            //writer.WriteLine(DateTime.Now + ":cb ma hoa ");
                            
                            //writer.WriteLine(DateTime.Now + ": ma hoa thanh cong ");    
                            newName.Append(serverBackupFolder + @"\" + fileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + fileExtension);
                            System.IO.File.Move(e.FullPath, newName.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        writer.WriteLine(DateTime.Now + ": Import " + System.IO.Path.GetFileName(e.FullPath) + " failed | Ex:" + ex.Message);
                    }
                }
                writer.Close();
            }
        }
        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            string path = PATH_LOG_SERVICE;
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(DateTime.Now + " File: {0} renamed to {1}", e.OldFullPath, e.FullPath);
                writer.Close();
            }
        }

        protected override void OnStart(string[] args)
        {
            this.WriteToFile("Service stared", DateTime.Now);
            this.WriteToFile(args.ToString(), DateTime.Now);
            string ThuMucTheoDoi;
            if (args == null)
                ThuMucTheoDoi = PATH_FOLDER_MONITOR;
            else
                ThuMucTheoDoi = args[0];
            watcher.Path = ThuMucTheoDoi; //Đường dẫn từ Desktop/Test

            //* Xem những thay đổi từ lần gần nhất truy cập và thay đổi của file txt*
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            // Cho phép theo dõi tất cả định dạng file
            watcher.Filter = "*.*";

            // Bắt sự kiện khi có thay đổi, thêm, xóa sửa file
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);

            // Bắt đầu theo dõi quá trình
            watcher.EnableRaisingEvents = true;
            thread.Start(args[1]);

        }

        protected override void OnStop()
        {
            /*watcher.EnableRaisingEvents = false;*/
            thread.Abort();
            this.WriteToFile("Service stopped", DateTime.Now);
        }
        public static void Alert(Object parameter)
        {
            int[] OnTime = getParameter(parameter.ToString());
            bool Flag = true;
            while (true)
            {
                while (Flag)
                {
                    DateTime Now = DateTime.Now;
                    if (Now.Hour == OnTime[0] && Now.Minute == OnTime[1] && Now.Second > 0 && Now.Second < 60)
                    {
                        
                        RunAllPackage();
                    }
                }
                WriteToFileThread("It to report", DateTime.Now);
                Flag = true;
                Thread.Sleep(60 * 1000);
            }

        }
        public static void RunAllPackage()
        {
            string path = @"D:\\RunningLog\\RunPackage_Log_" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss") + ".txt";
            string content = string.Empty;
            if (!Directory.Exists(@"D:\\RunningLog"))
            {
                Directory.CreateDirectory(@"D:\\RunningLog");
            }
            DateTime now = DateTime.Now;
            try
            {
                string pkgLocation;
                runtime.Package pkg;
                runtime.Application app;
                runtime.DTSExecResult pkgResults;
                MyEventListener eventListener = new MyEventListener();
                pkgLocation = @"D:\SSIS\SSIS_Bank\Integration Services Project1\DB_TransToDaily.dtsx"; ;
                app = new runtime.Application();
                pkg = app.LoadPackage(pkgLocation, eventListener);
                pkgResults = pkg.Execute(null, null, eventListener, null, null);
                if (pkgResults.ToString() == "Success")
                    content += DateTime.Now.ToString() + ": Execute Build Daily successfully\n";
                else
                    content += DateTime.Now.ToString() + ": Execute Build Daily failed\n";
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                content += DateTime.Now.ToString() + ": Error -> " + ex.Message.ToString() + "\n";
                System.IO.File.WriteAllText(path, content);
            }
            if (now.Day == 1)
            {
                try
                {
                    string pkgLocation;
                    runtime.Package pkg;
                    runtime.Application app;
                    runtime.DTSExecResult pkgResults;
                    MyEventListener eventListener = new MyEventListener();
                    pkgLocation = @"D:\SSIS\SSIS_Bank\Integration Services Project1\DB_DailyToMonthly.dtsx";
                    app = new runtime.Application();
                    pkg = app.LoadPackage(pkgLocation, eventListener);
                    DateTime yesterday = DateTime.Now.AddDays(-1);
                    pkg.Variables["User::nam"].Value = yesterday.Year;
                    pkg.Variables["User::thang"].Value = yesterday.Month;
                    pkgResults = pkg.Execute(null, null, eventListener, null, null);
                    if (pkgResults.ToString() == "Success")
                        content += DateTime.Now.ToString() + ": Execute Build Monthly successfully\n";
                    else
                        content += DateTime.Now.ToString() + ": Execute Build Monthly failed\n";
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    content += DateTime.Now.ToString() + ": Error -> " + ex.Message.ToString() + "\n";
                    System.IO.File.WriteAllText(path, content);
                }
                if (now.Month == 1 || now.Month == 4 || now.Month == 7 || now.Month == 10)
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
                        DateTime yesterday = DateTime.Now.AddDays(-1);
                        int du = yesterday.Month % 3 == 0 ? 0 : 1;
                        int quy = yesterday.Month / 3 + du;

                        pkg.Variables["User::nam"].Value = yesterday.Year;
                        pkg.Variables["User::quy"].Value = quy;
                        pkgResults = pkg.Execute(null, null, eventListener, null, null);
                        if (pkgResults.ToString() == "Success")
                            content += DateTime.Now.ToString() + ": Execute Build Quarterly successfully\n";
                        else
                            content += DateTime.Now.ToString() + ": Execute Build Quarterly failed\n";
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.Message);
                        content += DateTime.Now.ToString() + ": Error -> " + ex.Message.ToString() + "\n";
                        System.IO.File.WriteAllText(path, content);
                    }
                }
                if (now.Month == 1)
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
                        DateTime yesterday = DateTime.Now.AddDays(-1);
                        pkg.Variables["User::nam"].Value = yesterday.Year;
                        pkgResults = pkg.Execute(null, null, eventListener, null, null);
                        if (pkgResults.ToString() == "Success")
                            content += DateTime.Now.ToString() + ": Execute Build Yearly successfully\n";
                        else
                            content += DateTime.Now.ToString() + ": Execute Build Yearly failed\n";
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.Message);
                        content += DateTime.Now.ToString() + ": Error -> " + ex.Message.ToString() + "\n";
                        System.IO.File.WriteAllText(path, content);
                    }
                }
            }
            try
            {
                runInsertDPtoServer(@"D:\SSIS\SSIS_Bank\Integration Services Project1\SERVER_DBTransToServer.dtsx");
                runInsertDPtoServer(@"D:\SSIS\SSIS_Bank\Integration Services Project1\SERVER_RetrivalToServer.dtsx");
                runInsertDPtoServer(@"D:\SSIS\SSIS_Bank\Integration Services Project1\SERVER_Transaction_Detail_InvalidToServer.dtsx");
                runInsertDPtoServer(@"D:\SSIS\SSIS_Bank\Integration Services Project1\SERVER_Retrival_InvalidToServer.dtsx");
                runInsertDPtoServer(@"D:\SSIS\SSIS_Bank\Integration Services Project1\SERVER_DBDailyToServer.dtsx");
                runInsertDPtoServer(@"D:\SSIS\SSIS_Bank\Integration Services Project1\SERVER_DBMonthLyToServer.dtsx");
                runInsertDPtoServer(@"D:\SSIS\SSIS_Bank\Integration Services Project1\SERVER_DBQuarterLyToServer.dtsx");
                runInsertDPtoServer(@"D:\SSIS\SSIS_Bank\Integration Services Project1\SERVER_DBYearlyToServer.dtsx");
                content += DateTime.Now.ToString() + ": Insert to DB Server successfully\n";
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                content += DateTime.Now.ToString() + ": Error -> " + ex.Message.ToString() + "\n";
                System.IO.File.WriteAllText(path, content);
            }
            content += DateTime.Now.ToString() + ": Packages run successfully";
            System.IO.File.WriteAllText(path, content);
        }

        private static void runInsertDPtoServer(string location)
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
                    Console.Write("Package run failed");
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
        public static void WriteToFileThread(string text, DateTime time)
        {
            string path = @"D:\\ServiceLog.txt";
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(time + ": " + text);
                writer.Close();
            }
        }
        public static int[] getParameter(string s)
        {
            string[] Parameters = s.Split(',');
            int[] result = new int[2];
            result[0] = int.Parse(Parameters[0]);
            result[1] = int.Parse(Parameters[1]);
            return result;
        }
        public static string getHashSha256(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            hashString = "0x" + hashString.ToUpper();
            return hashString;
        }

        public static void MaHoaFileExcel(string PathFile, string sheetName, string colName)
        {
            //Đánh dấu batdau:

            //Console.Clear();//Xóa màn hình

            String link = PathFile;
            if (!System.IO.File.Exists(link))
            {
                //Nếu đường link k chính xác thì....
                WriteToFileThread("Duong dan khong chinh xac, vui long thu lai...", DateTime.Now);
                //Thread.Sleep(1000);// delay(1000)

            }

            else
            {
                try
                {
                    Excel.Application xlApp = new Excel.Application();

                    Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(link);
                    object valueMissing = System.Reflection.Missing.Value;

                    string conn = string.Empty;
                    DataTable dtExcel = new DataTable();

                    Excel.Worksheet xlWorksheet;

                    if (xlWorkbook.Sheets.Count >= 1)
                    {
                        try
                        {
                            xlWorksheet = (Excel.Worksheet)xlWorkbook.Sheets.get_Item(sheetName);
                        }
                        catch (Exception ex)
                        {
                            xlWorksheet = (Excel.Worksheet)xlWorkbook.Sheets.get_Item(1);
                        };

                        Excel.Range xlRange = xlWorksheet.UsedRange;

                        object[,] valueArray = (object[,])xlRange.get_Value(Excel.XlRangeValueDataType.xlRangeValueDefault);

                        for (int k = 1; k <= xlWorksheet.UsedRange.Columns.Count; ++k)
                        {
                            if (valueArray[1, k].ToString() == colName)
                            {
                                for (int row = 2; row <= xlWorksheet.UsedRange.Rows.Count; ++row)
                                {
                                    String giatri = valueArray[row, k].ToString();
                                    xlWorksheet.Cells[row, k] = getHashSha256(giatri.ToString());
                                }
                            }
                        }
                    }
                    xlWorkbook.Save();
                    xlWorkbook.Close(true, valueMissing, valueMissing);
                    xlApp.Quit();

                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkbook);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                }
                catch (Exception ex)
                {
                    WriteToFileThread(ex.ToString(), DateTime.Now);

                }
            }

        }
    }
}
