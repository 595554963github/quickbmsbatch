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
        }

        private void btnSelectBmsScript_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "BMS�ļ�(*.bms)|*.bms";
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
                    lblAvailableFormats.Text = $"��⵽��ʽ: {string.Join(", ", availableFormats)}";
                    AppendToRichTextBox($"�ļ���ɨ����ɣ��ҵ����¸�ʽ: {string.Join(", ", availableFormats)}\n");
                }
                else
                {
                    lblAvailableFormats.Text = "�ļ�����û���ļ�";
                    AppendToRichTextBox("�ļ�����û���ҵ��κ��ļ�\n");
                }
            }
            catch (Exception ex)
            {
                AppendToRichTextBox($"ɨ���ļ���ʱ����: {ex.Message}\n");
            }
        }

        private void btnSelectFormat_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(inputFolderPath))
            {
                AppendToRichTextBox("����ѡ���ļ���\n");
                return;
            }

            if (availableFormats.Count == 0)
            {
                AppendToRichTextBox("�ļ�����û�п��õ��ļ���ʽ\n");
                return;
            }

            var fileTypes = new List<string>();
            foreach (string format in availableFormats)
            {
                string extName = format.Substring(1).ToUpper() + " files";
                fileTypes.Add($"{extName}|*{format}");
            }
            fileTypes.Add("�����ļ�|*.*");

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = inputFolderPath;
                openFileDialog.Multiselect = true;
                openFileDialog.Filter = string.Join("|", fileTypes);

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFiles = openFileDialog.FileNames.ToList();
                    UpdateSelectedFilesLabel();
                    AppendToRichTextBox($"��ѡ�� {selectedFiles.Count} �� {Path.GetExtension(selectedFiles.First())} �ļ�\n");
                }
            }
        }

        private void UpdateSelectedFilesLabel()
        {
            if (selectedFiles.Count > 0)
            {
                string ext = Path.GetExtension(selectedFiles[0]);
                lblSelectedFiles.Text = $"��ѡ�� {selectedFiles.Count} �� {ext} �ļ�";
            }
            else
            {
                lblSelectedFiles.Text = "δѡ���ļ�";
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
                        AppendToRichTextBox($"���ڴ���: {Path.GetFileName(filePath)}\n");

                        string quickbmsExecutable = GetQuickBMSExecutable(filePath);
                        string? directoryName = Path.GetDirectoryName(filePath);
                        if (directoryName == null)
                        {
                            AppendToRichTextBox($"�޷���ȡ�ļ���·��: {filePath}\n");
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
                                AppendToRichTextBox($"��������ʧ�ܣ������ļ� {filePath} ʱ����\n");
                                continue;
                            }
                            process.WaitForExit();
                        }

                        currentFiles++;
                        int percentage = (int)((double)currentFiles / totalFiles * 100);
                        progress.Report(percentage);

                        AppendToRichTextBox($"���: {Path.GetFileName(filePath)}\n");
                    }
                    catch (Exception ex)
                    {
                        AppendToRichTextBox($"�����ļ� {filePath} ʱ����: {ex.Message}\n");
                    }
                }
            });
        }

        private async void btnExtract_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(bmsScriptPath))
            {
                AppendToRichTextBox("��ѡ��BMS�ű�\n");
                return;
            }

            if (selectedFiles.Count == 0)
            {
                AppendToRichTextBox("��ѡ��Ҫ������ļ�\n");
                return;
            }

            btnExtract.Enabled = false;
            progressBar.Value = 0;
            progressBar.Visible = true;
            AppendToRichTextBox($"��ʼ��� {selectedFiles.Count} ���ļ�...\n");

            try
            {
                var progress = new Progress<int>(percent =>
                {
                    progressBar.Value = percent;
                });

                await ExtractFilesAsync(bmsScriptPath, selectedFiles, progress);

                AppendToRichTextBox("������!\n");
            }
            catch (Exception ex)
            {
                AppendToRichTextBox($"��������г���: {ex.Message}\n");
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
                AppendToRichTextBox("������ļ���·�������ڣ���������������ѡ��\n");
                txtInputFolder.Text = "";
            }
        }
    }
}