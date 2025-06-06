using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using PlataformaEducacaoOnline.GestaoAluno.Domain;
using System.IO;

namespace PlataformaEducacaoOnline.GestaoAluno.Data
{
    public class CertificadoPdfGenerator : ICertificadoPdfGenerator
    {
        public byte[] GerarPdf(Certificado certificado)
        {
            using var document = new PdfDocument();
            var page = document.AddPage();
            using var gfx = XGraphics.FromPdfPage(page);
            var font = new XFont("Arial", 20, XFontStyle.Bold);

            gfx.DrawString("Certificado de Conclusão", font, XBrushes.Black, new XRect(0, 50, page.Width, 50), XStringFormats.Center);
            gfx.DrawString($"Matrícula: {certificado.MatriculaId}", new XFont("Arial", 12), XBrushes.Black, new XRect(0, 120, page.Width, 20), XStringFormats.Center);
            gfx.DrawString($"Data de Emissão: {certificado.DataEmissao:dd/MM/yyyy}", new XFont("Arial", 12), XBrushes.Black, new XRect(0, 150, page.Width, 20), XStringFormats.Center);

            using var stream = new MemoryStream();
            document.Save(stream, false);
            return stream.ToArray();
        }
    }
}
