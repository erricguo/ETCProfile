using DevExpress.XtraEditors;
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
    public partial class IDD_ETC_SEARCH : IDD_BaseForm
    {
        public IDD_ETC_SEARCH()
        {
            InitializeComponent();
        }

        int m_nDeviceType;
        public string m_strDeviceName { get; set; }
        public bool m_IsHaveETC { get; set; }
        public int GetComID()
        {
            return m_nCommID.SelectedIndex;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void m_ETCList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_ETCList.Items.Count >= 0)
            {
                RefreshETC(m_ETCList.SelectedIndex);
            }
        }

        private void RefreshETC(int selectedIndex)
        {
            string strETC = m_ETCList.Items[selectedIndex].ToString();

            int startdex = strETC.IndexOf("(");
            int enddex = strETC.IndexOf(")");
            string strComm = "";
            if (startdex > 0 && enddex == (strETC.Length - 1))
            {
                //strComm = strETC.Mid(startdex + 1, enddex - startdex - 1);
                //strComm = strComm.Right(strComm.Length - 3);
            }



            /*
            int nCommID = int.Parse(strComm);
            if (strETC.IndexOf("RF") >= 0)
            {

                m_nChannnelCount.Text = "6";
                m_nBandRate.SelectedIndex = 7;
                m_nCommID.SelectedIndex = nCommID - 1;
                m_strETCType = strETC;
                m_IsWireless.Checked = true;

                m_nDeviceType = 3;
            }
            else if (strETC.Find("DS-06") >= 0)
            {

                m_nChannnelCount = 6;

                m_nBandRate = 7;
                m_nCommID = nCommID - 1;
                m_strETCType = strETC;
                m_IsWireless = 0;
                m_nDeviceType = 1;

            }
            else if (strETC.Find("DS-12") >= 0)
            {

                m_nChannnelCount = 12;

                m_nBandRate = 7;
                m_nCommID = nCommID - 1;
                m_strETCType = strETC;
                m_IsWireless = 0;
                m_nDeviceType = 2;
            }

            m_strDeviceName = m_strDeviceList[nID];
            UpdateData(FALSE);*/
        }

    }
}
