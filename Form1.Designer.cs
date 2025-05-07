namespace QuickBMSBatchExtractor
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblBmsScript = new System.Windows.Forms.Label();
            this.txtBmsScript = new System.Windows.Forms.TextBox();
            this.btnSelectBmsScript = new System.Windows.Forms.Button();
            this.lblInputFolder = new System.Windows.Forms.Label();
            this.txtInputFolder = new System.Windows.Forms.TextBox();
            this.btnSelectInputFolder = new System.Windows.Forms.Button();
            this.lblAvailableFormats = new System.Windows.Forms.Label();
            this.btnSelectFormat = new System.Windows.Forms.Button();
            this.lblSelectedFiles = new System.Windows.Forms.Label();
            this.btnExtract = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.rtbFileNameInfo = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // lblBmsScript
            // 
            this.lblBmsScript.AutoSize = true;
            this.lblBmsScript.Location = new System.Drawing.Point(14, 20);
            this.lblBmsScript.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBmsScript.Name = "lblBmsScript";
            this.lblBmsScript.Size = new System.Drawing.Size(86, 17);
            this.lblBmsScript.TabIndex = 0;
            this.lblBmsScript.Text = "选择BMS脚本:";
            // 
            // txtBmsScript
            // 
            this.txtBmsScript.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBmsScript.Location = new System.Drawing.Point(104, 16);
            this.txtBmsScript.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBmsScript.Name = "txtBmsScript";
            this.txtBmsScript.Size = new System.Drawing.Size(645, 23);
            this.txtBmsScript.TabIndex = 1;
            // 
            // btnSelectBmsScript
            // 
            this.btnSelectBmsScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectBmsScript.Location = new System.Drawing.Point(756, 13);
            this.btnSelectBmsScript.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSelectBmsScript.Name = "btnSelectBmsScript";
            this.btnSelectBmsScript.Size = new System.Drawing.Size(88, 30);
            this.btnSelectBmsScript.TabIndex = 2;
            this.btnSelectBmsScript.Text = "选择脚本";
            this.btnSelectBmsScript.UseVisualStyleBackColor = true;
            this.btnSelectBmsScript.Click += new System.EventHandler(this.btnSelectBmsScript_Click);
            // 
            // lblInputFolder
            // 
            this.lblInputFolder.AutoSize = true;
            this.lblInputFolder.Location = new System.Drawing.Point(14, 54);
            this.lblInputFolder.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInputFolder.Name = "lblInputFolder";
            this.lblInputFolder.Size = new System.Drawing.Size(71, 17);
            this.lblInputFolder.TabIndex = 3;
            this.lblInputFolder.Text = "选择文件夹:";
            // 
            // txtInputFolder
            // 
            this.txtInputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInputFolder.Location = new System.Drawing.Point(104, 50);
            this.txtInputFolder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtInputFolder.Name = "txtInputFolder";
            this.txtInputFolder.Size = new System.Drawing.Size(645, 23);
            this.txtInputFolder.TabIndex = 4;
            // 
            // btnSelectInputFolder
            // 
            this.btnSelectInputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectInputFolder.Location = new System.Drawing.Point(756, 47);
            this.btnSelectInputFolder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSelectInputFolder.Name = "btnSelectInputFolder";
            this.btnSelectInputFolder.Size = new System.Drawing.Size(88, 30);
            this.btnSelectInputFolder.TabIndex = 5;
            this.btnSelectInputFolder.Text = "选择路径";
            this.btnSelectInputFolder.UseVisualStyleBackColor = true;
            this.btnSelectInputFolder.Click += new System.EventHandler(this.btnSelectInputFolder_Click);
            // 
            // lblAvailableFormats
            // 
            this.lblAvailableFormats.AutoSize = true;
            this.lblAvailableFormats.Location = new System.Drawing.Point(14, 88);
            this.lblAvailableFormats.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAvailableFormats.Name = "lblAvailableFormats";
            this.lblAvailableFormats.Size = new System.Drawing.Size(0, 17);
            this.lblAvailableFormats.TabIndex = 6;
            // 
            // btnSelectFormat
            // 
            this.btnSelectFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectFormat.Location = new System.Drawing.Point(756, 81);
            this.btnSelectFormat.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSelectFormat.Name = "btnSelectFormat";
            this.btnSelectFormat.Size = new System.Drawing.Size(88, 30);
            this.btnSelectFormat.TabIndex = 7;
            this.btnSelectFormat.Text = "选择格式";
            this.btnSelectFormat.UseVisualStyleBackColor = true;
            this.btnSelectFormat.Click += new System.EventHandler(this.btnSelectFormat_Click);
            // 
            // lblSelectedFiles
            // 
            this.lblSelectedFiles.AutoSize = true;
            this.lblSelectedFiles.Location = new System.Drawing.Point(14, 88);
            this.lblSelectedFiles.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectedFiles.Name = "lblSelectedFiles";
            this.lblSelectedFiles.Size = new System.Drawing.Size(68, 17);
            this.lblSelectedFiles.TabIndex = 8;
            this.lblSelectedFiles.Text = "未选择文件";
            // 
            // btnExtract
            // 
            this.btnExtract.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExtract.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExtract.Location = new System.Drawing.Point(756, 119);
            this.btnExtract.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExtract.Name = "btnExtract";
            this.btnExtract.Size = new System.Drawing.Size(88, 52);
            this.btnExtract.TabIndex = 9;
            this.btnExtract.Text = "解包";
            this.btnExtract.UseVisualStyleBackColor = true;
            this.btnExtract.Click += new System.EventHandler(this.btnExtract_Click);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(18, 119);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(731, 30);
            this.progressBar.TabIndex = 10;
            this.progressBar.Visible = false;
            // 
            // rtbFileNameInfo
            // 
            this.rtbFileNameInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbFileNameInfo.Location = new System.Drawing.Point(18, 177);
            this.rtbFileNameInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rtbFileNameInfo.Name = "rtbFileNameInfo";
            this.rtbFileNameInfo.Size = new System.Drawing.Size(835, 247);
            this.rtbFileNameInfo.TabIndex = 11;
            this.rtbFileNameInfo.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 437);
            this.Controls.Add(this.rtbFileNameInfo);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnExtract);
            this.Controls.Add(this.lblSelectedFiles);
            this.Controls.Add(this.btnSelectFormat);
            this.Controls.Add(this.lblAvailableFormats);
            this.Controls.Add(this.btnSelectInputFolder);
            this.Controls.Add(this.txtInputFolder);
            this.Controls.Add(this.lblInputFolder);
            this.Controls.Add(this.btnSelectBmsScript);
            this.Controls.Add(this.txtBmsScript);
            this.Controls.Add(this.lblBmsScript);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(581, 242);
            this.Name = "MainForm";
            this.Text = "quickbms批量解包工具";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBmsScript;
        private System.Windows.Forms.TextBox txtBmsScript;
        private System.Windows.Forms.Button btnSelectBmsScript;
        private System.Windows.Forms.Label lblInputFolder;
        private System.Windows.Forms.TextBox txtInputFolder;
        private System.Windows.Forms.Button btnSelectInputFolder;
        private System.Windows.Forms.Label lblAvailableFormats;
        private System.Windows.Forms.Button btnSelectFormat;
        private System.Windows.Forms.Label lblSelectedFiles;
        private System.Windows.Forms.Button btnExtract;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.RichTextBox rtbFileNameInfo;
    }
}