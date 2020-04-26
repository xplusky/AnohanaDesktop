using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ScreenProtect
{
    public partial class Setting : Form
    {
        Settingx configx = new Settingx();
        

        public Setting()
        {
            InitializeComponent();
            trackboxVol.Value = configx.Volume;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Microsoft.Win32.RegistryKey rootKey = Microsoft.Win32.Registry.CurrentUser;//本地计算机数据的配置 
            Microsoft.Win32.RegistryKey runKey = rootKey.CreateSubKey(@"SOFTWARE\AnohanaDesktop");

            try
            {
                runKey.SetValue("CofigVolume",trackboxVol.Value);
                rootKey.Close();// 刷新 关闭 保存修改 
            }
            catch { }
            configx.Save();
            this.Close();
        }

        private void cbSound_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void trackboxVol_Scroll(object sender, EventArgs e)
        {
            configx.Volume = trackboxVol.Value;
        }
    }
}
