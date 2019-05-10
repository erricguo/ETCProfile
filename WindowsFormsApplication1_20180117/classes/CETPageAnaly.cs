using DevExpress.Data;
using DevExpress.Utils;
using ETCProfiler;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ETCProfiler.classes.SuperTag;

namespace ETCProfiler.classes
{
    // CETPageAnaly 对话框

    public class CETPageAnaly
    {
        protected AnalysisBtn m_nViewID;
        public DataGridView GV_PageAnaly { get; set; } = new DataGridView();
        private List<string> m_strColumn = new List<string>();
        private List<string> m_strRow = new List<string>();
        public CETPageAnaly()
        {
            m_nViewID = 0;
            Init();
        }
        public bool Init()
        {
            SwitchReportView(0);
            CreateReflowerResult();
            return true;
        }

        

        private enum AnonymousEnum
        {
            //IDD = IDD_PAGE_ANALY
        }

        public void CreateReport(AnalysisBtn type)
        {
            switch (type)
            {
                case AnalysisBtn.None:
                    break;
                case AnalysisBtn.ReflowerResult:
                    CreateReflowerResult();
                    break;
                case AnalysisBtn.MaxMinReport:
                    CreateMaxMinReport();
                    break;
                case AnalysisBtn.TimeInTemplReport:
                    CreateTimeInTemplReport();
                    break;
                case AnalysisBtn.TemplUpDownReport:
                    CreateTemplUpDownReport();
                    break;
                case AnalysisBtn.SlopeReport:
                    CreateSlopeReport();
                    break;
                case AnalysisBtn.PeakValueReport:
                    CreatePeakValueReport();
                    break;
                case AnalysisBtn.ChartReport:
                    CreateChartReport();
                    break;
                default:
                    break;
            }
        }

        public void SwitchReportView(AnalysisBtn type)
        {
            //UpdateData(TRUE);
            if (type ==  AnalysisBtn.ChartReport)
            {
                type = m_nViewID;
            }
            m_strColumn.Clear();
            CETETCStage pETCStage = CETCManagerApp.Instance.m_pETETCStage;
            
            switch (type)
            {
                case AnalysisBtn.ReflowerResult:

                    m_strColumn.Add("判定内容\n判定范围\n判定条件");
                    for (int ii = 0; ii < CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition.Count; ii++)
                    {
                        if (CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_nShowStatus)
                        {
                            string strColumn = "";

                            string strTempCaption = "";
                            Type mType = null;
                            if (CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_strCaption.IndexOf("[") >= 0)
                            {
                                strTempCaption = CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_strCaption.Substring(CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_strCaption.IndexOf("["));
                            }
                            else
                            {
                                strTempCaption = CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_strCaption;
                            }


                            switch (CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_nConditionType)
                            {
                                case 0:
                                    strColumn = string.Format("{0}\n[°C]", strTempCaption);
                                    break;
                                case 1:
                                    strColumn = string.Format("{0}[S]\n({1}~{2}°C)\n({3}~{4}S)", strTempCaption,
                                                      CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_fBeginRangle,
                                                      CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_fEndRangle,
                                                      CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_fBeginCondition,
                                                      CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_fEndCondition);
                                    break;
                                case 2:
                                    strColumn = string.Format("{0}[S]\n({1}~{2}°C)\n({3}~{4}S)", strTempCaption,
                                                      CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_fBeginRangle,
                                                      CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_fEndRangle,
                                                      CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_fBeginCondition,
                                                      CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_fEndCondition);
                                    break;
                                case 3:
                                    strColumn = string.Format("{0}[S]\n>{1}°C\n({2}~{3}S)", strTempCaption,
                                                      CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_fBeginRangle,
                                                      CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_fBeginCondition,
                                                      CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_fEndCondition);
                                    break;
                                case 4:
                                    strColumn = string.Format("{0}[°C/S]\n({1}~{2}°C\n({3}~{4})", strTempCaption,
                                                      CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_fBeginRangle,
                                                      CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_fEndRangle,
                                                      CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_fBeginCondition,
                                                      CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_fEndCondition);
                                    break;
                                case 5:
                                    strColumn = string.Format("{0}[°C/S]\n({1}~{2}°C\n({3}~{4})", strTempCaption,
                                                      CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_fBeginRangle,
                                                      CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_fEndRangle,
                                                      CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_fBeginCondition,
                                                      CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_fEndCondition);
                                    break;
                                case 6:
                                    strColumn = string.Format("{0}[°C/S]\n({1}~{2}°C\n({3}~{4})", strTempCaption,
                                                      CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_fBeginRangle,
                                                      CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_fEndRangle,
                                                      CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_fBeginCondition,
                                                      CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_fEndCondition);
                                    break;
                                case 7:
                                    strColumn = string.Format("{0}[°C/S]\n({1}~{2}°C\n({3}~{4})", strTempCaption,
                                                      CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_fBeginRangle,
                                                      CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_fEndRangle,
                                                      CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_fBeginCondition,
                                                      CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_fEndCondition);
                                    break;
                            }
                            m_strColumn.Add(strColumn);
                        }
                    }
                    //m_strColumn.Add("  ");
                    break;

                case AnalysisBtn.MaxMinReport:
                    m_strColumn.Add("通道");
                    m_strColumn.Add("最大测量值\n[°C]");
                    m_strColumn.Add("达到最大时间\n(S)");
                    m_strColumn.Add("平均测量值\n[°C]");
                    m_strColumn.Add("偏差于0°C\n[°C]");
                    m_strColumn.Add("标准偏差");
                    m_strColumn.Add("最小测量值");
                    m_strColumn.Add("达到最小时间\n(S)");
                    break;
                case AnalysisBtn.TimeInTemplReport:

                    for (int ii = 0; ii < CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition.Count; ii++)
                    {
                        string strColumn;
                        switch (CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_nConditionType)
                        {
                            case 3:
                                m_strColumn.Add("通道");
                                string strTempCaption;
                                if (CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_strCaption.IndexOf("[") >= 0)
                                {
                                    strTempCaption = CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_strCaption.
                                                    Substring(CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].
                                                     m_strCaption.IndexOf("["));
                                }
                                else
                                {
                                    strTempCaption = CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_strCaption;
                                }

                                strColumn = string.Format("{0}[S]\n>{1}°C", strTempCaption,
                                                  CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_fBeginRangle,
                                                  CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_fEndRangle);
                                m_strColumn.Add(strColumn);
                                strColumn = string.Format("到达所需时间(S)\n>{0}°C",
                                                  CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_fBeginRangle,
                                                  CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_fEndRangle);
                                m_strColumn.Add(strColumn);
                                break;
                        }


                    }
                    break;
                case AnalysisBtn.TemplUpDownReport:

                    for (int ii = 0; ii < CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition.Count; ii++)
                    {
                        string strColumn;

                        // 			string strTempCaption;
                        // 			if (Frm_Main.GetMainForm().m_pETETCStage.m_AnalyseCondition[ii].m_strCaption.Find("[") >= 0)
                        // 			{
                        // 				strTempCaption = Frm_Main.GetMainForm().m_pETETCStage.m_AnalyseCondition[ii].m_strCaption.Left(Frm_Main.GetMainForm().m_pETETCStage.m_AnalyseCondition[ii].m_strCaption.Find("["));
                        // 			}
                        // 			else
                        // 			{
                        // 				strTempCaption = Frm_Main.GetMainForm().m_pETETCStage.m_AnalyseCondition[ii].m_strCaption;
                        // 			}

                        switch (CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_nConditionType)
                        {

                            case 1:
                                m_strColumn.Add("通道");
                                strColumn = string.Format("上升时间(S)\n({0}~{1}°C)",
                                                  CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_fBeginRangle,
                                                  CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_fEndRangle);
                                m_strColumn.Add(strColumn);
                                strColumn = string.Format("上升速率[°C/S]\n({0}~{1}°C)",
                                                  CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_fBeginRangle,
                                                  CETCManagerApp.Instance.m_pETETCStage.m_AnalyseCondition[ii].m_fEndRangle);
                                m_strColumn.Add(strColumn);

                                break;
                                // 			case 4:
                                // 				strColumn = string.Format("%s[°C/S]\n(%.1f~%.1f°C)",strTempCaption,Frm_Main.GetMainForm().m_pETETCStage.m_AnalyseCondition[ii].m_fBeginRangle,Frm_Main.GetMainForm().m_pETETCStage.m_AnalyseCondition[ii].m_fEndRangle);
                                // 				m_strColumn.Add(strColumn);
                                // 				break;
                        }

                    }
                    //
                    // 		strCol = string.Format("上升时间\n（%3.1f - %3.1f）\n(S)", pETCStage.m_ETTinCream.m_fStartTempl2, pETCStage.m_ETTinCream.m_fEndTempl2);
                    // 		m_strColumn.Add(strCol);
                    // 		strCol = string.Format("上升速率\n（%3.1f - %3.1f）\n[°C/S]", pETCStage.m_ETTinCream.m_fStartTempl2, pETCStage.m_ETTinCream.m_fEndTempl2);
                    // 		m_strColumn.Add(strCol);
                    // 		strCol = string.Format("上升时间\n（%3.1f - %3.1f）\n(S)", pETCStage.m_ETTinCream.m_fEndTempl2, pETCStage.m_ETTinCream.m_fStartTempl3);
                    // 		m_strColumn.Add(strCol);
                    // 		strCol = string.Format("上升速率\n（%3.1f - %3.1f）\n[°C/S]", pETCStage.m_ETTinCream.m_fEndTempl2, pETCStage.m_ETTinCream.m_fStartTempl3);
                    // 		m_strColumn.Add(strCol);

                    //m_strColumn.Add("  ");
                    break;
                case AnalysisBtn.SlopeReport:
                    m_strColumn.Add("通道");
                    m_strColumn.Add("正斜率[°C/S]");
                    m_strColumn.Add("正斜率时间");
                    m_strColumn.Add("上升时间");
                    m_strColumn.Add("到峰值的平均斜率");
                    m_strColumn.Add("液相线以上时间");
                    m_strColumn.Add("峰值温度");
                    m_strColumn.Add("峰值温度差");
                    m_strColumn.Add("负斜率");
                    break;
                case AnalysisBtn.PeakValueReport:
                    m_strColumn.Add("通道");
                    m_strColumn.Add("峰值温度");
                    m_strColumn.Add("峰值温度差");
                    m_strColumn.Add("到达时间");
                    break;
                case AnalysisBtn.ChartReport:
                    m_strColumn.Add("通道");
                    m_strColumn.Add("时间\n(S)");
                    m_strColumn.Add("距离\n[cm]");
                    //m_strColumn.Add("");
                    break;
            }

        }


        public void ResetColumnAndRows()
        {
            
            GV_PageAnaly.Columns.Clear();
            GV_PageAnaly.Rows.Clear();
        }
        public bool CreateReflowerResult()
        {
            ResetColumnAndRows();
            var theApp = CETCManagerApp.Instance;
            CETReflower pETReflower = theApp.m_pETETCStage.m_ETReflower;

            int nChannelCount = 0;
            for (int row = 0; row < theApp.m_pETETCStage.m_nChannelCount; row++)
            {
                if (theApp.m_IsShowChannel[row])
                {
                    nChannelCount++;
                }
            }

            // fill it up with stuff
            
            /*try
            {
                m_Grid.SetRowCount(nChannelCount + 1);//theApp.m_pETETCStage.m_nChannelCount + 1);
                m_Grid.SetColumnCount(m_strColumn.GetCount());
                m_Grid.SetFixedRowCount(1);
                m_Grid.SetFixedColumnCount(1);

                //
                int nHeigth = m_Grid.GetRowHeight(1);
                m_Grid.SetRowHeight(0, 1.7 * nHeigth);
                m_Grid.SetDefCellHeight(nHeigth);
            }
            catch (Exception e)
            {
                throw e;
            }*/

            // 头

            for (int col = 0; col < m_strColumn.Count; col++)
            {
                if (col == 0)
                    InsertColumn(m_strColumn[col], 100, HorzAlignment.Center, UnboundColumnType.String, typeof(string), true);
                else
                    InsertColumn(m_strColumn[col], 100, HorzAlignment.Far, UnboundColumnType.String, typeof(double), true);
            }

            for (int col = 0; col < theApp.m_pETETCStage.m_AnalyseCondition.Count; col++)
            {
                if (theApp.m_pETETCStage.m_AnalyseCondition[col].m_nShowStatus)
                {
                    switch (theApp.m_pETETCStage.m_AnalyseCondition[col].m_nConditionType)
                    {
                        case 0:
                            ((DataGridViewTextBoxColumnEX)GV_PageAnaly.Columns[col + 1]).Round_n = 1;
                            break;
                        case 1:
                        case 2:
                        case 3:
                            GV_PageAnaly.Columns[col + 1].ValueType = typeof(string);
                            ((DataGridViewTextBoxColumnEX)GV_PageAnaly.Columns[col + 1]).CellAlignment = HorzAlignment.Center;
                            break;
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                            ((DataGridViewTextBoxColumnEX)GV_PageAnaly.Columns[col + 1]).Round_n = 2;
                            break;
                    }
                }
            }
            theApp.m_pETETCStage.m_dChannelTempDifferenceMax = theApp.m_pETETCStage.CaclPeakDifference();
            double dSamplePeriod = 1.0 / theApp.m_pETETCStage.m_nSampleRate;
            // fill rows/cols with text
            int nTempRows = 0;
            for (int row = 0; row < theApp.m_pETETCStage.m_nChannelCount; row++)
            {
                GV_PageAnaly.Rows.Add();
                if (theApp.m_IsShowChannel[row])
                {
                    CETChannel pETChannel = theApp.m_pETETCStage.m_ETChannels.ElementAtOrDefault(row);
                    if (pETChannel != null)
                    {
                        // 如果没有数据
                        if (pETChannel.m_dTemperatureValues.Count == 0)
                        {
                            var Item = GV_PageAnaly.Rows[row].Cells[0];
                            Item.Value = $"[{row + 1}] {theApp.m_pETETCStage.m_ETProduct.m_SamplesPoints[row].m_strPointTitle}";
                            
                            continue;
                        }

                        // 计算回流结果
                        //     pETChannel.CaclReflowResult (&pETChannel.m_ReflowResult);
                        int nTempItemCount = 0;
                        //for (int col = 0; col < theApp.m_pETETCStage.m_AnalyseCondition.size() + 1; col++)
                        for (int col = 0; col < theApp.m_pETETCStage.m_AnalyseCondition.Count + 1; col++)
                        {
                            var Item = GV_PageAnaly.Rows[row].Cells[col];
                            if (col == 0)    // 填写通道名
                            {
                                
                                Item.Value = $"[{row+1}]{theApp.m_pETETCStage.m_ETProduct.m_SamplesPoints[row].m_strPointTitle}";
                            }
                            else
                            {
                                if (theApp.m_pETETCStage.m_AnalyseCondition[col-1].m_nShowStatus)
                                {
                                    switch (theApp.m_pETETCStage.m_AnalyseCondition[col-1].m_nConditionType)
                                    {
                                        case 0:
                                            {
                                                double dPeakTempTime = pETChannel.CaclPeakTemp();

                                                if (dPeakTempTime >= theApp.m_pETETCStage.m_AnalyseCondition[col-1].m_fBeginRangle &&
                                                        dPeakTempTime <= theApp.m_pETETCStage.m_AnalyseCondition[col-1].m_fEndRangle)
                                                {
                                                    Item.Style.ForeColor = Color.FromArgb(255, 0, 0);
                                                }
                                                else
                                                {

                                                    Item.Style.ForeColor = Color.FromArgb(255, 0, 0);
                                                }

                                                Item.Value = Math.Round(dPeakTempTime, 2);
                                            }
                                            break;

                                        case 1:
                                            {
                                                double fStartTempl = theApp.m_pETETCStage.m_AnalyseCondition[col-1].m_fBeginRangle;
                                                double fEndTempl = theApp.m_pETETCStage.m_AnalyseCondition[col-1].m_fEndRangle;

                                                TimeSpan timspan = TimeSpan.FromSeconds(pETChannel.GetTimeBetweenTemp(fStartTempl,
                                                                                fEndTempl) * dSamplePeriod);
                                                if (timspan.TotalSeconds == 0)
                                                {
                                                    Item.Style.ForeColor = Color.FromArgb(223, 223, 223);
                                                }
                                                else
                                                {
                                                    Item.Style.ForeColor = Color.FromArgb(0, 0, 0);

                                                }

                                                if (timspan.TotalSeconds >= theApp.m_pETETCStage.m_AnalyseCondition[col-1].m_fBeginCondition &&
                                                        timspan.TotalSeconds <= theApp.m_pETETCStage.m_AnalyseCondition[col-1].m_fEndCondition)
                                                {
                                                    Item.Style.ForeColor = Color.FromArgb(0, 0, 0);
                                                }
                                                else
                                                {

                                                    Item.Style.ForeColor = Color.FromArgb(255, 0, 0);
                                                }

                                                //   CString strTime = timspan.Format ("%H:%M:%S");
                                                //   Item.strText.Format (_T( "%s" ), strTime);
                                                if (theApp.m_pETETCStage.m_nTimeUnit == 0)
                                                {
                                                    string strTime = $"{timspan.Hours.ToString().PadLeft(2,'0')}:{timspan.Minutes.ToString().PadLeft(2, '0')}:{timspan.Seconds.ToString().PadLeft(2, '0')}";
                                                    Item.Value = strTime;
                                                }
                                                else
                                                {
                                                    Item.Value = timspan.TotalSeconds.ToString();
                                                }

                                            }

                                            break;
                                        case 2:
                                            {
                                                double fStartTempl = theApp.m_pETETCStage.m_AnalyseCondition[col-1].m_fBeginRangle;
                                                double fEndTempl = theApp.m_pETETCStage.m_AnalyseCondition[col-1].m_fEndRangle;

                                                TimeSpan timspan = TimeSpan.FromSeconds(pETChannel.GetTimeBetweenTemp(fStartTempl,
                                                                                fEndTempl) * dSamplePeriod);
                                                if (timspan.TotalSeconds == 0)
                                                {
                                                    Item.Style.ForeColor = Color.FromArgb(255, 0, 0);
                                                }
                                                else
                                                {
                                                    Item.Style.ForeColor = Color.FromArgb(0, 0, 0);
                                                }

                                                if (timspan.TotalSeconds >= theApp.m_pETETCStage.m_AnalyseCondition[col-1].m_fBeginCondition &&
                                                        timspan.TotalSeconds <= theApp.m_pETETCStage.m_AnalyseCondition[col-1].m_fEndCondition)
                                                {
                                                    Item.Style.ForeColor = Color.FromArgb(0, 0, 0);
                                                }
                                                else
                                                {

                                                    Item.Style.ForeColor = Color.FromArgb(255, 0, 0);
                                                }

                                                //    CString strTime = timspan.Format ("%H:%M:%S");
                                                //    Item.strText.Format (_T( "%s" ), strTime);
                                                if (theApp.m_pETETCStage.m_nTimeUnit == 0)
                                                {
                                                    string strTime = $"{timspan.Hours.ToString().PadLeft(2, '0')}:{timspan.Minutes.ToString().PadLeft(2, '0')}:{timspan.Seconds.ToString().PadLeft(2, '0')}";
                                                    Item.Value = strTime;
                                                }
                                                else
                                                {
                                                    Item.Value = timspan.TotalSeconds.ToString();
                                                }
                                            }
                                            break;
                                        case 3:
                                            {
                                                double fStartTempl = theApp.m_pETETCStage.m_AnalyseCondition[col-1].m_fBeginRangle;
                                                double fEndTempl = 300;//theApp.m_pETETCStage.m_AnalyseCondition[col-1].m_fEndRangle;

                                                TimeSpan timespan = TimeSpan.FromSeconds(pETChannel.GetTimeUpTemps(fStartTempl) * dSamplePeriod);
                                                if (timespan.TotalSeconds == 0)
                                                {
                                                    Item.Style.ForeColor = Color.FromArgb(255, 0, 0);
                                                    //	Item.crBkClr = RGB(223,223,223);
                                                }
                                                else
                                                {
                                                    Item.Style.ForeColor = Color.FromArgb(0, 0, 0);
                                                    //	Item.crBkClr = RGB(255,255,255);
                                                }

                                                if (timespan.TotalSeconds >= theApp.m_pETETCStage.m_AnalyseCondition[col-1].m_fBeginCondition &&
                                                        timespan.TotalSeconds <= theApp.m_pETETCStage.m_AnalyseCondition[col-1].m_fEndCondition)
                                                {
                                                    Item.Style.ForeColor = Color.FromArgb(0, 0, 0);
                                                }
                                                else
                                                {

                                                    Item.Style.ForeColor = Color.FromArgb(255, 0, 0);
                                                }


                                                //    CString strTime = timspan.Format ("%H:%M:%S");
                                                //   Item.strText.Format (_T( "%s" ), strTime);
                                                if (theApp.m_pETETCStage.m_nTimeUnit == 0)
                                                {
                                                    string strTime = $"{timespan.Hours.ToString().PadLeft(2, '0')}:{timespan.Minutes.ToString().PadLeft(2, '0')}:{timespan.Seconds.ToString().PadLeft(2, '0')}";
                                                    Item.Value = strTime;
                                                }
                                                else
                                                {
                                                    Item.Value = timespan.TotalSeconds.ToString();
                                                }
                                            }
                                            break;
                                        case 4:
                                            {
                                                double fStartTempl = theApp.m_pETETCStage.m_AnalyseCondition[col-1].m_fBeginRangle;
                                                double fEndTempl = theApp.m_pETETCStage.m_AnalyseCondition[col-1].m_fEndRangle;

                                                double fTempValue = pETChannel.GetSlopeByTemp(fStartTempl, fEndTempl);//;

                                                if (fTempValue >= theApp.m_pETETCStage.m_AnalyseCondition[col-1].m_fBeginCondition &&
                                                        fTempValue <= theApp.m_pETETCStage.m_AnalyseCondition[col-1].m_fEndCondition)
                                                {
                                                    Item.Style.ForeColor = Color.FromArgb(0, 0, 0);
                                                }
                                                else
                                                {

                                                    Item.Style.ForeColor = Color.FromArgb(255, 0, 0);
                                                }
                                                Item.Value = Math.Round(fTempValue, 2).ToString();
                                            }
                                            break;
                                        case 5:
                                            {
                                                double fStartTempl = theApp.m_pETETCStage.m_AnalyseCondition[col-1].m_fBeginRangle;
                                                //	  double			dPeakTempTime = pETChannel.CaclPeakTemp();//pETChannel.GetMaxTempTime (0, pETChannel.m_dTemperatureValues.GetCount ());

                                                double fEndTempl = pETChannel.CaclPeakTemp();//theApp.m_pETETCStage.m_AnalyseCondition[col-1].m_fEndRangle;

                                                double fTempValue = pETChannel.GetSlopeByTemp(fStartTempl, fEndTempl);
                                                if (fTempValue >= theApp.m_pETETCStage.m_AnalyseCondition[col-1].m_fBeginCondition &&
                                                        fTempValue <= theApp.m_pETETCStage.m_AnalyseCondition[col-1].m_fEndCondition)
                                                {
                                                    Item.Style.ForeColor = Color.FromArgb(0, 0, 0);
                                                }
                                                else
                                                {

                                                    Item.Style.ForeColor = Color.FromArgb(255, 0, 0);
                                                }

                                                Item.Value = Math.Round(fTempValue, 2).ToString();
                                            }
                                            break;
                                        case 6:
                                            {
                                                double fStartTempl = theApp.m_pETETCStage.m_AnalyseCondition[col-1].m_fBeginRangle;
                                                double fEndTempl = theApp.m_pETETCStage.m_AnalyseCondition[col-1].m_fEndRangle;

                                                double fTempValue = pETChannel.GetSlopeByTemp(fStartTempl, fEndTempl);
                                                if (fTempValue >= theApp.m_pETETCStage.m_AnalyseCondition[col-1].m_fBeginCondition &&
                                                        fTempValue <= theApp.m_pETETCStage.m_AnalyseCondition[col-1].m_fEndCondition)
                                                {
                                                    Item.Style.ForeColor = Color.FromArgb(0, 0, 0);
                                                }
                                                else
                                                {

                                                    Item.Style.ForeColor = Color.FromArgb(255, 0, 0);
                                                }

                                                Item.Value = Math.Round(fTempValue, 2).ToString();
                                            }
                                            break;
                                        case 7:
                                            {
                                                double fStartTempl = theApp.m_pETETCStage.m_AnalyseCondition[col-1].m_fBeginRangle;
                                                double fEndTempl = theApp.m_pETETCStage.m_AnalyseCondition[col-1].m_fEndRangle;

                                                double fTempValue = pETChannel.GetSlopeByTemp(fStartTempl, fEndTempl);
                                                if (fTempValue >= theApp.m_pETETCStage.m_AnalyseCondition[col-1].m_fBeginCondition &&
                                                        fTempValue <= theApp.m_pETETCStage.m_AnalyseCondition[col-1].m_fEndCondition)
                                                {
                                                    Item.Style.ForeColor = Color.FromArgb(0, 0, 0);
                                                }
                                                else
                                                {

                                                    Item.Style.ForeColor = Color.FromArgb(255, 0, 0);
                                                }

                                                Item.Value = Math.Round(fTempValue).ToString();
                                            }
                                            break;
                                    }
                                    //m_Grid.SetItem(&Item);
                                    nTempItemCount++;
                                }

                            }
                        }
                        nTempRows++;
                    }
                    
                }
            }
            // 	CSize szfontSize = m_Grid.GetTextExtent(0,0,"演示");
            // 	m_Grid.SetDefCellHeight(szfontSize.cy);
            // 	int nHeigth = m_Grid.GetRowHeight (1);
            // 	m_Grid.SetRowHeight (0, 1.7 * nHeigth);
            /*m_Grid.AutoSize();
            m_Grid.AutoSizeColumns();
            m_Grid.ExpandLastColumn();*/

            return true;
        }

        public bool CreateMaxMinReport()
        {
            ResetColumnAndRows();
            var theApp = CETCManagerApp.Instance;
            CETETCStage pETCStage = CETCManagerApp.Instance.m_pETETCStage;

            int nChannelCount = 0;
            for (int row = 0; row < theApp.m_pETETCStage.m_nChannelCount; row++)
            {
                if (theApp.m_IsShowChannel[row])
                {
                    nChannelCount++;
                }
            }

            // 头
            for (int col = 0; col < m_strColumn.Count; col++)
            {
                if (col == 0)
                    InsertColumn(m_strColumn[col], 100, HorzAlignment.Center, UnboundColumnType.String, typeof(string), true);
                else
                    InsertColumn(m_strColumn[col], 100, HorzAlignment.Far, UnboundColumnType.String, typeof(double), true);
            }

            for (int col = 1; col < m_strColumn.Count+1; col++)
            {
                    switch (col)
                    {
                        case 1:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            ((DataGridViewTextBoxColumnEX)GV_PageAnaly.Columns[col]).Round_n = 1;
                            break;
                        case 2:
                        case 7:
                            GV_PageAnaly.Columns[col].ValueType = typeof(string);
                            ((DataGridViewTextBoxColumnEX)GV_PageAnaly.Columns[col]).CellAlignment = HorzAlignment.Center;
                            break;
                    }
                
            }


            // fill rows/cols with text
            int nTempRows = 0;
            for (int row = 0; row < theApp.m_pETETCStage.m_nChannelCount; row++)
            {
                GV_PageAnaly.Rows.Add();
                if (theApp.m_IsShowChannel[row])
                {
                    CETChannel pETChannel = theApp.m_pETETCStage.m_ETChannels.ElementAtOrDefault(row);
                    if (pETChannel != null)
                    {
                        pETChannel.CaclReflowMaxMin(pETChannel.m_ReflowMaxMin);

                        for (int col = 0; col < m_strColumn.Count; col++)
                        {
                            var Item = GV_PageAnaly.Rows[row].Cells[col];

                            if (col < 1)
                            {
                                Item.Value = $"[{row + 1}] {theApp.m_pETETCStage.m_ETProduct.m_SamplesPoints[row].m_strPointTitle}";
                            }
                            else
                            {

                                switch (col)
                                {
                                    case 1:
                                        Item.Value = pETChannel.m_ReflowMaxMin.m_dMaxTemp;                                        
                                        break;
                                    case 2:
                                        {
                                            if (theApp.m_pETETCStage.m_nTimeUnit == 0)
                                            {
                                                var timespan = pETChannel.m_ReflowMaxMin.m_tmTimeReachedMax;
                                                string strTime = $"{timespan.Hours.ToString().PadLeft(2, '0')}:{timespan.Minutes.ToString().PadLeft(2, '0')}:{timespan.Seconds.ToString().PadLeft(2, '0')}";
                                                Item.Value = strTime;
                                            }
                                            else
                                            {
                                                Item.Value = pETChannel.m_ReflowMaxMin.m_tmTimeReachedMax.TotalSeconds;
                                            }
                                        }
                                        break;
                                    case 3:
                                        Item.Value = pETChannel.m_ReflowMaxMin.m_dAverageTemp;
                                        break;
                                    case 4:
                                        Item.Value = pETChannel.m_ReflowMaxMin.m_dRiseTimeTo0;
                                        break;
                                    case 5:
                                        Item.Value = pETChannel.m_ReflowMaxMin.m_dStdDev;
                                        break;
                                    case 6:
                                        Item.Value = pETChannel.m_ReflowMaxMin.m_dMinTemp;
                                        break;
                                    case 7:
                                        {
                                            //   CString strTime = pETChannel.m_ReflowMaxMin.m_tmTimeReachedMin.Format ("%H:%M:%S");
                                            //  Item.strText.Format (_T( "%s" ), strTime);
                                            if (theApp.m_pETETCStage.m_nTimeUnit == 0)
                                            {
                                                var timespan = pETChannel.m_ReflowMaxMin.m_tmTimeReachedMin;
                                                string strTime = $"{timespan.Hours.ToString().PadLeft(2, '0')}:{timespan.Minutes.ToString().PadLeft(2, '0')}:{timespan.Seconds.ToString().PadLeft(2, '0')}";
                                                Item.Value = strTime;
                                            }
                                            else
                                            {
                                                Item.Value = pETChannel.m_ReflowMaxMin.m_tmTimeReachedMin.TotalSeconds;
                                            }


                                        }
                                        break;
                                }
                            }
                            //m_Grid.SetItem(&Item);
                        }
                    }
                    nTempRows++;
                    
                }
            }
            return true;
        }

        public bool CreateTimeInTemplReport()
        {
            ResetColumnAndRows();
            var theApp = CETCManagerApp.Instance;
            CETETCStage pETCStage = CETCManagerApp.Instance.m_pETETCStage;

            // 头
            for (int col = 0; col < m_strColumn.Count; col++)
            {
                if (col == 0)
                    InsertColumn(m_strColumn[col], 100, HorzAlignment.Center, UnboundColumnType.String, typeof(string), true);
                else
                    InsertColumn(m_strColumn[col], 100, HorzAlignment.Far, UnboundColumnType.String, typeof(double), true);
            }

            for (int col = 1; col < m_strColumn.Count + 1; col++)
            {
                switch (col)
                {
                    case 1:
                    case 2:
                        GV_PageAnaly.Columns[col].ValueType = typeof(string);
                        ((DataGridViewTextBoxColumnEX)GV_PageAnaly.Columns[col]).CellAlignment = HorzAlignment.Center;                        
                        break;
                }
            }

            int nTempRows = 0;
            double dSamplePeriod = 1.0 / theApp.m_pETETCStage.m_nSampleRate;
            // fill rows/cols with text
            for (int row = 0; row < theApp.m_pETETCStage.m_nChannelCount; row++)
            {
                GV_PageAnaly.Rows.Add();
                if (theApp.m_IsShowChannel[row])
                {
                    CETChannel pETChannel = theApp.m_pETETCStage.m_ETChannels.ElementAtOrDefault(row);
                    if (pETChannel != null)
                    {
                        pETChannel.CaclReflowTimeAtTemperature(pETChannel.m_ReflowTimeAtTemperature);

                        

                        // 数据
                        for (int col = 0; col < theApp.m_pETETCStage.m_AnalyseCondition.Count; col++)
                        {
                            if (col == 0)
                            {
                                var Item = GV_PageAnaly.Rows[row].Cells[0];
                                Item.Value = $"[{row + 1}] {theApp.m_pETETCStage.m_ETProduct.m_SamplesPoints[row].m_strPointTitle}";
                            }
                            

                            switch (theApp.m_pETETCStage.m_AnalyseCondition[col].m_nConditionType)
                            {
                                case 3:
                                    {
                                        var Item = GV_PageAnaly.Rows[row].Cells[1];
                                        double fStartTempl = theApp.m_pETETCStage.m_AnalyseCondition[col].m_fBeginRangle;
                                        double fEndTempl = 300;

                                        TimeSpan timespan = TimeSpan.FromSeconds(pETChannel.GetTimeUpTemps(fStartTempl) * dSamplePeriod);

                                        if (timespan.TotalSeconds == 0)
                                        {
                                            Item.Style.ForeColor = Color.FromArgb(255, 0, 0);
                                        }
                                        else
                                        {
                                            Item.Style.ForeColor = Color.FromArgb(0, 0, 0);
                                        }

                                        if (timespan.TotalSeconds >= theApp.m_pETETCStage.m_AnalyseCondition[col].
                                                m_fBeginCondition &&
                                                timespan.TotalSeconds <= theApp.m_pETETCStage.m_AnalyseCondition[col].
                                                m_fEndCondition)
                                        {
                                            Item.Style.ForeColor = Color.FromArgb(0, 0, 0);
                                        }
                                        else
                                        {
                                            Item.Style.ForeColor = Color.FromArgb(255, 0, 0);
                                        }

                                        // CString strTime = timspan.Format ("%H:%M:%S");
                                        //  Item.strText.Format (_T( "%s" ), strTime);
                                        if (theApp.m_pETETCStage.m_nTimeUnit == 0)
                                        {
                                            string strTime = $"{timespan.Hours.ToString().PadLeft(2, '0')}:{timespan.Minutes.ToString().PadLeft(2, '0')}:{timespan.Seconds.ToString().PadLeft(2, '0')}";
                                            Item.Value = strTime;
                                        }
                                        else
                                        {
                                            Item.Value = timespan.TotalSeconds;
                                        }


                                        Item = GV_PageAnaly.Rows[row].Cells[2];

                                        fStartTempl = theApp.m_pETETCStage.m_AnalyseCondition[col].m_fBeginRangle;
                                        fEndTempl = theApp.m_pETETCStage.m_AnalyseCondition[col].m_fEndRangle;
                                        timespan = TimeSpan.FromSeconds(pETChannel.GetTimeBetweenTemp(0, fStartTempl) * dSamplePeriod);

                                        if (timespan.TotalSeconds == 0)
                                        {
                                            Item.Style.ForeColor = Color.FromArgb(255, 0, 0);
                                        }
                                        else
                                        {
                                            Item.Style.ForeColor = Color.FromArgb(0, 0, 0);
                                        }

                                        if (timespan.TotalSeconds >= theApp.m_pETETCStage.m_AnalyseCondition[col].
                                                m_fBeginCondition &&
                                                timespan.TotalSeconds <= theApp.m_pETETCStage.m_AnalyseCondition[col].
                                                m_fEndCondition)
                                        {
                                            Item.Style.ForeColor = Color.FromArgb(0, 0, 0);
                                        }
                                        else
                                        {
                                            Item.Style.ForeColor = Color.FromArgb(255, 0, 0);
                                        }

                                        //   strTime = timspan.Format ("%H:%M:%S");
                                        //   Item.strText.Format (_T( "%s" ), strTime);
                                        if (theApp.m_pETETCStage.m_nTimeUnit == 0)
                                        {
                                            string strTime = $"{timespan.Hours.ToString().PadLeft(2, '0')}:{timespan.Minutes.ToString().PadLeft(2, '0')}:{timespan.Seconds.ToString().PadLeft(2, '0')}";
                                            Item.Value = strTime;
                                        }
                                        else
                                        {
                                            Item.Value = timespan.TotalSeconds;
                                        }

                                    }
                                    //nTempCol += 2;

                                    break;
                            }
                        }

                    }
                    nTempRows++;
                    
                }
            }
            return true;
        }

        public bool CreateTemplUpDownReport()
        {
            ResetColumnAndRows();
            var theApp = CETCManagerApp.Instance;
            CETETCStage pETCStage = CETCManagerApp.Instance.m_pETETCStage;

            // 头
            for (int col = 0; col < m_strColumn.Count; col++)
            {
                if (col == 0)
                    InsertColumn(m_strColumn[col], 100, HorzAlignment.Center, UnboundColumnType.String, typeof(string), true);
                else
                    InsertColumn(m_strColumn[col], 100, HorzAlignment.Far, UnboundColumnType.String, typeof(double), true);
            }

            for (int col = 1; col < m_strColumn.Count + 1; col++)
            {
                switch (col)
                {
                    case 1:
                    case 2:
                        GV_PageAnaly.Columns[col].ValueType = typeof(string);
                        ((DataGridViewTextBoxColumnEX)GV_PageAnaly.Columns[col]).CellAlignment = HorzAlignment.Center;
                        break;
                }
            }


            int nTempRows = 0;
            double dSamplePeriod = 1.0 / theApp.m_pETETCStage.m_nSampleRate;
            // fill rows/cols with text
            for (int row = 0; row < theApp.m_pETETCStage.m_nChannelCount; row++)
            {
                if (theApp.m_IsShowChannel[row])
                {
                    GV_PageAnaly.Rows.Add();
                    CETChannel pETChannel = theApp.m_pETETCStage.m_ETChannels.ElementAtOrDefault(row);
                    if (pETChannel != null)
                    {
                        pETChannel.CaclReflowRisedrops(pETChannel.m_Risedrops);

                        for (int col = 0; col < theApp.m_pETETCStage.m_AnalyseCondition.Count; col++)
                        {
                            if (col == 0)
                            {
                                var Item = GV_PageAnaly.Rows[row].Cells[0];
                                Item.Value = $"[{row + 1}] {theApp.m_pETETCStage.m_ETProduct.m_SamplesPoints[row].m_strPointTitle}";
                            }

                            switch (theApp.m_pETETCStage.m_AnalyseCondition[col].m_nConditionType)
                            {
                                case 1:
                                    {
                                        var Item = GV_PageAnaly.Rows[row].Cells[1];

                                        double fStartTempl = theApp.m_pETETCStage.m_AnalyseCondition[col].m_fBeginRangle;
                                        double fEndTempl = theApp.m_pETETCStage.m_AnalyseCondition[col].m_fEndRangle;


                                        TimeSpan timespan = TimeSpan.FromSeconds(pETChannel.GetTimeBetweenTemp(fStartTempl,
                                                                        fEndTempl) * dSamplePeriod);
                                        if (timespan.TotalSeconds == 0)
                                        {
                                            Item.Style.ForeColor = Color.FromArgb(255, 0, 0);
                                        }
                                        else
                                        {
                                            Item.Style.ForeColor = Color.FromArgb(0, 0, 0);
                                        }

                                        if (timespan.TotalSeconds >= theApp.m_pETETCStage.m_AnalyseCondition[col].
                                                m_fBeginCondition &&
                                                timespan.TotalSeconds <= theApp.m_pETETCStage.m_AnalyseCondition[col].
                                                m_fEndCondition)
                                        {
                                            Item.Style.ForeColor = Color.FromArgb(0, 0, 0);
                                        }
                                        else
                                        {
                                            Item.Style.ForeColor = Color.FromArgb(255, 0, 0);
                                        }

                                        if (theApp.m_pETETCStage.m_nTimeUnit == 0)
                                        {
                                            string strTime = $"{timespan.Hours.ToString().PadLeft(2, '0')}:{timespan.Minutes.ToString().PadLeft(2, '0')}:{timespan.Seconds.ToString().PadLeft(2, '0')}";
                                            Item.Value = strTime;
                                        }
                                        else
                                        {
                                            Item.Value = timespan.TotalSeconds;
                                        }

                                        Item = GV_PageAnaly.Rows[row].Cells[2];

                                        fStartTempl = theApp.m_pETETCStage.m_AnalyseCondition[col].m_fBeginRangle;
                                        fEndTempl = theApp.m_pETETCStage.m_AnalyseCondition[col].m_fEndRangle;

                                        double fTempValue = pETChannel.GetSlopeByTemp(fStartTempl, fEndTempl);
                                        if (fTempValue >= theApp.m_pETETCStage.m_AnalyseCondition[col - 1].m_fBeginCondition &&
                                                fTempValue <= theApp.m_pETETCStage.m_AnalyseCondition[col - 1].m_fEndCondition)
                                        {
                                            Item.Style.ForeColor = Color.FromArgb(0, 0, 0);
                                        }
                                        else
                                        {
                                            Item.Style.ForeColor = Color.FromArgb(255, 0, 0);
                                        }

                                        Item.Value = fTempValue;
                                    }
                                    break;
                            }
                        }

                    }
                    
                    nTempRows++;
                }
            }

            return true;
        }

        
        
        public bool CreateSlopeReport(){ return true; }
        public bool CreatePeakValueReport() { return true; }
        public bool CreateChartReport() { return true; }

        public bool CreateDataReport(){ return true; }
        public virtual void OnOK(){ }
        public virtual void OnCancel(){ }
        //public virtual int PreTranslateMessage(MSG pMsg){ return 0; }

        private void InsertColumn(string name, int width, HorzAlignment alignCell, UnboundColumnType type, Type typeOri, bool readOnly)
        {
            var column = new DataGridViewTextBoxColumnEX();
            column.Name = $"column{GV_PageAnaly.Columns.Count + 1}";
            column.HeaderText = name;
            column.CellAlignment = alignCell;      //CELL位置
            column.ColumnType = type;
            column.SortMode = DataGridViewColumnSortMode.NotSortable; //SORT 需設定為NOT 標題才會真的置中
            column.ValueType = typeOri;
            column.Width = width;
            column.ReadOnly = readOnly;
            GV_PageAnaly.Columns.Add(column);
        }

    }

}
