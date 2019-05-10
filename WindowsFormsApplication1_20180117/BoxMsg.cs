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
    public partial class BoxMsg : IDD_BaseForm
    {
        public BoxMsg()
        {
            InitializeComponent();
        }

        public BoxMsg(string info)
        {
            InitializeComponent();
            memoEdit1.Text = info;
            Text = "";
        }

        public BoxMsg(string info, string title)
        {
            InitializeComponent();
            memoEdit1.Text = info;
            Text = title;
        }

        public BoxMsg(string info, string title, Size size)
        {
            InitializeComponent();
            memoEdit1.Text = info;
            Text = title;
            Size = size;
        }
    }
}