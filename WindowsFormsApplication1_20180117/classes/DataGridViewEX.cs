using DevExpress.Data;
using DevExpress.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ETCProfiler.classes
{
    public class DataGridViewTextBoxColumnEX : DataGridViewTextBoxColumn
    {
        public HorzAlignment CellAlignment { get; set; }
        public UnboundColumnType ColumnType { get; set; }
        public int Round_n { get; set; } = 1;

    }
}
