using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETCProfiler.Enums
{
    public class Statu
    {
        public enum MaxLength
        {
            MAX_PATH = 260,
            MAX_DEIVE = 3,
            MAX_DIR = 256,
            MAX_FNAME = 256,
            MAX_EXT = 256
        }

        public enum ColumnAlignment
        {
            LEFT,
            CENTER,
            RIGHT
        }

        public enum ColumnTitle
        {
            [Description("加热温区")]
            HOT_CHS,
            [Description("冷却温区")]
            COOL_CHS,
        }

        public enum ToolButtonDrawType
        {
            None,
            LABEL,
            SLOP,
        }

        public enum ToolButtonDownType
        {
            None,
            TEMPAREA,
            REFLINE,
            HVLINE,
            LABEL,
            SLOPE,
            GRIDLINE
        }


    }
}
