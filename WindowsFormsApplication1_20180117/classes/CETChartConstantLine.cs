using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETCProfiler.classes
{
    public class CETChartConstantLine 
    {
        //CETChartTemps = List<CETChartConstantLine>
        public string m_strName{get; set;}
        public int m_nType{get; set;}// 0 是X 1.是Y
        public float m_fXPostion{get; set;}
        public float m_fYPostion{get; set;}
        public Color m_clrLineColor{get; set;}
        public Color m_clrLabelFGColor{get; set;}
        public Color m_clrLabelBKColor{get; set;}

        public CETChartConstantLine()
        {

        }
    };

}
