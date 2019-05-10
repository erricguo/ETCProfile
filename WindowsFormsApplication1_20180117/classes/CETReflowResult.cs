using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETCProfiler.classes
{
    // 回流结果
    public class CETReflowResult
    {
        //正斜率(° C/ 秒)
        public double m_dPositiveSlope { get; set; }
        //	正斜率时间(mm:ss.tt)
        public TimeSpan m_tmPositiveSlopeTime { get; set; } = new TimeSpan();
        //	上升时间(120.0 - 160.0° C)(mm:ss.tt)
        public TimeSpan m_tmRiseTime120toPeakTime { get; set; } = new TimeSpan();
        //	到峰温( mm:ss.tt)的上升时间50.0° C
        public TimeSpan m_tmRiseTime50to150PeakTime { get; set; } = new TimeSpan();
        //	到峰温的平均斜率(° C/ 秒)
        public double m_dAverageSlope { get; set; }
        //	液相线(221.0 ° C)以上的时间(mm:ss.tt)
        public TimeSpan m_dTimeAboveLiquidusTime { get; set; } = new TimeSpan();
        //	峰值温度(° C)
        public double m_dPeakTemperature { get; set; }
        //	负斜率(° C/ 秒)
        public double m_dNegativeSlope { get; set; }

        public CETReflowResult()
        {
            //正斜率(° C/ 秒)
            m_dPositiveSlope = 2.3;
            //	正斜率时间(mm:ss.tt)
            TimeSpan ts4 = new TimeSpan(0, 1, 5, 12);
            m_tmPositiveSlopeTime = ts4;
            //	上升时间(120.0 - 160.0° C)(mm:ss.tt)
            m_tmRiseTime120toPeakTime = ts4;
            //	到峰温( mm:ss.tt)的上升时间50.0° C
            m_tmRiseTime50to150PeakTime = ts4;
            //	到峰温的平均斜率(° C/ 秒)
            m_dAverageSlope = 12;
            //	液相线(221.0 ° C)以上的时间(mm:ss.tt)
            m_dTimeAboveLiquidusTime = ts4;
            //	峰值温度(° C)
            m_dPeakTemperature = 12;
            //	负斜率(° C/ 秒)
            m_dNegativeSlope = 12;
        }
    }
}
