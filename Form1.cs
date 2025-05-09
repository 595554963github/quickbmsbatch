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
                var extensions = new HashSet<string>();

                foreach (string file in Directory.EnumerateFiles(folder, "*.*", SearchOption.AllDirectories))
                {
                    string ext = Path.GetExtension(file).ToLower();
                    if (!string.IsNullOrEmpty(ext))
                    {
                        extensions.Add(ext);
                    }
                }

                availableFormats = extensions.OrderBy(x => x).ToList();
                if (availableFormats.Count > 0)
                {
                    lblAvailableFormats.Text = $"检测到格式: {string.Join(", ", availableFormats)}";
                    AppendToRichTextBox($"文件夹扫描完成，找到以下格式: {string.Join(", ", availableFormats)}\n");
                }
                else
                {
                    lblAvailableFormats.Text = "文件夹中没有文件";
                    AppendToRichTextBox("文件夹中没有找到任何文件\n");
                }
            }
            catch (Exception ex)
            {
                AppendToRichTextBox($"扫描文件夹时出错: {ex.Message}\n");
            }
        }

        private void btnSelectFormat_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(inputFolderPath))
            {
                AppendToRichTextBox("请先选择文件夹\n");
                return;
            }

            if (availableFormats.Count == 0)
            {
                AppendToRichTextBox("文件夹中没有可用的文件格式\n");
                return;
            }

            var fileTypes = new List<string>();
            foreach (string format in availableFormats)
            {
                string extName = format.Substring(1).ToUpper() + " files";
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
                    AppendToRichTextBox($"已选择 {selectedFiles.Count} 个 {Path.GetExtension(selectedFiles.First())} 文件\n");
                }
            }
        }

        private void UpdateSelectedFilesLabel()
        {
            if (selectedFiles.Count > 0)
            {
                string ext = Path.GetExtension(selectedFiles[0]);
                lblSelectedFiles.Text = $"已选择 {selectedFiles.Count} 个 {ext} 文件";
            }
            else
            {
                lblSelectedFiles.Text = "未选择文件";
            }
        }

        private string GetQuickBMSExecutable(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            return fileInfo.Length > 3L * 1024 * 1024 * 1024 ? "quickbms_4gb_files.exe" : "quickbms.exe";
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
                        AppendToRichTextBox($"正在处理: {Path.GetFileName(filePath)}\n");

                        string quickbmsExecutable = GetQuickBMSExecutable(filePath);
                        string? directoryName = Path.GetDirectoryName(filePath);
                        if (directoryName == null)
                        {
                            AppendToRichTextBox($"无法获取文件夹路径: {filePath}\n");
                            continue;
                        }

                        string outputFolder = Path.Combine(
                            directoryName,
                            Path.GetFileNameWithoutExtension(filePath));

                        if (!Directory.Exists(outputFolder))
                        {
                            Directory.CreateDirectory(outputFolder);
                        }

                        string fileExtension = "*" + Path.GetExtension(filePath);
                        ProcessStartInfo startInfo = new ProcessStartInfo
                        {
                            FileName = quickbmsExecutable,
                            Arguments = $"-o -F \"{fileExtension}\" \"{bmsScript}\" \"{filePath}\" \"{outputFolder}\"",
                            UseShellExecute = false,
                            CreateNoWindow = true
                        };

                        using (Process? process = Process.Start(startInfo))
                        {
                            if (process == null)
                            {
                                AppendToRichTextBox($"启动进程失败，处理文件 {filePath} 时出错\n");
                                continue;
                            }
                            process.WaitForExit();
                        }

                        currentFiles++;
                        int percentage = (int)((double)currentFiles / totalFiles * 100);
                        progress.Report(percentage);

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
            progressBar.Value = 0;
            progressBar.Visible = true;
            AppendToRichTextBox($"开始解包 {selectedFiles.Count} 个文件...\n");

            try
            {
                var progress = new Progress<int>(percent =>
                {
                    progressBar.Value = percent;
                });

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
            }
        }

        private void txtInputFolder_TextChanged(object? sender, EventArgs e)
        {
            string inputPath = txtInputFolder.Text;
            if (!string.IsNullOrEmpty(inputPath) && Directory.Exists(inputPath))
            {
                inputFolderPath = inputPath;
                ScanFolderFormats(inputPath);
            }
            else if (!string.IsNullOrEmpty(inputPath) && !Directory.Exists(inputPath))
            {
                AppendToRichTextBox("输入的文件夹路径不存在，请检查后重新输入或选择。\n");
                txtInputFolder.Text = "";
            }
        }

        private void txtBmsScript_TextChanged(object? sender, EventArgs e)
        {
            string scriptPath = txtBmsScript.Text;
            if (!string.IsNullOrEmpty(scriptPath) && File.Exists(scriptPath) && scriptPath.EndsWith(".bms", StringComparison.OrdinalIgnoreCase))
            {
                bmsScriptPath = scriptPath;
                AppendToRichTextBox($"已设置BMS脚本: {Path.GetFileName(scriptPath)}\n");
            }
            else if (!string.IsNullOrEmpty(scriptPath) && (!File.Exists(scriptPath) || !scriptPath.EndsWith(".bms", StringComparison.OrdinalIgnoreCase)))
            {
                AppendToRichTextBox("输入的BMS脚本路径不存在或不是有效的BMS文件，请检查后重新输入或选择。\n");
                bmsScriptPath = "";
            }
            else
            {
                bmsScriptPath = "";
            }
        }
    }
}
