using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace ScreenProtect
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static string exePath="";
        [STAThread]
        
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            if (args.Length > 0)
            {
                string firstArgument = args[0].ToLower().Trim();
                string secondArgument = null;

                // Handle cases where arguments are separated by colon. 
                // Examples: /c:1234567 or /P:1234567
                if (firstArgument.Length > 2)
                {
                    secondArgument = firstArgument.Substring(3).Trim();
                    firstArgument = firstArgument.Substring(0, 2);
                }
                else if (args.Length > 1)
                    secondArgument = args[1];

                if (firstArgument == "/c")           // Configuration mode
                {
                    //MessageBox.Show("无设置");
                    Application.Run(new Setting());
                    
                    // TODO
                }
                else if (firstArgument == "/p")      // Preview mode
                {
                    // TODO
                    
                }
                else if (firstArgument == "/s" || firstArgument == "/S")      // Full-screen mode
                {
                    //MessageBox.Show("test");
                    runSav();
                }
                else if (firstArgument == "/t")
                {
                    //Process.Start(@"D:\Develop\VSWpf\AnohanaDesktop\AnohanaDesktop\bin\Debug\AnohanaDesktop.exe","/s");
                }
                else// Undefined argument 
                {
                    MessageBox.Show("Sorry, but the command line argument \"" + firstArgument +
                        "\" is not valid.", "ScreenSaver",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else    // No arguments - treat like /c
            {
                // TODO
            }
        }
        static void runSav()
        {
            try
            {
                Microsoft.Win32.RegistryKey rootKey = Microsoft.Win32.Registry.CurrentUser;//本地计算机数据的配置 
                Microsoft.Win32.RegistryKey runKey = rootKey.CreateSubKey(@"Software\AnohanaDesktop");

                try
                {
                    if (runKey.GetValue("ExePathName") == null)
                    {
                        MessageBox.Show("没有找到主程序，请先运行一次动态桌面程序！");
                        return;
                    }

                    exePath = runKey.GetValue("ExePathName").ToString();

                    rootKey.Close();// 刷新 关闭 保存修改 

                }
                catch { }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (exePath != "")
            {
                try
                {
                    Process proc = Process.Start(exePath,"/s");
                    if (proc != null)
                    {
                        proc.WaitForExit();
                        //MessageBox.Show(String.Format("外部程序 {0} 已经退出！", exePath), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //MessageBox.Show("test exit");
        }
    }
}