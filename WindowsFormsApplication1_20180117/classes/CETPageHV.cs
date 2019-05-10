using DevExpress.Data;
using DevExpress.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ETCProfiler.classes
{
    public class CETPageHV
    {
        public CETPageHV()
        {
            GV_PageHV.AllowUserToAddRows = false;
            GV_PageHV.AllowUserToDeleteRows = false;
        }
        public bool Init()
        {
            RefreshHVData();
            return true;
        }

        public DataGridView GV_PageHV { get; set; } = new DataGridView();
        //private Dictionary<int,DataGridViewColumn> m_strColumn = new Dictionary<int, DataGridViewColumn>();
        private List<string> m_strRow = new List<string>();
        Dictionary<string, CusCells> cells = new Dictionary<string, CusCells>();
        public DataGridView gv1 = new DataGridView();

        // 对话框数据
        private enum AnonymousEnum
        {
            //IDD = IDD_PAGE_TINCREAM
            
        }


        public void OnGridEndEdit(object pNotifyStruct, ref int pResult) { }
        public void OnGridStartEdit(object pNotifyStruct, ref int pResult) { }
        public bool RefreshHVData()
        {
            GV_PageHV.Columns.Clear();

            InsertColumn("XY数值", 100, HorzAlignment.Center, UnboundColumnType.String, typeof(string) , true);
            InsertColumn("坐标数值", 100, HorzAlignment.Far, UnboundColumnType.Decimal, typeof(decimal), false);

            //	m_Grid.DeleteAllItems();
            //沒用到
            /*CRect rect;
            GetClientRect(rect);*/

            // fill it up with stuff
            //m_Grid.SetEditable(TRUE);
            //m_Grid.EnableDragAndDrop(TRUE);


            // 获得行标题
            GetXYAxisRow();

            // 获得行数据

            var theApp = CETCManagerApp.Instance;

            for (int ii = 0; ii < theApp.m_pETETCStage.m_nHAxisNum; ii++)
            {
                for (int jj = 0; jj < theApp.m_pETETCStage.m_nChannelCount; jj++)
                {
                    CETChannel pETChannel = theApp.m_pETETCStage.m_ETChannels.ElementAtOrDefault(jj);                    
                    if (pETChannel != null)
                    {
                        int nXPos = (int)theApp.m_pETETCStage.m_fHAxis[ii];
                        if (nXPos < pETChannel.m_dTemperatureValues.Count)
                        {
                            theApp.m_pETETCStage.m_fHAxisValue[jj,ii] = pETChannel.m_dTemperatureValues[nXPos];
                        }
                        else
                        {
                            theApp.m_pETETCStage.m_fHAxisValue[jj,ii] = 999999;
                        }

                    }
                }
            }

            // 计算
            for (int ii = 0; ii < theApp.m_pETETCStage.m_nHAxisNum - 1; ii++)
            {
                for (int jj = 0; jj < theApp.m_pETETCStage.m_nChannelCount; jj++)
                {
                    if (theApp.m_pETETCStage.m_fHAxisValue[jj,ii] != 999999 && theApp.m_pETETCStage.m_fHAxisValue[jj,ii + 1] != 999999)
                    {
                        theApp.m_pETETCStage.m_fHAxisBetween[jj,ii] = theApp.m_pETETCStage.m_fHAxisValue[jj,ii + 1] - theApp.m_pETETCStage.m_fHAxisValue[jj,ii];
                    }
                    else
                    {
                        theApp.m_pETETCStage.m_fHAxisBetween[jj,ii] = 888888;
                    }

                }
            }


            ///

            for (int ii = 0; ii < theApp.m_pETETCStage.m_nVAxisNum; ii++)
            {
                for (int jj = 0; jj < theApp.m_pETETCStage.m_nChannelCount; jj++)
                {
                    double nXPos = theApp.m_pETETCStage.m_fVAxis[ii];
                    int s;
                    CETChannel pETChannel = theApp.m_pETETCStage.m_ETChannels.ElementAtOrDefault(jj);
                    if (pETChannel != null)
                    {
                        for (s = 0; s < pETChannel.m_dTemperatureValues.Count; s++)
                        {
                            if ((int)pETChannel.m_dTemperatureValues[s] == (int)nXPos)
                            {
                                theApp.m_pETETCStage.m_fVAxisValue[jj,ii] = (double)s / theApp.m_ETETC.m_nSampleRate;
                                break;
                            }

                        }
                    }
                }
            }


            for (int ii = 0; ii < theApp.m_pETETCStage.m_nVAxisNum - 1; ii++)
            {
                for (int jj = 0; jj < theApp.m_pETETCStage.m_nChannelCount; jj++)
                {
                    if (theApp.m_pETETCStage.m_fVAxisValue[jj,ii] != 999999 && theApp.m_pETETCStage.m_fVAxisValue[jj,ii + 1] != 999999)
                    {
                        theApp.m_pETETCStage.m_fVAxisBetween[jj,ii] = theApp.m_pETETCStage.m_fVAxisValue[jj,ii + 1] - theApp.m_pETETCStage.m_fVAxisValue[jj,ii];
                    }
                    else
                    {
                        theApp.m_pETETCStage.m_fVAxisBetween[jj,ii] = 888888;
                    }
                }
            }

            int nRowCount = 2 * theApp.m_pETETCStage.m_nHAxisNum - 1 + 2 * theApp.m_pETETCStage.m_nVAxisNum - 1;

            //用不到
            //int mRowCount = nRowCount + 2;
            int mRowCount = nRowCount + 1;
            int ColumnCount = 3 + theApp.m_pETETCStage.m_nChannelCount;

            for (int i = 0; i < theApp.m_pETETCStage.m_nChannelCount; i++)
            {                
                InsertColumn(string.Format("[{0}] {1}",i+1, theApp.m_pETETCStage.m_ETProduct.m_SamplesPoints[i].m_strPointTitle), 80, HorzAlignment.Far, UnboundColumnType.Decimal,  typeof(decimal), true);
            }
            /*try
            {
                m_Grid.SetRowCount(nRowCount + 2);
                m_Grid.SetColumnCount(3 + theApp.m_pETETCStage.m_nChannelCount);
                m_Grid.SetFixedRowCount(1);
                m_Grid.SetFixedColumnCount(1);
            }
            catch (CMemoryException* e)
            {
                e.ReportError();
                e.Delete();
                return FALSE;
            }*/


            // fill rows/cols with text
            for (int row = 0; row < mRowCount; row++)
            {
                GV_PageHV.Rows.Add();
                
                for (int col = 0; col < ColumnCount - 1; col++)
                {
                    //var Item = new CusCells();
                    var Item = GV_PageHV.Rows[row].Cells[col];
                    
                    /*GV_ITEM Item;
                    Item.mask = GVIF_TEXT | GVIF_FORMAT;
                    Item.row = row;
                    Item.col = col;*/
                    /*if (row < 1)
                    {
                        //Item.nFormat = DT_CENTER | DT_WORDBREAK;
                        if (col < 2)
                        {
                            Item.Value = string.Format(, m_strColumn[col]);
                        }
                        else
                        {
                            Item.strText.Format(_T("[%d]  %s "), col - 1, theApp.m_pETETCStage.m_ETProduct.m_SamplesPoints[col - 2].m_strPointTitle);

                        }
                    }
                    else if (col < 1)
                    {
                        Item.nFormat = DT_CENTER | DT_VCENTER | DT_SINGLELINE | DT_END_ELLIPSIS | DT_NOPREFIX;
                        if (row < nRowCount + 2)
                        {
                            Item.strText.Format(_T("%s"), m_strRow[row - 1]);
                        }
                    }
                    else
                    {*/
                    if (col == 0)
                    {
                        //Item.nFormat = DT_CENTER | DT_VCENTER | DT_SINGLELINE | DT_END_ELLIPSIS | DT_NOPREFIX;
                        //if (row < nRowCount + 2)
                        {

                            Item.Value = m_strRow[row];
                            Item.ReadOnly = true;
                            Item.Style.BackColor = Color.LightGray;
                            
                        }
                    }
                    else
                    // 数据
                    if (col == 1)
                    {
                        if (row  < 2 * theApp.m_pETETCStage.m_nHAxisNum - 1 && row  >= 0)
                        {
                            if ((row) % 2 == 0)
                            {
                                Item.Value = string.Format("{0:n1}", theApp.m_pETETCStage.m_fHAxis[(row) / 2]);
                                Item.ReadOnly = false;
                                Item.Style.BackColor = Color.LightGreen;
                                Item.Tag = new SuperTag() { CETPageHV_RowCaption = "X" + (row) / 2 };
                                
                            }
                            else
                            {
                                Item.Value = string.Format("{0:n1}", theApp.m_pETETCStage.m_fHAxis[(row ) / 2 + 1] - theApp.m_pETETCStage.m_fHAxis[(row ) / 2]);
                                Item.ReadOnly = true;
                                
                            }                            
                        }
                        else if (row  > 2 * theApp.m_pETETCStage.m_nHAxisNum - 1)
                        {
                            int nTempRow = row  - (2 * theApp.m_pETETCStage.m_nHAxisNum - 1) - 1;
                            if ((nTempRow) % 2 == 0)
                            {
                                Item.Value = string.Format("{0:n1}", theApp.m_pETETCStage.m_fVAxis[(nTempRow) / 2]);
                                Item.ReadOnly = false;
                                Item.Style.BackColor = Color.LightGreen;
                                Item.Tag = new SuperTag() { CETPageHV_RowCaption = "Y" + (nTempRow) / 2 };
                            }
                            else
                            {
                                Item.Value = string.Format("{0:n1}", theApp.m_pETETCStage.m_fVAxis[(nTempRow) / 2 + 1] - theApp.m_pETETCStage.m_fVAxis[(nTempRow) / 2]);
                                Item.ReadOnly = true;
                            }
                        }
                    }

                    if (col <= 1)
                    {
                        //GV_PageHV.SetRowCellValue(row, GV_PageHV.Columns[col].FieldName, Item.Value);
                        //cells.Add($"{row};{col}", Item);
                    }
                }

            }

            int nItemHRowCount = 2 * theApp.m_pETETCStage.m_nHAxisNum - 1;
            //int nItemHRowCount = mRowCount;
            // fill rows/cols with text
            for (int row = 0; row < nItemHRowCount; row++)
            {
                for (int col = 0; col < theApp.m_pETETCStage.m_nChannelCount; col++)
                {
                    //var Item = new CusCells();
                    var Item = GV_PageHV.Rows[row].Cells[col + 2];
                    /*GV_ITEM Item;
                    Item.mask = GVIF_TEXT | GVIF_FORMAT;
                    Item.row = row + 1;
                    Item.col = col + 2;
                    
                    Item.nFormat = DT_CENTER | DT_VCENTER | DT_SINGLELINE | DT_END_ELLIPSIS | DT_NOPREFIX;
                    */
                    // 数据
                    CETChannel pETChannel = theApp.m_pETETCStage.m_ETChannels.ElementAtOrDefault(col); ;
                    if (pETChannel != null)
                    {
                        if (row % 2 == 0)
                        {
                            if (theApp.m_pETETCStage.m_fHAxisValue[col,row / 2] != 999999)
                            {
                                Item.Value = string.Format("{0:n1}", theApp.m_pETETCStage.m_fHAxisValue[col,row / 2]);
                            }
                        }
                        else
                        {
                            if (theApp.m_pETETCStage.m_fHAxisBetween[col,row / 2] != 888888)
                            {
                                Item.Value = string.Format("{0:n1}", theApp.m_pETETCStage.m_fHAxisBetween[col,row / 2]);
                            }
                        }
                        Item.ReadOnly = true;
                        /*GV_PageHV.SetRowCellValue(row / 2, GV_PageHV.Columns[col].FieldName, Item.Value);
                        if (cells.ContainsKey($"{row / 2};{col}"))
                        {
                            cells[$"{row / 2};{col}"].Value = Item;
                        }*/
                        
                    }
                }

            }

            // fill rows/cols with text
            int nItemVRowCount = 2 * theApp.m_pETETCStage.m_nVAxisNum - 1;

            if (nItemHRowCount >= 0)
            {
                for (int i = 0; i < GV_PageHV.Columns.Count; i++)
                {
                    GV_PageHV.Rows[nItemHRowCount].Cells[i].Style.BackColor = Color.LightGray;
                }
            }

            for (int row = 0; row < nItemVRowCount; row++)
            {
                for (int col = 0; col < theApp.m_pETETCStage.m_nChannelCount; col++)
                {
                    //var Item = new CusCells();
                    var Item = GV_PageHV.Rows[row + (nItemHRowCount+1)].Cells[col + 2];
                    /*
                    GV_ITEM Item;
                    Item.mask = GVIF_TEXT | GVIF_FORMAT;
                    Item.row = row + 2 + nItemVRowCount;
                    Item.col = col + 2;

                    Item.nFormat = DT_CENTER | DT_VCENTER | DT_SINGLELINE | DT_END_ELLIPSIS | DT_NOPREFIX;
                    */
                    // 数据
                    CETChannel pETChannel = theApp.m_pETETCStage.m_ETChannels.ElementAtOrDefault(col); ;

                    if (pETChannel != null)
                    {
                        if (row % 2 == 0)
                        {
                            if (theApp.m_pETETCStage.m_fVAxisValue[col,row / 2] != 999999)
                            {
                                Item.Value = string.Format("{0:n1}", theApp.m_pETETCStage.m_fVAxisValue[col,row / 2]);
                            }
                            else
                            {
                                Item.Value = "";
                            }
                        }
                        else
                        {
                            if (theApp.m_pETETCStage.m_fVAxisBetween[col,row / 2] != 888888)
                            {
                                Item.Value = string.Format("{0:n1}", theApp.m_pETETCStage.m_fVAxisBetween[col,row / 2]);
                            }
                            else
                            {
                                Item.Value = "";
                            }

                        }
                        Item.ReadOnly = true;
                        /*GV_PageHV.SetRowCellValue(row / 2, GV_PageHV.Columns[col].FieldName, Item.Value);
                        if (cells.ContainsKey($"{row / 2};{col}"))
                        {
                            cells[$"{row / 2};{col}"].Value = Item;
                        }*/
                    }
                    //	m_Grid.SetItemState(row, col, GVIS_);
                    //m_Grid.SetItem(&Item);
                }

            }
            //m_Grid.AutoSize();
            /*for (int i = 0; i < GV_PageHV.RowCount; i++)
            {
                for (int j = 0; j < GV_PageHV.ColumnCount; j++)
                {
                    if (j==1)
                    {
                        if(GV_PageHV.Rows[i].Cells[j].Value != null)   
                            GV_PageHV.Rows[i].Cells[j].ReadOnly = false;
                        else
                            GV_PageHV.Rows[i].Cells[j].ReadOnly = true;
                    }
                    else
                    {                        
                        GV_PageHV.Rows[i].Cells[j].ReadOnly = true;
                    }
                }
                
            }*/
            return true;
        }

        //public void CellDataChanged(DataGridViewCell item)
        public void CellDataChanged(object item,object Value)
        {
            var theApp = CETCManagerApp.Instance;
            var tag = ((SuperTag)item).CETPageHV_RowCaption;
            var s = tag.ToString().LeftStart(1).ToInt();// int.Parse(tag.ToString().Substring(1, tag.Length - 1));
            if (tag.StartsWith("X"))
            {
                theApp.m_pETETCStage.m_fHAxis[s] = double.Parse(Value.ToString());
            }
            else
            {
                theApp.m_pETETCStage.m_fVAxis[s] = double.Parse(Value.ToString());
            }
            RefreshHVData();
        }


        public void GetXYAxisRow()
        {
            m_strRow.Clear();

            var theApp = CETCManagerApp.Instance;
            // 获得值
            for (int col = 0; col < 2 * theApp.m_pETETCStage.m_nHAxisNum - 1; col++)
            {
                string strTemRow;
                if (col % 2 == 0)
                {
                    strTemRow = string.Format("X{0}", col / 2 + 1);
                }
                else
                {
                    strTemRow = string.Format("X{0}-X{1}", col / 2 + 2, col / 2 + 1);
                }


                m_strRow.Add(strTemRow);
            }

            if (m_strRow.Count > 0)
            {
                m_strRow.Add("  ");
            }
            
            for (int row = 0; row < 2 * theApp.m_pETETCStage.m_nVAxisNum - 1; row++)
            {
                string strTemRow;
                if (row % 2 == 0)
                {
                    strTemRow = string.Format("Y{0}", row / 2 + 1);
                }
                else
                {
                    strTemRow = string.Format("Y{0}-Y{1}", row / 2 + 2, row / 2 + 1);
                }

                m_strRow.Add(strTemRow);
            }
        }

        //private void InsertColumn(int key, string name, DataGridViewContentAlignment alignHeader, DataGridViewContentAlignment alignRow, int width,Type type)
        //private void InsertColumn(int key, string name, HorzAlignment alignHeader, HorzAlignment alignRow, int width, UnboundColumnType type)
        private void InsertColumn(string name, int width, HorzAlignment alignCell, UnboundColumnType type, Type typeOri, bool readOnly)
        {
            var column = new DataGridViewTextBoxColumnEX();
            column.Name = $"column{GV_PageHV.Columns.Count + 1}";
            column.HeaderText = name;
            column.CellAlignment = alignCell;      //CELL位置
            column.ColumnType = type;
            column.SortMode = DataGridViewColumnSortMode.NotSortable; //SORT 需設定為NOT 標題才會真的置中
            column.ValueType = typeOri;
            column.Width = width;
            column.ReadOnly = readOnly;
            GV_PageHV.Columns.Add(column);
        }

    }

}
