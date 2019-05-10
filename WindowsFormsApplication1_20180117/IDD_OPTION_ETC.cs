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

namespace ETCProfiler
{
    public partial class IDD_OPTION_ETC : IDD_BaseForm
    {
        public IDD_OPTION_ETC()
        {
            InitializeComponent();
        }

        public int SampleRate
        {
            get
            {
                return m_nSampleRate.SelectedIndex;
            }
        }

        private void groupControl3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
