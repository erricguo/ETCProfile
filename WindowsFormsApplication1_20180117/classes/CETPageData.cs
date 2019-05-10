using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ETCProfiler.Enums.Statu;

namespace ETCProfiler.classes
{
    public class CETPageData
    {
        
        public CETPageData()
        {
            GV_PageData.AllowUserToAddRows = false;
            GV_PageData.AllowUserToDeleteRows = false;
            
        }
        public bool Init()
        {
            return true;
        }

        public DataGridView GV_PageData { get; set; } = new DataGridView();        
        //private DataTable dt = new DataTable();
        public Dictionary<int,DataGridViewTextBoxColumn> m_DataList { get; set; } = new Dictionary<int, DataGridViewTextBoxColumn>();
        public List<string> m_strRow { get; set; } = new List<string>();
        Dictionary<CusCells, object> cells = new Dictionary<CusCells, object>();

        // 对话框数据
        private enum AnonymousEnum
        {
            //IDD = IDD_PAGE_DATA
        }
        

        public bool UpdateETCData()
        {
            //m_DataList.Clear();
            GV_PageData.Columns.Clear();
            /*InsertColumn(0, "序号", HorzAlignment.Center, HorzAlignment.Center, 80, UnboundColumnType.String);
            InsertColumn(1, "时间(mm:ss.tt)", HorzAlignment.Center, HorzAlignment.Center, 120, UnboundColumnType.String);
            InsertColumn(2, "距离[cm]", HorzAlignment.Center, HorzAlignment.Center, 80, UnboundColumnType.String);*/

            /*InsertColumn(0, "序号", DataGridViewContentAlignment.MiddleCenter, DataGridViewContentAlignment.MiddleCenter, 80, typeof(string));
            InsertColumn(1, "时间(mm:ss.tt)", DataGridViewContentAlignment.MiddleCenter, DataGridViewContentAlignment.MiddleCenter, 120, typeof(string));
            InsertColumn(2, "距离[cm]", DataGridViewContentAlignment.MiddleCenter, DataGridViewContentAlignment.MiddleCenter, 80, typeof(string));*/
            InsertColumn( "序号", 80, HorzAlignment.Center,  UnboundColumnType.String , typeof(string));
            InsertColumn( "时间(mm:ss.tt)", 120, HorzAlignment.Center,  UnboundColumnType.String,  typeof(string));
            InsertColumn( "距离[cm]", 80, HorzAlignment.Center,  UnboundColumnType.String,  typeof(string));
            for (int ii = 0; ii < CETCManagerApp.Instance.m_pETETCStage.m_nChannelCount; ii++)
            {
                CETChannel pETChannel = CETCManagerApp.Instance.m_pETETCStage.m_ETChannels.ElementAtOrDefault(ii);
                if (pETChannel != null)
                {
                    //InsertColumn(3 + ii, CETCManagerApp.Instance.m_pETETCStage.m_ETProduct.m_SamplesPoints[ii].m_strPointTitle, DataGridViewContentAlignment.MiddleCenter, DataGridViewContentAlignment.MiddleRight, 60, typeof(double));
                    InsertColumn( CETCManagerApp.Instance.m_pETETCStage.m_ETProduct.m_SamplesPoints[ii].m_strPointTitle, 60, 
                                     HorzAlignment.Far,  UnboundColumnType.Decimal,  typeof(double));
                }
            }

            //用不到
            /*int nCount = 0;

            // 通道
            for (int ii = 0; ii < CETCManagerApp.Instance.m_pETETCStage.m_nChannelCount; ii++)
            {
                CETChannel pETChannel = CETCManagerApp.Instance.m_pETETCStage.m_ETChannels[ii];
                if (pETChannel != null)
                {
                    nCount = pETChannel.m_dTemperatureValues.Count;
                }
            }

            SetItemCount(nCount);*/

            return true;
        }

        private void SetItemCount(int nCount)
        {
            
        }

        //private void InsertColumn(int key,string name, HorzAlignment alignHeader, HorzAlignment alignRow, int width, UnboundColumnType type)
        //private void InsertColumn(int key, string name, DataGridViewContentAlignment alignHeader, DataGridViewContentAlignment alignRow, int width, Type type)
        private void InsertColumn(string name, int width, HorzAlignment alignCell,  UnboundColumnType type , Type typeOri)
        {
            var column = new DataGridViewTextBoxColumnEX();
            //var column = new DataGridViewTextBoxColumn();            
            /*var column = new GridColumn();
            column.Caption = name;
            column.AppearanceHeader.TextOptions.HAlignment = alignHeader;
            column.AppearanceCell.TextOptions.HAlignment = alignRow;
            column.UnboundType = type;
            column.Width = width;
            column.OptionsColumn.AllowSort = DefaultBoolean.False;*/

            column.Name = $"column{GV_PageData.Columns.Count + 1}";
            column.HeaderText = name;
            column.CellAlignment = alignCell;      //CELL位置
            column.ColumnType = type;
            column.SortMode = DataGridViewColumnSortMode.NotSortable; //SORT 需設定為NOT 標題才會真的置中
            column.ValueType = typeOri;
            column.Width = width;
            GV_PageData.Columns.Add(column);

        }

        public void GetdispinfoSampleDatalist()
        {
            int idx = 0;
            try
            {
                //var dicSort = from objDic in m_pageData.m_DataList orderby objDic.Key select objDic;
                /*foreach (var Item in m_pageData.m_DataList)
                {
                    GV_PageData.Columns.Add(Item.Value);
                }*/
                var m_pETETCStage = CETCManagerApp.Instance.m_pETETCStage;
                int mPointCount = Main_TempChart.Instance.GetSeriesPointsCount();
                
                for (int i = 0; i < mPointCount; i++)
                {

                    idx = GV_PageData.Rows.Add();
                    //GV_PageData.AddNewRow();
                    //int idx = GridControl.NewItemRowHandle;
                    
                    int nTotaleSeconds = i / m_pETETCStage.m_nSampleRate;

                    string strMS = string.Format("{0:n2}", (i % m_pETETCStage.m_nSampleRate) /
                                    (double)m_pETETCStage.m_nSampleRate);
                    string strTime = string.Format("{0:00}:{1:00}.{2}", nTotaleSeconds / 60, nTotaleSeconds % 60, strMS.Substring(strMS.Length - 2, 2));

                    string strItem = "";
                    string strValue = "";
                    if (m_pETETCStage.m_nTempUnit == 0)
                    {
                        strItem = string.Format("{0:n2}", i * 0.2);
                    }
                    else
                    {
                        //y=1.8*x+32
                        strItem = string.Format("{0:n2}", i * 0.2 + 32.0);
                    }



                    GV_PageData.Rows[idx].Cells[0].Value = (i + 1).ToString();
                    GV_PageData.Rows[idx].Cells[1].Value = strTime;
                    GV_PageData.Rows[idx].Cells[2].Value = strItem;

                    /*GV_PageData.SetRowCellValue(idx, GV_PageData.Columns[0].FieldName, (i + 1).ToString());
                    GV_PageData.SetRowCellValue(idx, GV_PageData.Columns[1].FieldName, strTime);
                    GV_PageData.SetRowCellValue(idx, GV_PageData.Columns[2].FieldName, strItem);*/

                    /*GV_PageData.Rows[idx][0] = (i + 1).ToString();
                    GV_PageData.Rows[idx][1] = strTime;
                    GV_PageData.Rows[idx][2] = strItem;*/

                    for (int j = 0; j < m_pETETCStage.m_ETChannels.Count; j++)
                    {
                        double dTemperatureValue = m_pETETCStage.m_ETChannels[j].m_dTemperatureValues[i];
                        if (m_pETETCStage.m_nTempUnit == 0)
                        {
                            strValue = string.Format("{0:n2}", dTemperatureValue);
                        }
                        else
                        {
                            //y=1.8*x+32
                            strValue = string.Format("{0:n2}", dTemperatureValue + 32.0);
                        }
                        GV_PageData.Rows[idx].Cells[j + 3].Value = strValue;
                        //GV_PageData.SetRowCellValue(idx, GV_PageData.Columns[j+3].FieldName, strValue);
                        //GV_PageData.Rows[idx][j+3] = strValue;
                    }
                }
            }
            catch (Exception e)
            {
                new BoxMsg(e.Message).ShowDialog();
            }
        }
    }

}
