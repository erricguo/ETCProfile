using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETCProfiler.classes
{
    public class CViewShowInfo
    {
        public bool m_IsShowAxisX { get; set; } = true;
        public bool m_IsShowAxisY { get; set; } = true;
        public bool m_IsShowGrid { get; set; } = true;
        public bool m_IsShowHV { get; set; } = false;
        public bool m_IsShowAllChannel { get; set; } = false;
        public bool m_IsShowTinCream { get; set; } = false;
        public bool m_IsShowNotes { get; set; } = false;
        public bool m_IsShowSlope { get; set; } = false;
        public bool m_IsShowReflowerZone { get; set; } = false;
    }
}
