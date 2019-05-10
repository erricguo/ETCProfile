using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using ETCProfiler.classes;
using System;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Drawing;
using AutoUpdaterDotNET;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using DevExpress.XtraPrinting;
using System.Drawing.Imaging;
using System.Linq;
using static ETCProfiler.classes.SuperTag;
using static ETCProfiler.Enums.Statu;

namespace ETCProfiler
{
    public partial class Main_Form : XtraForm
    {
        Main_TempChart main_TempChart;
        Main_DataAnasis main_DataAnasis;
        Main_DataManagement main_DataManagement;

        Dictionary<int, BarButtonItem> btnChannels = new Dictionary<int, BarButtonItem>();
        PopupMenu[] popupMenus = new PopupMenu[24];
        //List<Color> ChannelColor = new List<Color>();
        public Dictionary<int, Color> ChannelColor { get; } = new Dictionary<int, Color>();
        Pen FP = new Pen(Color.Black) { Width = 2 };

        bool m_btnChannelAllChecked = false;
        int m_NowChannelNo;
        bool bPortOpened;
        bool m_bFinish;
        string m_strTempFile;
        uint m_cxWidth;
        uint m_cxHeight;
        uint m_cxOffset;
        uint m_nLinesPerPage;
        uint m_cyPrinter;
        Font m_ListFont;
        Font m_fontPrinter;
        string m_strDeviceName;

        // 分析

        Rectangle m_rtCommentInfo;
        // 图表
        Rectangle m_rtReflowResult;
        Rectangle m_rtReflowMaxMin;
        Rectangle m_rtReflowTemplIn;
        Rectangle m_rtReflowUPDOWM;

        // 采样信息
        string[] m_strPrintInfo = new string[3];

        int m_nPrintMode;// 打印模式
        bool m_IsPrintPageFirst;  // 打印张数
        bool m_IsPrintPageSecond;  // 打印张数
        Point m_ptHeader; // 页眉

        // 第一页
        Rectangle m_rtTitleInfo;
        Rectangle m_rtProjectInfo;
        Rectangle m_rtProductInfo;
        Rectangle m_rtAnalyseChart;
        Rectangle m_rtAnalyseInfo;
        Rectangle m_rtReflowInfo;
        Rectangle m_rtSignatureInfo;
        Rectangle m_ptFooter; // 页脚
        Rectangle m_rtNoteInfo;
        // 第二页
        // Rectangle m_rtTitleInfo;
        Rectangle m_rtETCInfo;
        Rectangle m_rtProductDetailsInfo;
        Rectangle m_rtProductImage;
        Rectangle m_rtProductImage2;
        Rectangle m_rtProductChannel;
        Rectangle m_rtHVAxisInfo;

        public int m_nColorTheme;


        // 串口


        public int m_nPrintPosY;
        public int m_nDeviceStatus;
        public int m_nCommandType;
        void PrintPageHeader(uint nPageNumber/*, CPrintInfo pInfo*/)
        {


        }

        List<SSerInfo> m_SerInfoList = new List<SSerInfo>();





        /// <summary>
        /// 解決第一次載入winform時的控件速度
        /// 2019.03.28
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }


        public Main_Form()
        {
            InitializeComponent();
            Init();
        }

        private static Main_Form m_Instance = null;
        public static Main_Form Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new Main_Form();
                }
                return m_Instance;
            }
        }

        private void LoadAll(Form form)
        {
            if (form.Name == "Main_Form")
            {
                MultiLanguage.LoadLanguage(form, typeof(Main_Form));
            }
            else if (form.Name == "Main_DataManagement")
            {
                //form.Dock = DockStyle.None;
                form.Visible = false;
                //DockContent dockContent = form as DockContent;
                //MultiLanguage.LoadLanguageDcokContent(dockContent, typeof(Main_DataManagement));
                MultiLanguage.LoadLanguage(form, typeof(Main_DataManagement));
                form.Visible = true;
            }
            else if (form.Name == "Main_TempChart")
            {

                //DockContent dockContent = form as DockContent;
                // MultiLanguage.LoadLanguageDcokContent(dockContent, typeof(Main_TempChart));
                MultiLanguage.LoadLanguage(form, typeof(Main_TempChart));
            }
            else if (form.Name == "Main_DataAnasis")
            {
                //form.Dock = DockStyle.None;
                form.Visible = false;
                //DockContent dockContent = form as DockContent;
                //MultiLanguage.LoadLanguageDcokContent(dockContent, typeof(Main_DataAnasis));
                MultiLanguage.LoadLanguage(form, typeof(Main_DataAnasis));
                main_DataAnasis.SetGridWidth();
                form.Visible = true;
                //form.Dock = DockStyle.Fill;
            }

        }
        private void Frm_Main_Load(object sender, EventArgs e)
        {
            dockManager1.BeginUpdate();

            string language = Properties.Settings.Default.DefaultLanguage;
            if (language == "zh-CN")
            {
                barButton_LanguageCHS.PerformClick();
            }
            else if (language == "zh-CHT")
            {
                barButton_LanguageCHT.PerformClick();
            }
            else if (language == "en-US")
            {
                barButton_LanguageENG.PerformClick();
            }
            dockPanelDataAnasis.Visibility = DockVisibility.Visible;
            dockPanelManagement.Visibility = DockVisibility.Visible;
            RefreshPanes(true);
            dockManager1.EndUpdate();
        }

        private void Init()
        {
            dockManager1.BeginUpdate();
            CETCManagerApp.Instance.m_pETETCStage.ReadConditions();

            if (m_Instance == null)
            {
                m_Instance = this;
                main_DataManagement = new Main_DataManagement();
                main_TempChart = new Main_TempChart();
                main_DataAnasis = new Main_DataAnasis();
            }


            main_TempChart.TopLevel = false;
            main_TempChart.Parent = this;
            main_TempChart.Show();
            main_TempChart.Dock = DockStyle.Fill;

            main_DataAnasis.TopLevel = false;
            main_DataAnasis.ControlBox = false;
            main_DataAnasis.Dock = DockStyle.Fill;
            dockPanelDataAnasis.Controls.Add(main_DataAnasis);


            main_DataManagement.TopLevel = false;
            main_DataManagement.ControlBox = false;
            dockPanelManagement.Controls.Add(main_DataManagement);
            //main_DataManagement.Dock = DockStyle.Left;


            main_TempChart.Visible = true;
            main_DataAnasis.Visible = true;
            main_DataManagement.Visible = true;

            InitChannelBarButton();
            AutoSearchETCDevice();
            //SelectETCDevice();
            // 注册设备事件
            RegisterDeviceCommport();
            InitStatusBar();
            timer1.Enabled = true;

            barTool1.Offset = 0;
            barTool2.Offset = 0;
            barTool3.Offset = 0;


            dockManager1.EndUpdate();
        }

        private void InitChannelBarButton()
        {
            btnColor_1.EditValueChanged += btnColor_1_EditValueChanged;

            BarButtonItem btn = new BarButtonItem();
            btn.Caption = "";
            btn.ButtonStyle = BarButtonStyle.Check;
            btn.Name = "ToolButton_ChannelALL";
            btn.Glyph = DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/apply_16x16.png");
            btn.DownChanged += barButton_ChannelAll_ItemClick;
            barTool3.AddItem(btn);

            for (int i = 0; i < 24; i++)
            {
                popupMenus[i] = new PopupMenu();
                popupMenus[i].AddItem(btnColor_1);
                popupMenus[i].Name = $"pM_Channel_{i + 1}";
                popupMenus[i].BeforePopup += popupMenu_BeforePopup;
                popupMenus[i].Popup += PopUpMenu_Popup;


                btn = new BarButtonItem();
                btn.Caption = $"{i + 1}";
                btn.DropDownControl = popupMenus[i];
                btn.ButtonStyle = BarButtonStyle.CheckDropDown;
                btn.Name = $"ToolButton_Channel_{i + 1}";
                btn.Tag = new SuperTag() { BarButtonItemIndex = i };
                btn.Visibility = BarItemVisibility.Never;
                Random r = new Random(Guid.NewGuid().GetHashCode());
                Color clr;
                do
                {
                    clr = Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));
                    if (!ChannelColor.ContainsValue(clr))
                    {
                        ChannelColor.Add(i, clr);

                        CETCManagerApp.Instance.m_pETETCStage.m_ETProduct.m_SamplesPoints[i].m_clrColor = clr;
                        break;
                    }
                }
                while (true);

                btn.ItemAppearance.Normal.BackColor2 = clr;
                btn.DownChanged += barBtnChannel_DownChanged;

                btnChannels.Add(i, btn);
                var link = barTool3.AddItem(btn);
                if (i == 0)
                {
                    link.BeginGroup = true;
                }
            }

        }

        private void barButton_Channel_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void PopUpMenu_Popup(object sender, EventArgs e)
        {

        }

        private void barBtnChannel_DownChanged(object sender, ItemClickEventArgs e)
        {
            try
            {
                var series = main_TempChart.m_pChartSeries;
                var item = (BarButtonItem)e.Item;

                var s = series.ElementAtOrDefault(((SuperTag)e.Item.Tag).BarButtonItemIndex);
                if (s != null)
                {
                    series[((SuperTag)e.Item.Tag).BarButtonItemIndex].Visible = item.Down;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void Frm_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            //這邊要存DOCKPANEL的LAYOUT

            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
            /*if (m_bSaveLayout)
                dockPanel1.SaveAsXml(configFile);
            else if (File.Exists(configFile))
                File.Delete(configFile);

            if (File.Exists(configFile))
                dockPanelTempChart.SaveAsXml(configFile);*/
        }

        int RegisterDeviceCommport()
        {
            return 0;
        }

        void OnBeginPrinting()
        {

        }

        void InitStatusBar()
        {

            // 读取数据
            if (CETCManagerApp.Instance.m_nLinkStatus == 0)
            {
                string strTemp;
                int nPaneWidth;
                if (CETCManagerApp.Instance.m_ETETC.m_IsHaveWireLess)
                {
                    //nPaneWidth = rcStatusBar.Width() / 8;
                }
                else
                {
                    //nPaneWidth = rcStatusBar.Width() / 7;
                }

                barStaticItem1.Caption = "设备未连接 ";
                barStaticItem2.Caption = "";
                barStaticItem3.Caption = "";
                barStaticItem4.Caption = "";
                barStaticItem5.Caption = "";
                barStaticItem6.Caption = "";
                barStaticItem7.Caption = "";

                /*barStaticItem1.ItemAppearance.Normal.BackColor = Color.FromArgb(255, 232, 166);
                barStaticItem2.ItemAppearance.Normal.BackColor = Color.FromArgb(255, 232, 166);
                barStaticItem3.ItemAppearance.Normal.BackColor = Color.FromArgb(255, 232, 166);
                barStaticItem4.ItemAppearance.Normal.BackColor = Color.FromArgb(255, 232, 166);
                barStaticItem5.ItemAppearance.Normal.BackColor = Color.FromArgb(255, 232, 166);
                barStaticItem6.ItemAppearance.Normal.BackColor = Color.FromArgb(255, 232, 166);
                barStaticItem7.ItemAppearance.Normal.BackColor = Color.FromArgb(255, 232, 166);*/

                timer1.Start();
            }

        }

        internal void SetChannelColor()
        {
            int channelCount = CETCManagerApp.Instance.m_ETETC.m_nChannelCount;
            for (int i = 0; i < 24; i++)
            {
                if (i >= channelCount)
                {
                    btnChannels[i].Visibility = BarItemVisibility.Never;
                }
                else
                {
                    btnChannels[i].Down = true;
                    btnChannels[i].Visibility = BarItemVisibility.Always;
                    btnChannels[i].ItemAppearance.Normal.BackColor2 =
                             CETCManagerApp.Instance.m_pETETCStage.m_ETProduct.m_SamplesPoints[i].m_clrColor;
                }
            }


        }


        void SetupPrintLayout()
        {

        }

        void OnAnalyseReportPrint()
        {

        }

        void AutoSearchETCDevice()
        {
            // 遍历串口
            //EnumSerialPorts(m_SerInfoList, TRUE/*include all*/);

            // 遍历串口判断 ETC 类型
            int nCount = 0;
            for (int i = 0; i < m_SerInfoList.Count; i++)
            {
                string strTEmp = "TS\r\n";

                //Try out the overlapped functions
                if (m_SerInfoList[i].strFriendlyName.IndexOf("Prolific") >= 0)
                {
                    SerialPort port2 = new SerialPort()
                    {
                        PortName = "COM1",
                        BaudRate = 115200,
                        Parity = Parity.None,
                        DataBits = 8,
                        StopBits = StopBits.One,
                        ReadTimeout = 1000,
                        WriteTimeout = 5000,
                        NewLine = "\r",
                        Handshake = Handshake.None
                    };
                    int nCommID = 0;// int.Parse(m_SerInfoList[i].strPortName.Right(m_SerInfoList[i].strPortName.GetLength() - 3));
                    if (!port2.IsOpen)
                    {
                        port2.Open();

                        //
                        string strCMD = "ReadEnd\r\n";
                        port2.Write(strCMD);

                        char[] sRxBuf = new char[100];
                        Thread.Sleep(100);
                        // 读取
                        int dwRead = port2.Read(sRxBuf, 0, 80);

                        port2.Write(strTEmp);

                        Thread.Sleep(100);
                        // 读取
                        dwRead = port2.Read(sRxBuf, 0, 40);
                        sRxBuf[dwRead] = '\0';
                        string strCmd = new string(sRxBuf);
                        if (strCmd.IndexOf("OK") >= 0)
                        {
                            string strETC = "未知型号";
                            if (strCmd.IndexOf("RF") >= 0)
                            {
                                strETC = "RF(" + m_SerInfoList[i].strPortName + ")";
                            }
                            else if (strCmd.IndexOf("TS06") >= 0)
                            {
                                strETC = "DS-06 (" + m_SerInfoList[i].strPortName + ")";

                            }
                            else if (strCmd.IndexOf("TS12") >= 0)
                            {
                                strETC = "DS-12(" + m_SerInfoList[i].strPortName + ")";

                            }

                            nCount++;
                            m_SerInfoList[i].m_IsETCPro = true;
                            m_SerInfoList[i].strPortDesc = strETC;
                        }

                        //port2.SetMask(EV_TXEMPTY);
                        port2.Close();
                    }
                }
            }

            if (nCount > 0)
            {
                SelectETCDevice();
            }

        }

        bool SelectETCDevice()
        {
            IDD_ETC_SEARCH dlg = new IDD_ETC_SEARCH();
            dlg.ShowDialog();
            return true;
        }

        public void RefreshPanes(bool bForceAll)
        {

            /*if (m_paneDataChart.GetSafeHwnd())
            {
                //	m_paneDataChart.Refresh();
            }
            if (m_PaneReport.GetSafeHwnd())
            {
                //	m_PaneReport.Refresh(m_pActivePane);
            }*/

            SetChannelColor();

            if (Main_DataAnasis.Instance.m_pageAnaly != null)
            {
                Main_DataAnasis.Instance.m_pageAnaly.SwitchReportView(AnalysisBtn.ChartReport);
                Main_DataAnasis.Instance.m_pageAnaly.CreateReflowerResult();
            }

            if (Main_DataAnasis.Instance.m_pageProduct != null)
            {
                Main_DataAnasis.Instance.m_pageProduct.RefreshProduct();
            }

            if (Main_DataAnasis.Instance.m_pageData != null)
            {
                Main_DataAnasis.Instance.m_pageData.UpdateETCData();
                Main_DataAnasis.Instance.m_pageData.GetdispinfoSampleDatalist();
            }

            if (Main_DataAnasis.Instance.m_pageHV != null)
            {
                Main_DataAnasis.Instance.m_pageHV.RefreshHVData();
            }

            //if (Main_DataAnasis.Instance.m_pageTemplSetup != null)
            {
                Main_DataAnasis.Instance.RefreshData();
                //Main_DataAnasis.Instance.m_pageTemplSetup.RefreshHVData();
                //Main_DataAnasis.Instance.RefreshReflowData();
            }


        }

        private void barButton_File_ItemClick(object sender, ItemClickEventArgs e)
        {
            //打开
            if (e.Item.Name == nameof(barButton_Open) || e.Item.Name == nameof(ToolButton_Open))
            {
                CETCManagerApp.Instance.OnFileOpen();
            }
            //保存
            else if (e.Item.Name == nameof(barButton_Save) || e.Item.Name == nameof(ToolButton_Save))
            {
                CETCManagerApp.Instance.OnFileSave();
            }
            //另存为
            else if (e.Item.Name == nameof(barButton_SaveAs) || e.Item.Name == nameof(ToolButton_SaveAs))
            {
                CETCManagerApp.Instance.OnFileSaveAs();
            }
            //打印
            else if (e.Item.Name == nameof(barButton_Print) || e.Item.Name == nameof(ToolButton_Print))
            {
                IDD_PAGE_PRINT iDD_PAGE_PRINT = new IDD_PAGE_PRINT();
                MultiLanguage.LoadLanguage(iDD_PAGE_PRINT, typeof(IDD_PAGE_PRINT));
                iDD_PAGE_PRINT.ShowDialog();
            }
            //打印预览
            else if (e.Item.Name == nameof(barButton_PrintPreView) || e.Item.Name == nameof(ToolButton_PrintPreView))
            {
                IDD_PAGE_PRINT iDD_PAGE_PRINT = new IDD_PAGE_PRINT();
                MultiLanguage.LoadLanguage(iDD_PAGE_PRINT, typeof(IDD_PAGE_PRINT));
                iDD_PAGE_PRINT.ShowDialog();
            }
            //报表设置
            else if (e.Item.Name == nameof(barButton_ReportSetup))
            {

            }
            //导出PDF
            else if (e.Item.Name == nameof(barButton_ExportPDF) || e.Item.Name == nameof(ToolButton_ExportPDF))
            {

            }
            //导出WORD
            else if (e.Item.Name == nameof(barButton_ExportWORD) || e.Item.Name == nameof(ToolButton_ExportWORD))
            {

                PrintingSystem ps = new PrintingSystem();
                DevExpress.XtraPrintingLinks.CompositeLink compositeLink = new DevExpress.XtraPrintingLinks.CompositeLink();
                compositeLink.PrintingSystem = ps;


                PrintableComponentLink link = new PrintableComponentLink();
                var c = main_TempChart.GetChart();
                //Size size = c.Size;
                //c.Dock = DockStyle.None;
                //c.Size = new Size(400, 278);
                MemoryStream ms = new MemoryStream();
                c.ExportToImage(ms, ImageFormat.Bmp);
                PictureEdit pe = new PictureEdit();
                link.Images.Add(Image.FromStream(ms));
                link.Component = c;
                compositeLink.Links.Add(link);
                link = new PrintableComponentLink();
                link.Component = main_DataAnasis.GetGridControl(0);
                compositeLink.Links.Add(link);
                compositeLink.CreateDocument();
                compositeLink.ShowPreview();

            }
            //发送EMAIL
            else if (e.Item.Name == nameof(barButton_SendEMail) || e.Item.Name == nameof(ToolButton_SendEMail))
            {

            }
            //离开
            else if (e.Item.Name == nameof(barButton_Exit))
            {
                Close();
            }
        }

        private void barButton_View_DownChanged(object sender, ItemClickEventArgs e)
        {
            //状态条
            if (e.Item.Name == nameof(barButton_ViewStatuBar))
            {
                bar3.Visible = ((BarButtonItem)e.Item).Down;
            }
            else if (e.Item.Name == nameof(barButton_ViewTempArea) ||
                     e.Item.Name == nameof(ToolButton_TempArea))
            {
                main_DataManagement.m_IsViewReflower_Check(((BarButtonItem)e.Item).Down);
            }
            else if (e.Item.Name == nameof(barButton_ViewRefLine) ||
                     e.Item.Name == nameof(ToolButton_RefLine))
            {
                main_DataManagement.m_IsViewTinCream_Check(((BarButtonItem)e.Item).Down);
            }
            else if (e.Item.Name == nameof(barButton_ViewHVLine) ||
                     e.Item.Name == nameof(ToolButton_HVLine))
            {
                main_DataManagement.m_IsViewSimulation_Check(((BarButtonItem)e.Item).Down);
            }
            else if (e.Item.Name == nameof(barButton_ViewLabel))
            {
                main_DataManagement.m_IsViewNotes_Check(((BarButtonItem)e.Item).Down);
            }
            else if (e.Item.Name == nameof(barButton_ViewSlop))
            {
                main_DataManagement.m_IsViewSlope_Checke(((BarButtonItem)e.Item).Down);
            }
            else if (e.Item.Name == nameof(barButton_ViewGridLine) ||
                     e.Item.Name == nameof(ToolButton_GridLine))
            {
                main_DataManagement.m_IsViewGrid_Check(((BarButtonItem)e.Item).Down);
            }
        }

        public void ToolButton_SetDown(ToolButtonDownType type,bool check)
        {
            switch (type)
            {
                case ToolButtonDownType.None:
                    break;
                case ToolButtonDownType.TEMPAREA:
                    barButton_ViewTempArea.DownChanged -= barButton_View_DownChanged;
                    ToolButton_TempArea.DownChanged -= barButton_View_DownChanged;
                    barButton_ViewTempArea.Down = check;
                    ToolButton_TempArea.Down = check;
                    barButton_ViewTempArea.DownChanged += barButton_View_DownChanged;
                    ToolButton_TempArea.DownChanged += barButton_View_DownChanged;
                    break;
                case ToolButtonDownType.REFLINE:
                    barButton_ViewRefLine.DownChanged -= barButton_View_DownChanged;
                    ToolButton_RefLine.DownChanged -= barButton_View_DownChanged;
                    barButton_ViewRefLine.Down = check;
                    ToolButton_RefLine.Down = check;
                    barButton_ViewRefLine.DownChanged += barButton_View_DownChanged;
                    ToolButton_RefLine.DownChanged += barButton_View_DownChanged;
                    break;
                case ToolButtonDownType.HVLINE:
                    barButton_ViewHVLine.DownChanged -= barButton_View_DownChanged;
                    ToolButton_HVLine.DownChanged -= barButton_View_DownChanged;
                    barButton_ViewHVLine.Down = check;
                    ToolButton_HVLine.Down = check;
                    barButton_ViewHVLine.DownChanged += barButton_View_DownChanged;
                    ToolButton_HVLine.DownChanged += barButton_View_DownChanged;
                    break;
                case ToolButtonDownType.LABEL:
                    barButton_ViewLabel.DownChanged -= barButton_View_DownChanged;
                    barButton_ViewLabel.Down = check;
                    barButton_ViewLabel.DownChanged += barButton_View_DownChanged;
                    break;
                case ToolButtonDownType.SLOPE:
                    barButton_ViewSlop.DownChanged -= barButton_View_DownChanged;
                    barButton_ViewSlop.Down = check;
                    barButton_ViewSlop.DownChanged += barButton_View_DownChanged;
                    break;
                case ToolButtonDownType.GRIDLINE:
                    barButton_ViewGridLine.DownChanged -= barButton_View_DownChanged;
                    ToolButton_GridLine.DownChanged -= barButton_View_DownChanged;
                    barButton_ViewGridLine.Down = check;
                    ToolButton_GridLine.Down = check;
                    barButton_ViewGridLine.DownChanged += barButton_View_DownChanged;
                    ToolButton_GridLine.DownChanged += barButton_View_DownChanged;
                    break;
                default:
                    break;
            }
        }


        private void barButton_View_ItemClick(object sender, ItemClickEventArgs e)
        {
            
        }

        private void barButton_DataManager_ItemClick(object sender, ItemClickEventArgs e)
        {
            //实时监控
            if (e.Item.Name == nameof(barButton_Monitor))
            {
                var theApp = CETCManagerApp.Instance;
                switch (theApp.m_nLinkStatus)
                {
                    case 0:
                        {
                            IDD_ETC_SEARCH pageOptionETC = new IDD_ETC_SEARCH();
                            MultiLanguage.LoadLanguage(pageOptionETC, typeof(IDD_ETC_SEARCH));
                            if(pageOptionETC.ShowDialog() == DialogResult.OK)                            
                            {
                                theApp.m_ETETC.m_nComID = pageOptionETC.GetComID();
                                if (pageOptionETC.m_IsHaveETC)
                                {
                                    theApp.m_nLinkStatus = 1;
                                    m_strDeviceName = pageOptionETC.m_strDeviceName;
                                    OpenETCDevice();
                                }
                                else
                                {
                                    m_strDeviceName = "";
                                    theApp.m_nLinkStatus = 2;  // 选择了型号，但是没有设备。
                                }
                                main_TempChart.CreateAnalyseChart();
                            }
                            else
                            {
                                m_strDeviceName = "";
                                theApp.m_nLinkStatus = 0;
                            }
                        }
                        break;
                    case 1:
                        {
                            IDD_OPTION_MONITOR dlg = new IDD_OPTION_MONITOR();
                            
                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                IDD_SETUP_PROJECTINFO pageOptionProduct = new IDD_SETUP_PROJECTINFO();
                                theApp.m_pETETCStage.m_ETProjectInfo.m_strMeasureTime = DateTime.Now.ToString("%y%M%d-%h%m%s");

                                if (pageOptionProduct.ShowDialog()==DialogResult.OK)
                                {
                                    theApp.m_pETETCStage.LoadFileRealTime(m_strTempFile);
                                    theApp.m_pETETCStage.m_ETProjectInfo.m_strProjectName = pageOptionProduct.strProductName;
                                    theApp.m_pETETCStage.m_ETProduct.m_strName = pageOptionProduct.strProductName;
                                    theApp.m_pETETCStage.m_ETProduct.m_strProductCode = pageOptionProduct.strProductID;
                                    theApp.m_pETETCStage.m_ETProjectInfo.m_strCustomer = pageOptionProduct.strCustomor;
                                    theApp.m_pETETCStage.m_ETProduct.m_strDescription = pageOptionProduct.strProductDesc;

                                    theApp.m_pETETCStage.m_ETProjectInfo.m_strTinCream = pageOptionProduct.strTinCreamName;
                                    theApp.m_pETETCStage.m_ETTinCream.m_strName = pageOptionProduct.strTinCreamName;
                                    theApp.m_pETETCStage.m_ETTinCream.m_strManufacturers = pageOptionProduct.strTinCreamPara;
                                    theApp.m_pETETCStage.m_ETProjectInfo.m_strProductionline = pageOptionProduct.strProductLine;
                                    theApp.m_pETETCStage.m_ETProjectInfo.m_strHaiqiLongdu = pageOptionProduct.strHaiqiLongdu;
                                    theApp.m_pETETCStage.m_ETTinCream.m_strType = pageOptionProduct.strTinCream;

                                    theApp.m_pETETCStage.m_ETProjectInfo.m_strOperator = pageOptionProduct.strMeasureOperator;
                                    theApp.m_pETETCStage.m_ETProjectInfo.m_strMeasureTime = pageOptionProduct.strMeasureTime;
                                    theApp.m_pETETCStage.m_ETProjectInfo.m_strMeasureDesc = pageOptionProduct.strMeasureDesc;

                                    theApp.m_pETETCStage.m_ETProjectInfo.m_nSampleRate = pageOptionProduct.nSampleRate;
                                    theApp.m_pETETCStage.m_ETProjectInfo.m_nMeasureCount = pageOptionProduct.nMeasureCount;
                                    theApp.m_pETETCStage.m_ETProjectInfo.m_nDataTotalTime = pageOptionProduct.nMearsureLength;
                                    theApp.m_pETETCStage.m_ETProjectInfo.m_strDataDesc = pageOptionProduct.strDataDesc;

                                    RefreshPanes(true);
                                    main_TempChart.ShowChart(true);
                                    main_TempChart.CreateAnalyseChart();
                                    main_DataManagement.RefreshProjectInfo();
                                }
                            }

                        }
                        break;
                    case 2:
                        {
                            new BoxMsg("采样板没有连接，请确认采样板已经连接上USB端口", "提示").ShowDialog();
                        }
                        break;
                }

            }
            //数据下载
            else if (e.Item.Name == nameof(barButton_Download))
            {
                IDD_ETC_SEARCH iDD_ETC_SEARCH = new IDD_ETC_SEARCH();
                MultiLanguage.LoadLanguage(iDD_ETC_SEARCH, typeof(IDD_ETC_SEARCH));
                iDD_ETC_SEARCH.ShowDialog();
            }
            //无线下载
            else if (e.Item.Name == nameof(barButton_WifiDownload))
            {
               
            }
            //项目信息
            else if (e.Item.Name == nameof(barButton_Info))
            {
                IDD_SETUP_PROJECTINFO iDD_OPTION_PROJECTINFO = new IDD_SETUP_PROJECTINFO();
                MultiLanguage.LoadLanguage(iDD_OPTION_PROJECTINFO, typeof(IDD_SETUP_PROJECTINFO));
                iDD_OPTION_PROJECTINFO.ShowDialog();
            }
        }

        private void barButton_Analysis_ItemClick(object sender, ItemClickEventArgs e)
        {
            //标签
            if (e.Item.Name == nameof(barButton_Mark))
            {
                ToolButton_AddLabel.Down = true;
            }
            //标签列表
            else if (e.Item.Name == nameof(barButton_MarkList))
            {
                IDD_NOTE_LIST iDD_NOTE_LIST = new IDD_NOTE_LIST();
                MultiLanguage.LoadLanguage(iDD_NOTE_LIST, typeof(IDD_NOTE_LIST));
                iDD_NOTE_LIST.ShowDialog();
            }
            //斜率线
            else if (e.Item.Name == nameof(barButton_Slop))
            {
                ToolButton_AddSlop.Down = true;
            }
            //斜率线列表
            else if (e.Item.Name == nameof(barButton_SlopList))
            {
                IDD_RATELINE_LIST iDD_RATELINE_LIST = new IDD_RATELINE_LIST();
                MultiLanguage.LoadLanguage(iDD_RATELINE_LIST, typeof(IDD_RATELINE_LIST));
                iDD_RATELINE_LIST.ShowDialog();
            }
            //XY座标
            else if (e.Item.Name == nameof(barButton_XY))
            {
                IDD_OPTION_HV iDD_DIALOG3_TemControl = new IDD_OPTION_HV();
                MultiLanguage.LoadLanguage(iDD_DIALOG3_TemControl, typeof(IDD_OPTION_HV));
                if(iDD_DIALOG3_TemControl.ShowDialog() == DialogResult.OK)
                {
                    main_TempChart.CreateHVChart();
                }
            }
            //XY座标编辑
            else if (e.Item.Name == nameof(barButton_XYEdit))
            {

            }
            //温度设定
            else if (e.Item.Name == nameof(barButton_TempSetup))
            {
                main_DataAnasis.SetPage(1);
            }
            //分析设置
            else if (e.Item.Name == nameof(barButton_AnalysisSetup))
            {
                IDD_OPTION_ANALYSE_CONDITION iDD_OPTION_ANALYSE_CONDITION = new IDD_OPTION_ANALYSE_CONDITION();
                MultiLanguage.LoadLanguage(iDD_OPTION_ANALYSE_CONDITION, typeof(IDD_OPTION_ANALYSE_CONDITION));
                if(iDD_OPTION_ANALYSE_CONDITION.ShowDialog()==DialogResult.OK)
                {
                    Main_DataAnasis.Instance.m_pageAnaly.SwitchReportView(0);
                }
            }
        }

        private void barButton_Setup_ItemClick(object sender, ItemClickEventArgs e)
        {
            //采样版调试
            if (e.Item.Name == nameof(barButton_Debug) || e.Item.Name == nameof(ToolButton_Debug))
            {
                var theApp = CETCManagerApp.Instance;
                switch (theApp.m_nLinkStatus)
                {
                    case 0:
                        {
                            IDD_ETC_SEARCH pageOptionETC = new IDD_ETC_SEARCH();
                            if (pageOptionETC.ShowDialog() == DialogResult.OK)
                            {
                                theApp.m_ETETC.m_nComID = pageOptionETC.GetComID();
                                if (pageOptionETC.m_IsHaveETC)
                                {
                                    theApp.m_nLinkStatus = 1;
                                    m_strDeviceName = pageOptionETC.m_strDeviceName;
                                    OpenETCDevice();
                                }
                                else
                                {
                                    m_strDeviceName = "";
                                    theApp.m_nLinkStatus = 2;  // 选择了型号，但是没有设备。
                                }
                                main_TempChart.CreateAnalyseChart();
                            }
                            else
                            {
                                m_strDeviceName = "";
                                theApp.m_nLinkStatus = 0;
                            }
                        }
                        break;
                    case 1:
                        {
                            // TODO: 在此添加命令处理程序代码
                            m_nCommandType = 2;
                            theApp.m_IsTestCommand = true;
                            IDD_OPTION_ETC pageOptionETC = new IDD_OPTION_ETC();

                            if (pageOptionETC.ShowDialog() == DialogResult.OK)
                            {
                                m_nCommandType = 2;
                                theApp.m_nLinkStatus = 1;
                                if (pageOptionETC.SampleRate == 0)
                                {
                                    theApp.m_ETETC.m_nSampleRate = 1;
                                }
                                else if (pageOptionETC.SampleRate == 1)
                                {
                                    theApp.m_ETETC.m_nSampleRate = 2;
                                }
                                else if (pageOptionETC.SampleRate == 2)
                                {
                                    theApp.m_ETETC.m_nSampleRate = 5;
                                }
                                else if (pageOptionETC.SampleRate == 3)
                                {
                                    theApp.m_ETETC.m_nSampleRate = 10;
                                }
                                else if (pageOptionETC.SampleRate == 4)
                                {
                                    theApp.m_ETETC.m_nSampleRate = 20;
                                }
                                else
                                {
                                    theApp.m_ETETC.m_nSampleRate = 20;
                                }

                                theApp.m_pETETCStage.m_nSampleRate = theApp.m_ETETC.m_nSampleRate;
                            }
                            theApp.m_IsTestCommand = false;
                        }
                        break;
                    case 2:
                        {
                            new BoxMsg("采样板没有连接，请确认采样板已经连接上USB端口","提示").ShowDialog();
                        }
                        break;
                }


            }
            //产品管理
            else if (e.Item.Name == nameof(barButton_ProductManager))
            {
                IDD_OPTION_PRODUCT dlg = new IDD_OPTION_PRODUCT();
                //MultiLanguage.LoadLanguage(dlg, typeof(IDD_OPTION_REFLOWER));
                dlg.ShowDialog();
            }
            //回流炉管理
            else if (e.Item.Name == nameof(barButton_ReflowManager))
            {
                IDD_OPTION_REFLOWER iDD_OPTION_REFLOWER = new IDD_OPTION_REFLOWER();
                MultiLanguage.LoadLanguage(iDD_OPTION_REFLOWER, typeof(IDD_OPTION_REFLOWER));
                if(iDD_OPTION_REFLOWER.ShowDialog() == DialogResult.OK)
                {
                    main_TempChart.CreateAnalyseChart();
                    RefreshPanes(true);
                }
            }
            //简体中文
            else if (e.Item.Name == nameof(barButton_LanguageCHS))
            {
                dockManager1.BeginUpdate();
                main_DataManagement.MaximumSize = new Size(272, 1080);
                //修改默认语言
                MultiLanguage.SetDefaultLanguage("zh-CN");
                //对所有打开的窗口重新加载语言
                foreach (Form form in Application.OpenForms)
                {
                    LoadAll(form);
                }
                dockPanelManagement.Size = main_DataManagement.MaximumSize;
                dockManager1.EndUpdate();
            }
            //繁体中文
            else if (e.Item.Name == nameof(barButton_LanguageCHT))
            {
                dockManager1.BeginUpdate();
                main_DataManagement.MaximumSize = new Size(272, 1080);
                
                //修改默认语言
                MultiLanguage.SetDefaultLanguage("zh-CHT");
                //对所有打开的窗口重新加载语言
                foreach (Form form in Application.OpenForms)
                {
                    LoadAll(form);
                }
                dockPanelManagement.Size = main_DataManagement.MaximumSize;
                dockManager1.EndUpdate();
            }
            //英文
            else if (e.Item.Name == nameof(barButton_LanguageENG))
            {
                dockManager1.BeginUpdate();
                main_DataManagement.MaximumSize = new Size(300, 1080);
                //修改默认语言
                MultiLanguage.SetDefaultLanguage("en-US");
                //对所有打开的窗口重新加载语言
                foreach (Form form in Application.OpenForms)
                {
                    LoadAll(form);
                }
                dockPanelManagement.Size = main_DataManagement.MaximumSize;
                dockManager1.EndUpdate();
            }
        }

        private void barButton_Help_ItemClick(object sender, ItemClickEventArgs e)
        {
            //目录
            if (e.Item.Name == nameof(barButton_Contents))
            {
                /*FormSerialize _Serialize = new FormSerialize();
                string Cht = "resource_cht";
                _Serialize.Serialize($"{Cht}_{Name}", this);//序列化中文版
                _Serialize.Serialize($"{Cht}_{main_DataAnasis.Name}", main_DataAnasis);//序列化中文版
                _Serialize.Serialize($"{Cht}_{main_DataManagement.Name}", main_DataManagement);//序列化中文版
                _Serialize.Serialize($"{Cht}_{main_TempChart.Name}", main_TempChart);//序列化中文版*/
            }
            //关于
            else if (e.Item.Name == nameof(barButton_About) || e.Item.Name == nameof(ToolButton_About))
            {
                IDD_ABOUTBOX iDD_ABOUTBOX = new IDD_ABOUTBOX();
                MultiLanguage.LoadLanguage(iDD_ABOUTBOX, typeof(IDD_ABOUTBOX));
                iDD_ABOUTBOX.ShowDialog();
            }
            //检查更新
            else if (e.Item.Name == nameof(barButton_CheckUpdate))
            {
                AutoUpdater.CheckForUpdateEvent += AutoUpdaterOnCheckForUpdateEvent;
                AutoUpdater.ApplicationExitEvent += AutoUpdater_ApplicationExitEvent;
                AutoUpdater.ParseUpdateInfoEvent += AutoUpdaterOnParseUpdateInfoEvent;
                AutoUpdater.Start("http://kupoautos.com/ETC/AutoUpdater.json");
            }
        }

        private void ToolButton_AddLabel_DownChanged(object sender, ItemClickEventArgs e)
        {
            if (((BarButtonItem)e.Item).Down)
            {
                ToolButton_AddSlop.Down = false;
                SetToolButtonStatu(ToolButtonDrawType.LABEL);
            }
            else
            {
                SetToolButtonStatu(ToolButtonDrawType.None); 
            }
        }

        private void ToolButton_AddSlop_DownChanged(object sender, ItemClickEventArgs e)
        {
            if (((BarButtonItem)e.Item).Down)
            {
                ToolButton_AddLabel.Down = false;
                SetToolButtonStatu(ToolButtonDrawType.SLOP);
            }
            else
            {
                SetToolButtonStatu(ToolButtonDrawType.None);
            }
        }
        public void SetToolButtonStatu(ToolButtonDrawType type)
        {
            CETCManagerApp.Instance.ToolByttonNowStatu = type;

            
            switch (type)
            {
                case ToolButtonDrawType.None:
                    ToolButton_AddSlop.Down = false;
                    ToolButton_AddLabel.Down = false;
                    break;
                default:
                    break;
            }
        }



        public Bar GetBar()
        {
            return barFeatures;
        }


        private void AutoUpdaterOnParseUpdateInfoEvent(ParseUpdateInfoEventArgs args)
        {
            AutoUpdater.ParseUpdateInfoEvent -= AutoUpdaterOnParseUpdateInfoEvent;
            dynamic json = JsonConvert.DeserializeObject(args.RemoteData);
            args.UpdateInfo = new UpdateInfoEventArgs
            {
                CurrentVersion = json.version,
                ChangelogURL = json.changelog,
                Mandatory = json.mandatory,
                DownloadURL = json.url
            };
        }



        private void AutoUpdaterOnCheckForUpdateEvent(UpdateInfoEventArgs args)
        {
            AutoUpdater.CheckForUpdateEvent -= AutoUpdaterOnCheckForUpdateEvent;
            if (args != null)
            {
                if (args.IsUpdateAvailable)
                {
                    AutoUpdater.ShowUpdateForm();
                    

                    /*DialogResult dialogResult;
                    if (args.Mandatory)
                    {
                        dialogResult =
                            MessageBox.Show(
                                $@"There is new version {args.CurrentVersion} available. You are using version {args.InstalledVersion}. This is required update. Press Ok to begin updating the application.", @"Update Available",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                    }
                    else
                    {
                        dialogResult =
                            MessageBox.Show(
                                $@"There is new version {args.CurrentVersion} available. You are using version {
                                        args.InstalledVersion
                                    }. Do you want to update the application now?", @"Update Available",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Information);
                    }

                    // Uncomment the following line if you want to show standard update dialog instead.
                    AutoUpdater.ShowUpdateForm();
                    
                    if (dialogResult.Equals(DialogResult.Yes) || dialogResult.Equals(DialogResult.OK))
                    {
                        try
                        {
                            if (AutoUpdater.DownloadUpdate())
                            {
                                //Application.Exit();
                                Close();
                            }
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }*/
                }
                else
                {
                    new BoxMsg(@"已是最新版本", @"提示").ShowDialog();
                }
            }
            else
            {
                new BoxMsg(@"伺服器無回應，請稍後再試！",@"錯誤").ShowDialog();
            }
        }

        private void AutoUpdater_ApplicationExitEvent()
        {
            AutoUpdater.ApplicationExitEvent -= AutoUpdater_ApplicationExitEvent;
            Close();
        }

        private void btnColor_1_EditValueChanged(object sender, EventArgs e)
        {
            if (btnChannels[m_NowChannelNo].ItemAppearance.Normal.BackColor2 != (Color)btnColor_1.EditValue)
            {
                btnChannels[m_NowChannelNo].ItemAppearance.Normal.BackColor2 = (Color)btnColor_1.EditValue;
                CETCManagerApp.Instance.m_pETETCStage.m_ETProduct.m_SamplesPoints[m_NowChannelNo].m_clrColor = (Color)btnColor_1.EditValue;
                main_TempChart.UpdateChannelColor();
            }
        }

        private void customBarAndDockingController1_CustomDraw(object sender, CustomDraw.CustomDrawLinkArgs e)
        {
            e.DefaultDraw();
            //ToolButton_Channel1
            string itemCaption = e.Info.LinkInfo.Link.Item.Name;
            if (itemCaption.StartsWith("ToolButton_Channel_"))
            {
                int num = itemCaption.Replace("ToolButton_Channel_", "").ToInt() - 1;
                using (Brush p = new SolidBrush(btnChannels[num].ItemAppearance.Normal.BackColor2))
                {
                    if (num > 8) //10號以上
                    {
                        e.Graphics.FillRectangle(p, new Rectangle(e.Info.LinkInfo.CaptionRect.X, e.Info.LinkInfo.CaptionRect.Y + e.Info.LinkInfo.CaptionRect.Height - 4,
                                                              e.Info.LinkInfo.CaptionRect.Width, 4));
                    }
                    else
                    {
                        e.Graphics.FillRectangle(p, new Rectangle(e.Info.LinkInfo.CaptionRect.X-4, e.Info.LinkInfo.CaptionRect.Y + e.Info.LinkInfo.CaptionRect.Height - 4,
                                                              e.Info.LinkInfo.CaptionRect.Width+8, 4));
                    }
                }
                    
            }
        }


        private void popupMenu_BeforePopup(object sender, System.ComponentModel.CancelEventArgs e)
        {
            m_NowChannelNo = (sender as PopupMenu).Name.Replace("pM_Channel_", "").ToInt() - 1;
            btnColor_1.EditValue = btnChannels[m_NowChannelNo].ItemAppearance.Normal.BackColor2;
        }

        private void barButton_ChannelAll_ItemClick(object sender, ItemClickEventArgs e)
        {
            for (int i = 0; i < btnChannels.Count; i++)
            {
                btnChannels[i].Down = !m_btnChannelAllChecked;
            }
            m_btnChannelAllChecked = !m_btnChannelAllChecked;

            
        }

        bool OpenETCDevice()
        {
            string strComm = $"COM{CETCManagerApp.Instance.m_ETETC.m_nComID + 1}";
            OpenPort(strComm, "115200");

            return true;
        }

        void OpenPort(string strPortName, string strBaudRate)
        {
            //需要重写SERIAL COM
            /*m_strBaudRate = strBaudRate;
            m_strPortName = strPortName;
            m_bPortActivate = true;*/
        }


    }
}


