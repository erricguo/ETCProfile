using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETCProfiler.classes
{
    // 处于温度的时间
    public class CETReflowTimeAtTemperature
    {
        //	到达所需时间221.0° C (mm:ss.tt)
        //	以上的时间230.0° C (mm:ss.tt)
        //	到达所需时间230.0° C (mm:ss.tt)
        public TimeSpan[] m_tmTimeUp { get; set; } = new TimeSpan[12];
        public TimeSpan[] m_tmTimeReached { get; set; } = new TimeSpan[12];
        public double[] m_dTempReached { get; set; } = new double[12];
        public CETReflowTimeAtTemperature()
        {

        }
    }
}
