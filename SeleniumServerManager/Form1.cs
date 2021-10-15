using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;


namespace SeleniumServerManager
{
    public partial class Form1 : Form
    {
        public bool servicesRunning = false;
        public Process seleniumHub,seleniumNode;
        private List<Process> seleniumProcesses;

        public Form1()
        {
            InitializeComponent();

            //Getting the processes cmd.exe with Selenium in their names:
            //tasklist /fi "Windowtitle eq Selenium*" /fi "ImageName eq cmd.exe" /fo list /v

            Process proc = new Process();

            ProcessStartInfo finderInfo = new ProcessStartInfo
            {
                WorkingDirectory = @"C:\",
                CreateNoWindow = false,
                FileName = "cmd.exe",
                UseShellExecute = false,
                Verb = "runas",
                RedirectStandardOutput = true,
                Arguments = "tasklist /fi "+'\u0022'+"ImageName eq cmd.exe "+ '\u0022' + "/fi"+'\u0022'+"Windowtitle eq Selenium*"+'\u0022' + "/fo list /v"
                //Arguments = "tasklist /fi \"\"Windowtitle eq Selenium* \"\" /fi \"\"ImageName eq cmd.exe\"\" /fo list /v"
            };
            proc.StartInfo = finderInfo;

            proc.Start();

            
            //string output = proc.StandardOutput.ReadToEnd();

            //Console.WriteLine(output);
            proc.Close();
            //proc.WaitForExit();

            try
            {

                string appPath = Directory.GetCurrentDirectory() + @"\";
                seleniumHub = new Process();
                Console.WriteLine(appPath);
                ProcessStartInfo seleniumHubInfo = new ProcessStartInfo
                {

                    CreateNoWindow = false,
                    FileName = "Server_Hub_1.bat",
                    UseShellExecute = false,
                    Verb = "runas",
                    //Arguments = "java -jar selenium-server-standalone-3.141.59.jar -role hub"
                };
                seleniumHubInfo.WorkingDirectory = appPath;
                seleniumHub.StartInfo = seleniumHubInfo;
                seleniumHub.Start();

                seleniumNode = new Process();
                Console.WriteLine(appPath);
                ProcessStartInfo seleniumNodeInfo = new ProcessStartInfo
                {

                    CreateNoWindow = false,
                    FileName = "Server_Node_2.bat",
                    UseShellExecute = false,
                    Verb = "runas",
                    //Arguments = "java -jar selenium-server-standalone-3.141.59.jar -role hub"
                };
                seleniumNodeInfo.WorkingDirectory = appPath;
                seleniumNode.StartInfo = seleniumNodeInfo;
                seleniumNode.Start();

            }catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            Process[] localProcesses = Process.GetProcesses();

            Console.WriteLine(localProcesses.Length);
            foreach (var p in localProcesses)
            {
                if (p.MainWindowTitle != "")
                {
                    Console.WriteLine(p.MainWindowTitle);
                    if (p.MainWindowTitle == "SeleniumNode" || p.MainWindowTitle == "SeleniumHub")
                    {
                        seleniumProcesses.Add(p);
                        Console.WriteLine(p.MainWindowTitle);
                        Console.WriteLine(seleniumProcesses.Count);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void startButton_Click(object sender, EventArgs e)
        {

        }
    }
}
