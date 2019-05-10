using DevExpress.Utils;
using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using WeifenLuo.WinFormsUI.Docking;
using ETCProfiler.classes;
using DevExpress.XtraEditors;
using static ETCProfiler.classes.ShareFunc;
using static ETCProfiler.Enums.Statu;

namespace ETCProfiler
{
    public partial class Main_TempChart : XtraForm
    {
        public Main_TempChart()
        {
            InitializeComponent();
            if(m_Instance == null)
                m_Instance = this;
            chartMain.CrosshairEnabled = DefaultBoolean.False;
            m_nChartEditMode = 0;
            m_nZoomDrag = 0;
            m_pSelectChartSeriesPoint = null;
            m_pSelectChartSeries = null;

            m_IsShowAxisX = true;
            m_IsShowAxisY = true;
            m_IsShowGrid = true;
            m_pSelectAxisConstantLine = null;

            for (int ii = 0; ii < 12; ii++)
            {
                CETCManagerApp.Instance.m_IsShowChannel[ii] = true;
            }

            m_nZoomRateX = 15;
            m_nZoomRateY = 15;
            nMaxTimeCount = 0;

            m_nSlopeAddNum = 0;
            m_IsHVDrag = 0;
            m_pZoomSeries = null;
            ShowChart(false);
            m_IsEditHV = 0;
            m_pSlope = null;
            m_nShowSampleRate = 1;
        }

        public static Main_TempChart m_Instance;
        // 静态实例初始化函数
        public static Main_TempChart Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new Main_TempChart();
                }
                return m_Instance;
            }
        }

        public int m_nShowSampleRate{ get; set; }
        // 显示图形参数
        public bool m_IsShowAxisX{ get; set; }
        public bool m_IsShowAxisY{ get; set; }
        public bool m_IsShowGrid{ get; set; }
        public bool m_IsShowHV{ get; set; }


        public bool m_IsShowAllChannel{ get; set; }
        public bool m_IsShowReflowerZone{ get; set; }
        public bool m_IsShowTinCream{ get; set; }

        public bool m_IsShowNotes{ get; set; }
        public bool m_IsShowSlope { get; set; }

        public List<Point> m_AnalyseData { get; set; } = new List<Point>();


        //
        public int m_nChartEditMode{get;set;} // 编辑模式
        public int m_nSlopeAddNum{get;set;}   // 斜率模式
        public int m_nZoomDrag{get;set;}     // 框选模式
        public int m_IsHVDrag{get;set;}       // 是否水平线拖动
        public int m_nHVOrder{get;set;}      // 当前水平线选择
        public int m_IsH{get;set;}            // 是水平线还是垂直线
        public CETChartSlope m_pSlope { get; set; } = new CETChartSlope();
        ////////////////////////
        public SeriesPoint m_pSelectChartSeriesPoint { get; set; }
        public Series m_pSelectChartSeries { get; set; }
        public ConstantLine m_pSelectAxisConstantLine { get; set; }
        public ChartControl m_pChart
        {
            get
            {
                return chartMain;
            }
        }
        public SeriesCollection m_pChartSeries
        {
            get
            {
                return chartMain.Series;
            }
        }

        public int m_IsEditHV{get;set;}
        public Series m_pZoomSeries{get;set;}
        public int m_nZoomRateX{get;set;}
        public int m_nZoomRateY{get;set;}
        public int nMaxTimeCount{get;set;}
        public double[,] m_SlopePoint { get; set; } = new double[2,2];
        XYDiagram MainDiagram { get { return chartMain.Diagram as XYDiagram; } }
        static SeriesPoint basePoint = null;
        static SeriesPoint selectedPoint = null;
        Series NowSeries = null;
        int NowSlopeIndex = -1;

        public ChartControl GetChart()
        {
            return chartMain;
        }



        public void HideChartSeries(object sender, EventArgs e)
        {

        }


        private void Main_TempChart_Load(object sender, EventArgs e)
        {
            
        }

        public void ShowChart(bool v)
        {
            m_IsShowReflowerZone = false;
            m_IsShowTinCream = v;
            m_IsShowAllChannel = v;
            m_IsShowNotes = v;
            m_IsShowSlope = v;
            m_IsShowHV = false;
            m_IsShowGrid = true;            
        }

        public void CreateAnalyseChart()
        {
            int nCount = 0;
            try
            {
                //SeriesCollection pCollection = chartMain.Series;
                XYDiagram pDiagram = chartMain.Diagram as XYDiagram;

                if (m_pChartSeries != null && pDiagram != null)
                {
                    pDiagram.AxisX.ConstantLines.Clear();
                    pDiagram.AxisY.ConstantLines.Clear();
                    m_pChartSeries.Clear();                    

                    // 添加主曲线
                    CreateDataChannelChart();

                    // 显示模拟曲线 温控点和锡膏曲线
                    if (m_IsShowTinCream)
                    {
                        //	CreateTinCreamChart(pCollection);
                    }

                    if (m_IsShowReflowerZone)
                    {
                        CreateReflowerChart();
                    }
                    // 显示斜率
                    if (m_IsShowSlope)
                    {
                        CreateSlopeChart();
                    }
                    // 显示备注
                    if (m_IsShowNotes)
                    {
                        CreateNotesChart();
                    }

                    if (m_IsShowHV)
                    {
                        CreateHVChart();
                    }

                    CreateGridLines();
                    
                    // 		if (m_nChartEditMode == 3)
                    // 		{
                    // 			CXTPChartDiagram2D* pDiagram = DYNAMIC_DOWNCAST (CXTPChartDiagram2D,pCollection.GetAt (0).GetDiagram ());
                    // 			CXTPChartAxis*		pAxisX = pDiagram.GetAxisX ();
                    //
                    // 			CXTPChartAxisStrip* pAxisStrip = new CXTPChartAxisStrip();
                    // 			pAxisStrip.SetAxisMinValue (m_SlopePoint[0][0]);
                    // 			pAxisStrip.SetAxisMaxValue (m_SlopePoint[1][0]);
                    //
                    // 			pAxisX.GetStrips ().Add (pAxisStrip);
                    // 		}

                    // 添加放大曲线
                    // 		m_pZoomSeries = pCollection.Add(new Series());
                    // 		CXTPChartRangeBarSeriesStyle* pStyle = new CXTPChartRangeBarSeriesStyle();
                    // 		m_pZoomSeries.SetStyle(pStyle);
                    // 		pStyle.SetBarWidth(0.6);
                }

                // Set the X and Y Axis title for the series.
                //CXTPChartDiagram2D pDiagram = DYNAMIC_DOWNCAST(CXTPChartDiagram2D, pCollection.GetAt(0).GetDiagram());
                
                //Debug.Assert(pDiagram);

                if (pDiagram != null)
                {
                    Axis pAxisX = pDiagram.AxisX;
                    if (pAxisX != null)
                    {
                        pAxisX.Title.Text = "时间[秒]";
                        pAxisX.Title.Visibility = DefaultBoolean.True;
                        pAxisX.Title.Alignment = StringAlignment.Far;
                        //pAxisX.Alignment = AxisAlignment.Far;
                        pAxisX.Visibility = DefaultBoolean.True;
                    }

                    Axis pAxisY = pDiagram.AxisY;
                    if (pAxisY != null)
                    {

                        pAxisY.Title.Text = "温度[°C]";
                        pAxisY.Title.Visibility = DefaultBoolean.True;
                        //pAxisY.Alignment = AxisAlignment.Zero;
                        pAxisY.Visibility = DefaultBoolean.True;
                    }


                    // 设置坐标
                    pDiagram.AxisX.WholeRange.Auto = false;
                    pDiagram.AxisY.WholeRange.Auto = false;
                    pDiagram.AxisX.WholeRange.MinValue = CETCManagerApp.Instance.m_pETETCStage.m_fAxisXMin;
                    pDiagram.AxisX.WholeRange.MaxValue = CETCManagerApp.Instance.m_pETETCStage.m_fAxisXMax;
                    pDiagram.AxisX.WholeRange.SideMarginsValue = 0;

                    pDiagram.AxisY.WholeRange.MinValue = CETCManagerApp.Instance.m_pETETCStage.m_fAxisYMin;
                    pDiagram.AxisY.WholeRange.MaxValue = CETCManagerApp.Instance.m_pETETCStage.m_fAxisYMax;

                    pDiagram.AxisY.WholeRange.SideMarginsValue = 0;


                    pDiagram.AxisX.MinorCount = (int)CETCManagerApp.Instance.m_pETETCStage.m_fMinorScaleTicksX;
                    pDiagram.AxisY.MinorCount = (int)CETCManagerApp.Instance.m_pETETCStage.m_fMinorScaleTicksY;
                    // 设置可视范围
                    pDiagram.AxisX.VisualRange.Auto = false;
                    pDiagram.AxisY.VisualRange.Auto = false;
                    pDiagram.AxisX.VisualRange.MaxValue = CETCManagerApp.Instance.m_pETETCStage.m_fAxisXMax;
                    pDiagram.AxisX.VisualRange.MinValue = CETCManagerApp.Instance.m_pETETCStage.m_fAxisXMin;
                    pDiagram.AxisX.VisualRange.SideMarginsValue = 0;
                    pDiagram.AxisY.VisualRange.MaxValue = CETCManagerApp.Instance.m_pETETCStage.m_fAxisYMax;
                    pDiagram.AxisY.VisualRange.MinValue = CETCManagerApp.Instance.m_pETETCStage.m_fAxisYMin;
                    pDiagram.AxisY.VisualRange.SideMarginsValue = 0;



                    // 设置缩放
                    pDiagram.EnableAxisXScrolling = true;
                    pDiagram.EnableAxisYScrolling = true;
                    pDiagram.EnableAxisXZooming = true;

                    //pDiagram.AxisX.WholeRange.zoo SetZoomLimit(10);


                    pDiagram.AxisX.Color = Color.FromArgb(255, 0, 0);
                    //pDiagram.AxisY.WholeRange. GetRange().SetZoomLimit(5);
                    //pDiagram.AxisX.GridLines.Visible = true;
                    //pDiagram.AxisY.GridLines.Visible = true;


                    if (m_pChartSeries.Count > 0)
                    {
                        Series pSeries = m_pChartSeries[0];

                        nCount = pSeries.Points.Count / CETCManagerApp.Instance.m_pETETCStage.m_nSampleRate;

                        pDiagram.AxisX.WholeRange.MaxValue = nCount;
                        pDiagram.AxisX.VisualRange.MaxValue = nCount;
                        /*if (nCount > CETCManagerApp.Instance.m_pETETCStage.m_fAxisXMax)
                        {
                            var pRange = pDiagram.AxisX.WholeRange;
                            pRange.MaxValue = nCount;





                            if (pRange.GetViewMaxValue() == pRange.GetMaxValue())
                            {
                                double delta = pRange.GetViewMaxValue() - pRange.GetViewMinValue();

                                pRange.SetViewAutoRange(0);
                                pRange.SetViewMaxValue(delta);
                                pRange.SetViewMinValue(0);
                            }
                        }*/
                    }
                }
                else
                {
                    m_pChartSeries.Add(new Series());
                    XYDiagram pDiagram2 = chartMain.Diagram as XYDiagram;
                    if(pDiagram2 != null)
                    {
                        pDiagram2.AxisX.WholeRange.Auto = false;
                        pDiagram2.AxisX.WholeRange.SideMarginsValue = 0;
                        pDiagram2.AxisX.WholeRange.MaxValue = 600;
                        pDiagram2.AxisX.WholeRange.MinValue = 0;

                        pDiagram2.AxisY.WholeRange.Auto = false;
                        pDiagram2.AxisY.WholeRange.SideMarginsValue = 0;
                        pDiagram2.AxisY.WholeRange.MaxValue = 300;
                        pDiagram2.AxisY.WholeRange.MinValue = 0;
                    }
                }
            }
            catch(Exception e)
            {
                new BoxMsg(e.Message).ShowDialog();
            }
        }

        

        public int CreateDataChannelChart()
        {
            int nCount = 0;

            int nTempSamplesRate = 0;
            if (CETCManagerApp.Instance.m_pETETCStage.m_nSampleRate >= m_nShowSampleRate)
            {
                nTempSamplesRate = m_nShowSampleRate;
            }
            else
            {
                nTempSamplesRate = CETCManagerApp.Instance.m_pETETCStage.m_nSampleRate;
                m_nShowSampleRate = CETCManagerApp.Instance.m_pETETCStage.m_nSampleRate;
            }

            // 	CXTPChartDiagram2D*			pDiagram = DYNAMIC_DOWNCAST (CXTPChartDiagram2D,
            // 		pCollection.GetAt (0).GetDiagram ());
            //
            // 	CXTPChartAxisRange* pRange = pDiagram.GetAxisX ().GetRange ();
            // 	pRange.SetMaxValue (nCount);
            //
            // 	if (pRange.GetViewMaxValue () < 30 )
            // 	{
            // 		nTempSamplesRate = Frm_Main.GetMainForm().m_pETETCStage.m_nSampleRate;
            // 	}

            // 通道
            for (int ii = 0; ii < CETCManagerApp.Instance.m_pETETCStage.m_nChannelCount; ii++)
            {
                Series pSeries = null;                
                CETChannel pETChannel = CETCManagerApp.Instance.m_pETETCStage.m_ETChannels.ElementAtOrDefault(ii);
                if (pETChannel != null)
                {

                    pSeries = new Series(CETCManagerApp.Instance.m_pETETCStage.m_ETProduct.m_SamplesPoints[ii].m_strPointTitle,ViewType.Line);
                    pSeries.View.Color = CETCManagerApp.Instance.m_pETETCStage.m_ETProduct.m_SamplesPoints[ii].m_clrColor;
                    pSeries.Tag = new SuperTag() { SeriesMark = $"Channel{ii}" };
                    pSeries.ArgumentScaleType = ScaleType.Numerical;
                    //pSeries.ArgumentDataMember = "時間";
                    pSeries.ValueScaleType = ScaleType.Numerical;
                    ((LineSeriesView)pSeries.View).LineStyle.Thickness = 2;
                    //pSeries.ValueDataMembers.AddRange(new string[] { mField });
                    nCount = 0;

                    double dTemperatureValue;

                    for (int s = 0; s < pETChannel.m_dTemperatureValues.Count; s++)
                    {
                        if (s % (m_nShowSampleRate / nTempSamplesRate) == 0)
                        {
                            dTemperatureValue = pETChannel.m_dTemperatureValues[s];
                            float dSamplePeriod = (float)(nCount + 1) / CETCManagerApp.Instance.m_pETETCStage.m_nSampleRate;
                            SeriesPoint pSeriesPoint = new SeriesPoint(dSamplePeriod, dTemperatureValue);                            
                            pSeries.Points.Add(pSeriesPoint);
                        }
                        nCount++;
                    }


                    // 				for (int s = 0; s < pETChannel.m_dTemperatureValues.GetCount(); s++)
                    // 				{
                    // 					if (s % Frm_Main.GetMainForm().m_pETETCStage.m_nSampleRate == 0)
                    // 					{
                    // 						dTemperatureValue = pETChannel.m_dTemperatureValues[s];
                    // 						float dSamplePeriod = (double)(nCount + 1) / Frm_Main.GetMainForm().m_pETETCStage.m_nSampleRate;// / nTempSamplesRate;
                    // 						SeriesPoint* pSeriesPoint = new SeriesPoint(dSamplePeriod, dTemperatureValue);
                    // 						pSeries.GetPoints().Add(pSeriesPoint);
                    // 					}
                    //
                    // 					nCount++;
                    // 				}

                    if (CETCManagerApp.Instance.m_IsShowChannel[ii])
                    {
                        pSeries.Visible = true;
                    }
                    else
                    {
                        pSeries.Visible = false;
                    }

                    nMaxTimeCount = pETChannel.m_dTemperatureValues.Count;
                    m_pChartSeries.Add(pSeries);
                }
                
            }

            return nCount;
        }

        public void UpdateChannelColor()
        {
            for (int ii = 0; ii < m_pChartSeries.Count; ii++)
            {
                if(((SuperTag)m_pChartSeries[ii].Tag) != null)
                {
                    if(((SuperTag)m_pChartSeries[ii].Tag).SeriesMark.StartsWith("Channel"))
                    {
                        string strTemp = "";                        
                        AfxExtractSubString(ref strTemp, ((SuperTag)m_pChartSeries[ii].Tag).SeriesMark, 1, "Channel");
                        int idx = atoi(strTemp);
                        if(idx >= 0)
                            m_pChartSeries[idx].View.Color = CETCManagerApp.Instance.m_pETETCStage.m_ETProduct.m_SamplesPoints[idx].m_clrColor;
                    }
                }               
            }   
        }

        // 回流炉
        public void CreateReflowerChart()
        {
            XYDiagram pDiagram = chartMain.Diagram as XYDiagram;
            //ASSERT(pDiagram);
            if (pDiagram != null)
            {
                for (int i = m_pChartSeries.Count - 1; i >= 0; i--)
                {
                    if (((SuperTag)m_pChartSeries[i].Tag)?.SeriesMark == "Reflower")
                    {
                        m_pChartSeries.RemoveAt(i);
                    } 
                }

                /*for (int i = m_pChartSeries.Count - 1; i >= 0; i--)
                {
                    if (m_pChartSeries[i].Tag?.ToString() == "Reflower")
                    {
                        m_pChartSeries.RemoveAt(i);
                    }
                }*/

                for (int i = pDiagram.AxisX.ConstantLines.Count - 1; i >= 0; i--)
                {
                    if (((SuperTag)pDiagram.AxisX.ConstantLines[i].Tag)?.AxisXConstantLine == "Reflower")
                    {
                        pDiagram.AxisX.ConstantLines.RemoveAt(i);
                    }
                }

                for (int i = pDiagram.AxisY.ConstantLines.Count - 1; i >= 0; i--)
                {
                    if (((SuperTag)pDiagram.AxisY.ConstantLines[i].Tag)?.AxisYConstantLine == "Reflower")
                    {
                        pDiagram.AxisY.ConstantLines.RemoveAt(i);
                    }
                }

                if (m_IsShowReflowerZone)
                {


                    double dReflowerlenth = CETCManagerApp.Instance.m_pETETCStage.m_ETReflower.m_fInitTempl;
                    var pReflowerAreaData_Hot = CETCManagerApp.Instance.m_pETETCStage.m_ETReflower.m_CAreaData["Hot"];
                    var pReflowerAreaData_Cool = CETCManagerApp.Instance.m_pETETCStage.m_ETReflower.m_CAreaData["Cool"];

                    for (int ii = 0; ii < CETCManagerApp.Instance.m_pETETCStage.m_ETReflower.m_nSampleHeaterAreaCount; ii++)
                    {
                        Series pSeries = new Series("", ViewType.Line);
                        CETReflower pETReflower = CETCManagerApp.Instance.m_pETETCStage.m_ETReflower;

                        pSeries.CrosshairEnabled = DefaultBoolean.False;
                        pSeries.View.Color = Color.FromArgb(0, 255, 0);
                        pSeries.ArgumentScaleType = ScaleType.Numerical;
                        pSeries.ValueScaleType = ScaleType.Numerical;
                        pSeries.ShowInLegend = false;
                        pSeries.Tag = new SuperTag() { SeriesMark = "Reflower" };

                        ((LineSeriesView)pSeries.View).LineStyle.Thickness = 2;

                        // 竖线
                        // 获得时间
                        double dZonePosX = (dReflowerlenth / pETReflower.m_fSpeed) * 60.0;
                        double dTempWidth = (pReflowerAreaData_Hot[ii].m_fAreaLength / pETReflower.m_fSpeed) * 60.0;

                        SeriesPoint pSeriesPoint = new SeriesPoint(dZonePosX, pReflowerAreaData_Hot[ii].m_fAreaTemplTop);


                        pSeries.Points.Add(pSeriesPoint);
                        pSeriesPoint = new SeriesPoint(dZonePosX + dTempWidth, pReflowerAreaData_Hot[ii].m_fAreaTemplTop);
                        pSeries.Points.Add(pSeriesPoint);

                        // 横线
                        AxisX pAxisX = pDiagram.AxisX;
                        if (pAxisX != null)
                        {
                            var pAxisCustomLabels = pAxisX.ConstantLines;
                            ConstantLine pAxisCustomLabel = new ConstantLine();

                            // 获得时间
                            pAxisCustomLabel.AxisValue = dZonePosX.ToString("n1");

                            if (ii != CETCManagerApp.Instance.m_pETETCStage.m_ETReflower.m_nSampleHeaterAreaCount)
                            {
                                string strTingTemp = string.Format(" [{0:n1}°C]", pReflowerAreaData_Hot[ii].m_fAreaTemplTop);

                                pAxisCustomLabel.Title.Text = pReflowerAreaData_Hot[ii].m_strAreaTitle + strTingTemp;
                                pAxisCustomLabel.Title.Alignment = ConstantLineTitleAlignment.Far;
                            }

                            pAxisCustomLabel.Color = Color.FromArgb(0, 128, 0);
                            pAxisCustomLabel.Title.TextColor = Color.FromArgb(0, 128, 0);
                            pAxisCustomLabel.LineStyle.DashStyle = DashStyle.Dash;
                            pAxisCustomLabel.LineStyle.Thickness = 2;
                            pAxisCustomLabel.ShowInLegend = false;
                            pAxisCustomLabel.Tag = new SuperTag() { AxisXConstantLine = "Reflower" };
                            pAxisCustomLabels.Add(pAxisCustomLabel);

                            pSeries.Name = pAxisCustomLabel.AxisValue.ToString();
                        }

                        if (ii != CETCManagerApp.Instance.m_pETETCStage.m_ETReflower.m_nSampleHeaterAreaCount - 1)
                        {
                            dReflowerlenth += pReflowerAreaData_Hot[ii].m_fAreaLength;
                            m_pChartSeries.Add(pSeries);
                        }
                        else
                        {
                            if (CETCManagerApp.Instance.m_pETETCStage.m_ETReflower.m_nSampleCoolAreaCount > 0)
                            {
                                //dReflowerlenth += pReflowerAreaData_Hot[ii].m_fAreaLength;
                                dReflowerlenth += pReflowerAreaData_Cool[0].m_fAreaLength;
                                m_pChartSeries.Add(pSeries);
                            }
                        }

                    }

                    
                    // 		//
                    for (int ii = 0; ii < CETCManagerApp.Instance.m_pETETCStage.m_ETReflower.m_nSampleCoolAreaCount; ii++)
                    {
                        Series pSeries = new Series("", ViewType.Line);
                        CETReflower pETReflower = CETCManagerApp.Instance.m_pETETCStage.m_ETReflower;

                        pSeries.View.Color = Color.FromArgb(0, 255, 0);
                        pSeries.CrosshairEnabled = DefaultBoolean.False;
                        pSeries.ArgumentScaleType = ScaleType.Numerical;
                        pSeries.ValueScaleType = ScaleType.Numerical;
                        pSeries.ShowInLegend = false;
                        pSeries.Tag = new SuperTag() { SeriesMark = "Reflower" };
                        ((LineSeriesView)pSeries.View).LineStyle.Thickness = 2;

                        // 竖线
                        // 获得时间
                        double dZonePosX = (dReflowerlenth / pETReflower.m_fSpeed) * 60.0;//Frm_Main.GetMainForm().m_pETETCStage.m_nSampleRate*60;
                        //double dTempWith = (pETReflower.m_fAreaLength[ii + 12] / pETReflower.m_fSpeed) * 60.0;//Frm_Main.GetMainForm().m_pETETCStage.m_nSampleRate*60;
                        double dTempWith = (pReflowerAreaData_Cool[ii].m_fAreaLength / pETReflower.m_fSpeed) * 60.0;

                        //
                        //SeriesPoint pSeriesPoint = new SeriesPoint(dZonePosX, pETReflower.m_fAreaTemplTop[ii + 12]);
                        SeriesPoint pSeriesPoint = new SeriesPoint(dZonePosX, pReflowerAreaData_Cool[ii].m_fAreaTemplTop);
                        pSeries.Points.Add(pSeriesPoint);
                        //pSeriesPoint = new SeriesPoint(dZonePosX + dTempWith, pETReflower.m_fAreaTemplTop[ii + 12]);
                        pSeriesPoint = new SeriesPoint(dZonePosX + dTempWith, pReflowerAreaData_Cool[ii].m_fAreaTemplTop);
                        pSeries.Points.Add(pSeriesPoint);


                        //
                        // 横线
                        Axis pAxisX = pDiagram.AxisX;
                        if (pAxisX != null)
                        {
                            var pAxisCustomLabels = pAxisX.ConstantLines;
                            ConstantLine pAxisCustomLabel = new ConstantLine();

                            // 获得时间
                            pAxisCustomLabel.AxisValue = dZonePosX.ToString("n1");

                            if (ii != CETCManagerApp.Instance.m_pETETCStage.m_ETReflower.m_nSampleCoolAreaCount)
                            {
                                //string strTingTemp = string.Format(" [{0:n1}°C]", pETReflower.m_fAreaTemplTop[ii + 12]);
                                string strTingTemp = string.Format(" [{0:n1}°C]", pReflowerAreaData_Cool[ii].m_fAreaTemplTop);
                                //pAxisCustomLabel.Title.Text = pETReflower.m_strAreaTitle[ii + 12] + strTingTemp;
                                pAxisCustomLabel.Title.Text = pReflowerAreaData_Cool[ii].m_strAreaTitle + strTingTemp;
                                pAxisCustomLabel.Title.Alignment = ConstantLineTitleAlignment.Far;

                            }

                            pAxisCustomLabel.Color = Color.FromArgb(0, 128, 0);
                            pAxisCustomLabel.Title.TextColor = Color.FromArgb(0, 128, 0);
                            pAxisCustomLabel.LineStyle.DashStyle = DashStyle.Dash;
                            pAxisCustomLabel.LineStyle.Thickness = 2;
                            pAxisCustomLabel.ShowInLegend = false;
                            pAxisCustomLabel.Tag = new SuperTag() { AxisXConstantLine = "Reflower" };
                            pAxisCustomLabels.Add(pAxisCustomLabel);

                            pSeries.Name = pAxisCustomLabel.AxisValue.ToString();
                        }
                        if (ii != CETCManagerApp.Instance.m_pETETCStage.m_ETReflower.m_nSampleCoolAreaCount - 1)
                        {
                            //dReflowerlenth += pETReflower.m_fAreaLength[ii + 12];
                            dReflowerlenth += pReflowerAreaData_Cool[ii].m_fAreaLength;
                            m_pChartSeries.Add(pSeries);
                        }
                    }
                    //

                    /*Series pSeries2 = new Series("", ViewType.Line);
                    CETReflower pETReflower2 = Frm_Main.GetMainForm().m_pETETCStage.m_ETReflower;

                    pSeries2.View.Color = Color.FromArgb(0, 255, 0);

                    pSeries2.ArgumentScaleType = ScaleType.Numerical;
                    //pSeries2.ArgumentDataMember = "時間";
                    pSeries2.ValueScaleType = ScaleType.Numerical;
                    pSeries2.ShowInLegend = false;
                    ((LineSeriesView)pSeries2.View).LineStyle.Thickness = 2;

                    // 竖线
                    //
                    // 横线
                    Axis pAxisX2 = pDiagram.AxisX;
                    if (pAxisX2 != null)
                    {
                        // 获得时间
                        var pAxisCustomLabels = pAxisX2.ConstantLines;
                        ConstantLine pAxisCustomLabel = new ConstantLine();
                        string strAxis = ((pETReflower2.m_fInitTempl / pETReflower2.m_fSpeed) * 60.0).ToString();
                        pAxisCustomLabel.AxisValue = string.Format("n1", strAxis);
                        pAxisCustomLabel.Color = Color.FromArgb(255, 0, 0);
                        pAxisCustomLabel.Title.TextColor = Color.FromArgb(255, 0, 0);
                        pAxisCustomLabel.LineStyle.DashStyle = DashStyle.Solid;
                        pAxisCustomLabel.LineStyle.Thickness = 2;
                        pAxisCustomLabel.ShowInLegend = false;
                        pAxisCustomLabels.Add(pAxisCustomLabel);
                        pSeries2.Name = pAxisCustomLabel.AxisValue.ToString();
                    }

                    pCollection.Add(pSeries2);*/
                }
            }
        }

        public void CreateSlopeChart()
        {
            for (int i = chartMain.Series.Count - 1; i >= 0; i--)
            {
                try
                {
                    if (chartMain.Series[i].Name.StartsWith("Slope"))
                    {
                        chartMain.Series.RemoveAt(i);
                    }

                }
                catch (Exception)
                {
                    throw;
                }
            }

            if (m_IsShowSlope)
            {
                // 模拟参数曲线
                for (int ii = 0; ii < CETCManagerApp.Instance.m_pETETCStage.m_ETCSlopes.Count; ii++)
                {
                    var pERSlope = CETCManagerApp.Instance.m_pETETCStage.m_ETCSlopes.ElementAtOrDefault(ii);
                    if (pERSlope != null)
                    {
                        var cloneSlop = (Series)pERSlope.Clone();
                        string mName = ((TextAnnotation)pERSlope.Points[0].Annotations[0]).Name;
                        string value = ((TextAnnotation)pERSlope.Points[0].Annotations[0]).Text;
                        cloneSlop.Points[0].Annotations.AddTextAnnotation(mName, value);

                        chartMain.Series.Add(cloneSlop);
                    }
                }
            }

            // 斜率
            /*for (int ii = 0; ii < CETCManagerApp.Instance.m_pETETCStage.m_ETCSlopes.Count; ii++)
            {
                //Series pSeries = new Series("", ViewType.Line);
                var pETSlope = CETCManagerApp.Instance.m_pETETCStage.m_ETCSlopes.ElementAtOrDefault(ii);
                pSeries.Name = pETSlope.m_strTitle;
                pSeries.ShowInLegend = false;
                pSeries.ArgumentScaleType = ScaleType.Numerical;
                pSeries.ValueScaleType = ScaleType.Numerical;
                ((LineSeriesView)pSeries.View).LineStyle.Thickness = 2;

                SeriesPoint pSeriesPoint = new SeriesPoint(pETSlope.m_nXValue,pETSlope.m_nYValue);
                string strSlope;
                float fSlope = (pETSlope.m_nYValue1 - pETSlope.m_nYValue) / 
                                 (pETSlope.m_nXValue1 - pETSlope.m_nXValue);
                strSlope = string.Format("k={0:n1}", fSlope);
                pSeriesPoint.Annotations.AddTextAnnotation();
                ((TextAnnotation)pSeriesPoint.Annotations[0]).Text = strSlope;

                pSeries.Points.Add(pSeriesPoint);
                pSeriesPoint = new SeriesPoint(pETSlope.m_nXValue1, pETSlope.m_nYValue1);
                pSeriesPoint.Annotations.AddTextAnnotation();
                pSeries.Points.Add(pSeriesPoint);
                m_pChartSeries.Add(pSeries);
            }*/

        }

        public void CreateNotesChart()
        {
            for (int i = chartMain.AnnotationRepository.Count-1; i >= 0 ; i--)
            {
                try
                {
                    if (chartMain.AnnotationRepository[i].Name.StartsWith("Annotation"))
                    {
                        chartMain.AnnotationRepository.RemoveAt(i);
                    }

                }
                catch (Exception)
                {
                    continue;
                }
            }

            if (m_IsShowNotes)
            {
                // 模拟参数曲线
                for (int ii = 0; ii < CETCManagerApp.Instance.m_pETETCStage.m_ETCNotes.Count; ii++)
                {
                    TextAnnotation pETNote = CETCManagerApp.Instance.m_pETETCStage.m_ETCNotes.ElementAtOrDefault(ii);
                    if (pETNote != null)
                    {
                        chartMain.AnnotationRepository.Add((TextAnnotation)pETNote.Clone());
                    }
                }
            }
        }

        public void CreateHVChart()
        {
            XYDiagram pDiagram = chartMain.Diagram as XYDiagram;
            if (pDiagram != null)
            {
                
                for (int i = pDiagram.AxisX.ConstantLines.Count - 1; i >= 0; i--)
                {
                    if (((SuperTag)pDiagram.AxisX.ConstantLines[i].Tag)?.AxisXConstantLine == "HV")
                        pDiagram.AxisX.ConstantLines.RemoveAt(i);
                }
                for (int i = pDiagram.AxisY.ConstantLines.Count - 1; i >= 0; i--)
                {
                    if (((SuperTag)pDiagram.AxisY.ConstantLines[i].Tag)?.AxisYConstantLine == "HV")
                        pDiagram.AxisY.ConstantLines.RemoveAt(i);
                }

                if (m_IsShowHV)
                {
                    for (int ii = 0; ii < CETCManagerApp.Instance.m_pETETCStage.m_nHAxisNum; ii++)
                    {
                        Axis pAxisX = pDiagram.AxisX;
                        if (pAxisX != null)
                        {
                            var pAxisConstantLines = pAxisX.ConstantLines;
                            ConstantLine pAxisConstantLine = new ConstantLine();

                            string strAxis = string.Format("{0:n1}", CETCManagerApp.Instance.m_pETETCStage.m_fHAxis[ii]);
                            pAxisConstantLine.AxisValue = strAxis;
                            strAxis = string.Format("X{0}:[{1:n1}]", ii + 1, CETCManagerApp.Instance.m_pETETCStage.m_fHAxis[ii]);

                            pAxisConstantLine.ShowInLegend = false;
                            pAxisConstantLine.Title.Text = strAxis;
                            pAxisConstantLine.Title.Alignment = ConstantLineTitleAlignment.Near;

                            pAxisConstantLine.Color = Color.FromArgb(255, 0, 0);
                            pAxisConstantLine.Tag = new SuperTag() { AxisXConstantLine = "HV" };
                            pAxisConstantLines.Add(pAxisConstantLine);
                        }
                    }

                    for (int ii = 0; ii < CETCManagerApp.Instance.m_pETETCStage.m_nVAxisNum; ii++)
                    {
                        Axis pAxisY = pDiagram.AxisY;
                        if (pAxisY != null)
                        {
                            var pAxisCustomLabels = pAxisY.ConstantLines;
                            ConstantLine pAxisCustomLabel = new ConstantLine();

                            string strAxis = string.Format("{0:n1}", CETCManagerApp.Instance.m_pETETCStage.m_fVAxis[ii]);
                            pAxisCustomLabel.AxisValue = strAxis;
                            strAxis = string.Format("Y{0}:[{1:n1}]", ii + 1, CETCManagerApp.Instance.m_pETETCStage.m_fVAxis[ii]);

                            pAxisCustomLabel.ShowInLegend = false;
                            pAxisCustomLabel.Title.Text = strAxis;
                            pAxisCustomLabel.Title.Alignment = ConstantLineTitleAlignment.Far;
                            pAxisCustomLabel.Color = Color.FromArgb(0, 0, 0);
                            pAxisCustomLabel.Tag = new SuperTag() { AxisYConstantLine = "HV" };
                            pAxisCustomLabels.Add(pAxisCustomLabel);
                        }
                    }
                }
            }
        }

        public void CreateGridLines()
        {
            XYDiagram pDiagram = chartMain.Diagram as XYDiagram;
            if (pDiagram != null)
            {
                Axis pAxisX = pDiagram.AxisX;
                Axis pAxisY = pDiagram.AxisY;

                if (pAxisX != null)
                {
                    pAxisX.GridLines.Visible = m_IsShowGrid;
                }

                if (pAxisY != null)
                {
                    pAxisY.GridLines.Visible = m_IsShowGrid;
                }
            }
        }

        public int GetSeriesPointsCount()
        {
            if (chartMain.Series.Count > 0)
                return chartMain.Series[0].Points.Count;
            else
                return 0;
        }

        private void chartMain_MouseUp(object sender, MouseEventArgs e)
        {
            if (chartMain.Series.Count <= 0)
            {
                return;
            }


            switch (CETCManagerApp.Instance.ToolByttonNowStatu)
            {
                case ToolButtonDrawType.None:
                    break;
                case ToolButtonDrawType.LABEL:
                    if (e.Button == MouseButtons.Left)
                    {
                        TextAnnotation annotation = new TextAnnotation("Annotation" + (++CETCManagerApp.Instance.LabelCount).ToString(), "標籤" + (++CETCManagerApp.Instance.LabelIndex).ToString());
                        DiagramCoordinates point = MainDiagram.PointToDiagram(e.Location);
                        annotation.AnchorPoint = new PaneAnchorPoint();
                        ((PaneAnchorPoint)annotation.AnchorPoint).AxisXCoordinate.AxisValue = point.NumericalArgument;
                        ((PaneAnchorPoint)annotation.AnchorPoint).AxisYCoordinate.AxisValue = point.NumericalValue;
                        annotation.ShapePosition = new FreePosition();
                        FreePosition position = annotation.ShapePosition as FreePosition;
                        position.InnerIndents.Left = e.X + 5;
                        position.InnerIndents.Top = e.Y - annotation.Height - 5;
                        chartMain.AnnotationRepository.Add((TextAnnotation)annotation.Clone());
                        Main_Form.Instance.SetToolButtonStatu(ToolButtonDrawType.None);
                        CETCManagerApp.Instance.m_pETETCStage.m_ETCNotes.Add(annotation);
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        
                    }
                    break;
                case ToolButtonDrawType.SLOP:
                    if (e.Button == MouseButtons.Left)
                    {
                        if(NowSeries == null)
                        {
                            var slope = new Series("Slope" + (++CETCManagerApp.Instance.SlopesCount).ToString(), ViewType.Line);

                            //slope.View.Color = Color.Red;
                            ((LineSeriesView)slope.View).LineStyle.Thickness = 2;
                            //((LineSeriesView)slope.View).ColorEach = true;
                            ((LineSeriesView)slope.View).MarkerVisibility = DefaultBoolean.True;

                            DiagramCoordinates point = MainDiagram.PointToDiagram(e.Location);
                            
                            slope.Points.Add(new SeriesPoint(point.NumericalArgument, point.NumericalValue));
                            slope.Points.Add(new SeriesPoint(point.NumericalArgument, point.NumericalValue));
                            //slope.LabelsVisibility = DefaultBoolean.True;
                            slope.Label.Border.Visibility = DefaultBoolean.True;
                            slope.CrosshairEnabled = DefaultBoolean.False;
                            slope.Visible = true;
                            slope.Label.TextColor = Color.Black;
                            slope.ShowInLegend = false;                         
                            slope.Points[0].Color = slope.View.Color;
                            slope.Points[0].Annotations.AddTextAnnotation(slope.Name, "12345");
                            slope.Points[1].Color = slope.View.Color;
                            slope.Tag = new SuperTag() { SeriesIsDrawDown = true };
                            CETCManagerApp.Instance.m_pETETCStage.m_ETCSlopes.Add(slope);
                            NowSlopeIndex = CETCManagerApp.Instance.m_pETETCStage.m_ETCSlopes.Count - 1;
                            var cloneSlope = (Series)slope.Clone();
                            cloneSlope.Points[0].Annotations.AddTextAnnotation(slope.Name, "12345");
                            basePoint = cloneSlope.Points[0];
                            selectedPoint = cloneSlope.Points[1];
                            chartMain.Series.Add(cloneSlope);
                            NowSeries = cloneSlope;
                            //chartMain.CustomDrawSeriesPoint += chartMain_CustomDrawSeriesPoint;
                        }
                        else
                        {
                            
                            CETCManagerApp.Instance.m_pETETCStage.m_ETCSlopes[NowSlopeIndex].Points.RemoveAt(1);
                            CETCManagerApp.Instance.m_pETETCStage.m_ETCSlopes[NowSlopeIndex].Points.Add(new SeriesPoint(selectedPoint.NumericalArgument, selectedPoint.Values[0]));
                            ((TextAnnotation)CETCManagerApp.Instance.m_pETETCStage.m_ETCSlopes[NowSlopeIndex].Points[0].Annotations[0]).Text = ((TextAnnotation)basePoint.Annotations[0]).Text;
                            ((SuperTag)CETCManagerApp.Instance.m_pETETCStage.m_ETCSlopes[NowSlopeIndex].Tag).SeriesLabelString = ((TextAnnotation)basePoint.Annotations[0]).Text;
                            ((SuperTag)NowSeries.Tag).SeriesIsDrawDown = false;
                            NowSeries = null;
                            selectedPoint = null;
                            Main_Form.Instance.SetToolButtonStatu(ToolButtonDrawType.None);
                            //chartMain.CustomDrawSeriesPoint -= chartMain_CustomDrawSeriesPoint;
                        }

                    }
                    break;
                default:
                    break;
            }
        }

        private void chartMain_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        {
            if (CETCManagerApp.Instance.ToolByttonNowStatu != ToolButtonDrawType.SLOP) return;
            
            if (e.Series.Name.StartsWith("Slope"))
            {
                if (e.Series.Points.IndexOf(e.SeriesPoint) != 0)
                    e.LabelText = string.Empty;
                else
                {
                    if (((SuperTag)e.Series.Tag).SeriesIsDrawDown)
                    {
                        try
                        {
                            
                            double a = selectedPoint.NumericalArgument - basePoint.NumericalArgument;
                            double b = selectedPoint.Values[0] - basePoint.Values[0];
                            double slope = -a / b * 1.0;

                            e.LabelText = "K=" + slope.ToString("n1");
                            ((SuperTag)e.Series.Tag).SeriesLabelString = e.LabelText;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else
                    {
                        e.LabelText = ((SuperTag)e.Series.Tag).SeriesLabelString;
                    }
                }
                
            }
        }

        private void chartMain_ObjectHotTracked(object sender, HotTrackEventArgs e)
        {
            if (selectedPoint != null && (CETCManagerApp.Instance.ToolByttonNowStatu == ToolButtonDrawType.SLOP))
            {
                DiagramCoordinates point = MainDiagram.PointToDiagram(e.HitInfo.HitPoint);

                selectedPoint.NumericalArgument = point.NumericalArgument;

                selectedPoint.Values[0] = point.NumericalValue;


                double a = selectedPoint.NumericalArgument - basePoint.NumericalArgument;
                double b = selectedPoint.Values[0] - basePoint.Values[0];
                double slope = -a / b * 1.0;
                //((SuperTag)e.Series.Tag).SeriesLabelString = e.LabelText;

                ((TextAnnotation)basePoint.Annotations[0]).Text = "K=" + slope.ToString("n1");

                ((ChartControl)sender).RefreshData();
                return;
            }
        }

        private void chartMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //if (!CheckDataExist()) return;
            switch(CETCManagerApp.Instance.ToolByttonNowStatu)
            {
                case ToolButtonDrawType.None:
                    if (e.Button == MouseButtons.Left)
                    {
                        ChartHitInfo hi = this.chartMain.CalcHitInfo(e.Location);

                        if (hi.InAnnotation)
                        {
                            TextAnnotation an = (TextAnnotation)hi.Annotation;

                            IDD_DIALOG2 si = new IDD_DIALOG2("標籤屬性", an.Text);
                            if (si.ShowDialog() == DialogResult.OK)
                            {
                                if (an.Name.StartsWith("Annotation"))
                                {
                                    TextAnnotation annotation =
                                (TextAnnotation)chartMain.AnnotationRepository.GetElementByName(an.Name);
                                    if (annotation != null)
                                    {
                                        foreach (TextAnnotation t in CETCManagerApp.Instance.m_pETETCStage.m_ETCNotes)
                                        {
                                            if (t.Name == an.Name)
                                            {
                                                t.Text = si.InputText;
                                                break;
                                            }
                                        }
                                        annotation.Text = si.InputText;
                                    }
                                }
                                else if(an.Name.StartsWith("Slope"))
                                {
                                    TextAnnotation slope = (TextAnnotation)hi.Annotation;
                                    
                                    if (slope != null)
                                    {
                                        foreach (Series s2 in CETCManagerApp.Instance.m_pETETCStage.m_ETCSlopes)
                                        {
                                            if (s2.Name == slope.Name)
                                            {
                                                slope.Text = si.InputText;
                                                ((TextAnnotation)s2.Points[0].Annotations[0]).Text = si.InputText;
                                                //((SuperTag)s2.Tag).SeriesLabelString = si.InputText;
                                                break;
                                            }
                                        }
                                    }
                                }
                                
                            }
                        }
                        /*else if (hi.InSeriesLabel)
                        {
                            Series s = (Series)hi.Series;
                            IDD_DIALOG2 si = new IDD_DIALOG2("標籤屬性", ((SuperTag)s.Tag)?.SeriesLabelString);
                            if (si.ShowDialog() == DialogResult.OK)
                            {
                                Series series = chartMain.Series[s.Name];
                                if (series != null)
                                {
                                    foreach (Series s2 in CETCManagerApp.Instance.m_pETETCStage.m_ETCSlopes)
                                    {
                                        if (s2.Name == s.Name)
                                        {
                                            //((TextAnnotation)s2.Points[0].Annotations[0]).Text = si.InputText;
                                            ((SuperTag)s2.Tag).SeriesLabelString = si.InputText;
                                            break;
                                        }
                                    }
                                }
                            }
                        }*/
                    }
                    break;
            }
        }
    }
}
