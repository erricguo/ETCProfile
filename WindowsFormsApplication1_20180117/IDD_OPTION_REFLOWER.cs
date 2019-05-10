using System;
using System.ComponentModel;
using System.Data;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using ETCProfiler.classes;
using static ETCProfiler.classes.Extension;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using static ETCProfiler.Enums.Statu;

namespace ETCProfiler
{
    public partial class IDD_OPTION_REFLOWER : IDD_BaseForm
    {
        DataTable dtHot = new DataTable();
        DataTable dtCool = new DataTable();
        RepositoryItemTextEdit rite = new RepositoryItemTextEdit();

        readonly string[] ColumnFixedName_CHS = new string[7] { "上层温度[℃]", "下层温度[℃]", "上层风速[HZ]", "下层风速[HZ]", "温区宽度", "上层预测温度[℃]", "下层预测温度[℃]" };

        public IDD_OPTION_REFLOWER()
        {
            InitializeComponent();

            m_strReflowModel.Text = "";
            m_nHeaterCount.Text = "0";
            m_strReflowerProduct.Text = "";
            m_nReflowerSpeed.Text = "0";
            m_strReflowerSpeedUnit.Text = "";
            //m_strTempUnit.Text = "";
            m_strReflowerType.Text = "";
            //m_strLengthUnit.Text = "";


            m_nCoolCount.Text = "0";
            m_strReflowerNote.Text = "";


            m_nHeaterCount.Properties.MaxValue = 12;
            m_nHeaterCount.Properties.MinValue = 0;
            m_nCoolCount.Properties.MaxValue = 12;
            m_nCoolCount.Properties.MinValue = 0;
            m_nReflowerSpeed.Properties.MaxValue = 5000;
            m_nReflowerSpeed.Properties.MinValue = 0;

            m_nHeaterCount.Properties.IsFloatValue = false;
            m_nCoolCount.Properties.IsFloatValue = false;
            //m_nReflowerSpeed.Properties.IsFloatValue = false;

            m_strReflowerSpeedUnit.Properties.Items.Clear();
            m_strReflowerSpeedUnit.Properties.Items.AddRange(new string[] { "cm/min", "m/min", "mm/min" });

            GV_ReflowHot.OptionsBehavior.ReadOnly = false;
            GV_ReflowCool.OptionsBehavior.ReadOnly = false;
            GV_ReflowHot.OptionsBehavior.EditorShowMode = EditorShowMode.MouseUp;
            GV_ReflowCool.OptionsBehavior.EditorShowMode = EditorShowMode.MouseUp;

            rite.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            rite.Mask.EditMask = "n1";

            btnSave.Visible = false;
        }

        private void IDD_OPTION_REFLOWER_Load(object sender, EventArgs e)
        {
            
            //新增COLUMN
            InitTable(dtHot,"Hot", ColumnTitle.HOT_CHS);
            InitTable(dtCool,"Cool", ColumnTitle.COOL_CHS);
            //新增ROW
            RefreshReflowerList(GC_ReflowHot, GV_ReflowHot, dtHot, (int)m_nHeaterCount.Value , "Hot");
            RefreshReflowerList(GC_ReflowCool,GV_ReflowCool, dtCool, (int)m_nCoolCount.Value, "Cool");

            RefreshReflower();
            //CreateReflowerList();

        }

        private void InitTable(DataTable dt,string Type, ColumnTitle Title)
        {
            
            for (int col = 0; col < 13; col++)
            {
                if (col == 0)
                {
                    var column = dt.Columns.Add($"Reflow{Type}{col}",typeof(string));
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

        private void RefreshReflowerList(GridControl gc,GridView gv, DataTable dt ,int VisibleCount,string type)
        {
            gc.BeginUpdate();
            var pReflowerAreaData = CETCManagerApp.Instance.m_pETETCStage.m_ETReflower.m_CAreaData[type];
            for (int col = 1; col < 13; col++)
            {
                dt.Rows[0][col] = pReflowerAreaData[col-1].m_fAreaTemplTop;
                dt.Rows[1][col] = pReflowerAreaData[col-1].m_fAreaTemplButtom;
                dt.Rows[2][col] = pReflowerAreaData[col-1].m_fAreaFanSpeedTop;
                dt.Rows[3][col] = pReflowerAreaData[col-1].m_fAreaFanSpeedButtom;
                dt.Rows[4][col] = pReflowerAreaData[col-1].m_fAreaLength;
                dt.Rows[5][col] = pReflowerAreaData[col-1].m_fAreaForecastButtom;
                dt.Rows[6][col] = pReflowerAreaData[col-1].m_fAreaForecastTop;
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
                    column.ColumnEdit = rite;
                    column.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
                    column.DisplayFormat.FormatType = FormatType.Numeric;
                    column.DisplayFormat.FormatString = "n1";
                    column.Width = 50;


                }
                if (col > VisibleCount)
                {
                    column.Visible = false;
                }
            }

            gc.EndUpdate();
            /* var theApp = CETCManagerApp.Instance;
             m_ReflowerList.DeleteAllItems();
             CppSQLite3Table t = theApp.m_ETCDB.getTable("select * from ETCReflower;");
             for (int row = 0; row < t.numRows(); row++)
             {
                 t.setRow(row);

                 CString str = t.getStringField(1);
                 ConvertUtf8ToGBK(str);

                 m_ReflowerList.InsertItem(row, str);
                 m_ReflowerList.SetItemData(row, (DWORD)t.getIntField("ID"));
             }

             int nID = m_ReflowerList.GetItemData(0);
             theApp.m_pETETCStage.m_ETReflower.ReadFromDB(nID);
             if (m_ReflowerList.GetItemCount() > 0)
             {
                 m_ReflowerList.SetItemState(0, LVIS_SELECTED, LVIS_SELECTED);

                 RefreshReflower();
             }*/
        }

        private void CreateReflowerList()
        {
            /*gc.DataSource = null;
            gv.Columns.Clear();


            for (int i = 0; i < 13; i++)
            {
                var column = gv.Columns.Add();
                column.FieldName = $"ReflowHot{i}";
                if (i == 0)
                {
                    column.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    column.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    column.UnboundType = DevExpress.Data.UnboundColumnType.String;
                    column.OptionsColumn.ReadOnly = true;
                    column.Caption = Title;
                }
                else
                {
                    column.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
                    column.Caption = $"{i}";
                }
            }*/
        }

        private void RefreshReflower()
        {
            var theApp = CETCManagerApp.Instance;
            CETReflower pReflower = theApp.m_pETETCStage.m_ETReflower;
            m_strReflowerType.Text = pReflower.m_strTitle;
            //m_strTempUnit.Text = pReflower.m_strTemplUnit;
            //m_strLengthUnit.Text = pReflower.m_strLengthUnit;
            m_strReflowerSpeedUnit.Text = pReflower.m_strSpeedUnit;

            if (pReflower.m_strSpeedUnit.IndexOf("cm/min") >= 0)
            {
                m_nReflowerSpeed.Value = pReflower.m_fSpeed.ToDecimal();
            }
            else if (pReflower.m_strSpeedUnit.IndexOf("m/min") == 0)
            {
                m_nReflowerSpeed.Value = (pReflower.m_fSpeed / 10.0).ToDecimal();
            }
            else
            {
                m_nReflowerSpeed.Value = (pReflower.m_fSpeed * 10.0).ToDecimal();
            }

            m_nHeaterCount.Text = pReflower.m_nSampleHeaterAreaCount.ToString();
            m_nCoolCount.Text = pReflower.m_nSampleCoolAreaCount.ToString();
            m_strReflowerProduct.Text = pReflower.m_strProduct;
            m_strReflowModel.Text = pReflower.m_strModel;

            /* m_IsWidthSmall.CheckedChanged -= m_IsWidthSmall_CheckedChanged;
             m_IsSpeedSmall.CheckedChanged -= m_IsSpeedSmall_CheckedChanged;
             m_IsTemplSmall.CheckedChanged -= m_IsTemplSmall_CheckedChanged;*/

            
            m_IsWidthSmall.Checked = pReflower.m_IsWidthSmall;
            m_IsSpeedSmall.Checked = pReflower.m_IsSpeedSmall;
            m_IsTemplSmall.Checked = pReflower.m_IsTempSmall;
            m_strReflowerNote.Text = pReflower.m_strNotes;

          /*  m_IsWidthSmall.CheckedChanged += m_IsWidthSmall_CheckedChanged;
            m_IsSpeedSmall.CheckedChanged += m_IsSpeedSmall_CheckedChanged;
            m_IsTemplSmall.CheckedChanged += m_IsTemplSmall_CheckedChanged;*/
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                CETReflower pReflower = CETCManagerApp.Instance.m_pETETCStage.m_ETReflower;

                pReflower.m_strTitle = m_strReflowerType.Text;
                pReflower.m_strSpeedUnit = m_strReflowerSpeedUnit.Text;
                //pReflower.m_fSpeed = m_nReflowerSpeed.Value.Tofloat();

                if (pReflower.m_strSpeedUnit.IndexOf("cm/min") >= 0)
                {
                    pReflower.m_fSpeed = m_nReflowerSpeed.Value.Tofloat();
                }
                else if (pReflower.m_strSpeedUnit.IndexOf("m/min") == 0)
                {
                    pReflower.m_fSpeed = (m_nReflowerSpeed.Value * 10).Tofloat();
                }
                else
                {
                    pReflower.m_fSpeed = (m_nReflowerSpeed.Value / 10).Tofloat();
                }

                pReflower.m_nSampleHeaterAreaCount = m_nHeaterCount.Text.ToInt();
                pReflower.m_nSampleCoolAreaCount = m_nCoolCount.Text.ToInt();
                pReflower.m_strProduct = m_strReflowerProduct.Text;
                pReflower.m_strModel = m_strReflowModel.Text;

                pReflower.m_IsWidthSmall = m_IsWidthSmall.Checked;
                pReflower.m_IsSpeedSmall = m_IsSpeedSmall.Checked;
                pReflower.m_IsTempSmall = m_IsTemplSmall.Checked;
                pReflower.m_strNotes = m_strReflowerNote.Text;

                SaveTableToApp(GC_ReflowHot,"Hot");
                SaveTableToApp(GC_ReflowCool,"Cool");

                Main_TempChart.Instance.CreateReflowerChart();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        private void SaveTableToApp(GridControl gc,string type)
        {
            var pReflowerAreaData = CETCManagerApp.Instance.m_pETETCStage.m_ETReflower.m_CAreaData[type];
            DataTable dt = (DataTable)gc.DataSource;
            for (int col = 1; col < 13; col++)
            {         
                pReflowerAreaData[col-1].m_fAreaTemplTop = dt.Rows[0][col].ToString().Tofloat();
                pReflowerAreaData[col-1].m_fAreaTemplButtom = dt.Rows[1][col].ToString().Tofloat();
                pReflowerAreaData[col-1].m_fAreaFanSpeedTop = dt.Rows[2][col].ToString().Tofloat();
                pReflowerAreaData[col-1].m_fAreaFanSpeedButtom = dt.Rows[3][col].ToString().Tofloat();
                pReflowerAreaData[col-1].m_fAreaLength = dt.Rows[4][col].ToString().Tofloat();
                pReflowerAreaData[col-1].m_fAreaForecastButtom = dt.Rows[5][col].ToString().Tofloat();
                pReflowerAreaData[col-1].m_fAreaForecastTop = dt.Rows[6][col].ToString().Tofloat();
            }
        }

        private void m_IsWidthSmall_CheckedChanged(object sender, EventArgs e)
        {
            WidthConsistent(!m_IsWidthSmall.Checked, 4, GV_ReflowHot);
        }

        private void m_IsTemplSmall_CheckedChanged(object sender, EventArgs e)
        {
            TempConsistent(!m_IsTemplSmall.Checked, 0, 1);
        }

        private void m_IsSpeedSmall_CheckedChanged(object sender, EventArgs e)
        {
            SpeedConsistent(!m_IsSpeedSmall.Checked, 2, 3);
        }

        private void GV_Reflow_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var gv = (sender as GridView);

            //上下溫度
            if (e.RowHandle == 0 )
            {
                TempConsistent(!m_IsTemplSmall.Checked, 0, 1);
            }
            //上下溫度
            else if (e.RowHandle == 1)
            {
                TempConsistent(!m_IsTemplSmall.Checked, 1, 0);
            }
            //上下風速
            else if (e.RowHandle == 2)
            {
                SpeedConsistent(!m_IsSpeedSmall.Checked, 2, 3);
            }
            //上下風速
            else if (e.RowHandle == 3)
            {
                SpeedConsistent(!m_IsSpeedSmall.Checked, 3, 2);
            }
            //寬度
            else if (e.RowHandle == 4)
            {
                WidthConsistent(!m_IsWidthSmall.Checked,e.RowHandle, GV_ReflowHot);
            }
        }

        private void WidthConsistent(bool reset,int row , GridView gv)
        {
            
            GV_ReflowHot.CellValueChanged -= GV_Reflow_CellValueChanged;
            GV_ReflowCool.CellValueChanged -= GV_Reflow_CellValueChanged;
            if (reset)
            {
                var pReflowerAreaData = CETCManagerApp.Instance.m_pETETCStage.m_ETReflower.m_CAreaData["Hot"];
                for (int col = 1; col < 13; col++)
                {
                    GV_ReflowHot.SetRowCellValue(row, GV_ReflowHot.Columns[col], pReflowerAreaData[col - 1].m_fAreaLength);
                }

                pReflowerAreaData = CETCManagerApp.Instance.m_pETETCStage.m_ETReflower.m_CAreaData["Cool"];
                for (int col = 1; col < 13; col++)
                {
                    GV_ReflowCool.SetRowCellValue(row, GV_ReflowCool.Columns[col], pReflowerAreaData[col - 1].m_fAreaLength);
                }
            }
            else
            {
                var mValue = gv.GetRowCellValue(row, gv.Columns[1]).ToDecimal();
                for (int col = 1; col <= m_nHeaterCount.Value; col++)
                {
                    GV_ReflowHot.SetRowCellValue(row, GV_ReflowHot.Columns[col], mValue);
                }

                //mValue = GV_ReflowCool.GetRowCellValue(row, GV_ReflowCool.Columns[1]).ToDecimal();
                for (int col = 1; col <= m_nCoolCount.Value; col++)
                {
                    GV_ReflowCool.SetRowCellValue(row, GV_ReflowCool.Columns[col], mValue);
                }
            }
            GV_ReflowHot.CellValueChanged += GV_Reflow_CellValueChanged;
            GV_ReflowCool.CellValueChanged += GV_Reflow_CellValueChanged;
        }

        private void SpeedConsistent(bool reset, int row1, int row2)
        {
            GV_ReflowHot.CellValueChanged -= GV_Reflow_CellValueChanged;
            GV_ReflowCool.CellValueChanged -= GV_Reflow_CellValueChanged;
            if (reset)
            {
                var pReflowerAreaData = CETCManagerApp.Instance.m_pETETCStage.m_ETReflower.m_CAreaData["Hot"];
                for (int col = 1; col <= m_nHeaterCount.Value; col++)
                {
                    GV_ReflowHot.SetRowCellValue(2, GV_ReflowHot.Columns[col], pReflowerAreaData[col - 1].m_fAreaFanSpeedTop);
                    GV_ReflowHot.SetRowCellValue(3, GV_ReflowHot.Columns[col], pReflowerAreaData[col - 1].m_fAreaFanSpeedButtom);
                }

                pReflowerAreaData = CETCManagerApp.Instance.m_pETETCStage.m_ETReflower.m_CAreaData["Cool"];
                for (int col = 1; col <= m_nCoolCount.Value; col++)
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
        }

        private void TempConsistent(bool reset, int row1, int row2)
        {
            GV_ReflowHot.CellValueChanged -= GV_Reflow_CellValueChanged;
            GV_ReflowCool.CellValueChanged -= GV_Reflow_CellValueChanged;
            if (reset)
            {
                var pReflowerAreaData = CETCManagerApp.Instance.m_pETETCStage.m_ETReflower.m_CAreaData["Hot"];
                for (int col = 1; col <= m_nHeaterCount.Value; col++)
                {
                    GV_ReflowHot.SetRowCellValue(0, GV_ReflowHot.Columns[col], pReflowerAreaData[col - 1].m_fAreaTemplTop);
                    GV_ReflowHot.SetRowCellValue(1, GV_ReflowHot.Columns[col], pReflowerAreaData[col - 1].m_fAreaTemplButtom);
                }    

                pReflowerAreaData = CETCManagerApp.Instance.m_pETETCStage.m_ETReflower.m_CAreaData["Cool"];
                for (int col = 1; col <= m_nCoolCount.Value; col++)
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
        }

        private void m_nReflowerSpeed_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void m_strReflowerSpeedUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            CETReflower pReflower = CETCManagerApp.Instance.m_pETETCStage.m_ETReflower;
            if (m_strReflowerSpeedUnit.Text == "cm/min")
            {
                m_nReflowerSpeed.Value = pReflower.m_fSpeed.ToDecimal();
            }
            else if(m_strReflowerSpeedUnit.Text == "m/min")
            {
                m_nReflowerSpeed.Value = (pReflower.m_fSpeed / 10).ToDecimal();
            }
            else if (m_strReflowerSpeedUnit.Text == "mm/min")
            {
                m_nReflowerSpeed.Value = (pReflower.m_fSpeed * 10).ToDecimal();
            }
        }

        private void m_nHeaterCount_TextChanged(object sender, EventArgs e)
        {
            if (GV_ReflowHot.Columns.Count > 0)
            {
                for (int col = 12; col >= 1; col--)
                {
                    GV_ReflowHot.Columns[col].Visible = (col <= m_nHeaterCount.Value);
                   
                    if (GV_ReflowHot.Columns[col].Visible)
                    {
                        GV_ReflowHot.Columns[col].VisibleIndex = col;
                    }
                }
                GV_ReflowHot.FocusedColumn = (m_nHeaterCount.Value > 0) ? GV_ReflowHot.Columns[m_nHeaterCount.Value.ToInt()] : null;
                TempConsistent(!m_IsTemplSmall.Checked, 0, 1);
                WidthConsistent(!m_IsWidthSmall.Checked, 4 , GV_ReflowHot);
            }
        }

        private void m_nCoolCount_TextChanged(object sender, EventArgs e)
        {
            if (GV_ReflowCool.Columns.Count > 0)
            {
                for (int col = 12; col >= 1; col--)
                {
                    GV_ReflowCool.Columns[col].Visible = (col <= m_nCoolCount.Value);

                    if (GV_ReflowCool.Columns[col].Visible)
                    {
                        GV_ReflowCool.Columns[col].VisibleIndex = col;
                    }
                }
                GV_ReflowCool.FocusedColumn = (m_nCoolCount.Value > 0) ? GV_ReflowCool.Columns[m_nCoolCount.Value.ToInt()] : null;
                TempConsistent(!m_IsTemplSmall.Checked, 0, 1);
                WidthConsistent(!m_IsWidthSmall.Checked, 4, GV_ReflowCool);
            }
        }
    }
}
