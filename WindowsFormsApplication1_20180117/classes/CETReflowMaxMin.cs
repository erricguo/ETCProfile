using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETCProfiler.classes
{
    // 最大/最小
    public class CETReflowMaxMin
    {
        //  最大(° C)
        public double m_dMaxTemp { get; set; }
        //	达到最大(mm:ss.tt)
        public TimeSpan m_tmTimeReachedMax { get; set; } = new TimeSpan();
        //	平均测量值(° C)
        public double m_dAverageTemp { get; set; }
        //	偏差于.0° C
        public double m_dRiseTimeTo0 { get; set; }
        //	标准偏差
        public double m_dStdDev { get; set; }
        //	最小(° C)
        public double m_dMinTemp { get; set; }
        //	达到最小(mm:ss.tt)
        public TimeSpan m_tmTimeReachedMin { get; set; } = new TimeSpan();
        //	峰值温度差(° C)

        public CETReflowMaxMin()
        {
            //	达到最大(mm:ss.tt)
            m_dMaxTemp = 0;
            //	平均测量值(° C)
            m_dAverageTemp = 0;
            //	偏差于.0° C
            m_dRiseTimeTo0 = 0;
            //	标准偏差
            m_dStdDev = 0;
            //	最小(° C)
            m_dMinTemp = 0;
        }

    }
}
