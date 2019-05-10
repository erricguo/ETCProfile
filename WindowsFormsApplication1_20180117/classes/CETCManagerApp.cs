using Nini.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ETCProfiler.Enums.Statu;

namespace ETCProfiler.classes
{
    public class CETCManagerApp
    {
        public CETCManagerApp()
        {
            InitStage();
        }
        // 数据管理
        //	int nSampleRate;
        static string IniName = "etcpro.ini";
        public CETETCStage m_pETETCStage{get; set;}
        public CETETC m_ETETC { get; set; }
        public CETProjectInfo m_ETProjectInfo { get; set; } 
        public CppSQLite3DB m_ETCDB { get; set; } = new CppSQLite3DB();
        public string m_strDBFile{get; set;}
        public List<string> strCommArray { get; set; } = new List<string>();
        public System.IntPtr m_hwndDialog{get; set;}
        // 读取数据
        public int[,] m_nRealTimeData = new int[240, 12];
        // 总长度
        public int m_nRealTimeDataLength{get; set;}
        // 开始位置
        public int m_nRealStartPos{get; set;}
        // 结束位置
        public int m_nRealEndPos{get; set;} 
        public int m_IsFirstRead{get; set;}
        public int m_TotalDefaultChannelCount { get; set; } = 24;
        public bool[] m_IsShowChannel { get; set; } = new bool[24];

        public int m_nLinkStatus{get; set;} // 采样设备连接状态 0： 无设备；1：有设备连接

        public string m_strNote{get; set;}
        public string m_strProjectName{get; set;}
        public ToolButtonDrawType ToolByttonNowStatu { get; set; } = ToolButtonDrawType.None;
        public int LabelCount { get; set; } = 0;
        public int LabelIndex { get; set; } = 0;
        public int SlopesCount { get; set; } = 0;
        public int SlopesIndex { get; set; } = 0;
        //CXTPChartSeriesLabel m_pChartSeriesLabel; 暫時MARK
        //CXTPChartSeriesPoint m_pChartSeriesPoint; 暫時MARK
        public string strTemp{get; set;}

        public bool m_IsTestCommand{get; set;}

        private static CETCManagerApp m_Instance;

        public static CETCManagerApp Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new CETCManagerApp();

                }
                return m_Instance;
            }
        }

        private void InitStage()
        {
            m_pETETCStage = new CETETCStage();
            m_ETETC = new CETETC();
            m_ETProjectInfo = new CETProjectInfo();
            m_nLinkStatus = 0;
            m_pETETCStage.m_tmStartSamplesTime = DateTime.Now;
            //strTemp = m_ETETC.m_nSampleRate.ToString(); //沒作用？

            if (File.Exists(IniName))
            {
                IConfigSource mini = new IniConfigSource(IniName);
                m_pETETCStage.m_nSampleRate = mini.Configs["ETC"].GetInt("SampleRate", 1);
                m_pETETCStage.m_nTempUnit = mini.Configs["ETC"].GetInt("TempUnit", 0);
                m_pETETCStage.m_nTimeUnit = mini.Configs["ETC"].GetInt("TimeUnit", 0);
                m_pETETCStage.m_strManager[0] = mini.Configs["ETC"].GetString("Manager", "50");
                m_pETETCStage.m_strManager[1] = mini.Configs["ETC"].GetString("Manager1", "50");
                m_pETETCStage.m_strManager[2] = mini.Configs["ETC"].GetString("Manager2", "50");
            }
            else
            {
                m_pETETCStage.m_nSampleRate = 1;
                m_pETETCStage.m_nTempUnit = 0;
                m_pETETCStage.m_nTimeUnit = 0;
                m_pETETCStage.m_strManager[0] = "50";
                m_pETETCStage.m_strManager[1] = "50";
                m_pETETCStage.m_strManager[2] = "50";
            }
            if (m_pETETCStage.m_strManager[0] == "")
            {
                m_pETETCStage.m_strManager[0] = "经理确认：";
            }

            if (m_pETETCStage.m_strManager[1] == "")
            {
                m_pETETCStage.m_strManager[1] = "主管审核：";
            }

            if (m_pETETCStage.m_strManager[2] == "")
            {
                m_pETETCStage.m_strManager[2] = "测试人员：";
            }
            m_ETETC.m_nSampleRate = m_pETETCStage.m_nSampleRate;

            // 主曲线初始化
            /*for (int ii = 0; ii < 12; ii++)
            {
                CETChannel pETChannel = new CETChannel();
                pETChannel.m_strName = $"通道 {ii + 1}";
                m_pETETCStage.m_ETChannels.Add(pETChannel);
            }*/
            //m_pChartSeriesLabel = NULL; //暂时MARK
            //m_pChartSeriesPoint = NULL; //暂时MARK
            m_IsTestCommand = false;
            m_strProjectName = "";

            LoadProjectInfo();
        }

        private void LoadProjectInfo()
        {
            if (File.Exists(IniName))
            {
                IConfigSource mini = new IniConfigSource(IniName);
                m_pETETCStage.m_ETProjectInfo.m_strProjectName = mini.Configs["ProjectInfo"].GetString("ProjectName", "");
                m_pETETCStage.m_ETProjectInfo.m_strTinCream = mini.Configs["ProjectInfo"].GetString("TinCream", "");
                m_pETETCStage.m_ETProjectInfo.m_strOperator = mini.Configs["ProjectInfo"].GetString("Operator", "");
                m_pETETCStage.m_ETProjectInfo.m_strCustomer = mini.Configs["ProjectInfo"].GetString("Customer", "");
                m_pETETCStage.m_ETProduct.m_strDescription = mini.Configs["ProjectInfo"].GetString("Description", "");
                m_pETETCStage.m_ETProduct.m_strProductCode = mini.Configs["ProjectInfo"].GetString("ProductCode", "");
                m_pETETCStage.m_ETProduct.m_strName = mini.Configs["ProjectInfo"].GetString("Name", "");
                m_pETETCStage.m_ETTinCream.m_strType = mini.Configs["ProjectInfo"].GetString("Type", "");
                m_pETETCStage.m_ETTinCream.m_strManufacturers = mini.Configs["ProjectInfo"].GetString("Manufacturers", "");
                m_pETETCStage.m_ETProjectInfo.m_strMeasureDesc = mini.Configs["ProjectInfo"].GetString("MeasureDesc", "");
                m_pETETCStage.m_ETProjectInfo.m_strDataDesc = mini.Configs["ProjectInfo"].GetString("DataDesc", "");
                m_pETETCStage.m_ETProjectInfo.m_strProductionline = mini.Configs["ProjectInfo"].GetString("Productionline", "");
                m_pETETCStage.m_ETProjectInfo.m_strHaiqiLongdu = mini.Configs["ProjectInfo"].GetString("HaiqiLongdu", "");
                m_pETETCStage.m_ETTinCream.m_strName = mini.Configs["ProjectInfo"].GetString("TinCreamName", "");
            }
        }

        public string GetCurrentPath()
        {
            return Environment.CurrentDirectory;
        }

        public void RemoveChannelData()
        {
            for (int ii = 0; ii < m_pETETCStage.m_ETChannels.Count; ii++)
            {
                CETChannel pETChannel = m_pETETCStage.m_ETChannels.ElementAtOrDefault(ii);
                if (pETChannel != null)
                {
                    pETChannel.m_dTemperatureValues.Clear();
                }
            }
        }

        public bool AddPointData(int nPos, string strDataString)
        {
            if (strDataString.IndexOf("Data") == 0)
            {

                int nDataCount = strDataString.Split(',').Length;
                if (nDataCount == m_pETETCStage.m_nChannelCount - 1)
                {
                    string strLineString1 = strDataString.Substring(strDataString.Length - 5, 5);

                    for (int ii = 0; ii < m_pETETCStage.m_nChannelCount; ii++)
                    {
                        int nTempdata = 0;

                        //AfxExtractSubString(strTemp, strLineString1, ii, ','); 
                        string strTemp = strLineString1.Split(',')[ii];

                        //判断是否乱码
                        if (strTemp.IndexOf("-----") >= 0)
                        {
                            nTempdata = 0;
                        }
                        else
                        {
                            nTempdata = strTemp.ToInt();
                        }

                        CETChannel pETChannel = m_pETETCStage.m_ETChannels.ElementAtOrDefault(ii); ;
                        if (pETChannel != null)
                        {
                            double dTemperatureValue = nTempdata / 10.0;
                            //	double		dTimeValue = nPos;// / m_pETETCStage.m_nSampleRate;

                            //	m_fSampleUnit = 1.0/g_nSampleRate[m_nSampleRate];

                            if (dTemperatureValue >= 300 && pETChannel.m_dTemperatureValues.Count > 0)
                            {
                                dTemperatureValue = pETChannel.m_dTemperatureValues[pETChannel.m_dTemperatureValues.Count - 1];
                            }
                            if (pETChannel.m_dTemperatureValues.Count > 0)
                            {
                                if (Math.Abs(dTemperatureValue - pETChannel.m_dTemperatureValues[pETChannel.m_dTemperatureValues.Count - 1]) > 30)
                                {
                                    dTemperatureValue = pETChannel.m_dTemperatureValues[pETChannel.m_dTemperatureValues.Count - 1];
                                }
                            }


                            pETChannel.m_dTemperatureValues.Add(dTemperatureValue);


                        }

                    }
                }
                // 判断数据是否合法
            }
            else
            {
                return false;
            }

            return true;
        }

        void OnDataImport()
        {
            string ExtensionStr = "*.etd";
            //OpenFileDialog m_filedlg(TRUE, NULL, ExtensionStr, OFN_HIDEREADONLY | OFN_OVERWRITEPROMPT, "BMP文件(*.etd)|*.etd||");
            OpenFileDialog f = new OpenFileDialog();

            if (f.ShowDialog() == DialogResult.OK)
            {
                string m_strImagePath = f.FileName;
                m_pETETCStage.LoadFile(m_strImagePath);

                //                 CMainFrame* pFrame = (CMainFrame*)(AfxGetApp()->m_pMainWnd);
                //                 ((CETXTPChartViewAnalyse*)pFrame->m_pView)->CreateAnalyseChart();
                //                 pFrame->RefreshPanes(TRUE);
            }
        }

        public void OnFileOpen()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            //openFileDialog.Filter = "文本文件|*.*|C#文件|*.cs|所有文件|*.*";
            openFileDialog.Filter = "etb files|*.etb";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fName = openFileDialog.FileName;
                InitStage();
                m_pETETCStage.UnSerialize(fName);
                m_pETETCStage.ReadConditions();
                Main_TempChart.Instance.ShowChart(true);
                Main_TempChart.Instance.CreateAnalyseChart();               
                Main_Form.Instance.RefreshPanes(true);
                Main_DataManagement.Instance.RefreshProjectInfo();
            }
        }

        public void OnFileOpen(string fileName)
        {
            string fName = fileName;
            InitStage();
            m_pETETCStage.UnSerialize(fName);
            m_pETETCStage.ReadConditions();
            Main_TempChart.Instance.ShowChart(true);
            Main_TempChart.Instance.CreateAnalyseChart();
            Main_Form.Instance.RefreshPanes(true);
            Main_DataManagement.Instance.RefreshProjectInfo();

        }

        public void OnFileSave()
        {
            // TODO: 在此添加命令处理程序代码
            if (m_strProjectName == "")
            {
                m_pETETCStage.SaveStageData();
            }
            else
            {
                m_pETETCStage.SaveStageDataDirect(m_strProjectName);
            }

        }


        public void OnFileSaveAs()
        {
            // TODO: 在此添加命令处理程序代码
            m_pETETCStage.SaveStageData();
        }
    }
}
