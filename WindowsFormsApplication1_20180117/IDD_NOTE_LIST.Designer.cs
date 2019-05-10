namespace ETCProfiler
{
    partial class IDD_NOTE_LIST
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IDD_NOTE_LIST));
            this.panel1 = new System.Windows.Forms.Panel();
            this.GC_LabelList = new DevExpress.XtraGrid.GridControl();
            this.GV_LabelList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.panelBtn.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GC_LabelList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GV_LabelList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            this.SuspendLayout();
            // 
            // panelBtn
            // 
            this.panelBtn.Controls.Add(this.btnDelete);
            resources.ApplyResources(this.panelBtn, "panelBtn");
            this.panelBtn.Controls.SetChildIndex(this.btnCancel, 0);
            this.panelBtn.Controls.SetChildIndex(this.panelBetween, 0);
            this.panelBtn.Controls.SetChildIndex(this.btnOK, 0);
            this.panelBtn.Controls.SetChildIndex(this.btnDelete, 0);
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
            // panel1
            // 
            this.panel1.Controls.Add(this.GC_LabelList);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // GC_LabelList
            // 
            resources.ApplyResources(this.GC_LabelList, "GC_LabelList");
            this.GC_LabelList.MainView = this.GV_LabelList;
            this.GC_LabelList.Name = "GC_LabelList";
            this.GC_LabelList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit2,
            this.repositoryItemTextEdit1});
            this.GC_LabelList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GV_LabelList});
            // 
            // GV_LabelList
            // 
            this.GV_LabelList.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GV_LabelList.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GV_LabelList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn4,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.GV_LabelList.DetailHeight = 425;
            this.GV_LabelList.FixedLineWidth = 3;
            this.GV_LabelList.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.GV_LabelList.GridControl = this.GC_LabelList;
            this.GV_LabelList.Name = "GV_LabelList";
            this.GV_LabelList.OptionsBehavior.AlignGroupSummaryInGroupRow = DevExpress.Utils.DefaultBoolean.False;
            this.GV_LabelList.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.GV_LabelList.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.GV_LabelList.OptionsBehavior.AllowFixedGroups = DevExpress.Utils.DefaultBoolean.False;
            this.GV_LabelList.OptionsBehavior.AllowGroupExpandAnimation = DevExpress.Utils.DefaultBoolean.False;
            this.GV_LabelList.OptionsBehavior.AllowPartialGroups = DevExpress.Utils.DefaultBoolean.False;
            this.GV_LabelList.OptionsBehavior.AllowSortAnimation = DevExpress.Utils.DefaultBoolean.False;
            this.GV_LabelList.OptionsBehavior.ReadOnly = true;
            this.GV_LabelList.OptionsCustomization.AllowColumnMoving = false;
            this.GV_LabelList.OptionsCustomization.AllowFilter = false;
            this.GV_LabelList.OptionsCustomization.AllowGroup = false;
            this.GV_LabelList.OptionsCustomization.AllowMergedGrouping = DevExpress.Utils.DefaultBoolean.False;
            this.GV_LabelList.OptionsCustomization.AllowQuickHideColumns = false;
            this.GV_LabelList.OptionsCustomization.AllowSort = false;
            this.GV_LabelList.OptionsMenu.EnableColumnMenu = false;
            this.GV_LabelList.OptionsMenu.EnableFooterMenu = false;
            this.GV_LabelList.OptionsMenu.EnableGroupPanelMenu = false;
            this.GV_LabelList.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.False;
            this.GV_LabelList.OptionsMenu.ShowAutoFilterRowItem = false;
            this.GV_LabelList.OptionsMenu.ShowGroupSortSummaryItems = false;
            this.GV_LabelList.OptionsMenu.ShowSplitItem = false;
            this.GV_LabelList.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.GV_LabelList.OptionsSelection.MultiSelect = true;
            this.GV_LabelList.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.GV_LabelList.OptionsView.ColumnAutoWidth = false;
            this.GV_LabelList.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.GV_LabelList.OptionsView.ShowDetailButtons = false;
            this.GV_LabelList.OptionsView.ShowErrorPanel = DevExpress.Utils.DefaultBoolean.False;
            this.GV_LabelList.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.GV_LabelList.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.GV_LabelList.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn4
            // 
            resources.ApplyResources(this.gridColumn4, "gridColumn4");
            this.gridColumn4.FieldName = "gridColumn0";
            this.gridColumn4.Name = "gridColumn4";
            // 
            // gridColumn1
            // 
            resources.ApplyResources(this.gridColumn1, "gridColumn1");
            this.gridColumn1.FieldName = "gridColumn1";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            resources.ApplyResources(this.gridColumn2, "gridColumn2");
            this.gridColumn2.ColumnEdit = this.repositoryItemTextEdit1;
            this.gridColumn2.DisplayFormat.FormatString = "n1";
            this.gridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn2.FieldName = "gridColumn2";
            this.gridColumn2.Name = "gridColumn2";
            // 
            // repositoryItemTextEdit1
            // 
            resources.ApplyResources(this.repositoryItemTextEdit1, "repositoryItemTextEdit1");
            this.repositoryItemTextEdit1.Mask.EditMask = resources.GetString("repositoryItemTextEdit1.Mask.EditMask");
            this.repositoryItemTextEdit1.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("repositoryItemTextEdit1.Mask.MaskType")));
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // gridColumn3
            // 
            resources.ApplyResources(this.gridColumn3, "gridColumn3");
            this.gridColumn3.ColumnEdit = this.repositoryItemTextEdit1;
            this.gridColumn3.DisplayFormat.FormatString = "n1";
            this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn3.FieldName = "gridColumn3";
            this.gridColumn3.Name = "gridColumn3";
            // 
            // repositoryItemCheckEdit2
            // 
            resources.ApplyResources(this.repositoryItemCheckEdit2, "repositoryItemCheckEdit2");
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            this.repositoryItemCheckEdit2.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // simpleButton1
            // 
            resources.ApplyResources(this.simpleButton1, "simpleButton1");
            this.simpleButton1.Name = "simpleButton1";
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // IDD_NOTE_LIST
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IDD_NOTE_LIST";
            this.Controls.SetChildIndex(this.panelBtn, 0);
            this.Controls.SetChildIndex(this.panelLast, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panelBtn.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GC_LabelList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GV_LabelList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraGrid.GridControl GC_LabelList;
        private DevExpress.XtraGrid.Views.Grid.GridView GV_LabelList;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
    }
}