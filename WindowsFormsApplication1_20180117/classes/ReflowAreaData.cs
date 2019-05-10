using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETCProfiler.classes
{
    public class ReflowAreaData
    {
        public ReflowAreaData() { }
        public float m_fAreaLength { get; set; }
        public float m_fAreaTemplButtom { get; set; }
        public float m_fAreaTemplTop { get; set; }
        public string m_strAreaTitle { get; set; }
        public float m_fAreaFanSpeedButtom { get; set; }
        public float m_fAreaFanSpeedTop { get; set; }
        public float m_fAreaForecastButtom { get; set; }
        public float m_fAreaForecastTop { get; set; }
        
    }
}
