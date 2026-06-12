using M.Application.Pdf;

namespace M.Presentation.Cli;

public sealed class PdfCliHandler(MergePdfUseCase merge)
{
    public void Handle(string[] args)
    {
        if (args.Length < 3 || args.Length > 4)
            throw new ArgumentException("Uso: M -merge <input_dir> <output_dir> [filename]");

        var outputFileName = args.Length == 4 ? args[3] : "pdfMerge.pdf";
        merge.Execute(args[1], args[2], outputFileName);
    }
}
