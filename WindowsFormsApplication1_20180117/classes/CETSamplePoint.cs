

using System.Drawing;

namespace ETCProfiler.classes
{
    public class CETSamplePoint
    {

        public CETSamplePoint()
        {
            m_clrColor = Color.FromArgb(0, 255, 0);
            m_strPointTitle = "Channel1";
        }

        public double m_dPostionX { get; set; }
        public double m_dPostionY{ get; set; }
        public Color m_clrColor{ get; set; }
        public string m_strPointTitle{ get; set; }
        public int m_nPointPostion{ get; set; }

    }

}
