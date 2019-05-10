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
    public partial class IDD_DIALOG2 : IDD_BaseForm
    {
        public string InputText { get; set; }

        public IDD_DIALOG2()
        {
            InitializeComponent();
        }


        public IDD_DIALOG2(string Title,  string Value)
        {
            InitializeComponent();
            Text = Title;
            teInput.Text = Value;
        }


        public IDD_DIALOG2(string Title, string LabelText, string Value)
        {
            InitializeComponent();
            Text = Title;
            lbInput.Text = LabelText;
            teInput.Text = Value;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            InputText = teInput.Text;
        }
    }
}
