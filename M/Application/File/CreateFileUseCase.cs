using M.Domain.File;

namespace M.Application.File;

public sealed class CreateFileUseCase(IFileRepository repository)
{
    public bool Execute(string directoryPath, string fileName)
    {
        if (string.IsNullOrWhiteSpace(directoryPath) || string.IsNullOrWhiteSpace(fileName))
            return false;
        if (fileName.Contains('"') || fileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            return false;

        return repository.Create(directoryPath, fileName);
    }
}
