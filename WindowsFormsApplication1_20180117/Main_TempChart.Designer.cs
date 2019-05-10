namespace ETCProfiler
{
    partial class Main_TempChart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView1 = new DevExpress.XtraCharts.LineSeriesView();
            this.chartMain = new DevExpress.XtraCharts.ChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.chartMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).BeginInit();
            this.SuspendLayout();
            // 
            // chartMain
            // 
            xyDiagram1.AxisX.GridLines.Visible = true;
            xyDiagram1.AxisX.Label.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisX.VisualRange.Auto = false;
            xyDiagram1.AxisX.VisualRange.AutoSideMargins = false;
            xyDiagram1.AxisX.VisualRange.MaxValueSerializable = "600";
            xyDiagram1.AxisX.VisualRange.MinValueSerializable = "0";
            xyDiagram1.AxisX.VisualRange.SideMarginsValue = 0D;
            xyDiagram1.AxisX.WholeRange.Auto = false;
            xyDiagram1.AxisX.WholeRange.AutoSideMargins = false;
            xyDiagram1.AxisX.WholeRange.MaxValueSerializable = "600";
            xyDiagram1.AxisX.WholeRange.MinValueSerializable = "0";
            xyDiagram1.AxisX.WholeRange.SideMarginsValue = 0D;
            xyDiagram1.AxisY.Label.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.VisualRange.Auto = false;
            xyDiagram1.AxisY.VisualRange.AutoSideMargins = false;
            xyDiagram1.AxisY.VisualRange.MaxValueSerializable = "300";
            xyDiagram1.AxisY.VisualRange.MinValueSerializable = "0";
            xyDiagram1.AxisY.VisualRange.SideMarginsValue = 0D;
            xyDiagram1.AxisY.WholeRange.Auto = false;
            xyDiagram1.AxisY.WholeRange.AutoSideMargins = false;
            xyDiagram1.AxisY.WholeRange.MaxValueSerializable = "300";
            xyDiagram1.AxisY.WholeRange.MinValueSerializable = "0";
            xyDiagram1.AxisY.WholeRange.SideMarginsValue = 0D;
            this.chartMain.Diagram = xyDiagram1;
            this.chartMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartMain.Legend.Name = "Default Legend";
            this.chartMain.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartMain.Location = new System.Drawing.Point(0, 0);
            this.chartMain.Margin = new System.Windows.Forms.Padding(4);
            this.chartMain.Name = "chartMain";
            this.chartMain.OptionsPrint.ImageFormat = DevExpress.XtraCharts.Printing.PrintImageFormat.Metafile;
            this.chartMain.OptionsPrint.SizeMode = DevExpress.XtraCharts.Printing.PrintSizeMode.Zoom;
            series1.LegendName = "Default Legend";
            series1.Name = "Series 1";
            series1.View = lineSeriesView1;
            this.chartMain.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.chartMain.Size = new System.Drawing.Size(914, 510);
            this.chartMain.TabIndex = 4;
            this.chartMain.ObjectHotTracked += new DevExpress.XtraCharts.HotTrackEventHandler(this.chartMain_ObjectHotTracked);
            this.chartMain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chartMain_MouseDoubleClick);
            this.chartMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chartMain_MouseUp);
            // 
            // Main_TempChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(914, 510);
            this.Controls.Add(this.chartMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.Name = "Main_TempChart";
            this.Text = "温度曲线图";
            this.Load += new System.EventHandler(this.Main_TempChart_Load);
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraCharts.ChartControl chartMain;
    }
}