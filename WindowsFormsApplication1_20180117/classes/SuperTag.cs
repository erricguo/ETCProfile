using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETCProfiler.classes
{
    public class SuperTag
    {
        public enum AnalysisBtn
        {
            None = -1,
            ReflowerResult = 0,
            MaxMinReport = 1,
            TimeInTemplReport = 2,
            TemplUpDownReport = 3,
            SlopeReport = 4,
            PeakValueReport = 5,
            ChartReport = 6

        }
        public int Round_n { get; set; } = 0;
        public string CETPageHV_RowCaption { get; set; } = "";
        public int IDD_OPTION_ANALYSE_CONDITION_ColumnIndex { get; set; } = 0;
        public string FileFullPath { get; set; } = "";
        public int BarButtonItemIndex { get; set; } = 0;
        public string SeriesMark { get; set; } = "";
        public string AxisXConstantLine { get; set; } = "";
        public string AxisYConstantLine { get; set; } = "";
        public int GridControlWidth { get; set; } = 0;
        public AnalysisBtn AnalysisBtnType { get; set; } = AnalysisBtn.None;
        public bool SeriesIsDrawDown { get; set; } = false;
        public string SeriesLabelString { get; set; } = "";
    }
}
