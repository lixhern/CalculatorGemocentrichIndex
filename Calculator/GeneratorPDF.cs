using System.Diagnostics;
using Calculator;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;

class GeneratorPDF
{
    public static void GeneratePDF(string filename, PrintInfo printInfo)
    {
        Document document = new Document();
        Section section = document.AddSection();

        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.TopMargin = Unit.FromCentimeter(2);
        section.PageSetup.BottomMargin = Unit.FromCentimeter(2);
        section.PageSetup.LeftMargin = Unit.FromCentimeter(2.5);  
        section.PageSetup.RightMargin = Unit.FromCentimeter(2.5);

        Paragraph date = section.AddParagraph(printInfo.Date.ToString("dd.MM.yyyy"));
        date.Format.Font.Size = 12;
        date.Format.Alignment = ParagraphAlignment.Left;
        date.Format.SpaceAfter = "10pt";

        Paragraph title = section.AddParagraph($"Гомоцентрические индексы\n{printInfo.PatientName}");
        title.Format.Font.Size = 20; 
        title.Format.Font.Bold = true;
        title.Format.Alignment = ParagraphAlignment.Center;

        Table table = section.AddTable();
        table.Borders.Width = 0;
        table.AddColumn(Unit.FromCentimeter(9)); 
        table.AddColumn(Unit.FromCentimeter(9)); 

        var leftData = new (string, double)[]
        {
            ("Нейтрофилы 10(3)", printInfo.Neitrofil),
            ("Лимфоциты 10(3)", printInfo.Limfocit),
            ("Моноциты 10(3)", printInfo.Monict),
        };

        var rightData = new (string, double, string)[]
        {
            ("LCR ед.", printInfo.LCRed, GetColor(printInfo.LCRed, 120, (v, c) => v <= c)),
            ("CLR ед.", printInfo.CLRed, GetColor(printInfo.CLRed, 77.7f, (v,c) => v >= c)),
            ("CAR ед.", printInfo.CARed, GetColor(printInfo.CARed, 2.51f, (v,c) => v >= c)),
            ("NLPR ед.", printInfo.NLPRed, GetColor(printInfo.NLPRed, 1.83f, (v, c) => v >= c)),
            ("NLR ед.", printInfo.NLRed, GetColor(printInfo.NLRed, 3.8f, (v, c) => v >= c)),
            ("Cally index ед.", printInfo.Cally, GetColor(printInfo.Cally, 47, (v, c) => v <= c)),
            ("ТИГ ед.", printInfo.TIG, GetColor(printInfo.TIG, 12.8f, (v, c) => v <= c)),
            ("SIRI ед.", printInfo.SIRI, GetColor(printInfo.SIRI, 3.06f, (v, c) => v >= c)),
            ("PNI ед.", printInfo.PNI, GetColor(printInfo.PNI, 37, (v, c) => v <= c)),
            ("MII ед.", printInfo.MII, GetColor(printInfo.MII, 334, (v, c) => v >= c))
        };

        int maxRows = Math.Max(leftData.Length, rightData.Length);
        for (int i = 0; i < maxRows; i++)
        {
            Row row = table.AddRow();
            row.TopPadding = 3;
            row.BottomPadding = 3;

            if (i < leftData.Length)
            {
                Paragraph nameParagraph = row.Cells[0].AddParagraph(leftData[i].Item1);
                nameParagraph.Format.Font.Size = 14;
                nameParagraph.Format.Font.Bold = true;

                Paragraph valueParagraph = row.Cells[0].AddParagraph(leftData[i].Item2.ToString("0.######"));
                valueParagraph.Format.Font.Size = 14;
            }

            if (i < rightData.Length)
            {
                Paragraph nameParagraph = row.Cells[1].AddParagraph(rightData[i].Item1);
                nameParagraph.Format.Font.Size = 14;
                nameParagraph.Format.Font.Bold = true;
                nameParagraph.Format.LeftIndent = "3cm";

                Paragraph valueParagraph = row.Cells[1].AddParagraph(rightData[i].Item2.ToString("0.######"));
                valueParagraph.Format.Font.Size = 14;
                valueParagraph.Format.LeftIndent = "3cm"; 

                if (rightData[i].Item3 == "red")
                {
                    valueParagraph.Format.Font.Color = Colors.Red;
                    valueParagraph.Format.Font.Underline = Underline.Single;
                }
                else
                {
                    valueParagraph.Format.Font.Color = Colors.Green;
                }
            }
        }

        PdfDocumentRenderer renderer = new PdfDocumentRenderer(true);
        renderer.Document = document;
        renderer.RenderDocument();
        renderer.PdfDocument.Save(filename);

        Process.Start(new ProcessStartInfo(filename) { UseShellExecute = true });
    }

    private static string GetColor(float value, float compareDigit, Func<float, float, bool> compare)
    {
        return compare(value, compareDigit) ? "red" : "green";
    }
}
