using DevExpress.XtraGrid;
using DevExpress.XtraPrinting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ETCProfiler.classes
{

    public static class ShareFunc
    {
        [DllImport("msvcrt.dll", EntryPoint = "memcpy", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        public static extern IntPtr memcpy(IntPtr dest, IntPtr src, UIntPtr count);

        public static int MAXCOM = 256;
        public static Color ConvertFromWin32Color(int color)
        {
            int r = color & 0x000000FF;
            int g = (color & 0x0000FF00) >> 8;
            int b = (color & 0x00FF0000) >> 16;
            return Color.FromArgb(r, g, b);
        }
        public static int atoi(string s)
        {
            int i = -1;
            return (int.TryParse(s, out i)) ? i : -1;            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strOut">要获取的字串</param>
        /// <param name="str">要切割的字串</param>
        /// <param name="index">要获得第几个项目</param>
        /// <param name="c">分割字元</param>
        public static void AfxExtractSubString(ref string strOut,string str , int index ,char c)
        {
            strOut = "";
            try
            {
                var s = str.Split(c);
                if (s.Length >= index -1)
                {
                    strOut = s[index];
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strOut">要获取的字串</param>
        /// <param name="str">要切割的字串</param>
        /// <param name="index">要获得第几个项目</param>
        /// <param name="s">分割字串</param>
        public static void AfxExtractSubString(ref string strOut, string str, int index, string s)
        {
            strOut = "";
            try
            {
                string[] mStr = str.Split(new string[] { s }, StringSplitOptions.None);
                if (mStr.Length >= index - 1)
                {
                    strOut = mStr[index];
                }
            }
            catch
            {

            }
        }

        //將多個 GRID 列印出來
        public static void Print()
        {
            //GridControl[] grids = new GridControl[] { gridControl1, gridControl2 };
            GridControl[] grids = new GridControl[] {  };
            PrintingSystem ps = new PrintingSystem();
            DevExpress.XtraPrintingLinks.CompositeLink compositeLink = new DevExpress.XtraPrintingLinks.CompositeLink();
            compositeLink.PrintingSystem = ps;

            foreach (GridControl grid in grids)
            {
                PrintableComponentLink link = new PrintableComponentLink();
                link.Component = grid;
                compositeLink.Links.Add(link);
            }

            compositeLink.CreateDocument();
            compositeLink.ShowPreview();
        }

        public static void ListFiles(FileSystemInfo info)
        {
            if (!info.Exists) return;

            DirectoryInfo dir = info as DirectoryInfo;
            //不是目录 
            if (dir == null) return;

            FileSystemInfo[] files = dir.GetFileSystemInfos();
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo file = files[i] as FileInfo;
                //是文件 
                if (file != null)
                    Console.WriteLine(file.FullName + "\t " + file.Length);
                //对于子目录，进行递归调用 
                else
                    ListFiles(files[i]);

            }
        }
        public static JToken GetDirectory(DirectoryInfo directory)
        {
            return JToken.FromObject(new
            {
                directory = directory.EnumerateDirectories()
                    .ToDictionary(x => x.Name, x => GetDirectory(x)),
                file = directory.EnumerateFiles().Select(x => x.Name).ToList()
            });
        }

    }
}
