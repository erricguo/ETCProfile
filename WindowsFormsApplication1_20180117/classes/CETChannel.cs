using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETCProfiler.classes
{
    public class CETChannel
    {
        public int m_nID { get; set; }

        public string m_strName { get; set; }
        public Color m_clrColor { get; set; }

        public int m_nImagePosX { get; set; }
        public int m_nImagePosY { get; set; }

        public bool m_IsVisual { get; set; }
        public bool m_IsLock { get; set; }
        public bool m_IsActive { get; set; }
        public bool m_IsSelect { get; set; }


        public List<double> m_dTemperatureValues { get; set; } = new List<double>();

        // 分析数据列表

        // 回流结果
        public CETReflowResult m_ReflowResult { get; set; } = new CETReflowResult();
        // 最大/最小
        public CETReflowMaxMin m_ReflowMaxMin { get; set; } = new CETReflowMaxMin();
        // 处于温度的时间
        public CETReflowTimeAtTemperature m_ReflowTimeAtTemperature { get; set; } = new CETReflowTimeAtTemperature();
        // 斜率
        public CETReflowSlope m_ReflowSlope { get; set; } = new CETReflowSlope();
        // 峰值差
        public CETReflowRisedrops m_Risedrops { get; set; } = new CETReflowRisedrops();

        public double m_dPeakTemp { get; set; }
        public double m_dPeakTime { get; set; }

        public double m_dMinTemp { get; set; }
        public double m_dMinTime { get; set; }
        public double m_tmChannelPeakDifference { get; set; }
        public double m_tmTimeReachedPeak { get; set; }
        //public double m_dTempAverageTemp { get; set; }

        public CETChannel ()
        {
            m_strName = "测试";
            m_clrColor = Color.FromArgb(255, 0, 0);
        }
        public void CaclReflowMaxMin(CETReflowMaxMin pReflowMaxMin)
        {
            double m_dTempAverageTemp = 0;
            double dSamplePeriod = 1.0 / CETCManagerApp.Instance.m_pETETCStage.m_nSampleRate;
            //  最大(° C)
            if (m_dTemperatureValues.Count == 0)
            {
                return;
            }

            pReflowMaxMin.m_dMaxTemp = GetMaxTemp(0, m_dTemperatureValues.Count);

            //	达到最大(mm:ss.tt)

            pReflowMaxMin.m_tmTimeReachedMax = TimeSpan.FromSeconds(GetMaxTempTime(0, m_dTemperatureValues.Count) * dSamplePeriod);

            //	平均测量值(° C)
            for (int jj = 0; jj < m_dTemperatureValues.Count; jj++)
            {
                m_dTempAverageTemp += m_dTemperatureValues[jj];
            }

            pReflowMaxMin.m_dAverageTemp = m_dTempAverageTemp / m_dTemperatureValues.Count;

            //	偏差于.0° C
            pReflowMaxMin.m_dRiseTimeTo0 = GetMaxTemp(0, m_dTemperatureValues.Count);

            //	标准偏差

            pReflowMaxMin.m_dStdDev = Deviation(0, m_dTemperatureValues.Count);

            //	最小(° C)
            pReflowMaxMin.m_dMinTemp = GetMinTemp(0, m_dTemperatureValues.Count);
            //	达到最小(mm:ss.tt)
            //TimeSpan.FromSeconds m_tmTimeReachedMin = m_dMinTime; 沒用到
        }
        public void CaclReflowTimeAtTemperature(CETReflowTimeAtTemperature pReflowTimeAtTemperature)
        {
            double dSamplePeriod = (double)1.0 / CETCManagerApp.Instance.m_pETETCStage.m_nSampleRate;
            CETETCStage pETCStage = CETCManagerApp.Instance.m_pETETCStage;
            // 到达温度时间
            pReflowTimeAtTemperature.m_tmTimeReached[0] = TimeSpan.FromSeconds(GetTimeBetweenTemp(0, pETCStage.m_ETTinCream.m_fStartTempl2) * dSamplePeriod);
            pReflowTimeAtTemperature.m_tmTimeReached[1] = TimeSpan.FromSeconds(GetTimeBetweenTemp(0, pETCStage.m_ETTinCream.m_fEndTempl2) * dSamplePeriod);
            // 温度以上时间
            pReflowTimeAtTemperature.m_tmTimeUp[0] = TimeSpan.FromSeconds(GetTimeBetweenTemp(pETCStage.m_ETTinCream.m_fStartTempl2, 300) * dSamplePeriod);
            pReflowTimeAtTemperature.m_tmTimeUp[1] = TimeSpan.FromSeconds(GetTimeBetweenTemp(pETCStage.m_ETTinCream.m_fEndTempl2, 300) * dSamplePeriod);
        }
        public double CaclPeakTemp()
        {
            return GetMaxTemp(0, m_dTemperatureValues.Count);
        } 
        public double GetSlope(double dTime1, double dTime2, double dTemp1, double dTemp2)
        {
            if (dTime2 == dTime1)
            {
                return 0.0;
            }

            double dSlope = (dTemp2 - dTemp1) / (dTime2 - dTime1);

            return dSlope;
        }
        public double GetAverageSlope(int dTime1, int dTime2)
        {
            double dSamplePeriod = 1.0 / CETCManagerApp.Instance.m_pETETCStage.m_nSampleRate;
            double dSlope = 0;
            dSlope = (m_dTemperatureValues[dTime2] - m_dTemperatureValues[dTime1]) / ((dTime2 - dTime1) * dSamplePeriod);
            return dSlope;
        }
        public double GetMaxTemp(int dTime1, int dTime2)
        {
            double dPeakTemp = 0;
            for (int ii = dTime1; ii < dTime2; ii++)
            {
                dPeakTemp = dPeakTemp > m_dTemperatureValues[ii] ? dPeakTemp : m_dTemperatureValues[ii];
            }

            return dPeakTemp;
        }

        public double Deviation(int dTemp1, int dTemp2)
        {
            int i;
            double average = 0, s = 0;
            for (i = dTemp1; i < dTemp2; i++)
            {
                average += m_dTemperatureValues[i];
            }

            average /= (dTemp2 - dTemp1);//平均值

            for (i = dTemp1; i < dTemp2; i++)
            {
                s += Math.Pow(m_dTemperatureValues[i] - average, 2);// 偏离平均数的距离和
            }

            s = s / (dTemp2 - dTemp1);//方差
            s = Math.Sqrt(s); //标准差

            return s;
        }

        public double GetTimeBetweenTemp(double dTemp1, double dTemp2)
        {
            double nTimeSpan = 0;
            if (dTemp1 < dTemp2)
            {
                for (int jj = 0; jj < m_dTemperatureValues.Count; jj++)
                {
                    if (m_dTemperatureValues[jj] > dTemp1 && m_dTemperatureValues[jj] < dTemp2)
                    {
                        nTimeSpan++;
                    }

                    if (m_dTemperatureValues[jj] > dTemp2)
                    {
                        break;
                    }
                }
            }
            else
            {
                for (int jj = m_dTemperatureValues.Count - 1; jj >= 0; jj--)
                {
                    if (m_dTemperatureValues[jj] > dTemp2 && m_dTemperatureValues[jj] < dTemp1)
                    {
                        nTimeSpan++;
                    }

                    if (m_dTemperatureValues[jj] > dTemp1)
                    {
                        break;
                    }
                }
            }

            return nTimeSpan;
        }
        public double GetMinTemp(int dTime1, int dTime2)
        {
            double dSamplePeriod = 1.0 / CETCManagerApp.Instance.m_pETETCStage.m_nSampleRate;
            m_dMinTemp = 400;
            for (int ii = dTime1; ii < dTime2; ii++)
            {
                m_dMinTemp = m_dMinTemp > m_dTemperatureValues[ii] ? m_dTemperatureValues[ii] : m_dMinTemp;
                m_dMinTime = ii * dSamplePeriod;
            }

            return m_dMinTemp;
        }
        public double GetMaxTempTime(int dTime1, int dTime2)
        {
            double dMaxTemp = 0.0;
            int m_nMaxTempTime = 0;
            for (int ii = dTime1; ii < dTime2; ii++)
            {
                if (dMaxTemp < m_dTemperatureValues[ii])
                {
                    dMaxTemp = m_dTemperatureValues[ii];
                    m_nMaxTempTime = ii;
                }
            }

            return m_nMaxTempTime;
        }
        public void CaclReflowRisedrops(CETReflowRisedrops pRisedrops)
        {
            double dSamplePeriod = (double)1.0 / CETCManagerApp.Instance.m_pETETCStage.m_nSampleRate;
            CETETCStage pETCStage = CETCManagerApp.Instance.m_pETETCStage;
            // 到达温度时间
            pRisedrops.m_tmRiseTime[0] = TimeSpan.FromSeconds(GetTimeBetweenTemp(pETCStage.m_ETTinCream.m_fStartTempl1,
                                          pETCStage.m_ETTinCream.m_fStartTempl2) * dSamplePeriod);
            pRisedrops.m_tmRiseTime[1] = TimeSpan.FromSeconds(GetTimeBetweenTemp(pETCStage.m_ETTinCream.m_fStartTempl2,
                                          pETCStage.m_ETTinCream.m_fEndTempl2) * dSamplePeriod);
            pRisedrops.m_tmRiseTime[2] = TimeSpan.FromSeconds(GetTimeBetweenTemp(pETCStage.m_ETTinCream.m_fEndTempl2,
                                          pETCStage.m_ETTinCream.m_fStartTempl3) * dSamplePeriod);
            // 温度以上时间

            pRisedrops.m_dRiseSlope[0] = GetSlopeByTemp(pETCStage.m_ETTinCream.m_fStartTempl1,
                                          pETCStage.m_ETTinCream.m_fStartTempl2);
            pRisedrops.m_dRiseSlope[1] = GetSlopeByTemp(pETCStage.m_ETTinCream.m_fStartTempl2,
                                          pETCStage.m_ETTinCream.m_fEndTempl2);
            pRisedrops.m_dRiseSlope[2] = GetSlopeByTemp(pETCStage.m_ETTinCream.m_fEndTempl2,
                                          pETCStage.m_ETTinCream.m_fStartTempl3);
        }
        public double GetSlopeByTemp(double dTemp1, double dTemp2)
        {
            if (dTemp2 == dTemp1)
            {
                return 0.0;
            }

            double dSlope = 0;
            double dSamplePeriod = (double)1.0 / CETCManagerApp.Instance.m_pETETCStage.m_nSampleRate;
            double ddd = GetTimeBetweenTemp(dTemp1, dTemp2) * dSamplePeriod;

            if (ddd == 0)
            {
                dSlope = 0;
            }
            else
            {
                dSlope = (dTemp2 - dTemp1) / ddd;
            }

            return dSlope;
        }
        public double GetTimeUpTemps(double dTemp1)
        {
            double nStartTime = 0;

            for (int jj = 0; jj < m_dTemperatureValues.Count; jj++)
            {
                if (m_dTemperatureValues[jj] > dTemp1)
                {
                    nStartTime++;
                }
            }

            return nStartTime;
        }
        public double GetSlopeByTime(int dTime1, int dTime2)
        {
            double dSamplePeriod = 1.0 / CETCManagerApp.Instance.m_pETETCStage.m_nSampleRate;
            double dSlope = 0;
            dSlope = (m_dTemperatureValues[dTime2] - m_dTemperatureValues[dTime1]) / ((dTime2 - dTime1) * dSamplePeriod);
            return dSlope;
        }
    }

}
