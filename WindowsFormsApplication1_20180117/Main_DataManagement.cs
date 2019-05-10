using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using ETCProfiler.classes;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Windows.Forms;
using static ETCProfiler.classes.ShareFunc;
using System.Linq;
using System.Collections.Generic;
using DevExpress.XtraTreeList;
using DevExpress.XtraSplashScreen;
using static ETCProfiler.Enums.Statu;
//using WeifenLuo.WinFormsUI.Docking;

namespace ETCProfiler
{
    public partial class Main_DataManagement : XtraForm
    {
        public Main_DataManagement()
        {
            InitializeComponent();
            if (m_Instance == null)            
                m_Instance = this;

            //Init();
            Update(true);

            m_DataList.ClearNodes();
            if (Directory.Exists(@".\Data"))
            {
                GetDataList(@".\Data", m_DataList.Nodes);
            }
            else
            {
                Directory.CreateDirectory(@".\Data");
            }
        }

        private void Init()
        {
            //圖表分析
            /*m_IsViewGrid.Checked = true;
            m_IsViewNotes.Checked = true;
            m_IsViewReflower.Checked = true;
            m_IsViewSimulation.Checked = true;
            m_IsViewSlope.Checked = true;
            m_IsViewTinCream.Checked = true;*/

/*           m_fAxisXMain = 0.0;*/

            m_fAxisXMax.Value = 600;
            m_fAxisXMin.Value = 0;
            m_fAxisXMinor.Value = 5;
/*            m_fAxisYMain = 0.0;*/
            m_fAxisYMax.Value = 360;
            m_fAxisYMin.Value = 0;
            m_fAxisYMinor.Value = 5;
            m_nHNum.Value = 2;
//             m_strNoteContent = _T("");
//             m_fSlopeX1.Value = 0;
//             m_fSlopeX2.Value = 0;
//             m_fSlopeY1.Value = 0;
//             m_fSlopeY2.Value = 0;
            m_nVNum.Value = 2;


            //數據管理
            m_strCustomer.Text = "";
            m_nDataLength.Text = "0";
            m_strOperate.Text  = "";
            m_strProductLine.Text  = "";
            m_strProductName.Text  = "";
            m_nSampleCount.Text = "0";
            m_nSampleRate.Text = "0";
            m_strDateTime.Text  = "";
            m_strTinCreamName.Text  = "";
            m_strFileNameFilter.Text  = "";
            m_strHaiqiLongdu.Text  = "";
        }

        public void Update(bool value)
        {
            if (value)
            {
                var theApp = CETCManagerApp.Instance;
                m_nHNum.Value = theApp.m_pETETCStage.m_nHAxisNum;
                m_nVNum.Value = theApp.m_pETETCStage.m_nVAxisNum;

                m_fAxisXMax.Value = decimal.Parse(theApp.m_pETETCStage.m_fAxisXMax.ToString());
                m_fAxisXMin.Value = decimal.Parse(theApp.m_pETETCStage.m_fAxisXMin.ToString());
                //m_fAxisXMain = theApp.m_pETETCStage.m_fMainScaleTicksX;
                if (theApp.m_pETETCStage.m_fMinorScaleTicksX <= 0)
                {
                    theApp.m_pETETCStage.m_fMinorScaleTicksX = 1;
                }
                m_fAxisXMinor.Value = decimal.Parse(theApp.m_pETETCStage.m_fMinorScaleTicksX.ToString());


                m_fAxisYMax.Value = decimal.Parse(theApp.m_pETETCStage.m_fAxisYMax.ToString());
                m_fAxisYMin.Value = decimal.Parse(theApp.m_pETETCStage.m_fAxisYMin.ToString());
                //m_fAxisYMain = theApp.m_pETETCStage.m_fMainScaleTicksY;
                if (theApp.m_pETETCStage.m_fMinorScaleTicksY <= 0)
                {
                    theApp.m_pETETCStage.m_fMinorScaleTicksY = 1;
                }
                m_fAxisYMinor.Value = decimal.Parse(theApp.m_pETETCStage.m_fMinorScaleTicksY.ToString());

                /*m_IsViewSimulation.Checked = Main_TempChart.Instance.m_IsShowHV;
                m_IsViewGrid.Checked = Main_TempChart.Instance.m_IsShowGrid;
                m_IsViewTinCream.Checked = Main_TempChart.Instance.m_IsShowTinCream;
                m_IsViewReflower.Checked = Main_TempChart.Instance.m_IsShowReflowerZone;
                m_IsViewNotes.Checked = Main_TempChart.Instance.m_IsShowNotes;
                m_IsViewSlope.Checked = Main_TempChart.Instance.m_IsShowSlope;
                m_nHNum.Value = CETCManagerApp.Instance.m_pETETCStage.m_nHAxisNum;
                m_nVNum.Value = CETCManagerApp.Instance.m_pETETCStage.m_nVAxisNum;*/

            }
            else
            {
                Main_TempChart.Instance.m_IsShowHV = m_IsViewSimulation.Checked;
                Main_TempChart.Instance.m_IsShowGrid = m_IsViewGrid.Checked;
                Main_TempChart.Instance.m_IsShowTinCream = m_IsViewTinCream.Checked;
                Main_TempChart.Instance.m_IsShowReflowerZone = m_IsViewReflower.Checked;
                Main_TempChart.Instance.m_IsShowNotes = m_IsViewNotes.Checked;
                Main_TempChart.Instance.m_IsShowSlope = m_IsViewSlope.Checked;
                CETCManagerApp.Instance.m_pETETCStage.m_nHAxisNum = atoi(m_nHNum.Value.ToString());
                CETCManagerApp.Instance.m_pETETCStage.m_nVAxisNum = atoi(m_nVNum.Value.ToString());

                var Diagram = Main_TempChart.Instance.GetChart().Diagram as XYDiagram;
                
                // Define the whole range for the axes. 
                Diagram.AxisY.WholeRange.SetMinMaxValues(m_fAxisYMin.Value, m_fAxisYMax.Value);

                // Define the visible range for the axes.   
                Diagram.AxisY.VisualRange.SetMinMaxValues(m_fAxisYMin.Value, m_fAxisYMax.Value);

                Diagram.AxisY.MinorCount = (int)m_fAxisYMinor.Value;

                // Define the whole range for the axes. 
                Diagram.AxisY.WholeRange.SetMinMaxValues(m_fAxisYMin.Value, m_fAxisYMax.Value);

                // Define the visible range for the axes.   
                Diagram.AxisY.VisualRange.SetMinMaxValues(m_fAxisYMin.Value, m_fAxisYMax.Value);

                Diagram.AxisY.MinorCount = (int)m_fAxisYMinor.Value;
            }
        }

        private static Main_DataManagement m_Instance;
  

       // 静态实例初始化函数
        public static Main_DataManagement Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new Main_DataManagement();
                }
                return m_Instance;
            }
        }


        public bool IsViewGrid
        {
            get
            {
                return m_IsViewGrid.Checked;
            }
        }
        //为了保证在关闭某一浮动窗体之后，再打开时能够在原位置显示，要对浮动窗体处理，处理窗体的DockstateChanged事件，标签窗体dock位置改变，记录到公共类  

        /*public  void Main_DataManagement_DockStateChanged(object sender, EventArgs e)
        {
            //关闭时（dockstate为unknown） 不把dockstate保存  
            if (m_Instance != null)
            {
                if (this.DockState == DockState.Unknown || this.DockState == DockState.Hidden)
                {
                    return;
                }

                AppConfig.dockState1 = this.DockState;
            }
        }  */


        private void Main_DataManagement_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_Instance = null;  // 否则下次打开时报错，提示“无法访问已释放对象”  

        }

        private void Main_DataManagement_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true; // 点击关闭按钮，无效，窗口不会关闭
        }

        private void Main_DataManagement_Load(object sender, EventArgs e)
        {
          //  MultiLanguage.LoadLanguage(this, typeof(Main_DataManagement));
        }

        private void btnAdvanced_Click(object sender, EventArgs e)
        {
            IDD_DATALIST_FILTER dlg = new IDD_DATALIST_FILTER();
            dlg.ShowDialog();
        }

        public void RefreshProjectInfo()
        {
            var theApp = CETCManagerApp.Instance;
            m_strProductName.Text = theApp.m_pETETCStage.m_ETProjectInfo.m_strProjectName;
            m_strCustomer.Text = theApp.m_pETETCStage.m_ETProjectInfo.m_strCustomer;
            m_nDataLength.Text = theApp.m_pETETCStage.m_ETProjectInfo.m_nDataTotalTime.ToString();
            m_strDateTime.Text = theApp.m_pETETCStage.m_ETProjectInfo.m_strMeasureTime;
            m_nSampleRate.Text = theApp.m_pETETCStage.m_nSampleRate.ToString();
            m_nSampleCount.Text = theApp.m_pETETCStage.m_ETProjectInfo.m_nMeasureCount.ToString();
            m_strOperate.Text = theApp.m_pETETCStage.m_ETProjectInfo.m_strOperator;
            m_strTinCreamName.Text = theApp.m_pETETCStage.m_ETProjectInfo.m_strTinCream;
            m_strProductLine.Text = theApp.m_pETETCStage.m_ETProjectInfo.m_strProductionline;
            m_strHaiqiLongdu.Text = theApp.m_pETETCStage.m_ETProjectInfo.m_strHaiqiLongdu;

            //UpdateData(FALSE);
        }

        private void m_IsViewSimulation_CheckedChanged(object sender, EventArgs e)
        {
            Main_Form.Instance.ToolButton_SetDown(ToolButtonDownType.HVLINE, m_IsViewSimulation.Checked);
            Main_TempChart.Instance.m_IsShowHV = m_IsViewSimulation.Checked;
            Main_TempChart.Instance.CreateHVChart();     
        }

        public void m_IsViewSimulation_Check(bool check)
        {
            m_IsViewSimulation.Checked = check;
        }

        private void m_IsViewGrid_CheckedChanged(object sender, EventArgs e)
        {
            // TODO: 在此添加命令处理程序代码
            if (Main_TempChart.Instance.m_pChartSeries.Count == 0)
            {
                return;
            }
            Main_Form.Instance.ToolButton_SetDown(ToolButtonDownType.GRIDLINE, m_IsViewGrid.Checked);
            Main_TempChart.Instance.m_IsShowGrid = m_IsViewGrid.Checked;
            Main_TempChart.Instance.CreateGridLines();
            
        }

        public void m_IsViewGrid_Check(bool check)
        {
            m_IsViewGrid.Checked = check;
        }

        private void m_IsViewTinCream_CheckedChanged(object sender, EventArgs e)
        {
            // TODO: 在此添加命令处理程序代码
            if (Main_TempChart.Instance.m_pChartSeries.Count == 0)
            {
                return;
            }
            Main_Form.Instance.ToolButton_SetDown(ToolButtonDownType.REFLINE, m_IsViewTinCream.Checked);
            Main_TempChart.Instance.m_IsShowTinCream = m_IsViewTinCream.Checked;
            //Main_TempChart.Instance.CreateTinCreamChart();
        }

        public void m_IsViewTinCream_Check(bool check)
        {
            m_IsViewTinCream.Checked = check;
        }

        private void m_IsViewReflower_CheckedChanged(object sender, EventArgs e)
        {
            Main_Form.Instance.ToolButton_SetDown(ToolButtonDownType.TEMPAREA, m_IsViewReflower.Checked);
            Main_TempChart.Instance.m_IsShowReflowerZone = m_IsViewReflower.Checked;
            Main_TempChart.Instance.CreateReflowerChart();
        }

        public void m_IsViewReflower_Check(bool check)
        {
            m_IsViewReflower.Checked = check;
        }

        private void m_IsViewNotes_CheckedChanged(object sender, EventArgs e)
        {
            Main_Form.Instance.ToolButton_SetDown(ToolButtonDownType.LABEL, m_IsViewNotes.Checked);
            Main_TempChart.Instance.m_IsShowNotes = m_IsViewNotes.Checked;
            Main_TempChart.Instance.CreateNotesChart();
        }

        public void m_IsViewNotes_Check(bool check)
        {
            m_IsViewNotes.Checked = check;
        }

        private void m_IsViewSlope_CheckedChanged(object sender, EventArgs e)
        {
            Main_Form.Instance.ToolButton_SetDown(ToolButtonDownType.SLOPE, m_IsViewSlope.Checked);
            Main_TempChart.Instance.m_IsShowSlope = m_IsViewSlope.Checked;
            Main_TempChart.Instance.CreateSlopeChart();
        }
        public void m_IsViewSlope_Checke(bool check)
        {
            m_IsViewSlope.Checked = check;
        }

        private void m_fAxisX_TextChanged(object sender, EventArgs e)
        {
            if (m_fAxisXMin.Value > m_fAxisXMax.Value)
            {
                return;
            }

            var Diagram = Main_TempChart.Instance.GetChart().Diagram as XYDiagram;
            if (Diagram == null) return;

            // Define the whole range for the axes. 
            Diagram.AxisX.WholeRange.SetMinMaxValues(m_fAxisXMin.Value, m_fAxisXMax.Value);

            // Define the visible range for the axes.   
            Diagram.AxisX.VisualRange.SetMinMaxValues(m_fAxisXMin.Value, m_fAxisXMax.Value);

            Diagram.AxisX.MinorCount = (int)m_fAxisXMinor.Value;
        }

        private void m_fAxisY_TextChanged(object sender, EventArgs e)
        {
            if (m_fAxisYMin.Value > m_fAxisYMax.Value)
            {
                return;
            }

            var Diagram = Main_TempChart.Instance.GetChart().Diagram as XYDiagram;
            if (Diagram == null) return;

            // Define the whole range for the axes. 
            Diagram.AxisY.WholeRange.SetMinMaxValues(m_fAxisYMin.Value, m_fAxisYMax.Value);

            // Define the visible range for the axes.   
            Diagram.AxisY.VisualRange.SetMinMaxValues(m_fAxisYMin.Value, m_fAxisYMax.Value);

            Diagram.AxisY.MinorCount = (int)m_fAxisYMinor.Value;
        }

        private void m_fAxisMinor_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (atoi(e.NewValue.ToString()) < 1 || atoi(e.NewValue.ToString()) > 10)
            { 
                e.Cancel = true;
            }
        }

        private void m_nNum_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (atoi(e.NewValue.ToString()) < 0 || atoi(e.NewValue.ToString()) > 4)
            {
                e.Cancel = true;
            }
        }

        private void m_nNum_TextChanged(object sender, EventArgs e)
        {
            CETCManagerApp.Instance.m_pETETCStage.m_nHAxisNum = atoi(m_nHNum.Value.ToString());
            CETCManagerApp.Instance.m_pETETCStage.m_nVAxisNum = atoi(m_nVNum.Value.ToString());
            Main_TempChart.Instance.CreateHVChart();

            Main_DataAnasis.Instance.m_pageHV.RefreshHVData();
            Main_DataAnasis.Instance.RefreshData();
        }

        private void m_bHVSetup_Click(object sender, EventArgs e)
        {
            IDD_OPTION_HV dlg = new IDD_OPTION_HV();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                m_nHNum.TextChanged -= m_nNum_TextChanged;
                m_nVNum.TextChanged -= m_nNum_TextChanged;
                m_nHNum.Value = CETCManagerApp.Instance.m_pETETCStage.m_nHAxisNum;
                m_nVNum.Value = CETCManagerApp.Instance.m_pETETCStage.m_nVAxisNum;
                Main_TempChart.Instance.CreateHVChart();
                m_nHNum.TextChanged += m_nNum_TextChanged;
                m_nVNum.TextChanged += m_nNum_TextChanged;
            }
        }

        private void SamperateView_CheckedChanged(object sender, EventArgs e)
        {
            var btn = (sender as CheckButton);
            if (btn.Checked)
            {
                var s = "";
                AfxExtractSubString(ref s, btn.Name, 1, "SamperateView");

                Main_TempChart.Instance.m_nShowSampleRate = atoi(s);
                Main_TempChart.Instance.CreateAnalyseChart();
            }
        }


        //递归加载文件夹目录及目录下文件
        private void GetDataList(string path, TreeListNodes nodes)
        {
            
            foreach (string item in Directory.GetDirectories(path))
            {
                DirectoryInfo dir = new DirectoryInfo(Path.GetFullPath(item));

                if (dir != null)
                {
                    FileSystemInfo[] files = dir.GetFileSystemInfos();
                    if (files.Length > 0)
                    {
                        TreeListNode node = nodes.Add(Path.GetFileName(item));
                        node.SetValue(0, Path.GetFileName(item));
                        node.ImageIndex = -1;
                        node.SelectImageIndex = -1;
                        node.StateImageIndex = 2;
                        node.Tag = new SuperTag() { FileFullPath = Path.GetFullPath(item) };
                        GetDataList(item, node.Nodes);
                    }
                }
                
            }
            string[] strFiles = Directory.GetFiles(path);
            foreach (string str in strFiles)
            {
                var n = nodes.Add(Path.GetFileName(str));
                n.SetValue(0, Path.GetFileName(str));
                n.ImageIndex = -1;
                n.SelectImageIndex = -1;
                n.StateImageIndex = 3;
                n.Tag = new SuperTag() { FileFullPath = Path.GetFullPath(str) };
            }
        }

        // m_DataList.ActiveFilterString = $"[Column1] Like ('%{m_strFileNameFilter.Text}%')";
        private void m_strFileNameFilter_TextChanged(object sender, EventArgs e)
        {
            m_DataList.ActiveFilterString = $"[Column1] Like ('%{m_strFileNameFilter.Text}%')";
        }

        private void m_DataList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {

        }

        private void m_DataList_DoubleClick(object sender, EventArgs e)
        {
            TreeList tree = sender as TreeList;
            TreeListHitInfo hi = tree.CalcHitInfo(tree.PointToClient(Control.MousePosition));
            if (hi.Node != null)
            {
                FileInfo myInfo = new FileInfo(((SuperTag)hi.Node.Tag).FileFullPath);
                //判斷檔案是否存在
                if (myInfo.Exists)
                {
                    if (new BoxConfirm("确定要载入档案？", "询问").ShowDialog() == DialogResult.OK)
                    {
                        SplashScreenManager.ShowForm(typeof(WaitForm1));
                        CETCManagerApp.Instance.OnFileOpen(((SuperTag)hi.Node.Tag).FileFullPath);
                        SplashScreenManager.CloseForm();
                    }
                }
            }
        }

        private void m_DataList_Click(object sender, EventArgs e)
        {
            
        }

        private void m_strFileNameFilter_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            m_strFileNameFilter.Text = "";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            m_DataList.ClearNodes();
            if (Directory.Exists(@".\Data"))
            {
                m_DataList.BeginUpdate();
                GetDataList(@".\Data", m_DataList.Nodes);
                m_DataList.EndUpdate();
            }
            else
            {
                Directory.CreateDirectory(@".\Data");
            }
        }
    }
}
