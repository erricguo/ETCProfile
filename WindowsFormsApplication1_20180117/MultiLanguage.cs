﻿using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using WeifenLuo.WinFormsUI.Docking;

namespace ETCProfiler
{
    static class MultiLanguage
    {
        //当前默认语言
        public static string DefaultLanguage = "zh-CN";

        /// <summary>
        /// 修改默认语言
        /// </summary>
        /// <param name="lang">待设置默认语言</param>
        public static void SetDefaultLanguage(string lang)
        {
            
            DefaultLanguage = lang;
            Properties.Settings.Default.DefaultLanguage = lang;
            Properties.Settings.Default.Save();
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);
        }

        /*public static void LoadLanguageDcokContent(DockContent dockContent, Type dockContentType)
        {

            if (dockContent != null)
            {
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(dockContentType);
                resources.ApplyResources(dockContent, "$this");
                //resources.ApplyResources(form, form.Name);
                foreach (Control c in dockContent.Controls)
                {
                    resources.ApplyResources(c, c.Name);
                    LoadingDockContent(c, resources);
                }
            }
        }*/

        /// <summary>
        /// 加载语言
        /// </summary>
        /// <param name="form">加载语言的窗口</param>
        /// <param name="formType">窗口的类型</param>
        public static void LoadLanguage(Form form, Type formType)
        {
            
            if (form != null)
            {
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(formType);
                resources.ApplyResources(form, "$this");

                if (form.Name == "Main_Form")
                {
                    var bar = ((Main_Form)form).GetBar();
                    foreach (BarItemLink itemLink in bar.ItemLinks)
                    {
                        resources.ApplyResources(itemLink, itemLink.Item.Name);

                        BarSubItemLink subItemLink = itemLink as BarSubItemLink;
                        if (subItemLink != null)
                            Loading(subItemLink.Item as BarSubItem, resources);

                    }
                }
                
                foreach (Control c in form.Controls)
                {
                    
                    resources.ApplyResources(c, c.Name);
                    Loading(c, resources);
                    
                }
                

            }
        }
        private static void LoadingDockContent(Control control, System.ComponentModel.ComponentResourceManager resources)
        {
            foreach (Control c in control.Controls)
            {
                resources.ApplyResources(c, c.Name);
                LoadingDockContent(c, resources);
            }
        }
        /// <summary>
        /// 加载语言
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="resources">语言资源</param>
        private static void Loading(Control control, System.ComponentModel.ComponentResourceManager resources)
        {
            if (control is MenuStrip)
            {
                //将资源与控件对应
                resources.ApplyResources(control, control.Name);
                MenuStrip ms = (MenuStrip)control;
                if (ms.Items.Count > 0)
                {
                    foreach (ToolStripMenuItem c in ms.Items)
                    {
                        //遍历菜单
                        Loading(c, resources);
                    }
                }
            }
           

            foreach (Control c in control.Controls)
            {
                resources.ApplyResources(c, c.Name);
                Loading(c, resources);
            }
        }

        /// <summary>
        /// 遍历菜单
        /// </summary>
        /// <param name="item">菜单项</param>
        /// <param name="resources">语言资源</param>
        private static void Loading(ToolStripMenuItem item, System.ComponentModel.ComponentResourceManager resources)
        {
            if (item is ToolStripMenuItem)
            {
                resources.ApplyResources(item, item.Name);
                ToolStripMenuItem tsmi = (ToolStripMenuItem)item;
                if (tsmi.DropDownItems.Count > 0)
                {
                    foreach (ToolStripMenuItem c in tsmi.DropDownItems.OfType<ToolStripMenuItem>())
                    {
                        Loading(c, resources);
                    }
                }
            }
        }
        private static void Loading(TabPage item, System.ComponentModel.ComponentResourceManager resources)
        {
            if (item is TabPage)
            {
                resources.ApplyResources(item, item.Name);
                TabPage tsmi = (TabPage)item;
                                 
                 Loading(tsmi, resources);                    
                
            }
        }

        private static void Loading(BarSubItem subItem, System.ComponentModel.ComponentResourceManager resources)
        {
            foreach (BarItemLink itemLink in subItem.ItemLinks)
            {
                resources.ApplyResources(itemLink, itemLink.Item.Name);
                BarSubItemLink subItemLink = itemLink as BarSubItemLink;
                if (subItemLink != null)
                    Loading(subItemLink.Item as BarSubItem, resources);
            }
        }
    }
}

