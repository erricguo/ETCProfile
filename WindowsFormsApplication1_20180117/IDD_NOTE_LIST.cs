using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using ETCProfiler.classes;

namespace ETCProfiler
{
    public partial class IDD_NOTE_LIST : IDD_BaseForm
    {
        DataTable dt_LabelList = new DataTable();
        List<string> DelList = new List<string>();
        public IDD_NOTE_LIST()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            dt_LabelList.Columns.Clear();
            dt_LabelList.Columns.Add("gridColumn0", typeof(string));
            dt_LabelList.Columns.Add("gridColumn1", typeof(string));
            dt_LabelList.Columns.Add("gridColumn2", typeof(double));
            dt_LabelList.Columns.Add("gridColumn3", typeof(double));
            RefreshNoteList();
        }

        private void RefreshNoteList()
        {
            GC_LabelList.DataSource = null;

            for (int ii = 0; ii < CETCManagerApp.Instance.m_pETETCStage.m_ETCNotes.Count; ii++)
            {

                var pETNote = CETCManagerApp.Instance.m_pETETCStage.m_ETCNotes.ElementAtOrDefault(ii);
                if (pETNote != null)
                {
                    var row = dt_LabelList.Rows.Add();
                    row[0] = pETNote.Name;
                    row[1] = pETNote.Text;
                    row[2] = ((PaneAnchorPoint)pETNote.AnchorPoint).AxisXCoordinate.AxisValue;
                    row[3] = ((PaneAnchorPoint)pETNote.AnchorPoint).AxisYCoordinate.AxisValue;
                }
            }
            GC_LabelList.DataSource = dt_LabelList;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            for (int row = GV_LabelList.RowCount-1; row >=0 ; row--)
            {
                if (GV_LabelList.IsRowSelected(row))
                {
                    DelList.Add(GV_LabelList.GetRowCellValue(row, "gridColumn0").ToString());
                    GV_LabelList.DeleteRow(row);
                }
            }          
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            for (int i = CETCManagerApp.Instance.m_pETETCStage.m_ETCNotes.Count-1; i>=0; i--)
            {
                if (DelList.Contains(CETCManagerApp.Instance.m_pETETCStage.m_ETCNotes[i].Name))
                {
                    CETCManagerApp.Instance.m_pETETCStage.m_ETCNotes.RemoveAt(i);
                    Main_TempChart.Instance.GetChart().AnnotationRepository.RemoveAt(i);
                }
            }
            Main_TempChart.Instance.CreateNotesChart();
        }
    }
}
