using DevExpress.XtraEditors;
using ETCProfiler.classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ETCProfiler
{
    public partial class IDD_OPTION_HV : XtraForm
    {
        public IDD_OPTION_HV()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            seHAxisCount.Value = CETCManagerApp.Instance.m_pETETCStage.m_nHAxisNum.ToInt();
            seVAxisCount.Value = CETCManagerApp.Instance.m_pETETCStage.m_nVAxisNum.ToInt();
            seHAxis_1.Value    = CETCManagerApp.Instance.m_pETETCStage.m_fHAxis[0].ToInt();
            seHAxis_2.Value    = CETCManagerApp.Instance.m_pETETCStage.m_fHAxis[1].ToInt();
            seHAxis_3.Value    = CETCManagerApp.Instance.m_pETETCStage.m_fHAxis[2].ToInt();
            seHAxis_4.Value    = CETCManagerApp.Instance.m_pETETCStage.m_fHAxis[3].ToInt();
            seVAxis_1.Value    = CETCManagerApp.Instance.m_pETETCStage.m_fVAxis[0].ToInt();
            seVAxis_2.Value    = CETCManagerApp.Instance.m_pETETCStage.m_fVAxis[1].ToInt();
            seVAxis_3.Value    = CETCManagerApp.Instance.m_pETETCStage.m_fVAxis[2].ToInt();
            seVAxis_4.Value    = CETCManagerApp.Instance.m_pETETCStage.m_fVAxis[3].ToInt();

            seHAxisCount.Properties.MaxValue = 4;
            seHAxisCount.Properties.MinValue = 0;
            seVAxisCount.Properties.MaxValue = 4;
            seVAxisCount.Properties.MinValue = 0;

        }

        private void IDD_DIALOG3_TemControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnOK.PerformClick();
            }
            else if (e.KeyChar == (char)27)
            {
                Close();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            CETCManagerApp.Instance.m_pETETCStage.m_nHAxisNum = seHAxisCount.Value.ToInt();
            CETCManagerApp.Instance.m_pETETCStage.m_nVAxisNum = seVAxisCount.Value.ToInt();
            CETCManagerApp.Instance.m_pETETCStage.m_fHAxis[0] = seHAxis_1.Value.ToInt();
            CETCManagerApp.Instance.m_pETETCStage.m_fHAxis[1] = seHAxis_2.Value.ToInt();
            CETCManagerApp.Instance.m_pETETCStage.m_fHAxis[2] = seHAxis_3.Value.ToInt();
            CETCManagerApp.Instance.m_pETETCStage.m_fHAxis[3] = seHAxis_4.Value.ToInt();
            CETCManagerApp.Instance.m_pETETCStage.m_fVAxis[0] = seVAxis_1.Value.ToInt();
            CETCManagerApp.Instance.m_pETETCStage.m_fVAxis[1] = seVAxis_2.Value.ToInt();
            CETCManagerApp.Instance.m_pETETCStage.m_fVAxis[2] = seVAxis_3.Value.ToInt();
            CETCManagerApp.Instance.m_pETETCStage.m_fVAxis[3] = seVAxis_4.Value.ToInt();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
