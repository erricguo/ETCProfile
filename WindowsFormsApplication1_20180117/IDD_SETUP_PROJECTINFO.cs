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
using static ETCProfiler.classes.ShareFunc;
namespace ETCProfiler
{
    public partial class IDD_SETUP_PROJECTINFO : IDD_BaseForm
    {
        public IDD_SETUP_PROJECTINFO()
        {
            InitializeComponent();
        }

        public string strProductName { get { return m_strProductName.Text; } }
        public string strProductID { get { return m_strProductID.Text; } }
        public string strCustomor { get { return m_strCustomor.Text; } }
        public string strProductDesc { get { return m_strProductDesc.Text; } }

        public string strTinCreamName { get { return m_strTinCreamName.Text; } }
        public string strTinCreamPara { get { return m_strTinCreamPara.Text; } }
        public string strProductLine { get { return m_strProductLine.Text; } }
        public string strHaiqiLongdu { get { return m_strHaiqiLongdu.Text; } }
        public string strTinCream { get { return m_strTinCream.Text; } }

        public string strMeasureOperator { get { return m_strMeasureOperator.Text; } }
        public string strMeasureTime { get { return m_strMeasureTime.Text; } }
        public string strMeasureDesc { get { return m_strMeasureDesc.Text; } }

        public int nSampleRate { get { return atoi(m_nSampleRate.Text); } }
        public int nMeasureCount { get { return atoi(m_nMeasureCount.Text); } }
        public int nMearsureLength { get { return atoi(m_nMearsureLength.Text); } }
        public string strDataDesc { get { return m_strDataDesc.Text; } }
    }
}
