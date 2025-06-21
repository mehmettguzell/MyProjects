using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing.Imaging;
using SmartLGS.Core.Interfaces;
using System.Drawing;
using System;
using System.Linq;
using SmartLGS.Core.Models;

namespace SmartLGS.Services
{
    class PdfExportService : IPdfExportService
    {
        private readonly ILoadDataService _loadDataService;

        public PdfExportService(ILoadDataService loadDataService)
        {
            _loadDataService = loadDataService;
        }

        public void ExportDataGridViewToPdf(DataGridView dgv,
                                          string filePath,
                                          string username,
                                          int userId,
                                          int examId,
                                          string examName,
                                          List<(int examId, int net)> results,
                                          string DateOfExam,
                                          string StuName,
                                          string StuSurname)
        {
            Document pdfDoc = new Document(PageSize.A4, 20f, 20f, 20f, 20f);
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(filePath, FileMode.Create));
            pdfDoc.Open();

            string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
            BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            iTextSharp.text.Font titleFont = new iTextSharp.text.Font(bf, 14, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font headerFont = new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font normalFont = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font smallFont = new iTextSharp.text.Font(bf, 8, iTextSharp.text.Font.NORMAL);

            // Header Section
            pdfDoc.Add(new Paragraph("MARMARA EĞİTİM KURUMLARI", titleFont) { Alignment = Element.ALIGN_CENTER, SpacingAfter = 10f });
            PdfPTable headerTable = new PdfPTable(2) { WidthPercentage = 100, SpacingAfter = 10f };
            headerTable.SetWidths(new float[] { 1f, 1f });

            headerTable.AddCell(CreateCell("Ad Soyad:", headerFont, normalFont, StuName + " " + StuSurname));
            headerTable.AddCell(CreateCell("Sınav Adı:", headerFont, normalFont, examName));
            headerTable.AddCell(CreateCell("Sınav Tarihi:", headerFont, normalFont, DateOfExam));
            pdfDoc.Add(headerTable);

            // Subject Analysis Section
            pdfDoc.Add(new Paragraph("DERS ANALİZİ", headerFont) { SpacingBefore = 10f, SpacingAfter = 5f });
            PdfPTable dersTable = new PdfPTable(dgv.ColumnCount) { WidthPercentage = 100, SpacingAfter = 10f };
            dersTable.HeaderRows = 1;

            foreach (DataGridViewColumn column in dgv.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, headerFont))
                {
                    BackgroundColor = new BaseColor(240, 240, 240),
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Padding = 5f
                };
                dersTable.AddCell(cell);
            }

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!row.IsNewRow)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        PdfPCell pdfCell = new PdfPCell(new Phrase(cell.Value?.ToString() ?? string.Empty, normalFont))
                        {
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            Padding = 5f
                        };
                        dersTable.AddCell(pdfCell);
                    }
                }
            }
            pdfDoc.Add(dersTable);

            // Last Exams Results Section
            pdfDoc.Add(new Paragraph("ÖĞRENCİNİN SON SINAVLARDAKİ NETLERİ", headerFont) { SpacingBefore = 10f, SpacingAfter = 5f });
            List<LastNExam> netSummaries = _loadDataService.GetLastNExams(userId, 5);

            PdfPTable resultTable = new PdfPTable(9) { WidthPercentage = 100, SpacingAfter = 10f };
            resultTable.SetWidths(new float[] { 1f, 2f, 3f, 1f, 1f, 1f, 1f, 1f, 1f });

            string[] headers = { "NO", "TARIH", "SINAV ADI", "Turkce", "Inkilap", "Din", "Ingilizce", "Matematik", "Fen" };
            foreach (var head in headers)
                resultTable.AddCell(CreateHeaderCell(head, headerFont));

            int no = 1;
            foreach (var item in netSummaries)
            {
                resultTable.AddCell(CreateCell(no++.ToString(), normalFont));
                resultTable.AddCell(CreateCell(item.ExamDate.ToString("dd.MM.yyyy"), normalFont));
                resultTable.AddCell(CreateCell(item.ExamName, normalFont));
                resultTable.AddCell(CreateCell(item.TurkceNet.ToString("F2"), normalFont));
                resultTable.AddCell(CreateCell(item.InkilapNet.ToString("F2"), normalFont));
                resultTable.AddCell(CreateCell(item.DinNet.ToString("F2"), normalFont));
                resultTable.AddCell(CreateCell(item.IngilizceNet.ToString("F2"), normalFont));
                resultTable.AddCell(CreateCell(item.MatematikNet.ToString("F2"), normalFont));
                resultTable.AddCell(CreateCell(item.FenNet.ToString("F2"), normalFont));
            }

            pdfDoc.Add(resultTable);

            pdfDoc.Add(new Paragraph("NET PERFORMANS GRAFİĞİ", headerFont) { SpacingBefore = 10f, SpacingAfter = 5f });
            Chart chart = new Chart { Size = new Size(500, 300) };
            _loadDataService.LoadChartFromData(chart, results);
            using (MemoryStream ms = new MemoryStream())
            {
                chart.SaveImage(ms, ChartImageFormat.Png);
                iTextSharp.text.Image chartImage = iTextSharp.text.Image.GetInstance(ms.ToArray());
                chartImage.ScaleToFit(500f, 300f);
                chartImage.Alignment = Element.ALIGN_CENTER;
                pdfDoc.Add(chartImage);
            }

            pdfDoc.Close();
        }

        private PdfPCell CreateHeaderCell(string text, iTextSharp.text.Font font)
        {
            return new PdfPCell(new Phrase(text, font))
            {
                BackgroundColor = new BaseColor(240, 240, 240),
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 5f
            };
        }

        private PdfPCell CreateCell(string label, iTextSharp.text.Font labelFont, iTextSharp.text.Font valueFont, string value)
        {
            PdfPCell cell = new PdfPCell(new Phrase(label + " " + value, valueFont))
            {
                Border = iTextSharp.text.Rectangle.NO_BORDER,
                Padding = 5f
            };
            return cell;
        }
        private PdfPCell CreateCell(string text, iTextSharp.text.Font font)
        {
            return new PdfPCell(new Phrase(text, font))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 5f
            };
        }
    }
}