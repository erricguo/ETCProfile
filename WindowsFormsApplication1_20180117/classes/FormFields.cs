using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ETCProfiler.classes
{
    public class FormFields
    {
        public string Name;
        public string Text;
        public List<FormFields> ChildControls = new List<FormFields>();
        public FormFields()
        {
        }
        public FormFields(Control Ctrl)
        {
            Name = Ctrl.Name;
            Text = Ctrl.Text;
        }
    }
}
