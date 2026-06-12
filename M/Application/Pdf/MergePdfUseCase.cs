using M.Domain.Pdf;

namespace M.Application.Pdf;

public sealed class MergePdfUseCase(IPdfMerger merger)
{
    public void Execute(string inputDirectory, string outputDirectory, string outputFileName)
    {
        if (!Directory.Exists(inputDirectory))
            throw new DirectoryNotFoundException($"Diretório de entrada não encontrado: {inputDirectory}");
        if (!Directory.Exists(outputDirectory))
            throw new DirectoryNotFoundException($"Diretório de saída não encontrado: {outputDirectory}");
        if (string.IsNullOrWhiteSpace(outputFileName))
            throw new ArgumentException("O nome do arquivo de saída não pode ser vazio.");

        if (!string.Equals(Path.GetExtension(outputFileName), ".pdf", StringComparison.OrdinalIgnoreCase))
            outputFileName += ".pdf";

        merger.Merge(inputDirectory, Path.Combine(outputDirectory, outputFileName));
    }
}
