using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ETCProfiler
{
    public partial class BoxConfirm : IDD_BaseForm
    {
        public BoxConfirm()
        {
            InitializeComponent();
        }

        public BoxConfirm(string info, string title)
        {
            InitializeComponent();
            memoEdit1.Text = info;
            Text = title;
        }

        public BoxConfirm(string info,string title,Size size)
        {
            InitializeComponent();
            memoEdit1.Text = info;
            Text = title;
            Size = size;
        }
    }
}