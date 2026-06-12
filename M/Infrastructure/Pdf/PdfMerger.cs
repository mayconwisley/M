using M.Domain.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace M.Infrastructure.Pdf;

public sealed class PdfMerger : IPdfMerger
{
    private static readonly HashSet<string> SupportedExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".pdf", ".jpg", ".jpeg", ".png", ".bmp"
    };

    public void Merge(string inputDirectory, string outputFile)
    {
        var inputFiles = Directory
            .EnumerateFiles(inputDirectory, "*", SearchOption.TopDirectoryOnly)
            .Where(f => SupportedExtensions.Contains(Path.GetExtension(f)))
            .OrderBy(f => f)
            .ToArray();

        if (inputFiles.Length == 0)
            throw new ArgumentException("Nenhum arquivo suportado encontrado no diretório de entrada.");

        try
        {
            using var output = new PdfDocument();

            foreach (var file in inputFiles)
                AppendFile(output, file);

            output.Save(outputFile);
        }
        catch (Exception ex) when (ex is not ArgumentException)
        {
            throw new InvalidOperationException("Ocorreu um erro ao mesclar os arquivos.", ex);
        }
    }

    private static void AppendFile(PdfDocument output, string file)
    {
        var extension = Path.GetExtension(file).ToLowerInvariant();

        if (extension == ".pdf")
        {
            using var input = PdfReader.Open(file, PdfDocumentOpenMode.Import);
            for (var i = 0; i < input.PageCount; i++)
                output.AddPage(input.Pages[i]);
        }
        else
        {
            using var image = XImage.FromFile(file);
            var page = output.AddPage();
            page.Width  = XUnit.FromPoint(image.PointWidth);
            page.Height = XUnit.FromPoint(image.PointHeight);
            using var gfx = XGraphics.FromPdfPage(page);
            gfx.DrawImage(image, 0, 0);
        }
    }
}
