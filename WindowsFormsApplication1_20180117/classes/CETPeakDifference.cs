using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETCProfiler.classes
{
    public class CETPeakDifference
    {
        //峰值差(° C)
        public double m_dPeakDifference { get; set; }
        //到达时间(mm:ss.tt)
        TimeSpan m_tmTimeReached { get; set; }

        public CETPeakDifference()
        {

        }
    }
}
