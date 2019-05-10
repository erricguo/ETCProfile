using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using ETCProfiler.classes;

namespace ETCProfiler
{
    public partial class IDD_RATELINE_LIST : IDD_BaseForm
    {
        DataTable dt_SlopeList = new DataTable();
        List<string> DelList = new List<string>();
        public IDD_RATELINE_LIST()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            dt_SlopeList.Columns.Clear();
            dt_SlopeList.Columns.Add("gridColumn1", typeof(string));
            dt_SlopeList.Columns.Add("gridColumn2", typeof(string));
            dt_SlopeList.Columns.Add("gridColumn3", typeof(double));
            dt_SlopeList.Columns.Add("gridColumn4", typeof(double));
            dt_SlopeList.Columns.Add("gridColumn5", typeof(double));
            dt_SlopeList.Columns.Add("gridColumn6", typeof(double));            
            RefreshNoteList();
        }

        private void RefreshNoteList()
        {
            GC_SlopeList.DataSource = null;

            for (int ii = 0; ii < CETCManagerApp.Instance.m_pETETCStage.m_ETCSlopes.Count; ii++)
            {

                var pETNote = CETCManagerApp.Instance.m_pETETCStage.m_ETCSlopes.ElementAtOrDefault(ii);
                if (pETNote != null)
                {
                    try
                    {
                        var row = dt_SlopeList.Rows.Add();
                        row[0] = pETNote.Name;
                        //row[1] = ((SuperTag)pETNote.Tag).SeriesLabelString;
                        row[1] = ((TextAnnotation)pETNote.Points[0].Annotations[0]).Text;
                        row[2] = pETNote.Points[0].NumericalArgument;
                        row[3] = pETNote.Points[0].Values[0];
                        row[4] = pETNote.Points[1].NumericalArgument;
                        row[5] = pETNote.Points[1].Values[0];
                    }
                    catch (Exception)
                    {

                        continue;
                    }
                }
            }
            GC_SlopeList.DataSource = dt_SlopeList;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            for (int row = GV_SlopeList.RowCount - 1; row >= 0; row--)
            {
                if (GV_SlopeList.IsRowSelected(row))
                {
                    DelList.Add(GV_SlopeList.GetRowCellValue(row, "gridColumn1").ToString());
                    GV_SlopeList.DeleteRow(row);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            for (int i = CETCManagerApp.Instance.m_pETETCStage.m_ETCSlopes.Count - 1; i >= 0; i--)
            {
                if (DelList.Contains(CETCManagerApp.Instance.m_pETETCStage.m_ETCSlopes[i].Name))
                {
                    Main_TempChart.Instance.GetChart().Series.Remove(CETCManagerApp.Instance.m_pETETCStage.m_ETCSlopes[i]);
                    CETCManagerApp.Instance.m_pETETCStage.m_ETCSlopes.RemoveAt(i);
                }
            }
            Main_TempChart.Instance.CreateSlopeChart();
        }
    }
}
