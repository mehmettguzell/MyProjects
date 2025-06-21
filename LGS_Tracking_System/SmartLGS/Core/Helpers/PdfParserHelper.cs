using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using SmartLGS.Core.Models;
using SmartLGS.Core.Interfaces;

namespace SmartLGS.Core.Helpers
{
    public class PdfParserHelper : IPdfParserHelper
    {
        public ParsedExamData ParseExamPdf(string pdfPath)
        {
            string text = string.Empty;

            using (var reader = new PdfReader(pdfPath))
            using (var pdfDoc = new PdfDocument(reader))
            {
                int pageCount = pdfDoc.GetNumberOfPages();
                for (int i = 1; i <= pageCount; i++)
                {
                    text += PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(i));
                }
            }

            var parsed = new ParsedExamData
            {
                ExamDate = DateTime.Now.ToString("yyyy-MM-dd"),
                CorrectAnswers = new List<string>().ToArray(),
                WrongAnswers = new List<string>().ToArray(),
                EmptyAnswers = new List<string>().ToArray(),
                IsValid = false
            };

            var correct = new List<string>();
            var wrong = new List<string>();
            var empty = new List<string>();

            var lines = text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            bool inTable = false;

            foreach (var rawLine in lines)
            {
                var line = rawLine.Trim();

                if (line.Contains("DERS ANALİZİ"))
                {
                    inTable = true;
                    continue;
                }

                if (inTable && (line.StartsWith("Ders Adı") || line.StartsWith("Ders Adi")))
                    continue;

                if (inTable && line.StartsWith("ÖĞRENCİNİN") || line.Contains("NET PERFORMANS"))
                {
                    break;
                }

                if (inTable)
                {
                    var parts = Regex.Split(line, @"\s+");

                    if (parts.Length >= 6)
                    {
                        correct.Add(parts[1]);
                        wrong.Add(parts[2]);
                        empty.Add(parts[3]);
                    }
                }
            }

            parsed.CorrectAnswers = correct.ToArray();
            parsed.WrongAnswers = wrong.ToArray();
            parsed.EmptyAnswers = empty.ToArray();
            parsed.IsValid = correct.Count > 0 && correct.Count == wrong.Count && correct.Count == empty.Count;

            return parsed;
        }
    }
}
