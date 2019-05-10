
using SQLite;

namespace ETCProfiler.classes
{
    public class CETAnalyseCondition
    {
        public CETAnalyseCondition()
        {
            m_strCaption = "最高温度[°C]";
            m_nConditionType = 0;
            m_fBeginCondition = 50;
            m_fEndCondition = 50;
            m_fBeginRangle = 50;
            m_fEndRangle = 50;
            m_nShowStatus = true;
        }

        public CETAnalyseCondition(int nID,bool nShowStatus,int nConditionType, string strCaption,
                                   double fBeginRangle, double fEndRangle, double fBeginCondition, double fEndCondition,
                                    bool bReadonly)
        {
            m_nID             = nID;
            m_nShowStatus     = nShowStatus;
            m_nConditionType  = nConditionType;
            m_strCaption      = strCaption;
            m_fBeginRangle    = fBeginRangle;
            m_fEndRangle      = fEndRangle;
            m_fBeginCondition = fBeginCondition;
            m_fEndCondition   = fEndCondition;
            m_bReadonly       = bReadonly;
        }

        [PrimaryKey, AutoIncrement]
        public int m_nID{ get; set; }
        public bool m_nShowStatus{ get; set; } // 显示
        public int m_nConditionType{ get; set; } // 显示类型
        public string m_strCaption{ get; set; } // 标题

        //
        public double m_fAnanlyseResult{ get; set; }
        public int m_nAnanlyseResultStatus{ get; set; } // 0 正常，1，报警，2，未知
        // 条件
        public double m_fBeginRangle{ get; set; }
        public double m_fEndRangle{ get; set; }
        public double m_fBeginCondition{ get; set; }
        public double m_fEndCondition{ get; set; }
        public double m_fBeginRangle1{ get; set; }
        public double m_fEndRangle1{ get; set; }
        public double m_fBeginCondition1{ get; set; }
        public double m_fEndCondition1{ get; set; }
        public bool m_bReadonly { get; set; }
    }

}
