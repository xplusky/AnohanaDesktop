using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace AnohanaDesktop
{
	/// <summary>
	/// App.xaml 的交互逻辑
	/// </summary>
    public partial class App : Application
    {
        
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            
            string s = string.Empty;
            for (int i = 0; i < e.Args.Length; i++)
            {
                s += e.Args[i];
            }
            switch (s)
            {
                case "/s": //密码对话框实现代码
                    CommonData.Scrsav = true;
                    break;
                default: 
                    break;
            }
        }   
	}
}