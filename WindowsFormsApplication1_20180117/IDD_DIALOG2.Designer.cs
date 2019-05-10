namespace ETCProfiler
{
    partial class IDD_DIALOG2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IDD_DIALOG2));
            this.panel1 = new System.Windows.Forms.Panel();
            this.teInput = new DevExpress.XtraEditors.TextEdit();
            this.lbInput = new DevExpress.XtraEditors.LabelControl();
            this.panelBtn.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teInput.Properties)).BeginInit();
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
            // panel1
            // 
            this.panel1.Controls.Add(this.teInput);
            this.panel1.Controls.Add(this.lbInput);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // teInput
            // 
            resources.ApplyResources(this.teInput, "teInput");
            this.teInput.Name = "teInput";
            // 
            // lbInput
            // 
            resources.ApplyResources(this.lbInput, "lbInput");
            this.lbInput.Name = "lbInput";
            // 
            // IDD_DIALOG2
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Name = "IDD_DIALOG2";
            this.Controls.SetChildIndex(this.panelBtn, 0);
            this.Controls.SetChildIndex(this.panelLast, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panelBtn.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teInput.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.TextEdit teInput;
        private DevExpress.XtraEditors.LabelControl lbInput;
    }
}