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
                Label label = new Label();
                label.Location = new Point(100, 100);
                
                label.Text = "test";
                setLatest(CheckLatestChromeDriver().Result);
                Console.WriteLine("Latest Chrome Version: " + getLatest());

                CheckProcesses();

                if (seleniumProcesses.Count == 0)
                {
                    StartProcesses();
                    CheckProcesses();
                }
                UpdateLabels();

            }, null, startTimeSpan, periodTimeSpan);

            //StartProcesses();
            CheckProcesses();
            if (seleniumProcesses.Count == 0)
            {
                StartProcesses();
                CheckProcesses();
            }
            UpdateLabels();

        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (seleniumProcesses.Count < 1)
            {
                StartProcesses();
                UpdateLabels();
            }
        }
        public string getLatest() {
            return this.latestVersion;
        }
        public void setLatest(string v) {
            this.latestVersion = v;
        }

        public void StartProcesses() {
            try
            {
                string appPath = Directory.GetCurrentDirectory() + @"\";

                seleniumHub = new Process();
                //Console.WriteLine(appPath);
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
                //Console.WriteLine(appPath);
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
            if (seleniumProcesses.Count() >= 1)
            {
                foreach (Process p in seleniumProcesses.ToList())
                {
                    Console.WriteLine("Killing process: " + p.Id);
                    p.CloseMainWindow();
                    p.Close();
                    seleniumProcesses.Remove(p);
                }
            }
            UpdateLabels();
        }

        private void driverUpdateButton_Click(object sender, EventArgs e)
        {
            UpdateToLatestChromeDriver();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
           //CheckProcesses();
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

                    if (p.MainWindowTitle == "Administrator:  SeleniumNode" && !seleniumProcesses.Exists(x=> x.MainWindowTitle == "Administrator:  SeleniumNode"))
                    {
                        seleniumProcesses.Add(p);
                        //seleniumNodeProcessLabel.Text = "Selenium Node is running! | Process ID:" + p.Id;
                        Console.WriteLine(p.MainWindowTitle + " | " + p.Id);
                    }
                    if (p.MainWindowTitle == "Administrator:  SeleniumHub" && !seleniumProcesses.Exists(x => x.MainWindowTitle == "Administrator:  SeleniumHub")) {
                        seleniumProcesses.Add(p);
                        //seleniumServerProcessLabel.Text = "Selenium Hub is running! | Process ID:" + p.Id;
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
        async Task<string> CheckLatestChromeDriver() {
            Console.WriteLine("Checking latest chrome version...");
            String latest = await client.GetStringAsync("https://chromedriver.storage.googleapis.com/LATEST_RELEASE");
            Console.WriteLine("Latest Chrome Driver: " + latest);
            return latest;
        }
        public async void UpdateLabels()
        {
            string s = await CheckLatestChromeDriver();
            setLatest(s);
            CurrentChromeVersion();
            CheckProcesses();

            MethodInvoker updateLatest = delegate
            { 
                latestChromeLabel.Text ="Latest Chrome Version: " + getLatest();
            };
            latestChromeLabel.BeginInvoke(updateLatest);

            MethodInvoker updateCurrent = delegate
            {
                currentVersionLabel.Text = "Current Chrome Version: " + CurrentChromeVersion();
            };
            currentVersionLabel.BeginInvoke(updateCurrent);

            MethodInvoker updateProcesses = delegate
            {
                seleniumProcessesLabel.Text = "Selenium Processes found: " + seleniumProcesses.Count;
            };
            seleniumProcessesLabel.BeginInvoke(updateProcesses);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            UpdateLabels();
        }
        public async void UpdateToLatestChromeDriver()
        {
            try
            {
                var latest = await client.GetStringAsync("https://chromedriver.storage.googleapis.com/LATEST_RELEASE");
                stopProcesses();
                using (var client = new WebClient())
                {
                    client.DownloadFile("https://chromedriver.storage.googleapis.com/" + latest + "/chromedriver_win32.zip", "chromedriver.zip");
                }
                using (ZipArchive archive = ZipFile.Open(Directory.GetCurrentDirectory() + "/chromedriver.zip", ZipArchiveMode.Update))
                {
                    Console.WriteLine(archive.GetEntry("chromedriver.exe"));
                    ZipArchiveEntry file = archive.GetEntry("chromedriver.exe");
                    ZipFileExtensions.ExtractToFile(file, "chromedriver.exe", true);
                }
                latestVersion = latest;
                UpdateLabels();
                StartProcesses();
                MessageBox.Show("Chromedriver was updated to version: " + latest);
            }
            catch (Exception e)
            {
                MessageBox.Show("Encountered issue updating to latest version.\n Exception Details:" + e.Message);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/Maciox55/SeleniumServerManager");
        }

        private void downloadCurrentDriverBtn_Click(object sender, EventArgs e)
        {
            if (currentVersion != null)
            {
                UpdateChromeDriver(currentVersion);
            }
        }

        public async Task<string> ConvertChromeToWebDriver(string version)
        {
            string regexs = @"\w[0-9]";
            Regex regex = new Regex(regexs);
            Match m = regex.Match(currentVersion);
            if (m.Success)
            {
                responseString = await client.GetStringAsync("https://chromedriver.storage.googleapis.com/LATEST_RELEASE" + "_" + m.Groups[0]);
                Console.WriteLine("Driver for major chrome version"+m.Groups[0]+" is: "+responseString);
            }
            return responseString;
        }

        public async void UpdateChromeDriver(string version) {


           // responseString = await client.GetStringAsync("https://chromedriver.storage.googleapis.com/LATEST_RELEASE");

            https://chromedriver.storage.googleapis.com/index.html?path=72.0.3626.69/

            try {
                stopProcesses();
                string v = await ConvertChromeToWebDriver(currentVersion);

                using (var client = new WebClient())
                {
                
                     client.DownloadFile("https://chromedriver.storage.googleapis.com/"+v+"/chromedriver_win32.zip", "chromedriver.zip");
                }
                using (ZipArchive archive = ZipFile.Open(Directory.GetCurrentDirectory() + "/chromedriver.zip", ZipArchiveMode.Update))
                {
                    Console.WriteLine(archive.GetEntry("chromedriver.exe"));
                    ZipArchiveEntry file = archive.GetEntry("chromedriver.exe");
                    ZipFileExtensions.ExtractToFile(file, "chromedriver.exe", true);
                }

                
                StartProcesses();
                
                MessageBox.Show("Chromedriver was updated to version: " + responseString);
            }
            catch (Exception e)
            {
                MessageBox.Show("Encountered issue updating to "+version+" version.\n Exception Details:" + e.Message);
            }
        }

        public string CurrentChromeVersion() {
            string appPath = Directory.GetCurrentDirectory() + @"\";
            Process chromeChecker = new Process();
            ProcessStartInfo chromeCheckerInfo = new ProcessStartInfo
            {
                CreateNoWindow = false,
                FileName = "chromeVersion.bat",
                UseShellExecute = false,
                Verb = "runas"
            };
            chromeCheckerInfo.RedirectStandardOutput = true;
            chromeCheckerInfo.WorkingDirectory = appPath;
            chromeChecker.StartInfo = chromeCheckerInfo;
            chromeChecker.Start();
            string output = chromeChecker.StandardOutput.ReadToEnd();

            chromeChecker.WaitForExit();
            chromeChecker.Close();

            Regex r = new Regex(chromeversionRegex);
            Match m = r.Match(output);
            int matchCount = 0;
            if (m.Success)
            {
                Console.WriteLine("Current Chrome Version: "+m.Value);

                return m.Value;
            }
            else {
                return null;
             }
        }
    }
}
