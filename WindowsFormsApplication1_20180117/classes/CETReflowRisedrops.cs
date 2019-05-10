using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETCProfiler.classes
{
    // 峰值差
    public class CETReflowRisedrops
    {
        public TimeSpan[] m_tmRiseTime { get; set; } = new TimeSpan[4];
        public double[] m_dRiseSlope { get; set; } = new double[4];

        public CETReflowRisedrops()
        {

        }
    }
}
