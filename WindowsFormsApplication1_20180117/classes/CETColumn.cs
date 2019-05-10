using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ETCProfiler.Enums.Statu;

namespace ETCProfiler.classes
{
    public class CETColumn
    {
        public CETColumn() { }
        public int id { get; set; }
        public string name { get; set; } = "";
        public ColumnAlignment align { get; set; } = ColumnAlignment.LEFT;
        public int width { get; set; } = 50;


    }
}
