
namespace WindowsFormsApplication1_20180117.classes
{
    public class CETChartTemps
    {
        public string m_strName { get; set; }

        public int m_nType { get; set; } // 0 是X 1.是Y

        public float m_fXPostion { get; set; }
        public float m_fYPostion { get; set; }

        public uint m_clrLineColor { get; set; }
        public uint m_clrLabelFGColor { get; set; }
        public uint m_clrLabelBKColor { get; set; }
    }
}
