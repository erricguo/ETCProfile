namespace ETCProfiler
{
    partial class IDD_BaseForm
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
            this.panelBtn = new System.Windows.Forms.Panel();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.panelBetween = new System.Windows.Forms.Panel();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.panelLast = new System.Windows.Forms.Panel();
            this.panelBtn.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBtn
            // 
            this.panelBtn.Controls.Add(this.btnOK);
            this.panelBtn.Controls.Add(this.panelBetween);
            this.panelBtn.Controls.Add(this.btnCancel);
            this.panelBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBtn.Location = new System.Drawing.Point(10, 386);
            this.panelBtn.Name = "panelBtn";
            this.panelBtn.Padding = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.panelBtn.Size = new System.Drawing.Size(764, 36);
            this.panelBtn.TabIndex = 39;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOK.Location = new System.Drawing.Point(557, 0);
            this.btnOK.Name = "btnOK";
            this.btnOK.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.btnOK.Size = new System.Drawing.Size(90, 36);
            this.btnOK.TabIndex = 2;
            this.btnOK.Tag = "";
            this.btnOK.Text = "确定";
            // 
            // panelBetween
            // 
            this.panelBetween.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelBetween.Location = new System.Drawing.Point(647, 0);
            this.panelBetween.Name = "panelBetween";
            this.panelBetween.Size = new System.Drawing.Size(11, 36);
            this.panelBetween.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(658, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.btnCancel.Size = new System.Drawing.Size(90, 36);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            // 
            // panelLast
            // 
            this.panelLast.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelLast.Location = new System.Drawing.Point(10, 366);
            this.panelLast.Name = "panelLast";
            this.panelLast.Size = new System.Drawing.Size(764, 20);
            this.panelLast.TabIndex = 38;
            // 
            // IDD_BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 432);
            this.Controls.Add(this.panelLast);
            this.Controls.Add(this.panelBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "IDD_BaseForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "IDD_BaseForm";
            this.panelBtn.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Panel panelBtn;
        public System.Windows.Forms.Panel panelLast;
        public DevExpress.XtraEditors.SimpleButton btnOK;
        public DevExpress.XtraEditors.SimpleButton btnCancel;
        public System.Windows.Forms.Panel panelBetween;
    }
}