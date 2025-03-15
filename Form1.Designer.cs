namespace FileSplitter
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.numParts = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSplit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numParts)).BeginInit();
            this.SuspendLayout();
            
            // txtFilePath
            this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath.Location = new System.Drawing.Point(12, 12);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(320, 23);
            this.txtFilePath.TabIndex = 0;
            
            // btnBrowse
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(338, 12);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(90, 23);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "浏览...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            
            // numParts
            this.numParts.Location = new System.Drawing.Point(12, 50);
            this.numParts.Name = "numParts";
            this.numParts.Minimum = new decimal(new int[] {2, 0, 0, 0});
            this.numParts.Maximum = new decimal(new int[] {int.MaxValue, 0, 0, 0});
            this.numParts.ThousandsSeparator = true;
            this.numParts.Size = new System.Drawing.Size(100, 23);
            this.numParts.TabIndex = 2;
            
            // label1
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(118, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 15);
            this.label1.Text = "分割数量";
            
            // btnSplit
            this.btnSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSplit.Location = new System.Drawing.Point(338, 120);
            this.btnSplit.Name = "btnSplit";
            this.btnSplit.Size = new System.Drawing.Size(90, 30);
            this.btnSplit.TabIndex = 4;
            this.btnSplit.Text = "开始分割";
            this.btnSplit.UseVisualStyleBackColor = true;
            this.btnSplit.Click += new System.EventHandler(this.btnSplit_Click);
            
            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 160);
            this.MinimumSize = new System.Drawing.Size(456, 199);
            this.Controls.Add(this.btnSplit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numParts);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtFilePath);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Form1";
            this.Text = "文件分割工具";
            ((System.ComponentModel.ISupportInitialize)(this.numParts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.NumericUpDown numParts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSplit;
    }
}