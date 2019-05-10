namespace ETCProfiler
{
    partial class IDD_RATELINE_LIST
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.GC_SlopeList = new DevExpress.XtraGrid.GridControl();
            this.GV_SlopeList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.panelBtn.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GC_SlopeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GV_SlopeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            this.SuspendLayout();
            // 
            // panelBtn
            // 
            this.panelBtn.Controls.Add(this.btnDelete);
            this.panelBtn.Location = new System.Drawing.Point(10, 361);
            this.panelBtn.Size = new System.Drawing.Size(721, 36);
            this.panelBtn.Controls.SetChildIndex(this.btnCancel, 0);
            this.panelBtn.Controls.SetChildIndex(this.panelBetween, 0);
            this.panelBtn.Controls.SetChildIndex(this.btnOK, 0);
            this.panelBtn.Controls.SetChildIndex(this.btnDelete, 0);
            // 
            // panelLast
            // 
            this.panelLast.Location = new System.Drawing.Point(10, 341);
            this.panelLast.Size = new System.Drawing.Size(721, 20);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(514, 0);
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(615, 0);
            // 
            // panelBetween
            // 
            this.panelBetween.Location = new System.Drawing.Point(604, 0);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.GC_SlopeList);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(10, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(721, 330);
            this.panel1.TabIndex = 2;
            // 
            // GC_SlopeList
            // 
            this.GC_SlopeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GC_SlopeList.Location = new System.Drawing.Point(0, 0);
            this.GC_SlopeList.MainView = this.GV_SlopeList;
            this.GC_SlopeList.Name = "GC_SlopeList";
            this.GC_SlopeList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit2,
            this.repositoryItemTextEdit1});
            this.GC_SlopeList.Size = new System.Drawing.Size(721, 330);
            this.GC_SlopeList.TabIndex = 19;
            this.GC_SlopeList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GV_SlopeList});
            // 
            // GV_SlopeList
            // 
            this.GV_SlopeList.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GV_SlopeList.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GV_SlopeList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6});
            this.GV_SlopeList.DetailHeight = 425;
            this.GV_SlopeList.FixedLineWidth = 3;
            this.GV_SlopeList.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.GV_SlopeList.GridControl = this.GC_SlopeList;
            this.GV_SlopeList.Name = "GV_SlopeList";
            this.GV_SlopeList.OptionsBehavior.AlignGroupSummaryInGroupRow = DevExpress.Utils.DefaultBoolean.False;
            this.GV_SlopeList.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.GV_SlopeList.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.GV_SlopeList.OptionsBehavior.AllowFixedGroups = DevExpress.Utils.DefaultBoolean.False;
            this.GV_SlopeList.OptionsBehavior.AllowGroupExpandAnimation = DevExpress.Utils.DefaultBoolean.False;
            this.GV_SlopeList.OptionsBehavior.AllowPartialGroups = DevExpress.Utils.DefaultBoolean.False;
            this.GV_SlopeList.OptionsBehavior.AllowSortAnimation = DevExpress.Utils.DefaultBoolean.False;
            this.GV_SlopeList.OptionsCustomization.AllowColumnMoving = false;
            this.GV_SlopeList.OptionsCustomization.AllowFilter = false;
            this.GV_SlopeList.OptionsCustomization.AllowGroup = false;
            this.GV_SlopeList.OptionsCustomization.AllowMergedGrouping = DevExpress.Utils.DefaultBoolean.False;
            this.GV_SlopeList.OptionsCustomization.AllowQuickHideColumns = false;
            this.GV_SlopeList.OptionsCustomization.AllowSort = false;
            this.GV_SlopeList.OptionsMenu.EnableColumnMenu = false;
            this.GV_SlopeList.OptionsMenu.EnableFooterMenu = false;
            this.GV_SlopeList.OptionsMenu.EnableGroupPanelMenu = false;
            this.GV_SlopeList.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.False;
            this.GV_SlopeList.OptionsMenu.ShowAutoFilterRowItem = false;
            this.GV_SlopeList.OptionsMenu.ShowGroupSortSummaryItems = false;
            this.GV_SlopeList.OptionsMenu.ShowSplitItem = false;
            this.GV_SlopeList.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.GV_SlopeList.OptionsSelection.MultiSelect = true;
            this.GV_SlopeList.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.GV_SlopeList.OptionsView.ColumnAutoWidth = false;
            this.GV_SlopeList.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.GV_SlopeList.OptionsView.ShowDetailButtons = false;
            this.GV_SlopeList.OptionsView.ShowErrorPanel = DevExpress.Utils.DefaultBoolean.False;
            this.GV_SlopeList.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.GV_SlopeList.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.GV_SlopeList.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "斜率名称";
            this.gridColumn1.FieldName = "gridColumn1";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 200;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "斜率值";
            this.gridColumn2.FieldName = "gridColumn2";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 80;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "X1 (S)";
            this.gridColumn3.ColumnEdit = this.repositoryItemTextEdit1;
            this.gridColumn3.DisplayFormat.FormatString = "n1";
            this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn3.FieldName = "gridColumn3";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            this.gridColumn3.Width = 80;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Mask.EditMask = "n1";
            this.repositoryItemTextEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Y1 (℃)";
            this.gridColumn4.ColumnEdit = this.repositoryItemTextEdit1;
            this.gridColumn4.DisplayFormat.FormatString = "n1";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn4.FieldName = "gridColumn4";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 4;
            this.gridColumn4.Width = 80;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "X2 (S)";
            this.gridColumn5.ColumnEdit = this.repositoryItemTextEdit1;
            this.gridColumn5.DisplayFormat.FormatString = "n1";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn5.FieldName = "gridColumn5";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 5;
            this.gridColumn5.Width = 80;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Y2 (℃)";
            this.gridColumn6.ColumnEdit = this.repositoryItemTextEdit1;
            this.gridColumn6.DisplayFormat.FormatString = "n1";
            this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn6.FieldName = "gridColumn6";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 6;
            this.gridColumn6.Width = 80;
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            this.repositoryItemCheckEdit2.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // btnDelete
            // 
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDelete.Location = new System.Drawing.Point(0, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 36);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "删除斜率线";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // IDD_RATELINE_LIST
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 407);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "IDD_RATELINE_LIST";
            this.ShowIcon = false;
            this.Text = "斜率线列表";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.panelBtn, 0);
            this.Controls.SetChildIndex(this.panelLast, 0);
            this.panelBtn.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GC_SlopeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GV_SlopeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraGrid.GridControl GC_SlopeList;
        private DevExpress.XtraGrid.Views.Grid.GridView GV_SlopeList;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
    }
}