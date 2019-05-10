namespace ETCProfiler
{
    partial class IDD_DIALOG1
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
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            this.panelBtn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelBtn
            // 
            this.panelBtn.Location = new System.Drawing.Point(10, 265);
            this.panelBtn.Size = new System.Drawing.Size(453, 36);
            // 
            // panelLast
            // 
            this.panelLast.Location = new System.Drawing.Point(10, 245);
            this.panelLast.Size = new System.Drawing.Size(453, 20);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(246, 0);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(347, 0);
            // 
            // panelBetween
            // 
            this.panelBetween.Location = new System.Drawing.Point(336, 0);
            // 
            // memoEdit1
            // 
            this.memoEdit1.Location = new System.Drawing.Point(13, 13);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Size = new System.Drawing.Size(447, 226);
            this.memoEdit1.TabIndex = 40;
            // 
            // IDD_DIALOG1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 311);
            this.ControlBox = false;
            this.Controls.Add(this.memoEdit1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IDD_DIALOG1";
            this.Text = "配置文件";
            this.Controls.SetChildIndex(this.panelBtn, 0);
            this.Controls.SetChildIndex(this.panelLast, 0);
            this.Controls.SetChildIndex(this.memoEdit1, 0);
            this.panelBtn.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.MemoEdit memoEdit1;
    }
}