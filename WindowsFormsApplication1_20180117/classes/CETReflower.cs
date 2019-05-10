

using System.Collections.Generic;

namespace ETCProfiler.classes
{
    public class CETReflower
    {
        public CETReflower()
        {
            m_strTitle = "";

            //
            m_strTemplUnit = "OC";
            m_strSpeedUnit = "cm/min";
            m_strLengthUnit = "OC";
            m_nSampleHeaterAreaCount = 8;
            m_nSampleCoolAreaCount = 4;
            m_fSpeed = 60;

            for (int i = 0; i < m_CAreaDataHot.Length; i++)
            {
                m_CAreaDataHot[i] = new ReflowAreaData();
            }

            for (int i = 0; i < m_CAreaDataCool.Length; i++)
            {
                m_CAreaDataCool[i] = new ReflowAreaData();
            }

            m_CAreaDataHot[0].m_fAreaLength = 32;
            m_CAreaDataHot[1].m_fAreaLength = 36;
            m_CAreaDataHot[2].m_fAreaLength = 36;
            m_CAreaDataHot[3].m_fAreaLength = 36;
            m_CAreaDataHot[4].m_fAreaLength = 36;
            m_CAreaDataHot[5].m_fAreaLength = 36;
            m_CAreaDataHot[6].m_fAreaLength = 36;
            m_CAreaDataHot[7].m_fAreaLength = 36;

            m_CAreaDataCool[0].m_fAreaLength = 36;
            m_CAreaDataCool[1].m_fAreaLength = 36;
            m_CAreaDataCool[2].m_fAreaLength = 36;
            m_CAreaDataCool[3].m_fAreaLength = 36;

            m_CAreaDataHot[0].m_fAreaTemplButtom = 70;
            m_CAreaDataHot[1].m_fAreaTemplButtom = 90;
            m_CAreaDataHot[2].m_fAreaTemplButtom = 120;
            m_CAreaDataHot[3].m_fAreaTemplButtom = 140;
            m_CAreaDataHot[4].m_fAreaTemplButtom = 180;
            m_CAreaDataHot[5].m_fAreaTemplButtom = 215;
            m_CAreaDataHot[6].m_fAreaTemplButtom = 240;
            m_CAreaDataHot[7].m_fAreaTemplButtom = 130;

            m_CAreaDataCool[0].m_fAreaTemplButtom = 140;
            m_CAreaDataCool[1].m_fAreaTemplButtom = 180;
            m_CAreaDataCool[2].m_fAreaTemplButtom = 215;
            m_CAreaDataCool[3].m_fAreaTemplButtom = 240;

            m_CAreaDataHot[0].m_fAreaTemplTop = 120;
            m_CAreaDataHot[1].m_fAreaTemplTop = 140;
            m_CAreaDataHot[2].m_fAreaTemplTop = 150;
            m_CAreaDataHot[3].m_fAreaTemplTop = 160;
            m_CAreaDataHot[4].m_fAreaTemplTop = 250;
            m_CAreaDataHot[5].m_fAreaTemplTop = 260;
            m_CAreaDataHot[6].m_fAreaTemplTop = 270;
            m_CAreaDataHot[7].m_fAreaTemplTop = 200;

            m_CAreaDataCool[0].m_fAreaTemplTop = 250;
            m_CAreaDataCool[1].m_fAreaTemplTop = 260;
            m_CAreaDataCool[2].m_fAreaTemplTop = 270;
            m_CAreaDataCool[3].m_fAreaTemplTop = 200;

            for (int ii = 0; ii < 20; ii++)
            {
                m_CAreaDataHot[ii].m_strAreaTitle = string.Format("加热区{0}", ii + 1);
                m_CAreaDataHot[ii].m_fAreaFanSpeedTop = 12;
                m_CAreaDataHot[ii].m_fAreaFanSpeedButtom = 12;
                m_CAreaDataHot[ii].m_fAreaForecastTop = 12;
                m_CAreaDataHot[ii].m_fAreaForecastButtom = 12;
            }

            for (int ii = 0; ii < 20; ii++)
            {
                m_CAreaDataCool[ii].m_strAreaTitle = string.Format("冷却区{0}", ii + 1);
                m_CAreaDataCool[ii].m_fAreaFanSpeedTop = 12;
                m_CAreaDataCool[ii].m_fAreaFanSpeedButtom = 12;
                m_CAreaDataCool[ii].m_fAreaForecastTop = 12;
                m_CAreaDataCool[ii].m_fAreaForecastButtom = 12;
            }

            m_fInitTempl = 30.0f;
            m_IsTempSmall = true;
            m_IsWidthSmall = true;
            m_IsSpeedSmall = true;
            m_strNotes = "";

            m_CAreaData.Add("Hot", m_CAreaDataHot);
            m_CAreaData.Add("Cool", m_CAreaDataCool);
        }
        // 显示
        public string m_strModel { get; set; }
        public string m_strProduct { get; set; }
        public string m_strTitle { get; set; }
        // 
        public string m_strTemplUnit { get; set; }
        public string m_strSpeedUnit { get; set; }
        public string m_strLengthUnit { get; set; }

        public int m_nSampleHeaterAreaCount { get; set; }
        public int m_nSampleCoolAreaCount { get; set; }
        public float m_fSpeed { get; set; }
        public float m_fInitTempl { get; set; }

        public Dictionary<string, ReflowAreaData[]> m_CAreaData = new Dictionary<string, ReflowAreaData[]>();
        private ReflowAreaData[] m_CAreaDataHot { get; set; } = new ReflowAreaData[20];
        private ReflowAreaData[] m_CAreaDataCool { get; set; } = new ReflowAreaData[20];


        public bool m_IsTempSmall { get; set; }
        public bool m_IsWidthSmall { get; set; }
        public bool m_IsSpeedSmall { get; set; }

        public string m_strNotes { get; set; }

        public void ReadFromDB(int nID) { }
        public void UpdateReflowerToDB(int nID) { }
        public void InsertReflowerToDB(int nID) { }
        public void DeleteReflowerFromDB(int nID) { }
    }

}
