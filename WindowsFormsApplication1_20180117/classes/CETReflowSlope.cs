using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETCProfiler.classes
{
    // 斜率
    public class CETReflowSlope
    {
        //正斜率(° C/ 秒)
        public double m_dPositiveSlope { get; set; }

        //平均斜率(° C/ 秒)
        public double m_dAverageSlope { get; set; }

        public CETReflowSlope()
        {

        }
    }
}
