namespace ETCProfiler
{
    partial class BoxMsg
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
            this.panelBtn.Location = new System.Drawing.Point(10, 216);
            this.panelBtn.Size = new System.Drawing.Size(444, 36);
            // 
            // panelLast
            // 
            this.panelLast.Location = new System.Drawing.Point(10, 196);
            this.panelLast.Size = new System.Drawing.Size(444, 20);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(237, 0);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(338, 0);
            this.btnCancel.Visible = false;
            // 
            // panelBetween
            // 
            this.panelBetween.Location = new System.Drawing.Point(327, 0);
            this.panelBetween.Visible = false;
            // 
            // memoEdit1
            // 
            this.memoEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoEdit1.EditValue = "";
            this.memoEdit1.Location = new System.Drawing.Point(10, 10);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.memoEdit1.Properties.Appearance.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.memoEdit1.Properties.Appearance.ForeColor = System.Drawing.Color.White;
            this.memoEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.memoEdit1.Properties.Appearance.Options.UseFont = true;
            this.memoEdit1.Properties.Appearance.Options.UseForeColor = true;
            this.memoEdit1.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.memoEdit1.Size = new System.Drawing.Size(444, 186);
            this.memoEdit1.TabIndex = 41;
            // 
            // BoxMsg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 262);
            this.Controls.Add(this.memoEdit1);
            this.Name = "BoxMsg";
            this.Text = "BoxMsg";
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