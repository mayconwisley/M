namespace M.Domain.Pdf;

public interface IPdfMerger
{
    void Merge(string inputDirectory, string outputFile);
}
