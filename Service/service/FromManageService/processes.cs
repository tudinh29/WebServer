using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;
using System.ServiceProcess;

namespace FromManageService
{
    class processes
    {
        public static void InstallService()
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Minimized;
            startInfo.FileName = "cmd.exe";
            startInfo.WorkingDirectory = getPath();
    
            startInfo.Arguments = "/c " + "installutil DetectFile.exe";
            process.StartInfo = startInfo;
            process.Start();
        }
        public static void UninstallService()
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Minimized;
            startInfo.FileName = "cmd.exe";
            startInfo.WorkingDirectory = getPath();

            startInfo.Arguments = "/c " + "installutil /u DetectFile.exe";
            process.StartInfo = startInfo;
            process.Start();
        }
        public static string getPath()
        {
            string PathRoot = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string SearchString = "\\service\\";
            
            int endIndex = PathRoot.LastIndexOf(SearchString);
            if (endIndex != -1)
            {
                string PathDetectFile = PathRoot.Substring(0, endIndex + SearchString.Length);
                PathDetectFile = PathDetectFile + "DetectFile\\bin\\Debug\\";
                return PathDetectFile;
            }
            
            return PathRoot;
        }
        public static bool isExistService(string ServiceName)
        {
            ServiceController ctl = ServiceController.GetServices().FirstOrDefault(s => s.ServiceName == ServiceName);
            if (ctl == null)
                return false;
            else
                return true;
        }
    }
}
