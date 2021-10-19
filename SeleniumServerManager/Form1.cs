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
using System.Text.RegularExpressions;

namespace SeleniumServerManager
{
    public partial class Form1 : Form
    {
        public bool servicesRunning = false;
        public Process seleniumHub,seleniumNode;
        private List<Process> seleniumProcesses = new List<Process>();

        public FileStream ioStreamer;
        public string path;
        public Form1 parent;
        private string responseString;
        private static readonly HttpClient client = new HttpClient();

        public string chromeversionRegex = @"([0-9]+[.])\S+";
        public string latestVersion, currentVersion;

        public Form1()
        {
            InitializeComponent();
            string directory = Directory.GetCurrentDirectory();
            path = directory + "\\settings.xml";

            

            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromMinutes(5);
            //Call every 5 minutes
            var timer = new System.Threading.Timer((e) =>
            {
                CheckLatestChromeDriver();
                UpdateLabels();
                CheckProcesses();
                Console.WriteLine("test");

                if (seleniumProcesses.Count == 0)
                {
                    
                    StartProcesses();
                }

            }, null, startTimeSpan, periodTimeSpan);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            StartProcesses();
        }

        public void StartProcesses() {
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

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void stopProcesses()
        {

            foreach (Process p in seleniumProcesses)
            {
                
                Console.WriteLine("Killing process: " + p.Id);
                p.CloseMainWindow();
                p.Close();
                seleniumProcesses.Remove(p);
            }
        }

        private void driverUpdateButton_Click(object sender, EventArgs e)
        {

        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            CheckProcesses();
            if (seleniumProcesses.Count >= 1)
            {
                Console.WriteLine("Killing all Selenium related processes");
                stopProcesses();
            }
        }

        public void CheckProcesses() {

            Process[] localProcesses = Process.GetProcesses();

            Console.WriteLine(localProcesses.Length);
            foreach (Process p in localProcesses)
            {
                if (p.MainWindowTitle != "")
                {
                    //Console.WriteLine(p.MainWindowTitle + " | " + p.Id);
                    if (p.MainWindowTitle == "Administrator:  SeleniumNode" || p.MainWindowTitle == "Administrator:  SeleniumHub")
                    {

                        seleniumProcesses.Add(p);
                        Console.WriteLine(p.MainWindowTitle + " | " + p.Id);

                    }
                }
            }
            Console.WriteLine("Found Selenium Processes: " + seleniumProcesses.Count);
            
        }

        public string CMDCommand(string command, bool noWindow, bool shellexe)
        {
            
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                CreateNoWindow = noWindow,
                FileName = "cmd.exe",
                UseShellExecute = shellexe,
                Verb = "runas",
                Arguments = command
            };

            if (shellexe == false)
            {
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardInput = true;
                process.StartInfo = startInfo;
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                process.Close();
                return output;
            }
            else {
                process.StartInfo = startInfo;
                process.Start();
                return null;
            }
        }
        public async void CheckLatestChromeDriver() {
            Console.WriteLine("Checking latest chrome version");
            latestVersion = await client.GetStringAsync("https://chromedriver.storage.googleapis.com/LATEST_RELEASE");
            
        }
        public void UpdateLabels()
        {
            //latestChromeLabel.Text = "Latest Chrome Driver Version: " + latestVersion;
        }


        public async void UpdateChromeDriver(string version) {
            //responseString = await client.GetStringAsync("https://chromedriver.storage.googleapis.com/LATEST_RELEASE");
            stopProcesses();
            using (var client = new WebClient())
            {
                client.DownloadFile("https://chromedriver.storage.googleapis.com/" + version + "/chromedriver_win32.zip", "chromedriver.zip");
            }
            using (ZipArchive archive = ZipFile.Open(Directory.GetCurrentDirectory() + "/chromedriver.zip", ZipArchiveMode.Update))
            {
                Console.WriteLine(archive.GetEntry("chromedriver.exe"));
                ZipArchiveEntry file = archive.GetEntry("chromedriver.exe");
                ZipFileExtensions.ExtractToFile(file, "chromedriver.exe", true);
            }

            currentVersionLabel.Text = responseString;
            MessageBox.Show("Chromedriver was updated to version: " + responseString);
        }

        public string CurrentChromeVersion() {

            string output = CMDCommand("powershell -command" + +,false,false);

            Regex r = new Regex(chromeversionRegex);
            Match m = r.Match(output);
            int matchCount = 0;
            if (m.Success)
            {
                
                //Console.WriteLine("Match" + (++matchCount));
                for (int i = 1; i <= 2; i++)
                {
                    Group g = m.Groups[i];
                    //Console.WriteLine("Group"+i+"="+g);
                }
                return m.Value;
            }
            return null;
        }
    }
}
