namespace ETCProfiler
{
    partial class IDD_OPTION_ANALYSE_CONDITION
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IDD_OPTION_ANALYSE_CONDITION));
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.GC_PageAnalysis = new DevExpress.XtraGrid.GridControl();
            this.GV_PageAnalysis = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckedComboBoxEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.m_nTempUnit = new DevExpress.XtraEditors.ComboBoxEdit();
            this.panel5 = new System.Windows.Forms.Panel();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.panel3 = new System.Windows.Forms.Panel();
            this.m_nTimeUnit = new DevExpress.XtraEditors.ComboBoxEdit();
            this.panel4 = new System.Windows.Forms.Panel();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton5 = new DevExpress.XtraEditors.SimpleButton();
            this.btnDefault = new DevExpress.XtraEditors.SimpleButton();
            this.panelBtn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GC_PageAnalysis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GV_PageAnalysis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckedComboBoxEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_nTempUnit.Properties)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_nTimeUnit.Properties)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBtn
            // 
            resources.ApplyResources(this.panelBtn, "panelBtn");
            // 
            // panelLast
            // 
            resources.ApplyResources(this.panelLast, "panelLast");
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            // 
            // panelBetween
            // 
            resources.ApplyResources(this.panelBetween, "panelBetween");
            // 
            // repositoryItemCheckEdit1
            // 
            resources.ApplyResources(this.repositoryItemCheckEdit1, "repositoryItemCheckEdit1");
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // GC_PageAnalysis
            // 
            this.GC_PageAnalysis.EmbeddedNavigator.Margin = ((System.Windows.Forms.Padding)(resources.GetObject("GC_PageAnalysis.EmbeddedNavigator.Margin")));
            resources.ApplyResources(this.GC_PageAnalysis, "GC_PageAnalysis");
            this.GC_PageAnalysis.MainView = this.GV_PageAnalysis;
            this.GC_PageAnalysis.Name = "GC_PageAnalysis";
            this.GC_PageAnalysis.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckedComboBoxEdit1,
            this.repositoryItemComboBox1});
            this.GC_PageAnalysis.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GV_PageAnalysis});
            // 
            // GV_PageAnalysis
            // 
            this.GV_PageAnalysis.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GV_PageAnalysis.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GV_PageAnalysis.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8});
            this.GV_PageAnalysis.DetailHeight = 602;
            this.GV_PageAnalysis.FixedLineWidth = 4;
            this.GV_PageAnalysis.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.GV_PageAnalysis.GridControl = this.GC_PageAnalysis;
            this.GV_PageAnalysis.Name = "GV_PageAnalysis";
            this.GV_PageAnalysis.OptionsBehavior.AlignGroupSummaryInGroupRow = DevExpress.Utils.DefaultBoolean.False;
            this.GV_PageAnalysis.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.GV_PageAnalysis.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.GV_PageAnalysis.OptionsBehavior.AllowFixedGroups = DevExpress.Utils.DefaultBoolean.False;
            this.GV_PageAnalysis.OptionsBehavior.AllowGroupExpandAnimation = DevExpress.Utils.DefaultBoolean.False;
            this.GV_PageAnalysis.OptionsBehavior.AllowPartialGroups = DevExpress.Utils.DefaultBoolean.False;
            this.GV_PageAnalysis.OptionsBehavior.AllowSortAnimation = DevExpress.Utils.DefaultBoolean.False;
            this.GV_PageAnalysis.OptionsCustomization.AllowColumnMoving = false;
            this.GV_PageAnalysis.OptionsCustomization.AllowFilter = false;
            this.GV_PageAnalysis.OptionsCustomization.AllowGroup = false;
            this.GV_PageAnalysis.OptionsCustomization.AllowMergedGrouping = DevExpress.Utils.DefaultBoolean.False;
            this.GV_PageAnalysis.OptionsCustomization.AllowQuickHideColumns = false;
            this.GV_PageAnalysis.OptionsCustomization.AllowSort = false;
            this.GV_PageAnalysis.OptionsMenu.EnableColumnMenu = false;
            this.GV_PageAnalysis.OptionsMenu.EnableFooterMenu = false;
            this.GV_PageAnalysis.OptionsMenu.EnableGroupPanelMenu = false;
            this.GV_PageAnalysis.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.False;
            this.GV_PageAnalysis.OptionsMenu.ShowAutoFilterRowItem = false;
            this.GV_PageAnalysis.OptionsMenu.ShowGroupSortSummaryItems = false;
            this.GV_PageAnalysis.OptionsMenu.ShowSplitItem = false;
            this.GV_PageAnalysis.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.GV_PageAnalysis.OptionsView.ColumnAutoWidth = false;
            this.GV_PageAnalysis.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.GV_PageAnalysis.OptionsView.ShowDetailButtons = false;
            this.GV_PageAnalysis.OptionsView.ShowErrorPanel = DevExpress.Utils.DefaultBoolean.False;
            this.GV_PageAnalysis.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.GV_PageAnalysis.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.GV_PageAnalysis.OptionsView.ShowGroupPanel = false;
            this.GV_PageAnalysis.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.GV_PageAnalysis_CustomRowCellEdit);
            // 
            // gridColumn1
            // 
            resources.ApplyResources(this.gridColumn1, "gridColumn1");
            this.gridColumn1.ColumnEdit = this.repositoryItemCheckEdit1;
            this.gridColumn1.FieldName = "m_nShowStatus";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            resources.ApplyResources(this.gridColumn2, "gridColumn2");
            this.gridColumn2.ColumnEdit = this.repositoryItemComboBox1;
            this.gridColumn2.FieldName = "m_nConditionType";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.UnboundType = DevExpress.Data.UnboundColumnType.String;
            // 
            // repositoryItemComboBox1
            // 
            resources.ApplyResources(this.repositoryItemComboBox1, "repositoryItemComboBox1");
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("repositoryItemComboBox1.Buttons"))))});
            this.repositoryItemComboBox1.Items.AddRange(new object[] {
            resources.GetString("repositoryItemComboBox1.Items"),
            resources.GetString("repositoryItemComboBox1.Items1"),
            resources.GetString("repositoryItemComboBox1.Items2"),
            resources.GetString("repositoryItemComboBox1.Items3"),
            resources.GetString("repositoryItemComboBox1.Items4"),
            resources.GetString("repositoryItemComboBox1.Items5"),
            resources.GetString("repositoryItemComboBox1.Items6"),
            resources.GetString("repositoryItemComboBox1.Items7")});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // gridColumn3
            // 
            resources.ApplyResources(this.gridColumn3, "gridColumn3");
            this.gridColumn3.FieldName = "m_strCaption";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.UnboundType = DevExpress.Data.UnboundColumnType.String;
            // 
            // gridColumn4
            // 
            resources.ApplyResources(this.gridColumn4, "gridColumn4");
            this.gridColumn4.DisplayFormat.FormatString = "n1";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn4.FieldName = "m_fBeginRangle";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            // 
            // gridColumn5
            // 
            resources.ApplyResources(this.gridColumn5, "gridColumn5");
            this.gridColumn5.DisplayFormat.FormatString = "n1";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn5.FieldName = "m_fEndRangle";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            // 
            // gridColumn6
            // 
            resources.ApplyResources(this.gridColumn6, "gridColumn6");
            this.gridColumn6.DisplayFormat.FormatString = "n1";
            this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn6.FieldName = "m_fBeginCondition";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            // 
            // gridColumn7
            // 
            resources.ApplyResources(this.gridColumn7, "gridColumn7");
            this.gridColumn7.DisplayFormat.FormatString = "n1";
            this.gridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn7.FieldName = "m_fEndCondition";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            // 
            // gridColumn8
            // 
            resources.ApplyResources(this.gridColumn8, "gridColumn8");
            this.gridColumn8.FieldName = "m_bReadonly";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
            // 
            // repositoryItemCheckedComboBoxEdit1
            // 
            resources.ApplyResources(this.repositoryItemCheckedComboBoxEdit1, "repositoryItemCheckedComboBoxEdit1");
            this.repositoryItemCheckedComboBoxEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("repositoryItemCheckedComboBoxEdit1.Buttons"))))});
            this.repositoryItemCheckedComboBoxEdit1.Name = "repositoryItemCheckedComboBoxEdit1";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.panel2);
            resources.ApplyResources(this.groupControl2, "groupControl2");
            this.groupControl2.Name = "groupControl2";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.panel4);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.m_nTempUnit);
            resources.ApplyResources(this.panel6, "panel6");
            this.panel6.Name = "panel6";
            // 
            // m_nTempUnit
            // 
            resources.ApplyResources(this.m_nTempUnit, "m_nTempUnit");
            this.m_nTempUnit.Name = "m_nTempUnit";
            this.m_nTempUnit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("m_nTempUnit.Properties.Buttons"))))});
            this.m_nTempUnit.Properties.Items.AddRange(new object[] {
            resources.GetString("m_nTempUnit.Properties.Items")});
            this.m_nTempUnit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.labelControl3);
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Name = "panel5";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Options.UseTextOptions = true;
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.labelControl3, "labelControl3");
            this.labelControl3.Name = "labelControl3";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.m_nTimeUnit);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // m_nTimeUnit
            // 
            resources.ApplyResources(this.m_nTimeUnit, "m_nTimeUnit");
            this.m_nTimeUnit.Name = "m_nTimeUnit";
            this.m_nTimeUnit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("m_nTimeUnit.Properties.Buttons"))))});
            this.m_nTimeUnit.Properties.Items.AddRange(new object[] {
            resources.GetString("m_nTimeUnit.Properties.Items"),
            resources.GetString("m_nTimeUnit.Properties.Items1")});
            this.m_nTimeUnit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.labelControl2);
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Options.UseTextOptions = true;
            this.labelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.labelControl2, "labelControl2");
            this.labelControl2.Name = "labelControl2";
            // 
            // simpleButton1
            // 
            resources.ApplyResources(this.simpleButton1, "simpleButton1");
            this.simpleButton1.Name = "simpleButton1";
            // 
            // simpleButton2
            // 
            resources.ApplyResources(this.simpleButton2, "simpleButton2");
            this.simpleButton2.Name = "simpleButton2";
            // 
            // simpleButton3
            // 
            resources.ApplyResources(this.simpleButton3, "simpleButton3");
            this.simpleButton3.Name = "simpleButton3";
            // 
            // simpleButton4
            // 
            resources.ApplyResources(this.simpleButton4, "simpleButton4");
            this.simpleButton4.Name = "simpleButton4";
            // 
            // simpleButton5
            // 
            resources.ApplyResources(this.simpleButton5, "simpleButton5");
            this.simpleButton5.Name = "simpleButton5";
            this.simpleButton5.Click += new System.EventHandler(this.simpleButton5_Click);
            // 
            // btnDefault
            // 
            resources.ApplyResources(this.btnDefault, "btnDefault");
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // IDD_OPTION_ANALYSE_CONDITION
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.simpleButton5);
            this.Controls.Add(this.simpleButton4);
            this.Controls.Add(this.simpleButton3);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.GC_PageAnalysis);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IDD_OPTION_ANALYSE_CONDITION";
            this.Controls.SetChildIndex(this.GC_PageAnalysis, 0);
            this.Controls.SetChildIndex(this.panelBtn, 0);
            this.Controls.SetChildIndex(this.panelLast, 0);
            this.Controls.SetChildIndex(this.groupControl2, 0);
            this.Controls.SetChildIndex(this.simpleButton1, 0);
            this.Controls.SetChildIndex(this.simpleButton2, 0);
            this.Controls.SetChildIndex(this.simpleButton3, 0);
            this.Controls.SetChildIndex(this.simpleButton4, 0);
            this.Controls.SetChildIndex(this.simpleButton5, 0);
            this.Controls.SetChildIndex(this.btnDefault, 0);
            this.panelBtn.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GC_PageAnalysis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GV_PageAnalysis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckedComboBoxEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_nTempUnit.Properties)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_nTimeUnit.Properties)).EndInit();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl GC_PageAnalysis;
        private DevExpress.XtraGrid.Views.Grid.GridView GV_PageAnalysis;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel5;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit m_nTempUnit;
        private DevExpress.XtraEditors.ComboBoxEdit m_nTimeUnit;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private DevExpress.XtraEditors.SimpleButton simpleButton5;
        private DevExpress.XtraEditors.SimpleButton btnDefault;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit repositoryItemCheckedComboBoxEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
    }
}