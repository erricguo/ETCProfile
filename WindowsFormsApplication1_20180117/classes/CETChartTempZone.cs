using System.Drawing;

namespace ETCProfiler.classes
{
    class CETChartTempZone // ETChartTempArea.cpp
    {
        public string m_strName{get; set;}

        public float m_fTopTemp{get; set;}
        public float m_fBottomTemp{get; set;}
        public float m_fLength{get; set;}

        public Color m_clrPointColor{get; set;}
        public Color m_clrLabelFGColor{get; set;}
        public Color m_clrLabelBKColor{get; set;}

        public CETChartTempZone()
        {

        }
    }
}
