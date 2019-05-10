using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using WeifenLuo.WinFormsUI.Docking;
using ETCProfiler.classes;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using static ETCProfiler.Enums.Statu;
using static ETCProfiler.classes.SuperTag;

namespace ETCProfiler
{


    public partial class Main_DataAnasis : XtraForm
    {
        public enum PageEnum
        {
            Analysis,
            Data,
            HV
        }
        //Reflow Temp------------------------------
        DataTable dtHot = new DataTable();
        DataTable dtCool = new DataTable();
        readonly string[] ColumnFixedName_CHS = new string[7] { "上层温度[℃]", "下层温度[℃]", "上层风速[HZ]", "下层风速[HZ]", "温区宽度", "上层预测温度[℃]", "下层预测温度[℃]" };
        //-----------------------------------------

        public CETPageAnaly m_pageAnaly { get; set; } = new CETPageAnaly();
        public CETWZPageProduct m_pageProduct { get; set; } = new CETWZPageProduct();
        public CETPageData m_pageData { get; set; } = new CETPageData();
        public CETPageHV m_pageHV { get; set; } = new CETPageHV();
        //public CETPageTemplSetup m_pageTemplSetup { get; set; } = new CETPageTemplSetup();
        public Dictionary<int,DataGridViewCell> CanEditCellRowList = new Dictionary<int, DataGridViewCell>();
        RepositoryItemTextEdit rite = new RepositoryItemTextEdit();
        RepositoryItemTextEdit riteNum_n1 = new RepositoryItemTextEdit();
        RepositoryItemTextEdit riteNum_n2 = new RepositoryItemTextEdit();
        public int GC_ReflowHot_Width { get; set; }

        private static Main_DataAnasis m_Instance;
        public static Main_DataAnasis Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new Main_DataAnasis();
                }
                return m_Instance;
            }
        }

        public Main_DataAnasis()
        {
            InitializeComponent();
            if (m_Instance == null)
                m_Instance = this;

            //GV_PageData1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            rite.ReadOnly = true;

            riteNum_n1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            riteNum_n1.Mask.EditMask = "n1";

            riteNum_n2.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            riteNum_n2.Mask.EditMask = "n2";


            GV_ReflowHot.OptionsBehavior.ReadOnly = false;
            GV_ReflowCool.OptionsBehavior.ReadOnly = false;
            GV_ReflowHot.OptionsBehavior.EditorShowMode = EditorShowMode.MouseUp;
            GV_ReflowCool.OptionsBehavior.EditorShowMode = EditorShowMode.MouseUp;

            GV_PageAnaly.OptionsView.ColumnHeaderAutoHeight = DefaultBoolean.True;
        }

        public GridControl GetGridControl(int page)
        {
            if (page == 0)
            {
                return GC_PageAnaly;
            }
            else if (page == 4)
            {
                return GC_PageData;
            }
            return null;
        }
        
        ///#region 为了保证在关闭某一浮动窗体之后，再打开时能够在原位置显示，要对浮动窗体处理，处理窗体的DockstateChanged事件，标签窗体dock位置改变，记录到公共类
        
        /*public void Main_DataAnasis_DockStateChanged(object sender, EventArgs e)
        {
            //关闭时（dockstate为unknown） 不把dockstate保存
            if (Instance != null)
            {
                if (this.DockState == DockState.Unknown || this.DockState == DockState.Hidden)
                {
                    return;
                }
                AppConfig.dockState = this.DockState;
            }
        }*/
        private void Main_DataAnasis_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            m_Instance = null;
        }

        private void Main_DataAnasis_Load(object sender, EventArgs e)
        {
            btnReflowerResult.Tag = new SuperTag() { AnalysisBtnType = AnalysisBtn.ReflowerResult };
            btnMaxMinReport.Tag = new SuperTag() { AnalysisBtnType = AnalysisBtn.MaxMinReport };
            btnTimeInTemplReport.Tag = new SuperTag() { AnalysisBtnType = AnalysisBtn.TimeInTemplReport };
            btnTemplUpDownReport.Tag = new SuperTag() { AnalysisBtnType = AnalysisBtn.TemplUpDownReport };
        }

        
        public void SetPage(int index)
        {
            TabControl.SelectedTabPageIndex = index;
        }

        public void SetGridWidth()
        {
            if(((SuperTag)GC_ReflowHot.Tag)?.GridControlWidth > 0 )
            {
                GC_ReflowHot.Width = ((SuperTag)GC_ReflowHot.Tag).GridControlWidth;
            }
        }

        public void RefreshData()
        {
            //GV_PageData.Assign(m_pageData.GV_PageData,false);
            //GV_PageHV.Assign(m_pageHV.GV_PageHV, false);

            //var gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            
            Clone(m_pageAnaly.GV_PageAnaly, GV_PageAnaly, GC_PageAnaly, 0, false, PageEnum.Analysis);
            Clone(m_pageData.GV_PageData,GV_PageData,GC_PageData,-1,false, PageEnum.Data);
            Clone(m_pageHV.GV_PageHV,GV_PageHV,GC_PageHV,0,true, PageEnum.HV);
            RefreshReflowData();
            SetGridWidth();
        }
        public void RefreshReflowData()
        {
            
            //新增COLUMN
            InitTable(dtHot, "Hot", ColumnTitle.HOT_CHS);
            InitTable(dtCool, "Cool", ColumnTitle.COOL_CHS);
            //新增ROW
            CETReflower pReflower = CETCManagerApp.Instance.m_pETETCStage.m_ETReflower;
            RefreshReflowerList(GC_ReflowHot, GV_ReflowHot, dtHot, pReflower.m_nSampleHeaterAreaCount, "Hot");
            RefreshReflowerList(GC_ReflowCool, GV_ReflowCool, dtCool, pReflower.m_nSampleCoolAreaCount, "Cool");

            m_nReflowStartWidth.Value = pReflower.m_fInitTempl.ToDecimal();
            m_nReflowerSpeed.Value = pReflower.m_fSpeed.ToDecimal();

            //GC_ReflowHot.Width = 530;
            TempConsistent(!pReflower.m_IsTempSmall, 0, 1);
            SpeedConsistent(!pReflower.m_IsSpeedSmall, 2, 3);
            WidthConsistent(!pReflower.m_IsWidthSmall, 4, GV_ReflowHot);
        }


        public void Clone(DataGridView gv1,GridView gv2,GridControl gc2,int fixedColumn,bool checkReadOnly, PageEnum pEnum)
        {
            try
            {
                int SumWidth = 0;
                var rowidx = gv2.FocusedRowHandle;
                gc2.BeginUpdate();
                gv2.Columns.Clear();
                gc2.DataSource = GetDataGridViewAsDataTable(gv1, checkReadOnly);
                for (int i = 0; i < gv1.Columns.Count; i++)
                {
                    var column = gv2.Columns[i];
                    if (fixedColumn != -1 && fixedColumn == i)
                    {
                        column.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    }
                    column.Caption = gv1.Columns[i].HeaderText;
                    column.Name = gv1.Columns[i].Name;
                    column.FieldName = gv1.Columns[i].Name;
                    column.Visible = true;
                    column.VisibleIndex = i;
                    column.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    column.AppearanceCell.TextOptions.HAlignment = ((DataGridViewTextBoxColumnEX)gv1.Columns[i]).CellAlignment;
                    column.UnboundType = ((DataGridViewTextBoxColumnEX)gv1.Columns[i]).ColumnType;
                    column.Width = gv1.Columns[i].Width;
                    SumWidth += column.Width;
                    column.OptionsColumn.ReadOnly = gv1.Columns[i].ReadOnly;
                    column.AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
                    

                    if (column.ColumnType == typeof(double) ||
                        column.ColumnType == typeof(decimal))
                    {
                        column.DisplayFormat.FormatType = FormatType.Numeric;
                        column.DisplayFormat.FormatString = $"n{((DataGridViewTextBoxColumnEX)gv1.Columns[i]).Round_n}";                       
                    }
                }

                gv2.FocusedRowHandle = rowidx;

                switch (pEnum)
                {
                    case PageEnum.Analysis:
                        gv2.BestFitColumns();
                        break;
                    case PageEnum.Data:
                        break;
                    case PageEnum.HV:
                        break;
                    default:
                        break;
                }
                gc2.EndUpdate();
            }
            catch(Exception e)
            {
                new BoxMsg(e.Message).ShowDialog();
            }
            /*
            for (int i = 0; i < gv1.RowCount; i++)
            {
               
                var row = (DataGridViewRow)gv1.Rows[i].Clone();
                int intColIndex = 0;
                foreach (DataGridViewCell cell in gv1.Rows[i].Cells)
                {
                    row.Cells[intColIndex].Value = cell.Value;
                    row.Cells[intColIndex].Tag = cell.Tag;
                    intColIndex++;
                }
                gv2.Rows.Add(row);
            }*/

            

//            GV_PageData.Columns.Add();
            /*gv2.Columns.Clear();
            for (int i = 0; i < gv1.Columns.Count; i++)
            {
                gv2.Columns.Add(gv1.Columns[i]);
            }*/

            /*for (int i = 0; i < gv1.RowCount; i++)
            {
                var row = (DataGridViewRow)gv1.Rows[i].Clone();
                int intColIndex = 0;
                foreach (DataGridViewCell cell in gv1.Rows[i].Cells)
                {
                    row.Cells[intColIndex].Value = cell.Value;
                    row.Cells[intColIndex].Tag = cell.Tag;
                    intColIndex++;
                }
                gv2.Rows.Add(row);
            }*/
        }

        private DataTable GetDataGridViewAsDataTable(DataGridView _DataGridView,bool checkReadOnly)
        {
            try
            {
                if (_DataGridView.ColumnCount == 0) return null;
                DataTable dtSource = new DataTable();
                //////create columns
                foreach (DataGridViewColumn col in _DataGridView.Columns)
                {
                    if (col.ValueType == null) dtSource.Columns.Add(col.Name, typeof(string));
                    else dtSource.Columns.Add(col.Name, col.ValueType);
                    dtSource.Columns[col.Name].Caption = col.HeaderText;
                    dtSource.Columns[col.Name].ReadOnly = col.ReadOnly;                   
                }

                if (checkReadOnly) CanEditCellRowList.Clear();

                ///////insert row data                
                foreach (DataGridViewRow row in _DataGridView.Rows)
                {
                    DataRow drNewRow = dtSource.NewRow();
                    foreach (DataColumn col in dtSource.Columns)
                    {
                        if (row.Cells[col.ColumnName].Value != null)
                        {

                            drNewRow[col.ColumnName] = row.Cells[col.ColumnName].Value;
                            if (checkReadOnly && !row.Cells[col.ColumnName].ReadOnly && col.ColumnName == "column2")
                            {
                                if (!CanEditCellRowList.ContainsKey(row.Index))
                                {
                                    CanEditCellRowList.Add(row.Index, row.Cells[col.ColumnName]);
                                } 
                            }
                        }
                        
                    }
                    dtSource.Rows.Add(drNewRow);
                }
                return dtSource;
            }
            catch
            {
                return null;
            }
        }

        private void GV_PageHV_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GV_PageHV.CellValueChanged -= GV_PageHV_CellValueChanged;
            m_pageHV.CellDataChanged(((SuperTag)CanEditCellRowList[e.RowHandle].Tag), GV_PageHV.GetRowCellValue(e.RowHandle,e.Column));
            Main_TempChart.Instance.CreateHVChart();
            m_pageHV.RefreshHVData();
            RefreshData();
            GV_PageHV.CellValueChanged += GV_PageHV_CellValueChanged;
        }

        private void GV_PageHV_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "column2")
            {
                if (CanEditCellRowList.ContainsKey(e.RowHandle))
                {
                    e.Appearance.BackColor = CanEditCellRowList[e.RowHandle].Style.BackColor;
                }
            }
        }

        private void GV_PageHV_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "column2")
            {
                if (!CanEditCellRowList.ContainsKey(e.RowHandle))
                {
                    e.RepositoryItem = rite;
                }
            }
        }

        private void GV_PageAnaly_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
               e.Appearance.ForeColor = m_pageAnaly.GV_PageAnaly.Rows[e.RowHandle].Cells[e.Column.FieldName].Style.ForeColor;
               e.Appearance.BackColor = m_pageAnaly.GV_PageAnaly.Rows[e.RowHandle].Cells[e.Column.FieldName].Style.BackColor;
            }
            catch (Exception)
            {


            }
        }

        private void GV_PageAnaly_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            /*if (e.Column.FieldName == "column2")
            {
                e.RepositoryItem = riteNum_n1;
            }*/
            

        }

        //Reflow Temperature Setup ------------------------------------------------------------------------------------

        private void InitTable(DataTable dt, string Type, ColumnTitle Title)
        {
            if (dt.Columns.Count > 0 )
            {
                return;
            }

            for (int col = 0; col < 13; col++)
            {
                if (col == 0)
                {
                    var column = dt.Columns.Add($"Reflow{Type}{col}", typeof(string));
                    column.Caption = Title.GetEnumDescription();
                    column.DataType = typeof(string);
                }
                else
                {
                    var column = dt.Columns.Add($"Reflow{Type}{col}", typeof(decimal));
                    column.Caption = col.ToString();

                }
            }

            for (int row = 0; row < 7; row++)
            {
                var nowrow = dt.Rows.Add();
                nowrow[0] = ColumnFixedName_CHS[row];
            }
        }

        private void RefreshReflowerList(GridControl gc, GridView gv, DataTable dt, int VisibleCount, string type)
        {
            gc.BeginUpdate();
            int SumWidth = 20;
            var pReflowerAreaData = CETCManagerApp.Instance.m_pETETCStage.m_ETReflower.m_CAreaData[type];
            for (int col = 1; col < 13; col++)
            {
                dt.Rows[0][col] = pReflowerAreaData[col - 1].m_fAreaTemplTop;
                dt.Rows[1][col] = pReflowerAreaData[col - 1].m_fAreaTemplButtom;
                dt.Rows[2][col] = pReflowerAreaData[col - 1].m_fAreaFanSpeedTop;
                dt.Rows[3][col] = pReflowerAreaData[col - 1].m_fAreaFanSpeedButtom;
                dt.Rows[4][col] = pReflowerAreaData[col - 1].m_fAreaLength;
                dt.Rows[5][col] = pReflowerAreaData[col - 1].m_fAreaForecastButtom;
                dt.Rows[6][col] = pReflowerAreaData[col - 1].m_fAreaForecastTop;
            }

            gc.DataSource = dt;

            for (int col = 0; col < 13; col++)
            {
                var column = gv.Columns[col];
                if (col == 0)
                {
                    column.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    column.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    column.UnboundType = DevExpress.Data.UnboundColumnType.String;
                    column.OptionsColumn.ReadOnly = true;
                    column.Width = 110;

                }
                else
                {
                    column.ColumnEdit = riteNum_n1;
                    column.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
                    column.DisplayFormat.FormatType = FormatType.Numeric;
                    column.DisplayFormat.FormatString = "n1";
                    column.Width = 50;


                }

                if (col > VisibleCount)
                {
                    column.Visible = false;
                }
                else    
                    SumWidth += column.Width;
            }

            gc.Tag = new SuperTag() { GridControlWidth = SumWidth };
            gc.EndUpdate();
        }

        private void GV_Reflow_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var pReflower = CETCManagerApp.Instance.m_pETETCStage.m_ETReflower;

            //上下溫度
            if (e.RowHandle == 0)
            {
                TempConsistent(!pReflower.m_IsTempSmall, 0, 1);
            }
            //上下溫度
            else if (e.RowHandle == 1)
            {
                TempConsistent(!pReflower.m_IsTempSmall, 1, 0);
            }
            //上下風速
            else if (e.RowHandle == 2)
            {
                SpeedConsistent(!pReflower.m_IsSpeedSmall, 2, 3);
            }
            //上下風速
            else if (e.RowHandle == 3)
            {
                SpeedConsistent(!pReflower.m_IsSpeedSmall, 3, 2);
            }
            //寬度
            else if (e.RowHandle == 4)
            {
                WidthConsistent(!pReflower.m_IsWidthSmall, e.RowHandle, GV_ReflowHot);
            }

            SaveTableToApp(GC_ReflowHot, "Hot");
            SaveTableToApp(GC_ReflowCool, "Cool");

            Main_TempChart.Instance.CreateReflowerChart();
        }

        private void TempConsistent(bool reset, int row1, int row2)
        {
            GV_ReflowHot.BeginUpdate();
            GV_ReflowCool.BeginUpdate();
            GV_ReflowHot.CellValueChanged -= GV_Reflow_CellValueChanged;
            GV_ReflowCool.CellValueChanged -= GV_Reflow_CellValueChanged;
            if (reset)
            {
                var pReflower = CETCManagerApp.Instance.m_pETETCStage.m_ETReflower;
                var pReflowerAreaData = pReflower.m_CAreaData["Hot"];
                for (int col = 1; col <= pReflower.m_nSampleHeaterAreaCount; col++)
                {
                    GV_ReflowHot.SetRowCellValue(0, GV_ReflowHot.Columns[col], pReflowerAreaData[col - 1].m_fAreaTemplTop);
                    GV_ReflowHot.SetRowCellValue(1, GV_ReflowHot.Columns[col], pReflowerAreaData[col - 1].m_fAreaTemplButtom);
                }

                pReflowerAreaData = pReflower.m_CAreaData["Cool"];
                for (int col = 1; col <= pReflower.m_nSampleCoolAreaCount; col++)
                {
                    GV_ReflowCool.SetRowCellValue(0, GV_ReflowCool.Columns[col], pReflowerAreaData[col - 1].m_fAreaTemplTop);
                    GV_ReflowCool.SetRowCellValue(1, GV_ReflowCool.Columns[col], pReflowerAreaData[col - 1].m_fAreaTemplButtom);
                }
            }
            else
            {
                for (int col = 1; col < GV_ReflowHot.Columns.Count; col++)
                {
                    var mValue = GV_ReflowHot.GetRowCellValue(row1, GV_ReflowHot.Columns[col]).ToDecimal();
                    GV_ReflowHot.SetRowCellValue(row2, GV_ReflowHot.Columns[col], mValue);
                }

                for (int col = 1; col < GV_ReflowCool.Columns.Count; col++)
                {
                    var mValue = GV_ReflowCool.GetRowCellValue(row1, GV_ReflowCool.Columns[col]).ToDecimal();
                    GV_ReflowCool.SetRowCellValue(row2, GV_ReflowCool.Columns[col], mValue);
                }
            }
            GV_ReflowHot.CellValueChanged += GV_Reflow_CellValueChanged;
            GV_ReflowCool.CellValueChanged += GV_Reflow_CellValueChanged;
            GV_ReflowHot.EndUpdate();
            GV_ReflowCool.EndUpdate();
        }

        private void SpeedConsistent(bool reset, int row1, int row2)
        {
            GV_ReflowHot.BeginUpdate();
            GV_ReflowCool.BeginUpdate();
            GV_ReflowHot.CellValueChanged -= GV_Reflow_CellValueChanged;
            GV_ReflowCool.CellValueChanged -= GV_Reflow_CellValueChanged;
            if (reset)
            {
                var pReflower = CETCManagerApp.Instance.m_pETETCStage.m_ETReflower;
                var pReflowerAreaData = pReflower.m_CAreaData["Hot"];
                for (int col = 1; col <= pReflower.m_nSampleHeaterAreaCount; col++)
                {
                    GV_ReflowHot.SetRowCellValue(2, GV_ReflowHot.Columns[col], pReflowerAreaData[col - 1].m_fAreaFanSpeedTop);
                    GV_ReflowHot.SetRowCellValue(3, GV_ReflowHot.Columns[col], pReflowerAreaData[col - 1].m_fAreaFanSpeedButtom);
                }

                pReflowerAreaData = pReflower.m_CAreaData["Cool"];
                for (int col = 1; col <= pReflower.m_nSampleCoolAreaCount; col++)
                {
                    GV_ReflowCool.SetRowCellValue(2, GV_ReflowCool.Columns[col], pReflowerAreaData[col - 1].m_fAreaFanSpeedTop);
                    GV_ReflowCool.SetRowCellValue(3, GV_ReflowCool.Columns[col], pReflowerAreaData[col - 1].m_fAreaFanSpeedButtom);
                }
            }
            else
            {
                for (int col = 1; col < GV_ReflowHot.Columns.Count; col++)
                {
                    var mValue = GV_ReflowHot.GetRowCellValue(row1, GV_ReflowHot.Columns[col]).ToDecimal();
                    GV_ReflowHot.SetRowCellValue(row2, GV_ReflowHot.Columns[col], mValue);
                }

                for (int col = 1; col < GV_ReflowCool.Columns.Count; col++)
                {
                    var mValue = GV_ReflowCool.GetRowCellValue(row1, GV_ReflowCool.Columns[col]).ToDecimal();
                    GV_ReflowCool.SetRowCellValue(row2, GV_ReflowCool.Columns[col], mValue);
                }
            }
            GV_ReflowHot.CellValueChanged += GV_Reflow_CellValueChanged;
            GV_ReflowCool.CellValueChanged += GV_Reflow_CellValueChanged;
            GV_ReflowHot.EndUpdate();
            GV_ReflowCool.EndUpdate();
        }

        private void WidthConsistent(bool reset, int row, GridView gv)
        {
            GV_ReflowHot.BeginUpdate();
            GV_ReflowCool.BeginUpdate();
            GV_ReflowHot.CellValueChanged -= GV_Reflow_CellValueChanged;
            GV_ReflowCool.CellValueChanged -= GV_Reflow_CellValueChanged;
            var pReflower = CETCManagerApp.Instance.m_pETETCStage.m_ETReflower;
            if (reset)
            {
                var pReflowerAreaData = pReflower.m_CAreaData["Hot"];
                for (int col = 1; col < 13; col++)
                {
                    GV_ReflowHot.SetRowCellValue(row, GV_ReflowHot.Columns[col], pReflowerAreaData[col - 1].m_fAreaLength);
                }

                pReflowerAreaData = pReflower.m_CAreaData["Cool"];
                for (int col = 1; col < 13; col++)
                {
                    GV_ReflowCool.SetRowCellValue(row, GV_ReflowCool.Columns[col], pReflowerAreaData[col - 1].m_fAreaLength);
                }
            }
            else
            {
                var mValue = gv.GetRowCellValue(row, gv.Columns[1]).ToDecimal();
                for (int col = 1; col <= pReflower.m_nSampleHeaterAreaCount; col++)
                {
                    GV_ReflowHot.SetRowCellValue(row, GV_ReflowHot.Columns[col], mValue);
                }

                //mValue = GV_ReflowCool.GetRowCellValue(row, GV_ReflowCool.Columns[1]).ToDecimal();
                for (int col = 1; col <= pReflower.m_nSampleCoolAreaCount; col++)
                {
                    GV_ReflowCool.SetRowCellValue(row, GV_ReflowCool.Columns[col], mValue);
                }
            }
            GV_ReflowHot.CellValueChanged += GV_Reflow_CellValueChanged;
            GV_ReflowCool.CellValueChanged += GV_Reflow_CellValueChanged;
            GV_ReflowHot.EndUpdate();
            GV_ReflowCool.EndUpdate();
        }

        private void SaveTableToApp(GridControl gc, string type)
        {
            var pReflowerAreaData = CETCManagerApp.Instance.m_pETETCStage.m_ETReflower.m_CAreaData[type];
            DataTable dt = (DataTable)gc.DataSource;
            for (int col = 1; col < 13; col++)
            {
                pReflowerAreaData[col - 1].m_fAreaTemplTop = dt.Rows[0][col].ToString().Tofloat();
                pReflowerAreaData[col - 1].m_fAreaTemplButtom = dt.Rows[1][col].ToString().Tofloat();
                pReflowerAreaData[col - 1].m_fAreaFanSpeedTop = dt.Rows[2][col].ToString().Tofloat();
                pReflowerAreaData[col - 1].m_fAreaFanSpeedButtom = dt.Rows[3][col].ToString().Tofloat();
                pReflowerAreaData[col - 1].m_fAreaLength = dt.Rows[4][col].ToString().Tofloat();
                pReflowerAreaData[col - 1].m_fAreaForecastButtom = dt.Rows[5][col].ToString().Tofloat();
                pReflowerAreaData[col - 1].m_fAreaForecastTop = dt.Rows[6][col].ToString().Tofloat();
            }
        }

        private void m_nReflowStartWidth_TextChanged(object sender, EventArgs e)
        {
            CETReflower pReflower = CETCManagerApp.Instance.m_pETETCStage.m_ETReflower;
            pReflower.m_fInitTempl = m_nReflowStartWidth.Value.Tofloat();
            Main_TempChart.Instance.CreateReflowerChart();
        }

        private void m_nReflowerSpeed_TextChanged(object sender, EventArgs e)
        {
            CETReflower pReflower = CETCManagerApp.Instance.m_pETETCStage.m_ETReflower;
            pReflower.m_fSpeed = m_nReflowerSpeed.Value.Tofloat();
            Main_TempChart.Instance.CreateReflowerChart();
        }

        private void AnalysisBtn_CheckedChanged(object sender, EventArgs e)
        {
            var btn = (CheckButton)sender;
            if (btn.Checked)
            {
                var type = ((SuperTag)btn.Tag).AnalysisBtnType;
                if (type >= 0)
                {
                    m_pageAnaly.SwitchReportView(type);
                    m_pageAnaly.CreateReport(type);
                    Clone(m_pageAnaly.GV_PageAnaly, GV_PageAnaly, GC_PageAnaly, 0, false, PageEnum.Analysis);
                }                
            }
        }



        //---------------------------------------------------------------------------------


    }
}
