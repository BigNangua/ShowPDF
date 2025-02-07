using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PdfiumViewer;
using System.IO;

namespace ShowPDF
{
    public partial class Form1 : Form
    {
        // http://www.pdf995.com/samples/pdf.pdf
        public Form1()
        {
            InitializeComponent();
            string pdfUrl = "http://www.pdf995.com/samples/pdf.pdf";
            // 调用方法加载 PDF
            LoadPdfFromUrl(pdfUrl);
        }

        /// PDF显示
        /// </summary>
        /// <param name="pdfUrl">PDF文件路径（网络路径）</param>
        private void LoadPdfFromUrl(string pdfUrl)
        {
            // 使用 Guid 生成唯一的文件名
            string uniquePdfFileName = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

            try
            {
                // 如果 pdfViewer1.Document 已经有文档，先释放掉
                if (pdfViewer1.Document != null)
                {
                    pdfViewer1.Document.Dispose();  // 释放当前的文档资源（假设 `Dispose` 是释放资源的正确方法）
                    pdfViewer1.Document = null;    // 清空文档
                }

                // 下载 PDF 文件到唯一的本地路径
                using (var webClient = new System.Net.WebClient())
                {
                    webClient.DownloadFile(pdfUrl, uniquePdfFileName);
                }

                // 加载新的 PDF 文件
                pdfViewer1.Document = OpenDocument(uniquePdfFileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("下载或加载 PDF 文件时出错: " + ex.Message);
            }
        }

        private PdfDocument OpenDocument(string fileName)
        {
            try
            {
                return PdfDocument.Load(this, fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
