using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickBMSBatchExtractor
{
    public partial class MainForm : Form
    {
        private string bmsScriptPath = "";
        private string inputFolderPath = "";
        private List<string> selectedFiles = new List<string>();
        private List<string> availableFormats = new List<string>();
        private bool hasNoExtensionFiles = false; 

        public MainForm()
        {
            InitializeComponent();
            txtInputFolder.TextChanged += txtInputFolder_TextChanged;
            txtBmsScript.TextChanged += txtBmsScript_TextChanged;
        }

        private void btnSelectBmsScript_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "BMS文件(*.bms)|*.bms";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    bmsScriptPath = openFileDialog.FileName;
                    txtBmsScript.Text = bmsScriptPath;
                }
            }
        }

        private void btnSelectInputFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    inputFolderPath = folderBrowserDialog.SelectedPath;
                    txtInputFolder.Text = inputFolderPath;
                    ScanFolderFormats(inputFolderPath);
                }
            }
        }

        private void ScanFolderFormats(string folder)
        {
            try
            {
                availableFormats.Clear();
                hasNoExtensionFiles = false;
                var extensions = new HashSet<string>();

                foreach (string file in Directory.EnumerateFiles(folder, "*", SearchOption.AllDirectories))
                {
                    string ext = Path.GetExtension(file).ToLower();
                    if (string.IsNullOrEmpty(ext))
                    {
                        hasNoExtensionFiles = true;
                    }
                    else
                    {
                        extensions.Add(ext);
                    }
                }

                availableFormats = extensions.OrderBy(x => x).ToList();
                UpdateFormatDisplay();
            }
            catch (Exception ex)
            {
                AppendToRichTextBox($"扫描文件夹时出错: {ex.Message}\n");
            }
        }

        private void UpdateFormatDisplay()
        {
            string displayText = "检测到格式: ";
            if (hasNoExtensionFiles)
            {
                displayText += "无后缀文件, ";
                lblAvailableFormats.ForeColor = Color.Red;
            }
            else
            {
                lblAvailableFormats.ForeColor = Color.Black;
            }

            if (availableFormats.Count > 0)
            {
                displayText += string.Join(", ", availableFormats);
            }
            else if (!hasNoExtensionFiles)
            {
                displayText = "文件夹中没有文件";
            }

            lblAvailableFormats.Text = displayText.TrimEnd(',', ' ');
        }

        private void btnSelectFormat_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(inputFolderPath))
            {
                AppendToRichTextBox("请先选择文件夹\n");
                return;
            }

            List<string> fileTypes = new List<string>();
            if (hasNoExtensionFiles)
            {
                fileTypes.Add("无后缀文件|*.*"); 
            }

            foreach (string format in availableFormats)
            {
                string extName = format.Substring(1).ToUpper() + " Files";
                fileTypes.Add($"{extName}|*{format}");
            }

            fileTypes.Add("所有文件|*.*");

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = inputFolderPath;
                openFileDialog.Multiselect = true;
                openFileDialog.Filter = string.Join("|", fileTypes);

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFiles = openFileDialog.FileNames.ToList();
                    UpdateSelectedFilesLabel();
                    AppendToRichTextBox($"已选择 {selectedFiles.Count} 个文件（包含无后缀文件: {selectedFiles.Any(f => string.IsNullOrEmpty(Path.GetExtension(f)))}）\n");
                }
            }
        }

        private void UpdateSelectedFilesLabel()
        {
            if (selectedFiles.Count > 0)
            {
                bool hasNoExt = selectedFiles.Any(f => string.IsNullOrEmpty(Path.GetExtension(f)));
                lblSelectedFiles.Text = hasNoExt ?
                    $"已选择 {selectedFiles.Count} 个文件（包含无后缀文件）" :
                    $"已选择 {selectedFiles.Count} 个 {Path.GetExtension(selectedFiles[0])} 文件";
            }
            else
            {
                lblSelectedFiles.Text = "未选择文件";
            }
        }

        private string GetQuickBMSExecutable(string filePath)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(filePath);
                return fileInfo.Length > 3L * 1024 * 1024 * 1024 ? "quickbms_4gb_files.exe" : "quickbms.exe";
            }
            catch
            {
                return "quickbms.exe";
            }
        }

        private void AppendToRichTextBox(string text)
        {
            if (this.rtbFileNameInfo.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate {
                    rtbFileNameInfo.AppendText(text);
                });
            }
            else
            {
                rtbFileNameInfo.AppendText(text);
            }
        }

        private async Task ExtractFilesAsync(string bmsScript, List<string> files, IProgress<int> progress)
        {
            await Task.Run(() =>
            {
                int totalFiles = files.Count;
                int currentFiles = 0;

                foreach (string filePath in files)
                {
                    try
                    {
                        if (!File.Exists(filePath))
                        {
                            AppendToRichTextBox($"跳过不存在的文件: {Path.GetFileName(filePath)}\n");
                            continue;
                        }

                        AppendToRichTextBox($"正在处理: {Path.GetFileName(filePath)}\n");

                        string quickbmsExecutable = GetQuickBMSExecutable(filePath);

                        string? directoryName = Path.GetDirectoryName(filePath);
                        if (string.IsNullOrEmpty(directoryName))
                        {
                            AppendToRichTextBox($"无法获取文件目录: {filePath}\n");
                            continue;
                        }

                        string outputFolder = Path.Combine(
                            directoryName,
                            Path.GetFileNameWithoutExtension(filePath));

                        Directory.CreateDirectory(outputFolder);

                        string fileExtension = Path.GetExtension(filePath) ?? "*";
                        string arguments = $"-o -F \"*{fileExtension}\" \"{bmsScript}\" \"{filePath}\" \"{outputFolder}\"";

                        ProcessStartInfo startInfo = new ProcessStartInfo
                        {
                            FileName = quickbmsExecutable,
                            Arguments = arguments,
                            UseShellExecute = false,
                            CreateNoWindow = true
                        };

                        using Process? process = Process.Start(startInfo);
                        if (process == null)
                        {
                            AppendToRichTextBox($"无法启动解包进程: {quickbmsExecutable}\n");
                            continue;
                        }

                        process.WaitForExit();

                        currentFiles++;
                        progress.Report((int)((double)currentFiles / totalFiles * 100));
                        AppendToRichTextBox($"完成: {Path.GetFileName(filePath)}\n");
                    }
                    catch (Exception ex)
                    {
                        AppendToRichTextBox($"处理文件 {filePath} 时出错: {ex.Message}\n");
                    }
                }
            });
        }

        private async void btnExtract_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(bmsScriptPath))
            {
                AppendToRichTextBox("请选择BMS脚本\n");
                return;
            }

            if (selectedFiles.Count == 0)
            {
                AppendToRichTextBox("请选择要解包的文件\n");
                return;
            }

            btnExtract.Enabled = false;
            progressBar.Visible = true;
            progressBar.Value = 0;
            AppendToRichTextBox($"开始解包 {selectedFiles.Count} 个文件...\n");

            try
            {
                var progress = new Progress<int>(percent => progressBar.Value = percent);
                await ExtractFilesAsync(bmsScriptPath, selectedFiles, progress);
                AppendToRichTextBox("解包完成!\n");
            }
            catch (Exception ex)
            {
                AppendToRichTextBox($"解包过程中出错: {ex.Message}\n");
            }
            finally
            {
                btnExtract.Enabled = true;
                progressBar.Visible = false;
            }
        }

        private void txtInputFolder_TextChanged(object? sender, EventArgs e)
        {
            string inputPath = txtInputFolder.Text;
            if (!string.IsNullOrEmpty(inputPath))
            {
                if (Directory.Exists(inputPath))
                {
                    inputFolderPath = inputPath;
                    ScanFolderFormats(inputPath);
                }
                else
                {
                    AppendToRichTextBox("输入的文件夹路径不存在，请检查后重新输入或选择。\n");
                    txtInputFolder.Text = "";
                }
            }
        }

        private void txtBmsScript_TextChanged(object? sender, EventArgs e)
        {
            string scriptPath = txtBmsScript.Text;
            if (!string.IsNullOrEmpty(scriptPath))
            {
                if (File.Exists(scriptPath) && scriptPath.EndsWith(".bms", StringComparison.OrdinalIgnoreCase))
                {
                    bmsScriptPath = scriptPath;
                    AppendToRichTextBox($"已设置BMS脚本: {Path.GetFileName(scriptPath)}\n");
                }
                else
                {
                    AppendToRichTextBox("输入的BMS脚本路径不存在或不是有效的BMS文件，请检查后重新输入或选择。\n");
                    bmsScriptPath = "";
                }
            }
            else
            {
                bmsScriptPath = "";
            }
        }
    }
}
