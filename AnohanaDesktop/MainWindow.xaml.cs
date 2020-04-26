using System;
using System.Windows;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Diagnostics;
using System.Collections;
using System.Windows.Input;
using System.IO;

//本代码仅供研究学习！禁止用于任何不良用途！
//【桂叶出品】
//http://hi.baidu.com/plusky

namespace AnohanaDesktop
{
	public partial class MainWindow : Window
	{
		//下面代码都是设定全局变量，便于其它函数使用。
        private System.Windows.Forms.NotifyIcon _notifyIcon = null;
		DispatcherTimer timerPlayanime = new DispatcherTimer();
		DispatcherTimer timerScrSaver = new DispatcherTimer();
		DispatcherTimer timerUpdate = new DispatcherTimer();

		ConfigSetting config = new ConfigSetting();
		DateTime dateTimeNow = new DateTime();
		int volume = 0;
		Opinion opinion = new Opinion();

        //storyboard
        ArrayList _storyboards = new ArrayList();
        Storyboard SBBegin;
        Storyboard SBNanoHana;
        Storyboard SBNanoHanaSpin;
        Storyboard SBSmallHana;
        Storyboard SBSmallHanaSpin;
        Storyboard SBBigHana;
        Storyboard SBBigHanaSpin;
        Storyboard SBextHana;
        Storyboard SBextHanaSpin;
        Storyboard SBAnimationMusic;
        Storyboard SBcMediumHana;
        Storyboard SBcMediumHanaSpin;
        Storyboard SBcBigHana;
        Storyboard SBcBigHanaSpin;

		//菜单项
		System.Windows.Forms.MenuItem colorEgg = new System.Windows.Forms.MenuItem("secret base ～君がくれたもの～");
        System.Windows.Forms.MenuItem highNum = new System.Windows.Forms.MenuItem("多");
        System.Windows.Forms.MenuItem lowNum = new System.Windows.Forms.MenuItem("普通");
        System.Windows.Forms.MenuItem lessNum = new System.Windows.Forms.MenuItem("少");

		System.Windows.Forms.MenuItem topMost = new System.Windows.Forms.MenuItem("置顶");
		System.Windows.Forms.MenuItem runAtStartup = new System.Windows.Forms.MenuItem("开机启动");
		System.Windows.Forms.MenuItem about = new System.Windows.Forms.MenuItem("关于");
		System.Windows.Forms.MenuItem exit = new System.Windows.Forms.MenuItem("退出");

		//鼠标穿透相关
		const int WS_EX_TRANSPARENT = 0x00000020;
		const int GWL_EXSTYLE = -20;
		[DllImport("user32.dll")]
		static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);
		[DllImport("user32.dll")]
		static extern int GetWindowLong(IntPtr hwnd, int index);

		//钉在桌面(最下层)
		[DllImport("User32.dll", EntryPoint = "FindWindow")]
		private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
		[DllImport("user32")]
		private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
		[DllImport("user32.dll")]
		public static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
		
		//入口
		public MainWindow()
		{       
			InitializeComponent();

            //storyboard
            SBBegin = (Storyboard)Resources["SBBegin"];
            SBNanoHana = (Storyboard)Resources["SBNanoHana"];
            SBNanoHanaSpin = (Storyboard)Resources["SBNanoHanaSpin"];
            SBSmallHana = (Storyboard)Resources["SBSmallHana"];
            SBSmallHanaSpin = (Storyboard)Resources["SBSmallHanaSpin"];
            SBBigHana = (Storyboard)Resources["SBBigHana"];
            SBBigHanaSpin = (Storyboard)Resources["SBBigHanaSpin"];
            SBextHana = (Storyboard)Resources["SBextHana"];
            SBextHanaSpin = (Storyboard)Resources["SBextHanaSpin"];
            SBAnimationMusic = (Storyboard)Resources["SBAnimationMusic"];
            SBcMediumHana = (Storyboard)Resources["SBcMediumHana"];
            SBcMediumHanaSpin = (Storyboard)Resources["SBcMediumHanaSpin"];
            SBcBigHana = (Storyboard)Resources["SBcBigHana"];
            SBcBigHanaSpin = (Storyboard)Resources["SBcBigHanaSpin"];


			RegistrySet();
			RegistryGet();
			this.SourceInitialized += MainWindow_SourceInitialized;
            SBAnimationMusic.Begin();
            SBAnimationMusic.Stop();
 			SBNanoHana.Begin();
            SBNanoHanaSpin.Begin();
            SBSmallHana.Begin();
            SBSmallHanaSpin.Begin();
            SBBigHana.Begin();
            SBBigHanaSpin.Begin();
            SBextHana.Begin();
            SBextHanaSpin.Begin();
            SBAnimationMusic.Completed += SBAnimationMusic_Completed;
			if (CommonData.Scrsav)//判断是否是屏保调用
			{
				timerScrSaver.Tick += timerScrSaver_Tick;
				dateTimeNow = DateTime.Now;
				timerScrSaver.Interval = new TimeSpan(0, 0, 0, 0, 500);
				timerScrSaver.Start();
				MainXamlWindow.anohanaed2_mp3.Volume = ((double)volume)/100.0;
			    Deactivated += MainWindow_Deactivated;
				MouseDown += MainWindow_MouseDown;
				PlayAnime();
			}
			else 
			{
				ReaadConfig();
				InitialTray();
				//debug

				SBBegin.Begin();
				this.MainXamlWindow.Animation.Visibility = Visibility.Hidden;
				timerPlayanime.Tick += timer1_Tick;
				timerPlayanime.Interval = new TimeSpan(0, 0, 0, 0, 500);

			}

            
			// 在此点下面插入创建对象所需的代码。
			
		}

        void SBAnimationMusic_Completed(object sender, EventArgs e)
        {
            
        }

		void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
		{
			System.Windows.Application.Current.Shutdown();
		}
		void MainWindow_Deactivated(object sender,EventArgs e)
		{
			System.Windows.Application.Current.Shutdown();
		}


	    void MainWindow_SourceInitialized(object sender, EventArgs e)
		{
			if (CommonData.Scrsav == true)
			{ }
			else
			{
				mousePierce();
			}

			if (topMost.Checked)
			{

			}
			else
			{
				setWindowDown();
			}
			
		}

		void setWindowDown()
		{
			User32 user32 = new User32();
			try
			{

				SetWindowPos(new WindowInteropHelper(this).Handle, 1, 0, 0, 0, 0, 0x13);

			}

			catch (ApplicationException exx)
			{

				System.Windows.MessageBox.Show(this, exx.Message, "Pin to Desktop");

			}
		}
		
		//为了能够穿透窗体;
        const int WS_EX_TOOLWINDOW = 0x00000080;
		void mousePierce()
		{
			var hwnd = new WindowInteropHelper(this).Handle;
			int extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
            SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT | WS_EX_TOOLWINDOW);
		}
        

		void RegistrySet()
		{
			Microsoft.Win32.RegistryKey rootKey = Microsoft.Win32.Registry.CurrentUser;//本地计算机数据的配置 
			Microsoft.Win32.RegistryKey runKey = rootKey.CreateSubKey(@"Software\AnohanaDesktop");
			
				try
				{
					runKey.SetValue("ExePathName", Process.GetCurrentProcess().MainModule.FileName);
					rootKey.Close();// 刷新 关闭 保存修改 
				}
				catch { }
		}
		void RegistryGet()
		{
			try
			{
				Microsoft.Win32.RegistryKey rootKey = Microsoft.Win32.Registry.CurrentUser;//本地计算机数据的配置 
				Microsoft.Win32.RegistryKey runKey = rootKey.OpenSubKey(@"Software\AnohanaDesktop");
				Microsoft.Win32.RegistryKey runKey2 = rootKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");

				try
				{
					volume = (int)runKey.GetValue("CofigVolume");
					
				}
				catch { }
				try
				{
					if (runKey2.GetValue("CofigVolume").ToString()!="")
					{
						runAtStartup.Checked = true;
					}
					
				}
				catch { }
				rootKey.Close();// 刷新 关闭 保存修改 
			}
			catch (System.Exception ex)
			{
				System.Windows.MessageBox.Show(ex.ToString(), "error");
			}
			
		}


		//屏保相关
		void timerScrSaver_Tick(object sender, EventArgs e)
		{
			if (this.IsActive == false)
			{
				System.Windows.Application.Current.Shutdown();
			}
			if (DateTime.Now - dateTimeNow >= new TimeSpan(0, 1, 40))
			{
				dateTimeNow = DateTime.Now;
				PlayAnime();
			}
		}
		private void MainWindow_MouseMove(object sender, EventArgs e)
		{
			System.Windows.Application.Current.Shutdown();
		}
		private void Form1_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			System.Windows.Application.Current.Shutdown();
		}

		//读取设置
		public void ReaadConfig()
		{
			this.Topmost = config.topMost;
			topMost.Checked = config.topMost;
			runAtStartup.Checked = runStartup();
		}

		bool runStartup()
		{
			Microsoft.Win32.RegistryKey rootKey = Microsoft.Win32.Registry.CurrentUser;//本地计算机数据的配置 
			Microsoft.Win32.RegistryKey runKey = rootKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
			try
			{
				if (runKey.GetValue("那朵花动态桌面") != null)
					return true;
				else
					return false;
			}
			catch { return false; }
		}

		//系统托盘
		private void InitialTray()
		{
			//设置托盘的各个属性
            _notifyIcon = new System.Windows.Forms.NotifyIcon();
			_notifyIcon.Text = "那朵花动态桌面";
            _notifyIcon.Icon = new System.Drawing.Icon(Directory.GetParent(Process.GetCurrentProcess().MainModule.FileName).ToString() + @"\Resources\Icon\Icon1.ico");
			_notifyIcon.Visible = true;
			_notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(notifyIcon_MouseClick);

			topMost.Click += new EventHandler(topMost_Click);

			//彩蛋secret base pv
			colorEgg.Click += new EventHandler(colorEgg_Click);

			//setting
            System.Windows.Forms.MenuItem setting = new System.Windows.Forms.MenuItem("花朵数量", new System.Windows.Forms.MenuItem[] { highNum, lowNum, lessNum });
			
			//开机启动
			runAtStartup.Click += new EventHandler(runAtStartup_Click);
			
			//关于选项
			
			about.Click += new EventHandler(about_Click);

			//退出菜单项
			
			exit.Click += new EventHandler(exit_Click);

			//关联托盘控件
			System.Windows.Forms.MenuItem[] childen = new System.Windows.Forms.MenuItem[] { /*setting, help,*/colorEgg,setting,topMost,runAtStartup, about, exit };

            lowNum.Click += new EventHandler(lowNum_Click);
			highNum.Click += new EventHandler(highNum_Click);
			lessNum.Click += new EventHandler(lessNum_Click);
			
			_notifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu(childen);
			if (config.setNum == 1)
			{
				lowNum_Click(null,null);
			}
			if (config.setNum == 2)
			{
                highNum_Click(null, null);
			}
			if (config.setNum == 3)
			{
                lessNum_Click(null, null);
			}
		}


		//数量设置
		void lessNum_Click(object sender, EventArgs e)
		{
			lowNum.Checked = false;
			highNum.Checked = false;
			lessNum.Checked = true;
			config.setNum = 3;
            bigghanagrid.Visibility = Visibility.Collapsed;
            smallhanagrid.Visibility = Visibility.Collapsed;
            nanohanagrid.Visibility = Visibility.Visible;
            extHanaGrid.Visibility = Visibility.Collapsed;
		}

		void highNum_Click(object sender, EventArgs e)
		{
			lowNum.Checked = false;
			highNum.Checked = true;
			lessNum.Checked = false;
			config.setNum = 2;
            bigghanagrid.Visibility = Visibility.Visible;
            smallhanagrid.Visibility = Visibility.Visible;
            nanohanagrid.Visibility = Visibility.Visible;
            extHanaGrid.Visibility = Visibility.Visible;
                
		}

		void lowNum_Click(object sender, EventArgs e)
		{
			lowNum.Checked = true;
			highNum.Checked = false;
			lessNum.Checked = false;
			config.setNum = 1;
            bigghanagrid.Visibility = Visibility.Visible;
            smallhanagrid.Visibility = Visibility.Visible;
            nanohanagrid.Visibility = Visibility.Visible;
            extHanaGrid.Visibility = Visibility.Collapsed;
		}
		
        void setNumber()
        {

        }

		private void notifyIcon_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
		{
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				this.Activate();
			}
            
		}     
		void about_Click(object sender, EventArgs e)//关于
		{
			About aboutForm = new About();
			aboutForm.ShowDialog();
		}
		void topMost_Click(object sender, EventArgs e)//置顶
		{
			if (topMost.Checked == true)
			{
				this.Topmost = false;


				topMost.Checked = false;
				setWindowDown();
			}
			else
			{
				this.Topmost = true;

				topMost.Checked = true;
			}
		}

		//开机启动
		void runAtStartup_Click(object sender, EventArgs e)
		{
			if (runAtStartup.Checked == true)
			{

				RunWhenStart(false, "那朵花动态桌面", Process.GetCurrentProcess().MainModule.FileName);
				runAtStartup.Checked = false;
			}
			else
			{
				RunWhenStart(true, "那朵花动态桌面", Process.GetCurrentProcess().MainModule.FileName);
				runAtStartup.Checked = true;
			}
			GC.Collect();
		}
		private void RunWhenStart(bool bFlag, string strName, string strPath)
		{
			Microsoft.Win32.RegistryKey rootKey = Microsoft.Win32.Registry.CurrentUser;//本地计算机数据的配置 
			Microsoft.Win32.RegistryKey runKey = rootKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
			if (bFlag == true)//创建开机启动 
			{
				try
				{
					runKey.SetValue(strName, strPath);
					rootKey.Close();// 刷新 关闭 保存修改 
				}
				catch { }
			}
			else
			{
				try
				{
					runKey.DeleteValue(strName);
					rootKey.Close();
				}
				catch { }
			}
		}

		//彩蛋secret base
		public void colorEgg_Click(object sender, EventArgs e)
		{
			dateTimeNow = DateTime.Now;
			timerPlayanime.Start();
			colorEgg.Enabled = false;
			PlayAnime();
		}
		void timer1_Tick(object sender, EventArgs e)
		{
			if (DateTime.Now - dateTimeNow >= new TimeSpan(0, 1, 40))
			{
				timerPlayanime.Stop();
				this.MainXamlWindow.Animation.Visibility = System.Windows.Visibility.Collapsed;
                SBAnimationMusic.Stop();
                SBcBigHana.Stop();
                SBcBigHanaSpin.Stop();
                SBcMediumHana.Stop();
                SBcMediumHanaSpin.Stop();
				sbBegin();
			}
			

		}
		
		public void sbBegin()
		{
            SBBegin.Begin();

			this.MainXamlWindow.Visibility = System.Windows.Visibility.Visible;
			colorEgg.Enabled = true;

            SBcBigHanaSpin.Stop();
            SBcMediumHanaSpin.Stop();
            SBcBigHana.Stop();
            SBcMediumHana.Stop();
		}//播放
		
		void PlayAnime()
		{
			
			this.MainXamlWindow.Animation.Visibility = System.Windows.Visibility.Visible;
            SBAnimationMusic.Begin();
            SBcBigHanaSpin.Begin();
            SBcMediumHanaSpin.Begin();
            SBcBigHana.Begin();
            SBcMediumHana.Begin();
            
			
		}
        
		//退出时操作、保存设置
		private void exit_Click(object sender, EventArgs e)//退出
		{
			Storyboard sbEnd = (Storyboard)this.FindResource("SBEnd");
			sbEnd.Completed += new EventHandler(sbEnd_Completed);
			this.BeginStoryboard(sbEnd);
			

		}
		void sbEnd_Completed(object sender, EventArgs e)
		{
			System.Windows.Application.Current.Shutdown();
		}
		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try 
			{
				_notifyIcon.Dispose();
				config.topMost = this.Topmost;
				config.Save();
			}
			catch { }
		}

		private void MainXamlWindow_LocationChanged(object sender, EventArgs e)
		{
			
		}

		private void MainXamlWindow_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			
		}

		private void MainXamlWindow_Activated(object sender, EventArgs e)
		{
			if (topMost.Checked)
			{ }
			else
			{ setWindowDown(); }
		}

        private void MainXamlWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.F2)
            {
                System.Windows.MessageBox.Show("haha");
            }
        }

		

		
	}
}