using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using ETCProfiler.classes;
using SQLite;

namespace ETCProfiler
{
    public partial class IDD_OPTION_ANALYSE_CONDITION : IDD_BaseForm
    {
        DataTable dt_AnalysisDefault = new DataTable();
        DataTable dt_Analysis = new DataTable();
        RepositoryItemTextEdit rite = new RepositoryItemTextEdit();
        RepositoryItemTextEdit riteReadonly = new RepositoryItemTextEdit();

        public IDD_OPTION_ANALYSE_CONDITION()
        {
            InitializeComponent();
            InitTable();

            rite.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            rite.Mask.EditMask = "n1";
            rite.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

            riteReadonly.ReadOnly = true;
            GV_PageAnalysis.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseUp;

            /*m_nTimeUnit.Properties.Items.Clear();
            m_nTimeUnit.Properties.Items.AddRange(new string[] { "时:分:秒", "秒" });
            m_nTimeUnit.SelectedIndex = 0;

            m_nTempUnit.Properties.Items.Clear();
            m_nTempUnit.Properties.Items.AddRange(new string[] { "摄氏温度" });
            m_nTempUnit.SelectedIndex = 0;*/

            LoadData();
        }



        private void InitTable()
        {
            //显示
            dt_AnalysisDefault.Columns.Add("m_nShowStatus", typeof(bool));
            //type
            dt_AnalysisDefault.Columns.Add("m_nConditionType", typeof(string));
            //caption
            dt_AnalysisDefault.Columns.Add("m_strCaption", typeof(string));
            //begin range
            dt_AnalysisDefault.Columns.Add("m_fBeginRangle", typeof(object));
            //end range
            dt_AnalysisDefault.Columns.Add("m_fEndRangle", typeof(object));
            //begin condition
            dt_AnalysisDefault.Columns.Add("m_fBeginCondition", typeof(object));
            //end condition
            dt_AnalysisDefault.Columns.Add("m_fEndCondition", typeof(object));
            dt_AnalysisDefault.Columns.Add("m_bReadonly", typeof(bool));

            dt_Analysis = dt_AnalysisDefault.Clone();

            InitDefaultData();
            SetDataTableToGrid(dt_AnalysisDefault);
        }

        public void InitDefaultData()
        {
            dt_AnalysisDefault.Rows.Add(new object[] { true, repositoryItemComboBox1.Items[0], "最高温度1", "---",  "---",  "---",   "---",true });
            dt_AnalysisDefault.Rows.Add(new object[] { true, repositoryItemComboBox1.Items[2], "恒温时间1", 51, 90, 60,  120 ,false });
            dt_AnalysisDefault.Rows.Add(new object[] { true, repositoryItemComboBox1.Items[1], "预热时间1", 110, 120, 50, 50 ,false });
            dt_AnalysisDefault.Rows.Add(new object[] { true, repositoryItemComboBox1.Items[2], "恒温时间2", 120, 150, 50, 50 ,false });
            dt_AnalysisDefault.Rows.Add(new object[] { true, repositoryItemComboBox1.Items[2], "恒温时间3", 120, 160, 50, 50 ,false });
            dt_AnalysisDefault.Rows.Add(new object[] { true, repositoryItemComboBox1.Items[3], "超温时间1", 232, 300, 50, 50 ,false });
            dt_AnalysisDefault.Rows.Add(new object[] { true, repositoryItemComboBox1.Items[4], "平均斜率1", 50, 150, 0, 2    ,false });
            dt_AnalysisDefault.Rows.Add(new object[] { true, repositoryItemComboBox1.Items[4], "平均斜率2", 50, 230, 50, 50  ,false });
            dt_AnalysisDefault.Rows.Add(new object[] { true, repositoryItemComboBox1.Items[5], "最高斜率1", 50, 300, 50, 50  ,false });
            dt_AnalysisDefault.Rows.Add(new object[] { true, repositoryItemComboBox1.Items[6], "升温斜率1", 50, 50, 50, 50   ,false });
            dt_AnalysisDefault.Rows.Add(new object[] { true, repositoryItemComboBox1.Items[7], "降温斜率1", 51, 50, 50, 50   ,false });
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            SetDataTableToGrid(dt_AnalysisDefault);
        }

        private void SetDataTableToGrid(DataTable dt)
        {
            GC_PageAnalysis.DataSource = dt;

            for (int col = 0; col < GV_PageAnalysis.Columns.Count; col++)
            {
                GV_PageAnalysis.Columns[col].Tag = new SuperTag() { IDD_OPTION_ANALYSE_CONDITION_ColumnIndex = col };
                if (col >= 3 || col <= 6)
                {
                    GV_PageAnalysis.Columns[col].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    GV_PageAnalysis.Columns[col].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    GV_PageAnalysis.Columns[col].DisplayFormat.FormatString = "n1";
                }
            }
        }

        private void GV_PageAnalysis_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (!(bool)GV_PageAnalysis.GetRowCellValue(e.RowHandle, "m_bReadonly"))
            {
                //int value = 0;
                //if (int.TryParse(e.CellValue.ToString(), out value))
                if( ((SuperTag)e.Column.Tag).IDD_OPTION_ANALYSE_CONDITION_ColumnIndex == 3 ||
                    ((SuperTag)e.Column.Tag).IDD_OPTION_ANALYSE_CONDITION_ColumnIndex == 4 ||
                    ((SuperTag)e.Column.Tag).IDD_OPTION_ANALYSE_CONDITION_ColumnIndex == 5 ||
                    ((SuperTag)e.Column.Tag).IDD_OPTION_ANALYSE_CONDITION_ColumnIndex == 6)
                {
                    e.RepositoryItem = rite;
                }
            }
            else
            {
                if (e.CellValue.ToString() == "---")
                {
                    e.RepositoryItem = riteReadonly;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void SaveData()
        {
            var pETETCStage = CETCManagerApp.Instance.m_pETETCStage;
            pETETCStage.m_nTempUnit = m_nTempUnit.SelectedIndex;
            pETETCStage.m_nTempUnit = m_nTempUnit.SelectedIndex;

            pETETCStage.m_AnalyseCondition.Clear();

            for (int row = 0; row < GV_PageAnalysis.RowCount; row++)
            {
                CETAnalyseCondition item = new CETAnalyseCondition()
                {
                    m_nID = row,
                    m_nShowStatus = (bool)GV_PageAnalysis.GetRowCellValue(row, "m_nShowStatus"),
                    m_nConditionType = repositoryItemComboBox1.Items.IndexOf(GV_PageAnalysis.GetRowCellValue(row, "m_nConditionType")),
                    m_strCaption = GV_PageAnalysis.GetRowCellValue(row, "m_strCaption").ToString(),
                    m_fBeginRangle = GV_PageAnalysis.GetRowCellValue(row, "m_fBeginRangle").ToDouble(),
                    m_fEndRangle = GV_PageAnalysis.GetRowCellValue(row, "m_fEndRangle").ToDouble(),
                    m_fBeginCondition = GV_PageAnalysis.GetRowCellValue(row, "m_fBeginCondition").ToDouble(),
                    m_fEndCondition = GV_PageAnalysis.GetRowCellValue(row, "m_fEndCondition").ToDouble(),
                    m_bReadonly = (bool)GV_PageAnalysis.GetRowCellValue(row, "m_bReadonly")
                };
                pETETCStage.m_AnalyseCondition.Add(item);
            }

            pETETCStage.SaveConditions();
        }

        private void LoadData()
        {
            var pETETCStage = CETCManagerApp.Instance.m_pETETCStage;
            m_nTempUnit.SelectedIndex = pETETCStage.m_nTempUnit;
            m_nTempUnit.SelectedIndex = pETETCStage.m_nTempUnit;

            try
            {          
                if (pETETCStage.m_AnalyseCondition.Count > 0)
                {
                    for (int i = 0; i < pETETCStage.m_AnalyseCondition.Count; i++)
                    {
                        int m_nConditionType = pETETCStage.m_AnalyseCondition[i].m_nConditionType;
                        bool m_nShowStatus = pETETCStage.m_AnalyseCondition[i].m_nShowStatus;
                        string m_strCaption = pETETCStage.m_AnalyseCondition[i].m_strCaption;
                        bool m_bReadonly = pETETCStage.m_AnalyseCondition[i].m_bReadonly;
                        object m_fBeginRangle = "---";
                        object m_fEndRangle = "---";
                        object m_fBeginCondition = "---";
                        object m_fEndCondition = "---";

                        if (!m_bReadonly)
                        {
                            m_fBeginRangle = pETETCStage.m_AnalyseCondition[i].m_fBeginRangle;
                            m_fEndRangle = pETETCStage.m_AnalyseCondition[i].m_fEndRangle;
                            m_fBeginCondition = pETETCStage.m_AnalyseCondition[i].m_fBeginCondition;
                            m_fEndCondition = pETETCStage.m_AnalyseCondition[i].m_fEndCondition;
                        }

                        dt_Analysis.Rows.Add(new object[] { m_nShowStatus, repositoryItemComboBox1.Items[m_nConditionType],
                        m_strCaption, m_fBeginRangle, m_fEndRangle, m_fBeginCondition, m_fEndCondition, m_bReadonly });

                    }
                    SetDataTableToGrid(dt_Analysis);
                }
                else
                {
                    InsertDefaultDataToDB();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void InsertDefaultDataToDB()
        {
            try
            {
                var m_AnalyseCondition = CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition;
                for (int row = 0; row < dt_AnalysisDefault.Rows.Count; row++)
                {
                    var item = new CETAnalyseCondition()
                    {
                        m_nID = row + 1,
                        m_nShowStatus = (bool)dt_AnalysisDefault.Rows[row][0],
                        m_nConditionType = repositoryItemComboBox1.Items.IndexOf(dt_AnalysisDefault.Rows[row][1]),
                        m_strCaption = dt_AnalysisDefault.Rows[row][2].ToString(),
                        m_fBeginRangle = dt_AnalysisDefault.Rows[row][3].ToDouble(),
                        m_fEndRangle = dt_AnalysisDefault.Rows[row][4].ToDouble(),
                        m_fBeginCondition = dt_AnalysisDefault.Rows[row][5].ToDouble(),
                        m_fEndCondition = dt_AnalysisDefault.Rows[row][6].ToDouble(),
                        m_bReadonly = (bool)dt_AnalysisDefault.Rows[row][7]
                    };

                    var db = new SQLiteConnection("Config.etc");
                    db.Insert(item);
                }
                CETCManagerApp.Instance.m_pETETCStage.ReadConditions();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ReadDataFromDB()
        {

        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {

        }
    }
}
