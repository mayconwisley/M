using M.Domain.File;

namespace M.Application.File;

public sealed class CopyFileUseCase(IFileRepository repository)
{
    public bool Execute(string filePath, string destinationDirectory, bool cut = false)
    {
        if (string.IsNullOrWhiteSpace(filePath) || string.IsNullOrWhiteSpace(destinationDirectory))
            return false;

        return repository.Copy(filePath, destinationDirectory, cut);
    }
}
