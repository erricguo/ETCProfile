using DevExpress.XtraCharts;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ETCProfiler.classes
{
    public class CETETCStage
    {
        CViewShowInfo m_ViewShowInfo = new CViewShowInfo();

        public class SPHEADER
        {
            public uint headID { get; set; } // fourCC code
            public uint version { get; set; } // version number
        }
        public CETETCStage()
        {

            m_nChannelCount = 6;

            m_nTempUnit = 0;
            m_nTimeUnit = 0;

            m_IsPeakTemp[0] = 1;
            m_IsPeakTemp[1] = 1;
            m_fDefinePeakTemp[0] = 217;         // 自定义温度参考线
            m_fDefinePeakTemp[1] = 245;
            m_IsTempTime[0] = 1;
            m_IsTempTime[1] = 1;
            m_IsTempTime[2] = 1;
            m_IsTempTime[3] = 0;

            m_fDefineTempTime[0,0] = 0;
            m_fDefineTempTime[0,1] = 36;
            m_fDefineTempTime[1,0] = 36;
            m_fDefineTempTime[1,1] = 150;
            m_fDefineTempTime[2,0] = 150;
            m_fDefineTempTime[2,1] = 190;
            m_fDefineTempTime[3,0] = 0;
            m_fDefineTempTime[3,1] = 0;
            m_IsSlopeTime[0] = 1;
            m_IsSlopeTime[1] = 1;
            m_IsSlopeTime[2] = 1;
            m_IsSlopeTime[3] = 0;

            m_fDefineTempSlope[0,0] = 0;
            m_fDefineTempSlope[0,1] = 36;
            m_fDefineTempSlope[1,0] = 36;
            m_fDefineTempSlope[1,1] = 150;
            m_fDefineTempSlope[2,0] = 150;
            m_fDefineTempSlope[2,1] = 190;
            m_fDefineTempSlope[3,0] = 0;
            m_fDefineTempSlope[3,1] = 0;


            m_nSampleRate = 20;  // 1 ,2 4 ,6 ,20
            m_dSamplePeriod = (double)1.0 / m_nSampleRate;

            m_tmStartSamplesTime = DateTime.Now;
            m_strManager[0] = "50";
            m_strManager[1] = "50";
            m_strManager[2] = "50";

            m_nVAxisNum = 2;
            m_nHAxisNum = 2;
            m_fHAxis[0] = 50;
            m_fHAxis[1] = 100;
            m_fHAxis[2] = 150;
            m_fHAxis[3] = 200;

            m_fVAxis[0] = 50;
            m_fVAxis[1] = 100;
            m_fVAxis[2] = 150;
            m_fVAxis[3] = 200;

            m_fAxisXMin = 0;
            m_fAxisXMax = 600;
            m_fAxisYMin = 0;
            m_fAxisYMax = 300;
            for (int ii = 0; ii < 12; ii++)
            {
                m_fVAxisValue[ii,0] = 0;
                m_fVAxisValue[ii,1] = 0;
                m_fVAxisValue[ii,2] = 0;
                m_fVAxisValue[ii,3] = 0;

                m_fVAxisBetween[ii,0] = 0;
                m_fVAxisBetween[ii,1] = 0;
                m_fVAxisBetween[ii,2] = 0;

                m_fHAxisValue[ii,0] = 0;
                m_fHAxisValue[ii,1] = 0;
                m_fHAxisValue[ii,2] = 0;
                m_fHAxisValue[ii,3] = 0;

                m_fHAxisBetween[ii,0] = 0;
                m_fHAxisBetween[ii,1] = 0;
                m_fHAxisBetween[ii,2] = 0;


            }

            m_fMainScaleTicksX = 10;
            m_fMinorScaleTicksX = 5;

            m_fMainScaleTicksY = 10;
            m_fMinorScaleTicksY = 5;

        }

        public SPHEADER m_Header { get; set; } = new SPHEADER(); // 项目保存文件头

        // 开始时间
        public DateTime m_tmStartSamplesTime { get; set; }
        // 持续时间
        public int m_nTimeLength{ get; set; }
        // 采样频率
        public int m_nSampleRate{ get; set; } // 1 ,2 4 ,6 ,20
        public double m_dSamplePeriod{ get; set; }
        // 具体数据
        public int m_nChannelCount{ get; set; }

        // 主曲线
        public List<CETChannel> m_ETChannels { get; set; } = new List<CETChannel> (); // 通道测试数据

        // 模拟数据模拟曲线
        public List<CETChannel> m_ETCSimulations { get; set; } = new List<CETChannel> ();

        // notes （标签）
        public List<Series> m_ETCSlopes { get; set; } = new List<Series> ();

        //  备注 （标签）
        //public List<CETChartNote> m_ETCNotes { get; set; } = new List<CETChartNote> ();
        public List<TextAnnotation> m_ETCNotes { get; set; } = new List<TextAnnotation>(); 

        // 自定义温度参考线
        public List<CETChartTemp> m_ETTemps { get; set; } = new List<CETChartTemp> ();

        // 自定义温区线
        public List<CETChartTemp> m_ETCTempZones { get; set; } = new List<CETChartTemp> ();

        public int[] m_IsPeakTemp { get; set; } = new int[2];
        public double[] m_fDefinePeakTemp { get; set; } = new double[2]; // 自定义温度参考线

        public int[] m_IsTempTime { get; set; } = new int[4];
        public double[,] m_fDefineTempTime { get; set; } = new double[4, 2]; // 温度时间

        public int[] m_IsSlopeTime { get; set; } = new int[4];
        public double[,] m_fDefineTempSlope { get; set; } = new double[4, 2]; // 温度斜率
        // 分析数据
        public int m_nTopTemp{ get; set; }

        public double m_dChannelTempDifferenceMax{ get; set; }
        public double m_dChannelTempDifferenceMin{ get; set; }
        // 锡膏参数
        public CETProduct m_ETProduct { get; set; } = new CETProduct();
        public CETTinCream m_ETTinCream { get; set; } = new CETTinCream();
        // 产品参数

        // 回流炉参数
        public CETReflower m_ETReflower { get; set; } = new CETReflower();
        // 用户参数
        //	CETCustomer m_ETCustomer; 	// 用户信息
        // 采样板设置
        public CETETC m_ETETC { get; set; } = new CETETC();

        public CETProjectInfo m_ETProjectInfo { get; set; } = new CETProjectInfo();
        public CETProjectInfo m_ETProjectInfoFilter { get; set; } = new CETProjectInfo();

        public int m_nTempUnit{ get; set; }
        public int m_nTimeUnit{ get; set; }

        // 显示参数
        /// <summary>
        /// 垂直線
        /// </summary>
        public int m_nVAxisNum{ get; set; }
        /// <summary>
        /// 水平線
        /// </summary>
        public int m_nHAxisNum{ get; set; }
        /// <summary>
        /// 加熱區
        /// </summary>
        public double[] m_fHAxis { get; set; } = new double[12];
        /// <summary>
        /// 加熱區區間相減
        /// </summary>
        public double[,] m_fHAxisBetween { get; set; } = new double[24, 4];

        public double[,] m_fHAxisValue { get; set; } = new double[24, 4];
        /// <summary>
        /// 冷卻區
        /// </summary>
        public double[] m_fVAxis { get; set; } = new double[12];
        public double[,] m_fVAxisBetween { get; set; } = new double[24, 4];
        public double[,] m_fVAxisValue { get; set; } = new double[24, 4];
        public double m_fAxisXMin{ get; set; }
        public double m_fAxisXMax{ get; set; }


        public double m_fAxisYMin{ get; set; }
        public double m_fAxisYMax{ get; set; }

        public double m_fMainScaleTicksX{ get; set; }
        public double m_fMinorScaleTicksX{ get; set; }

        public double m_fMainScaleTicksY{ get; set; }
        public double m_fMinorScaleTicksY{ get; set; }


        public string[] m_strManager { get; set; } = new string[3];
        // 分析条件
        //	CETConditions m_AnalyseCondition;
        public List<CETAnalyseCondition> m_AnalyseCondition { get; set; } = new List<CETAnalyseCondition>();
        //////////////////// 文件操作 //////////////////////
        public bool LoadFile(string strReadFilePath)
        {
            //	string strDLLPath = CETCManagerApp.Instance.GetCurrentPath();

            string strDLLPath = strReadFilePath;


            char[] buffer = new char[1024];

            try
            {
                FileStream fsFile = new FileStream(@strReadFilePath, FileMode.Open);

                int nItem;
                int IsDataBegin = 0;
                int nCount = 0;
                CETCManagerApp.Instance.RemoveChannelData();
                while (fsFile.ReadByte() != -1)
                {
                    //
                    string strTime = new string(buffer);
                    if (IsDataBegin == 1)
                    {
                        // 分析数据
                        if (strTime.IndexOf("End") == 0)
                        {
                            break;
                        }

                        CETCManagerApp.Instance.AddPointData(nCount, strTime);
                        nCount++;
                    }
                    else
                    {
                        string strItem = strTime.Mid(9, 15);
                        if (strTime.IndexOf("DataTime") >= 0)
                        {
                            IsDataBegin = 1;
                        }
                    }
                }


                fsFile.Close();
                return true;
            }
            catch (Exception e)
            {
                //throw e;
                return false;
            }
        }
        public bool SaveFile(string strSaveFilePath){ return true; }
        public double CaclPeakDifference()
        {
            double dMaxPeak = 0;
            double dMinPeak = 400;
            for (int row = 0; row < CETCManagerApp.Instance.m_pETETCStage.m_nChannelCount; row++)
            {
                CETChannel pETChannel = CETCManagerApp.Instance.m_pETETCStage.m_ETChannels.ElementAtOrDefault(row); ;
                double m_dPeakTemp = 0;
                double m_dMinTemp = 400;
                if (pETChannel != null)
                {
                    for (int ii = 0; ii < pETChannel.m_dTemperatureValues.Count; ii++)
                    {
                        // 最小值
                        //		 m_dMinTemp = m_dPeakTemp > pETChannel.m_dTemperatureValues[ii] ? pETChannel.m_dTemperatureValues[ii] :m_dPeakTemp;
                        // 最大值
                        m_dPeakTemp = m_dPeakTemp > pETChannel.m_dTemperatureValues[ii] ? m_dPeakTemp : pETChannel.
                                        m_dTemperatureValues[ii];
                    }
                }
                //////////////////////////////////////////////////////////////////////////
                dMaxPeak = dMaxPeak > m_dPeakTemp ? dMaxPeak : m_dPeakTemp;
                dMinPeak = dMinPeak > m_dPeakTemp ? m_dPeakTemp : dMinPeak;
            }
            m_dChannelTempDifferenceMax = dMaxPeak;
            m_dChannelTempDifferenceMin = dMinPeak;
            return dMaxPeak;
        }
        public bool OpenStageData()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            //openFileDialog.Filter = "文本文件|*.*|C#文件|*.cs|所有文件|*.*";
            openFileDialog.Filter = "etb files|*.etb";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                RemoveChannelData();
                string strFileName = openFileDialog.FileName;


                UnSerialize(strFileName);

                CETCManagerApp.Instance.m_strProjectName = strFileName;
                return true;

            }

            return false;
        }
        public bool OpenStageData(string strFilepath)
        {
            RemoveChannelData();
            UnSerialize(strFilepath);
            CETCManagerApp.Instance.m_strProjectName = strFilepath;
            return true;
        }
        public bool SaveStageData()
        {
            //string localFilePath, fileNameExt, newFileName, FilePath; 
            SaveFileDialog sfd = new SaveFileDialog();

            //设置文件类型 
            //sfd.Filter = "数据库备份文件（*.bak）|*.bak|数据文件（*.mdf）|*.mdf|日志文件（*.ldf）|*.ldf";
            sfd.Filter = "etb files|*.etb";

            //设置默认文件类型显示顺序 
            sfd.FilterIndex = 1;

            //保存对话框是否记忆上次打开的目录 
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string localFilePath = sfd.FileName.ToString(); //获得文件路径 
                string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1); //获取文件名，不带路径
                Serialize(localFilePath);
                CETCManagerApp.Instance.m_strProjectName = localFilePath;
                return true;
            }

            return false;

            
        }
        public bool SaveStageData(string strFilepath)
        { 
            string fileNameExt = strFilepath.Substring(strFilepath.LastIndexOf("\\") + 1); //获取文件名，不带路径
            Serialize(strFilepath);
            CETCManagerApp.Instance.m_strProjectName = strFilepath;
            return true;
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="fName"></param>
        public void Serialize(string fName)
        {
            try
            {
                if (!File.Exists(fName))
                {
                    File.Create(fName).Close();
                }
                FileStream fs = new FileStream(fName, FileMode.Open, FileAccess.ReadWrite);
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter format = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                using (BinaryWriter br = new BinaryWriter(fs, Encoding.UTF8))
                {
                    var headID1 = 'N' | ('E' << 8) | ('O' << 16) | ('N' << 24);
                    br.Write(headID1);
                    br.Write(m_Header.version);


                    br.Write(m_ETProjectInfo.m_strProjectName);
                    br.Write(m_ETProjectInfo.m_strOperator);
                    br.Write(m_ETProjectInfo.m_strMeasureTime);
                    br.Write(m_ETProjectInfo.m_strPrintTime);
                    br.Write(m_ETProjectInfo.m_nDataTotalTime);//总时长
                    br.Write(m_ETProjectInfo.m_nSampleRate);//采样周期
                    br.Write(m_ETProjectInfo.m_nMeasureCount); //采样通道数
                    br.Write(m_ETProjectInfo.m_strCustomer); //焊锡信号
                    br.Write(m_ETProjectInfo.m_strProduct);//产品名称
                    br.Write(m_ETProjectInfo.m_strReflower);//回流焊型号
                    br.Write(m_ETProjectInfo.m_strTinCream);//焊锡信号
                    br.Write(m_ETProjectInfo.m_strProductionline);
                    br.Write(m_ETProjectInfo.m_strHaiqiLongdu);
                    br.Write(m_ViewShowInfo.m_IsShowAxisX ? 1: 0);
                    br.Write(m_ViewShowInfo.m_IsShowAxisY ? 1 : 0);
                    br.Write(m_ViewShowInfo.m_IsShowGrid ? 1 : 0);
                    br.Write(m_ViewShowInfo.m_IsShowHV ? 1 : 0);
                    br.Write(m_ViewShowInfo.m_IsShowAllChannel ? 1 : 0);
                    br.Write(m_ViewShowInfo.m_IsShowTinCream ? 1 : 0);
                    br.Write(m_ViewShowInfo.m_IsShowNotes ? 1 : 0);
                    br.Write(m_ViewShowInfo.m_IsShowSlope ? 1 : 0);
                    br.Write(m_ViewShowInfo.m_IsShowReflowerZone ? 1 : 0);

                    // 采样板信息
                    br.Write(m_ETETC.m_nChannelCount);
                    br.Write(m_ETETC.m_nBandRate);
                    br.Write(m_ETETC.m_nComID);
                    br.Write(m_ETETC.m_strETCName);
                    br.Write(m_ETETC.m_IsHaveWireLess ? 1 : 0);

                    // 回流炉信息
                    br.Write(m_ETReflower.m_strTitle);
                    br.Write(m_ETReflower.m_strTemplUnit);
                    br.Write(m_ETReflower.m_strLengthUnit);
                    br.Write(m_ETReflower.m_strSpeedUnit);
                    br.Write(m_ETReflower.m_fSpeed);
                    br.Write(m_ETReflower.m_nSampleHeaterAreaCount);
                    br.Write(m_ETReflower.m_strProduct);
                    br.Write(m_ETReflower.m_strModel);
                    br.Write(m_ETReflower.m_IsTempSmall ? 1 : 0);
                    br.Write(m_ETReflower.m_IsWidthSmall ? 1 : 0);
                    br.Write(m_ETReflower.m_IsSpeedSmall ? 1 : 0);
                    br.Write(m_ETReflower.m_strNotes);
                    br.Write(m_ETReflower.m_fInitTempl);


                    var pReflowerAreaData_Hot = CETCManagerApp.Instance.m_pETETCStage.m_ETReflower.m_CAreaData["Hot"];
                    for (int ii = 0; ii < 12; ii++)
                    {
                        br.Write(pReflowerAreaData_Hot[ii].m_fAreaLength);
                        br.Write(pReflowerAreaData_Hot[ii].m_fAreaTemplTop);
                        br.Write(pReflowerAreaData_Hot[ii].m_fAreaTemplButtom);
                        br.Write(pReflowerAreaData_Hot[ii].m_fAreaFanSpeedTop);
                        br.Write(pReflowerAreaData_Hot[ii].m_fAreaFanSpeedButtom);
                        br.Write(pReflowerAreaData_Hot[ii].m_fAreaFanSpeedButtom);
                        br.Write(pReflowerAreaData_Hot[ii].m_fAreaFanSpeedTop);
                        br.Write(pReflowerAreaData_Hot[ii].m_fAreaForecastButtom);
                        br.Write(pReflowerAreaData_Hot[ii].m_fAreaForecastTop);
                    }

                    var pReflowerAreaData_Cool = CETCManagerApp.Instance.m_pETETCStage.m_ETReflower.m_CAreaData["Cool"];
                    for (int ii = 0; ii < 12; ii++)
                    {
                        br.Write(pReflowerAreaData_Cool[ii].m_fAreaLength);
                        br.Write(pReflowerAreaData_Cool[ii].m_fAreaTemplTop);
                        br.Write(pReflowerAreaData_Cool[ii].m_fAreaTemplButtom);
                        br.Write(pReflowerAreaData_Cool[ii].m_fAreaFanSpeedTop);
                        br.Write(pReflowerAreaData_Cool[ii].m_fAreaFanSpeedButtom);
                        br.Write(pReflowerAreaData_Cool[ii].m_fAreaFanSpeedButtom);
                        br.Write(pReflowerAreaData_Cool[ii].m_fAreaFanSpeedTop);
                        br.Write(pReflowerAreaData_Cool[ii].m_fAreaForecastButtom);
                        br.Write(pReflowerAreaData_Cool[ii].m_fAreaForecastTop);
                    }

                    for (int ii = 0; ii < 16 * 9; ii++)
                    {
                        br.Write(4);
                        //br.ReadBytes(4);
                    }


                    // 锡膏信息
                    br.Write(m_ETTinCream.m_strName);
                    br.Write(m_ETTinCream.m_strManufacturers);
                    br.Write(m_ETTinCream.m_strType);
                    // 预热
                    br.Write(m_ETTinCream.m_fStartTempl1);
                    br.Write(m_ETTinCream.m_fMinSlopeValue1);
                    br.Write(m_ETTinCream.m_fMaxSlopeValue1);
                    br.Write(m_ETTinCream.m_fTargetSlopeValue1);
                    // 浸泡
                    br.Write(m_ETTinCream.m_fStartTempl2);
                    br.Write(m_ETTinCream.m_fEndTempl2);
                    br.Write(m_ETTinCream.m_fMinSlopeValue2);
                    br.Write(m_ETTinCream.m_fMaxSlopeValue2);
                    br.Write(m_ETTinCream.m_fTargetSlopeValue2);
                    br.Write(m_ETTinCream.m_fTimeRangeMin2);
                    br.Write(m_ETTinCream.m_fTimeRangeMax2);
                    br.Write(m_ETTinCream.m_fTimeRangeTarget2);
                    // 回流
                    br.Write(m_ETTinCream.m_fStartTempl3);
                    br.Write(m_ETTinCream.m_fMinSlopeValue3);
                    br.Write(m_ETTinCream.m_fMaxSlopeValue3);
                    br.Write(m_ETTinCream.m_fTargetSlopeValue3);

                    br.Write(m_ETTinCream.m_fTimeRangeMin3);
                    br.Write(m_ETTinCream.m_fTimeRangeMax3);
                    br.Write(m_ETTinCream.m_fTimeRangeTarget3);

                    br.Write(m_ETTinCream.m_fMinTemplValue3);
                    br.Write(m_ETTinCream.m_fMaxTemplValue3);
                    br.Write(m_ETTinCream.m_fTargetTemplValue3);

                    // 冷却
                    br.Write(m_ETTinCream.m_fMinSlopeValue4);
                    br.Write(m_ETTinCream.m_fMaxSlopeValue4);
                    br.Write(m_ETTinCream.m_fTargetSlopeValue4);

                    // 水平垂直线

                    br.Write(m_nHAxisNum);
                    for (int i = 0; i < 4; i++)
                    {
                        br.Write(m_fHAxis[i]);
                    }

                    br.Write(m_nVAxisNum);
                    for (int i = 0; i < 4; i++)
                    {
                        br.Write(m_fVAxis[i]);
                    }

                    // 产品数据

                    br.Write(m_ETProduct.m_strName);
                    br.Write(m_ETProduct.m_strProductCode);
                    br.Write(m_ETProduct.m_strDescription);
                    br.Write(m_ETProduct.m_strManufacturers);
                    br.Write(m_ETProduct.m_strCustomor);

                    br.Write(m_ETProduct.m_strProductImage[0]);
                    br.Write(m_ETProduct.m_strProductImage[1]);
                    br.Write(m_ETProduct.m_dWidth);
                    br.Write(m_ETProduct.m_dHeight);
                    br.Write(m_ETProduct.m_dThickness);

                    for (int ii = 0; ii < 24; ii++)
                    {
                        br.Write(m_ETProduct.m_SamplesPoints[ii].m_dPostionY);
                        br.Write(m_ETProduct.m_SamplesPoints[ii].m_dPostionX);
                        br.Write(m_ETProduct.m_SamplesPoints[ii].m_clrColor.ToArgb());
                        br.Write(m_ETProduct.m_SamplesPoints[ii].m_strPointTitle); //Encoding.GetEncoding("gb2312").GetString(br.ReadBytes(8));
                        br.Write(m_ETProduct.m_SamplesPoints[ii].m_nPointPostion);
                    }

                    // 数据通道
                    var theApp = CETCManagerApp.Instance;
                    for (int ii = 0; ii < m_ETProjectInfo.m_nMeasureCount; ii++)
                    {
                        CETChannel pETChannel = m_ETChannels.ElementAtOrDefault(ii);
                        if (pETChannel != null)
                        {
                            br.Write(theApp.m_pETETCStage.m_ETProduct.m_SamplesPoints[ii].m_strPointTitle);
                            br.Write(m_ETProduct.m_SamplesPoints[ii].m_clrColor.ToArgb());
                            br.Write(pETChannel.m_dTemperatureValues.Count);
                            for (int s = 0; s < pETChannel.m_dTemperatureValues.Count; s++)
                            {
                                double dTemperatureValue = pETChannel.m_dTemperatureValues[s];
                                br.Write(dTemperatureValue);
                            }
                        }
                    }

                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// 解析文件
        /// </summary>
        /// <param name="fName"></param>
        public void UnSerialize(string fName)
        {
            try
            {
                FileStream fs = new FileStream(fName, FileMode.Open, FileAccess.Read);
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter format = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                using (BinaryReader br = new BinaryReader(fs, Encoding.GetEncoding("gb2312")))
                {
                    uint headID = br.ReadUInt32();
                    uint version = br.ReadUInt32();
                    var headID1 = 'N' | ('E' << 8) | ('O' << 16) | ('N' << 24);

                    m_ETProjectInfo.m_strProjectName = br.ReadString();
                    m_ETProjectInfo.m_strOperator = br.ReadString();
                    m_ETProjectInfo.m_strMeasureTime = br.ReadString();
                    m_ETProjectInfo.m_strPrintTime = br.ReadString();
                    m_ETProjectInfo.m_nDataTotalTime = br.ReadInt32();//总时长
                    m_ETProjectInfo.m_nSampleRate = br.ReadInt32();//采样周期
                    m_ETProjectInfo.m_nMeasureCount = br.ReadInt32(); //采样通道数
                    m_ETProjectInfo.m_strCustomer = br.ReadString(); //焊锡信号
                    m_ETProjectInfo.m_strProduct = br.ReadString();//产品名称
                    m_ETProjectInfo.m_strReflower = br.ReadString();//回流焊型号
                    m_ETProjectInfo.m_strTinCream = br.ReadString();//焊锡信号
                    m_ETProjectInfo.m_strProductionline = br.ReadString();
                    m_ETProjectInfo.m_strHaiqiLongdu = br.ReadString();
                    m_ViewShowInfo.m_IsShowAxisX = br.ReadInt32() == 1 ? true : false;
                    m_ViewShowInfo.m_IsShowAxisY = br.ReadInt32() == 1 ? true : false;
                    m_ViewShowInfo.m_IsShowGrid = br.ReadInt32() == 1 ? true : false;
                    m_ViewShowInfo.m_IsShowHV = br.ReadInt32() == 1 ? true : false;
                    m_ViewShowInfo.m_IsShowAllChannel = br.ReadInt32() == 1 ? true : false;
                    m_ViewShowInfo.m_IsShowTinCream = br.ReadInt32() == 1 ? true : false;
                    m_ViewShowInfo.m_IsShowNotes = br.ReadInt32() == 1 ? true : false;
                    m_ViewShowInfo.m_IsShowSlope = br.ReadInt32() == 1 ? true : false;
                    m_ViewShowInfo.m_IsShowReflowerZone = br.ReadInt32() == 1 ? true : false;

                    // 采样板信息
                    m_ETETC.m_nChannelCount = br.ReadInt32();
                    m_ETETC.m_nBandRate = br.ReadUInt32();
                    m_ETETC.m_nComID = br.ReadInt32();
                    m_ETETC.m_strETCName = br.ReadString();
                    m_ETETC.m_IsHaveWireLess = br.ReadInt32() == 1 ? true : false;

                    // 回流炉信息
                    m_ETReflower.m_strTitle = br.ReadString();
                    m_ETReflower.m_strTemplUnit = br.ReadString();
                    m_ETReflower.m_strLengthUnit = br.ReadString();
                    m_ETReflower.m_strSpeedUnit = br.ReadString();
                    m_ETReflower.m_fSpeed = BitConverter.ToSingle(br.ReadBytes(4), 0);
                    m_ETReflower.m_nSampleHeaterAreaCount = br.ReadInt32();
                    m_ETReflower.m_strProduct = br.ReadString();
                    m_ETReflower.m_strModel = br.ReadString();
                    m_ETReflower.m_IsTempSmall = br.ReadInt32() == 1 ? true : false;
                    m_ETReflower.m_IsWidthSmall = br.ReadInt32() == 1 ? true : false;
                    m_ETReflower.m_IsSpeedSmall = br.ReadInt32() == 1 ? true : false;
                    m_ETReflower.m_strNotes = br.ReadString();
                    m_ETReflower.m_fInitTempl = BitConverter.ToSingle(br.ReadBytes(4), 0);


                    var pReflowerAreaData_Hot = CETCManagerApp.Instance.m_pETETCStage.m_ETReflower.m_CAreaData["Hot"];
                    for (int ii = 0; ii < 12; ii++)
                    {
                        pReflowerAreaData_Hot[ii].m_fAreaLength = BitConverter.ToSingle(br.ReadBytes(4), 0);
                        pReflowerAreaData_Hot[ii].m_fAreaTemplTop = BitConverter.ToSingle(br.ReadBytes(4), 0);
                        pReflowerAreaData_Hot[ii].m_fAreaTemplButtom = BitConverter.ToSingle(br.ReadBytes(4), 0);
                        pReflowerAreaData_Hot[ii].m_fAreaFanSpeedTop = BitConverter.ToSingle(br.ReadBytes(4), 0);
                        pReflowerAreaData_Hot[ii].m_fAreaFanSpeedButtom = BitConverter.ToSingle(br.ReadBytes(4), 0);
                        pReflowerAreaData_Hot[ii]. m_fAreaFanSpeedButtom = BitConverter.ToSingle(br.ReadBytes(4), 0);
                        pReflowerAreaData_Hot[ii]. m_fAreaFanSpeedTop = BitConverter.ToSingle(br.ReadBytes(4), 0);
                        pReflowerAreaData_Hot[ii]. m_fAreaForecastButtom = BitConverter.ToSingle(br.ReadBytes(4), 0);
                        pReflowerAreaData_Hot[ii]. m_fAreaForecastTop = BitConverter.ToSingle(br.ReadBytes(4), 0);
                    }

                    var pReflowerAreaData_Cool = CETCManagerApp.Instance.m_pETETCStage.m_ETReflower.m_CAreaData["Cool"];
                    for (int ii = 0; ii < 12; ii++)
                    {
                        pReflowerAreaData_Cool[ii].m_fAreaLength = BitConverter.ToSingle(br.ReadBytes(4), 0);
                        pReflowerAreaData_Cool[ii].m_fAreaTemplTop = BitConverter.ToSingle(br.ReadBytes(4), 0);
                        pReflowerAreaData_Cool[ii].m_fAreaTemplButtom = BitConverter.ToSingle(br.ReadBytes(4), 0);
                        pReflowerAreaData_Cool[ii].m_fAreaFanSpeedTop = BitConverter.ToSingle(br.ReadBytes(4), 0);
                        pReflowerAreaData_Cool[ii].m_fAreaFanSpeedButtom = BitConverter.ToSingle(br.ReadBytes(4), 0);
                        pReflowerAreaData_Cool[ii].m_fAreaFanSpeedButtom = BitConverter.ToSingle(br.ReadBytes(4), 0);
                        pReflowerAreaData_Cool[ii].m_fAreaFanSpeedTop = BitConverter.ToSingle(br.ReadBytes(4), 0);
                        pReflowerAreaData_Cool[ii].m_fAreaForecastButtom = BitConverter.ToSingle(br.ReadBytes(4), 0);
                        pReflowerAreaData_Cool[ii].m_fAreaForecastTop = BitConverter.ToSingle(br.ReadBytes(4), 0);
                    }

                    for (int ii = 0; ii < 16*9; ii++)
                    {
                        br.ReadBytes(4);
                    }


                    // 锡膏信息
                    m_ETTinCream.m_strName = br.ReadString();
                    m_ETTinCream.m_strManufacturers = br.ReadString();
                    m_ETTinCream.m_strType = br.ReadString();
                    // 预热
                    m_ETTinCream.m_fStartTempl1 = BitConverter.ToSingle(br.ReadBytes(4), 0);
                    m_ETTinCream.m_fMinSlopeValue1 = BitConverter.ToSingle(br.ReadBytes(4), 0);
                    m_ETTinCream.m_fMaxSlopeValue1 = BitConverter.ToSingle(br.ReadBytes(4), 0);
                    m_ETTinCream.m_fTargetSlopeValue1 = BitConverter.ToSingle(br.ReadBytes(4), 0);
                    // 浸泡
                    m_ETTinCream.m_fStartTempl2 = BitConverter.ToSingle(br.ReadBytes(4), 0);
                    m_ETTinCream.m_fEndTempl2 = BitConverter.ToSingle(br.ReadBytes(4), 0);
                    m_ETTinCream.m_fMinSlopeValue2 = BitConverter.ToSingle(br.ReadBytes(4), 0);
                    m_ETTinCream.m_fMaxSlopeValue2 = BitConverter.ToSingle(br.ReadBytes(4), 0);
                    m_ETTinCream.m_fTargetSlopeValue2 = BitConverter.ToSingle(br.ReadBytes(4), 0);
                    m_ETTinCream.m_fTimeRangeMin2 = BitConverter.ToSingle(br.ReadBytes(4), 0);
                    m_ETTinCream.m_fTimeRangeMax2 = BitConverter.ToSingle(br.ReadBytes(4), 0);
                    m_ETTinCream.m_fTimeRangeTarget2 = BitConverter.ToSingle(br.ReadBytes(4), 0);
                    // 回流
                    m_ETTinCream.m_fStartTempl3 = BitConverter.ToSingle(br.ReadBytes(4), 0);
                    m_ETTinCream.m_fMinSlopeValue3 = BitConverter.ToSingle(br.ReadBytes(4), 0);
                    m_ETTinCream.m_fMaxSlopeValue3 = BitConverter.ToSingle(br.ReadBytes(4), 0);
                    m_ETTinCream.m_fTargetSlopeValue3 = BitConverter.ToSingle(br.ReadBytes(4), 0);

                    m_ETTinCream.m_fTimeRangeMin3 = BitConverter.ToSingle(br.ReadBytes(4), 0);
                    m_ETTinCream.m_fTimeRangeMax3 = BitConverter.ToSingle(br.ReadBytes(4), 0);
                    m_ETTinCream.m_fTimeRangeTarget3 = BitConverter.ToSingle(br.ReadBytes(4), 0);

                    m_ETTinCream.m_fMinTemplValue3 = BitConverter.ToSingle(br.ReadBytes(4), 0);
                    m_ETTinCream.m_fMaxTemplValue3 = BitConverter.ToSingle(br.ReadBytes(4), 0);
                    m_ETTinCream.m_fTargetTemplValue3 = BitConverter.ToSingle(br.ReadBytes(4), 0);

                    // 冷却
                    m_ETTinCream.m_fMinSlopeValue4 = BitConverter.ToSingle(br.ReadBytes(4), 0);
                    m_ETTinCream.m_fMaxSlopeValue4 = BitConverter.ToSingle(br.ReadBytes(4), 0);
                    m_ETTinCream.m_fTargetSlopeValue4 = BitConverter.ToSingle(br.ReadBytes(4), 0);

                    // 水平垂直线

                    m_nHAxisNum = br.ReadInt32();
                    for (int i = 0; i < 4; i++)
                    {
                        m_fHAxis[i] = br.ReadDouble();
                    }

                    m_nVAxisNum = br.ReadInt32();
                    for (int i = 0; i < 4; i++)
                    {
                        m_fVAxis[i] = br.ReadDouble();
                    }

                    // 产品数据

                    m_ETProduct.m_strName = br.ReadString();
                    m_ETProduct.m_strProductCode = br.ReadString();
                    m_ETProduct.m_strDescription = br.ReadString();
                    m_ETProduct.m_strManufacturers = br.ReadString();
                    m_ETProduct.m_strCustomor = br.ReadString();

                    m_ETProduct.m_strProductImage[0] = br.ReadString();
                    m_ETProduct.m_strProductImage[1] = br.ReadString();
                    m_ETProduct.m_dWidth = br.ReadDouble();
                    m_ETProduct.m_dHeight = br.ReadDouble();
                    m_ETProduct.m_dThickness = br.ReadDouble();

                    for (int ii = 0; ii < 24; ii++)
                    {
                        m_ETProduct.m_SamplesPoints[ii].m_dPostionY = br.ReadDouble();
                        m_ETProduct.m_SamplesPoints[ii].m_dPostionX = br.ReadDouble();
                        m_ETProduct.m_SamplesPoints[ii].m_clrColor = ShareFunc.ConvertFromWin32Color(br.ReadInt32());
                        m_ETProduct.m_SamplesPoints[ii].m_strPointTitle = br.ReadString(); //Encoding.GetEncoding("gb2312").GetString(br.ReadBytes(8));
                        m_ETProduct.m_SamplesPoints[ii].m_nPointPostion = br.ReadInt32();
                    }

                    double dTemperatureValue;
                    int nPointCount;
                    // 数据通道                    
                    for (int ii = 0; ii < m_ETProjectInfo.m_nMeasureCount; ii++)
                    {
                        CETChannel pETChannel = new CETChannel();
                        m_ETProduct.m_SamplesPoints[ii].m_strPointTitle = br.ReadString();
                        pETChannel.m_strName = m_ETProduct.m_SamplesPoints[ii].m_strPointTitle;
                        m_ETProduct.m_SamplesPoints[ii].m_clrColor = ShareFunc.ConvertFromWin32Color(br.ReadInt32());
                        nPointCount = br.ReadInt32();
                        for (int s = 0; s < nPointCount; s++)
                        {
                            dTemperatureValue = br.ReadDouble();
                            pETChannel.m_dTemperatureValues.Add(dTemperatureValue);
                        }
                        m_ETChannels.Add(pETChannel);
                    }
                    
                }
            }
            catch
            {

            }
        }

        public void RemoveChannelData()
        {
            for (int ii = 0; ii < m_ETChannels.Count; ii++)
            {
                CETChannel pETChannel = m_ETChannels.ElementAtOrDefault(ii);
                if (pETChannel != null)
                {
                    pETChannel.m_dTemperatureValues.Clear();
                }
            }
            m_ETChannels.Clear();
        }
        public bool ParseDataFile(string strReadFilePath)
        {

            return true;
        }
        //這邊之後要改
        public void ReadConditions()
        {
            var db = new SQLiteConnection("Config.etc");
            var t = db.Query<CETAnalyseCondition>("select * from CETAnalyseCondition ");
            m_AnalyseCondition.Clear();

            if (t.Count <= 0)
            {
                m_AnalyseCondition.Add(new CETAnalyseCondition(0, true, 0, "最高温度1", 0, 0, 0, 0, true));
                m_AnalyseCondition.Add(new CETAnalyseCondition(1, true, 2, "恒温时间1", 51, 90, 60, 120, false));
                m_AnalyseCondition.Add(new CETAnalyseCondition(2, true, 1, "预热时间1", 110, 120, 50, 50, false));
                m_AnalyseCondition.Add(new CETAnalyseCondition(3, true, 2, "恒温时间2", 120, 150, 50, 50, false));
                m_AnalyseCondition.Add(new CETAnalyseCondition(4, true, 2, "恒温时间3", 120, 160, 50, 50, false));
                m_AnalyseCondition.Add(new CETAnalyseCondition(5, true, 3, "超温时间1", 232, 300, 50, 50, false));
                m_AnalyseCondition.Add(new CETAnalyseCondition(6, true, 4, "平均斜率1", 50, 150, 0, 2, false));
                m_AnalyseCondition.Add(new CETAnalyseCondition(7, true, 4, "平均斜率2", 50, 230, 50, 50, false));
                m_AnalyseCondition.Add(new CETAnalyseCondition(8, true, 5, "最高斜率1", 50, 300, 50, 50, false));
                m_AnalyseCondition.Add(new CETAnalyseCondition(9, true, 6, "升温斜率1", 50, 50, 50, 50, false));
                m_AnalyseCondition.Add(new CETAnalyseCondition(10, true, 7, "降温斜率1", 51, 50, 50, 50, false));
            }
            else
            {
                m_AnalyseCondition.AddRange(t);
            }

            /*for (int row = 0; row < t.Count; row++)
            {
                CETAnalyseCondition item = new CETAnalyseCondition();
                // 产品名称
                item.m_nID = t[row].m_nID;
                item.m_nConditionType = t[row].m_nConditionType;
                // 产品编码
                item.m_strCaption = t[row].m_strCaption;
                // 预热

                item.m_fBeginRangle = t[row].m_fBeginRangle;
                item.m_fEndRangle = t[row].m_fEndRangle;
                item.m_fBeginCondition = t[row].m_fBeginCondition;
                item.m_fEndCondition = t[row].m_fEndCondition;

                item.m_fBeginRangle1    = t[row].m_fBeginRangle1;
                item.m_fEndRangle1      = t[row].m_fEndRangle1;
                item.m_fBeginCondition1 = t[row].m_fBeginCondition1;
                item.m_fEndCondition1   = t[row].m_fEndCondition1;

                // 浸泡
                item.m_nShowStatus = t[row].m_nShowStatus;
                item.m_bReadonly = t[row].m_bReadonly;

                m_AnalyseCondition.Add(item);
            }*/
            
        }
        //這邊之後要改
        public void UpdateConditionToDB(int nID)
        {
            // 打开一个通用的流，以模板的方式向表中插入多项数据
            string strSQL;
            string strTempSql;

            
            strSQL = "update ETCondition set ";


            // 高度
            strTempSql = $"Type = {m_AnalyseCondition[nID].m_nConditionType},";
            strSQL += strTempSql;

            // 设备名称
            string strItem = m_AnalyseCondition[nID].m_strCaption;
            //ConvertGBKToUtf8(strItem);
            strTempSql = $"Titile = '{strItem}',";
            strSQL += strTempSql;

            // 高度
            strTempSql = $"StartRangle = {m_AnalyseCondition[nID].m_fBeginRangle.ToString("0.0")},";
            strSQL += strTempSql;

            // 串口号
            strTempSql = $"EndRangle = {m_AnalyseCondition[nID].m_fEndRangle.ToString("0.0")},";
            strSQL += strTempSql;

            // 高度
            strTempSql = $"StartCondition = {m_AnalyseCondition[nID].m_fBeginCondition.ToString("0.0")},";
            strSQL += strTempSql;

            // 串口号
            strTempSql = $"EndCondition = {m_AnalyseCondition[nID].m_fEndCondition.ToString("0.0")},";
            strSQL += strTempSql;


            // 高度
            strTempSql = $"StartRangle1 = {m_AnalyseCondition[nID].m_fBeginRangle1.ToString("0.0")},";
            strSQL += strTempSql;

            // 串口号
            strTempSql = $"EndRangle1 = {m_AnalyseCondition[nID].m_fEndRangle1.ToString("0.0")},";
            strSQL += strTempSql;

            // 高度
            strTempSql = $"StartCondition1 = {m_AnalyseCondition[nID].m_fBeginCondition1.ToString("0.0")},";
            strSQL += strTempSql;

            // 串口号
            strTempSql = $"EndCondition1 = {m_AnalyseCondition[nID].m_fEndCondition1.ToString("0.0")},";
            strSQL += strTempSql;

            // 高度
            strTempSql = $"IsShow = {m_AnalyseCondition[nID].m_nShowStatus} ";
            strSQL += strTempSql;

            strTempSql = $"WHERE ID = {nID};";
            strSQL += strTempSql;

            //	AfxMessageBox(strSQL);
            // 执行写操作
            //int nRows = CETCManagerApp.Instance.m_ETCDB.execDML(strSQL);
        }
        //這邊之後要改
        public void DeleteConditionFromDB(int nID)
        {
            string strSQL = $"delete from ETCondition where ID = {nID}";
            // 执行写操作
            //int nRows = CETCManagerApp.Instance.m_ETCDB.execDML(strSQL);
        }
        //這邊之後要改
        public void InsertConditionToDB(int nID)
        {
            // 打开一个通用的流，以模板的方式向表中插入多项数据
            string strSQL;
            string strTempSql;

            strSQL = "insert into ETCondition (ID,Type,Titile,StartRangle,EndRangle,StartCondition,StartRangle1,EndRangle1,StartCondition1,EndCondition1,IsShow) values ( ";

            strTempSql=$"{nID},";
            strSQL += strTempSql;

            // 高度
            strTempSql=$"{m_AnalyseCondition[nID].m_nConditionType},";
            strSQL += strTempSql;

            // 设备名称
            //ConvertGBKToUtf8(m_AnalyseCondition[nID].m_strCaption);
            strTempSql = $"'{m_AnalyseCondition[nID].m_strCaption}',";
            strSQL += strTempSql;

            // 高度
            strTempSql=$"{m_AnalyseCondition[nID].m_fBeginRangle.ToString("0.0")}, ";
            strSQL += strTempSql;

            // 高度
            strTempSql = $"{m_AnalyseCondition[nID].m_fEndRangle.ToString("0.0")},";
            strSQL += strTempSql;

            // 串口号
            strTempSql = $"{m_AnalyseCondition[nID].m_fBeginCondition.ToString("0.0")},";
            strSQL += strTempSql;

            // 高度
            strTempSql = $"{m_AnalyseCondition[nID].m_fEndCondition.ToString("0.0")},";
            strSQL += strTempSql;

            // 高度
            strTempSql = $"{m_AnalyseCondition[nID].m_fBeginRangle1.ToString("0.0")},";
            strSQL += strTempSql;

            // 高度
            strTempSql = $"{m_AnalyseCondition[nID].m_fEndRangle1.ToString("0.0")},";
            strSQL += strTempSql;

            // 串口号
            strTempSql = $"{m_AnalyseCondition[nID].m_fBeginCondition1.ToString("0.0")},";
            strSQL += strTempSql;

            // 高度
            strTempSql = $"{m_AnalyseCondition[nID].m_fEndCondition1.ToString("0.0")},";
            strSQL += strTempSql;

            // 串口号
            strTempSql=$"{m_AnalyseCondition[nID].m_nShowStatus} ); ";
            strSQL += strTempSql;

            // 执行写操作
            //int nRows = CETCManagerApp.Instance.m_ETCDB.execDML(strSQL);
        }

        //這邊之後要改
        public void SerializeFilter(string ar) { }
        public bool OpenStageDataFilter(string strFilepath)
        {
            RemoveChannelData();
            SerializeFilter(strFilepath);
            return true;
        }
        //這邊之後要改
        public void SaveConditions()
        {
            var db = new SQLiteConnection("Config.etc");
            var t1 = db.DeleteAll<CETAnalyseCondition>();
            var t2 = db.InsertAll(m_AnalyseCondition);

            return;

            string strSQL;

            strSQL = "delete from ETCondition";
            // 执行写操作
            //int nRows = theApp.m_ETCDB.execDML(strSQL);


            // Add records
            int ii = 0;
            //for (c1_Iter =; c1_Iter != m_AnalyseCondition.end(); c1_Iter++)
            foreach(CETAnalyseCondition c1_Iter in m_AnalyseCondition)
            {
                string strTempSql;

                strSQL =
                    "insert into ETCondition (ID,Type,Titile,StartRangle,EndRangle,StartCondition,EndCondition,StartRangle1,EndRangle1,StartCondition1,EndCondition1,IsShow) values ( ";

                strTempSql = $"{ii},";
                strSQL += strTempSql;

                // 高度
                strTempSql = $"{c1_Iter.m_nConditionType},";
                strSQL += strTempSql;

                // 设备名称
                string strItemText = c1_Iter.m_strCaption;
                //ConvertGBKToUtf8(strItemText);
                strTempSql = $"'{strItemText}',";
                strSQL += strTempSql;

                // 高度
                strTempSql = $"{c1_Iter.m_fBeginRangle.ToString("0.0")},";
                strSQL += strTempSql;

                // 高度
                strTempSql = $"{c1_Iter.m_fEndRangle.ToString("0.0")},";
                strSQL += strTempSql;

                // 串口号
                strTempSql = $"{c1_Iter.m_fBeginCondition.ToString("0.0")},";
                strSQL += strTempSql;

                // 高度
                strTempSql = $"{c1_Iter.m_fEndCondition.ToString("0.0")},";
                strSQL += strTempSql;

                // 高度
                strTempSql = $"{c1_Iter.m_fBeginRangle1.ToString("0.0")},";
                strSQL += strTempSql;

                // 高度
                strTempSql = $"{c1_Iter.m_fEndRangle1.ToString("0.0")},";
                strSQL += strTempSql;

                // 串口号
                strTempSql = $"{c1_Iter.m_fBeginCondition1.ToString("0.0")},";
                strSQL += strTempSql;

                // 高度
                strTempSql = $"{c1_Iter.m_fEndCondition1.ToString("0.0")},";
                strSQL += strTempSql;

                // 串口号
                strTempSql = $"{c1_Iter.m_nShowStatus} ); ";
                strSQL += strTempSql;

                // 执行写操作
                //int nRows = theApp.m_ETCDB.execDML(strSQL);
                ii++;
            }
        }
        //這邊之後要改
        public bool LoadFileRealTime(string strReadFilePath)
        {
            string strDLLPath = strReadFilePath;
            char[] buffer = new char[1024];

            try
            {
                FileStream fsFile = new FileStream(@strReadFilePath, FileMode.Open);

                int nItem;
                int IsDataBegin = 0;
                int nCount = 0;
                int idx = 0;
                CETCManagerApp.Instance.RemoveChannelData();
                while (true)
                {
                    
                    var c = (char)fsFile.ReadByte();
                    if (c != -1)
                    {
                        buffer[idx] = c;
                    }
                    else
                    {
                        break;
                    }
                    idx++;

                    //
                    string strTime = new string(buffer);
                    if (IsDataBegin == 1)
                    {
                        // 分析数据
                        if (strTime.IndexOf("End") == 0)
                        {
                            break;
                        }

                        CETCManagerApp.Instance.AddPointData(nCount, strTime);
                        nCount++;
                    }
                    else
                    {
                        string strItem = strTime.Mid(9, 15);
                        if (strTime.IndexOf("DataTime") >= 0)
                        {
                            IsDataBegin = 1;
                        }
                    }
                }

                CETCManagerApp.Instance.m_pETETCStage.m_ETProjectInfo.m_nDataTotalTime = nCount;
                //這邊之後要改
                CETCManagerApp.Instance.m_pETETCStage.m_ETProjectInfo.m_strMeasureTime = DateTime.Now.ToString("yMd-Hms");
                fsFile.Close();
                return true;
            }
            catch (Exception e)
            {
                //throw e;
                return false;
            }
        }
        public bool SaveStageDataDirect(string strFilepath)
        {
            string strFileName = strFilepath;

            Serialize(strFileName);

            return true;
        }
        public bool SaveStageDataDirect()
        {
            string TempString = $"Project1.etb";

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            //openFileDialog.Filter = "文本文件|*.*|C#文件|*.cs|所有文件|*.*";
            openFileDialog.Filter = "etb files|*.etb";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string strFileName = openFileDialog.FileName;
                if (strFileName.Right(4) != ".etb")
                {
                    strFileName += ".etb";
                }

                Serialize(strFileName);

                return true;
            }

            return false;
        }


    }


    internal static class DefineConstants
    {
        public const int CURRENT_FILE_VERSION = 0x03000000;
    }
}