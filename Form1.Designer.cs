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
            lblBmsScript = new Label();
            txtBmsScript = new TextBox();
            btnSelectBmsScript = new Button();
            lblInputFolder = new Label();
            txtInputFolder = new TextBox();
            btnSelectInputFolder = new Button();
            lblAvailableFormats = new Label();
            btnSelectFormat = new Button();
            lblSelectedFiles = new Label();
            btnExtract = new Button();
            progressBar = new ProgressBar();
            rtbFileNameInfo = new RichTextBox();
            lblNoExtensionWarning = new Label();
            SuspendLayout();
            // 
            // lblBmsScript
            // 
            lblBmsScript.AutoSize = true;
            lblBmsScript.Location = new Point(14, 20);
            lblBmsScript.Margin = new Padding(4, 0, 4, 0);
            lblBmsScript.Name = "lblBmsScript";
            lblBmsScript.Size = new Size(86, 17);
            lblBmsScript.TabIndex = 0;
            lblBmsScript.Text = "选择BMS脚本:";
            // 
            // txtBmsScript
            // 
            txtBmsScript.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBmsScript.Location = new Point(104, 16);
            txtBmsScript.Margin = new Padding(4);
            txtBmsScript.Name = "txtBmsScript";
            txtBmsScript.Size = new Size(645, 23);
            txtBmsScript.TabIndex = 1;
            // 
            // btnSelectBmsScript
            // 
            btnSelectBmsScript.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSelectBmsScript.Location = new Point(756, 13);
            btnSelectBmsScript.Margin = new Padding(4);
            btnSelectBmsScript.Name = "btnSelectBmsScript";
            btnSelectBmsScript.Size = new Size(88, 30);
            btnSelectBmsScript.TabIndex = 2;
            btnSelectBmsScript.Text = "选择脚本";
            btnSelectBmsScript.UseVisualStyleBackColor = true;
            btnSelectBmsScript.Click += btnSelectBmsScript_Click;
            // 
            // lblInputFolder
            // 
            lblInputFolder.AutoSize = true;
            lblInputFolder.Location = new Point(14, 54);
            lblInputFolder.Margin = new Padding(4, 0, 4, 0);
            lblInputFolder.Name = "lblInputFolder";
            lblInputFolder.Size = new Size(71, 17);
            lblInputFolder.TabIndex = 3;
            lblInputFolder.Text = "选择文件夹:";
            // 
            // txtInputFolder
            // 
            txtInputFolder.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtInputFolder.Location = new Point(104, 50);
            txtInputFolder.Margin = new Padding(4);
            txtInputFolder.Name = "txtInputFolder";
            txtInputFolder.Size = new Size(645, 23);
            txtInputFolder.TabIndex = 4;
            // 
            // btnSelectInputFolder
            // 
            btnSelectInputFolder.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSelectInputFolder.Location = new Point(756, 47);
            btnSelectInputFolder.Margin = new Padding(4);
            btnSelectInputFolder.Name = "btnSelectInputFolder";
            btnSelectInputFolder.Size = new Size(88, 30);
            btnSelectInputFolder.TabIndex = 5;
            btnSelectInputFolder.Text = "选择路径";
            btnSelectInputFolder.UseVisualStyleBackColor = true;
            btnSelectInputFolder.Click += btnSelectInputFolder_Click;
            // 
            // lblAvailableFormats
            // 
            lblAvailableFormats.AutoSize = true;
            lblAvailableFormats.Location = new Point(14, 105);
            lblAvailableFormats.Margin = new Padding(4, 0, 4, 0);
            lblAvailableFormats.Name = "lblAvailableFormats";
            lblAvailableFormats.Size = new Size(0, 17);
            lblAvailableFormats.TabIndex = 7;
            // 
            // btnSelectFormat
            // 
            btnSelectFormat.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSelectFormat.Location = new Point(756, 102);
            btnSelectFormat.Margin = new Padding(4);
            btnSelectFormat.Name = "btnSelectFormat";
            btnSelectFormat.Size = new Size(88, 30);
            btnSelectFormat.TabIndex = 8;
            btnSelectFormat.Text = "选择格式";
            btnSelectFormat.UseVisualStyleBackColor = true;
            btnSelectFormat.Click += btnSelectFormat_Click;
            // 
            // lblSelectedFiles
            // 
            lblSelectedFiles.AutoSize = true;
            lblSelectedFiles.Location = new Point(14, 133);
            lblSelectedFiles.Margin = new Padding(4, 0, 4, 0);
            lblSelectedFiles.Name = "lblSelectedFiles";
            lblSelectedFiles.Size = new Size(68, 17);
            lblSelectedFiles.TabIndex = 9;
            lblSelectedFiles.Text = "未选择文件";
            // 
            // btnExtract
            // 
            btnExtract.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExtract.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnExtract.Location = new Point(769, 154);
            btnExtract.Margin = new Padding(4);
            btnExtract.Name = "btnExtract";
            btnExtract.Size = new Size(75, 52);
            btnExtract.TabIndex = 10;
            btnExtract.Text = "解包";
            btnExtract.UseVisualStyleBackColor = true;
            btnExtract.Click += btnExtract_Click;
            // 
            // progressBar
            // 
            progressBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            progressBar.Location = new Point(18, 165);
            progressBar.Margin = new Padding(4);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(731, 30);
            progressBar.TabIndex = 11;
            progressBar.Visible = false;
            // 
            // rtbFileNameInfo
            // 
            rtbFileNameInfo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbFileNameInfo.Location = new Point(18, 225);
            rtbFileNameInfo.Margin = new Padding(4);
            rtbFileNameInfo.Name = "rtbFileNameInfo";
            rtbFileNameInfo.Size = new Size(826, 200);
            rtbFileNameInfo.TabIndex = 12;
            rtbFileNameInfo.Text = "";
            // 
            // lblNoExtensionWarning
            // 
            lblNoExtensionWarning.AutoSize = true;
            lblNoExtensionWarning.ForeColor = Color.Red;
            lblNoExtensionWarning.Location = new Point(14, 88);
            lblNoExtensionWarning.Margin = new Padding(4, 0, 4, 0);
            lblNoExtensionWarning.Name = "lblNoExtensionWarning";
            lblNoExtensionWarning.Size = new Size(0, 17);
            lblNoExtensionWarning.TabIndex = 6;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(862, 437);
            Controls.Add(rtbFileNameInfo);
            Controls.Add(progressBar);
            Controls.Add(btnExtract);
            Controls.Add(lblSelectedFiles);
            Controls.Add(btnSelectFormat);
            Controls.Add(lblAvailableFormats);
            Controls.Add(lblNoExtensionWarning);
            Controls.Add(btnSelectInputFolder);
            Controls.Add(txtInputFolder);
            Controls.Add(lblInputFolder);
            Controls.Add(btnSelectBmsScript);
            Controls.Add(txtBmsScript);
            Controls.Add(lblBmsScript);
            Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            Margin = new Padding(4);
            MinimumSize = new Size(581, 242);
            Name = "MainForm";
            Text = "quickbms批量解包工具";
            ResumeLayout(false);
            PerformLayout();
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
        private System.Windows.Forms.Label lblNoExtensionWarning;
    }
}
