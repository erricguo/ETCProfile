
using System.Drawing;

namespace ETCProfiler.classes
{
    public class CETChartNote
    {
        public CETChartNote() { }

        public float m_nXValue{ get; set; }
        public float m_nYValue{ get; set; }

        public Color m_clrPointColor{ get; set; }
        public Color m_clrLabelFGColor { get; set; }
        public Color m_clrLabelBKColor { get; set; }

        public string m_strTitle{ get; set; }

    }

}
