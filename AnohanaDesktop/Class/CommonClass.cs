using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//公共数据
namespace AnohanaDesktop
{
    public static class CommonData
    {
        //是否为屏保模式
        private static bool scrsav=false;

        public static bool Scrsav
        {
            get { return scrsav; }
            set { scrsav = value; }
        }

        //版本
        public static string VER = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
    }

}
