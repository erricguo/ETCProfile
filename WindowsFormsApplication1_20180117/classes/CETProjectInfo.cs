
namespace ETCProfiler.classes
{
    // CETProjectInfo 命令目标

    public class CETProjectInfo
    {
        public CETProjectInfo()
        {
            m_strProjectName = "";
            m_strOperator = "";

            m_strMeasureTime = "";
            m_strPrintTime = "";

            m_nDataTotalTime = 0; // 总时长
            m_nSampleRate = 16;    // 采样周期
            m_nMeasureCount = 6; // 采样通道数
            m_strCustomer = ""; // 焊锡信号
                                //
            m_strProduct = "";  // 产品名称
            m_strReflower = ""; // 回流焊型号
            m_strTinCream = ""; // 焊锡信号

            m_strProductionline = "";
            m_strHaiqiLongdu = "";
        }
        public string m_strProjectName { get; set; }
        public string m_strOperator { get; set; }

        public string m_strMeasureTime { get; set; }
        public string m_strPrintTime { get; set; }

        public int m_nDataTotalTime { get; set; } // 总时长
        public int m_nSampleRate { get; set; } // 采样周期
        public int m_nMeasureCount { get; set; } // 采样通道数
        public string m_strCustomer { get; set; } // 焊锡信号
                                     //
        public string m_strProduct { get; set; } // 产品名称
        public string m_strReflower { get; set; } // 回流焊型号
        public string m_strTinCream { get; set; } // 焊锡信号

        public string m_strProductionline { get; set; }

        public string m_strMeasureDesc { get; set; }
        public string m_strDataDesc { get; set; }
        public string m_strHaiqiLongdu { get; set; }
    }

}
