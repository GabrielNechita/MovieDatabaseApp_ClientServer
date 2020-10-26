using System.Collections.Generic;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.Drawing;

namespace MovieDatabase.Model
{
    public class PdfReport : IReport
    {

        public void GenerateReport(List<Movie> movieList)
        {
            string text = "";

            PdfDocument document = new PdfDocument();

            PdfPage page = document.Pages.Add();

            PdfGraphics graphics = page.Graphics;

            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

            foreach (var movie in movieList)
            {

                text = text + movie.ToString() + "\n";
            }

            graphics.DrawString(text, font, PdfBrushes.Black, new PointF(0, 0));

            document.Save("Output.pdf");

            document.Close(true);
        }
    }
}
