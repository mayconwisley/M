using M.Domain.File;

namespace M.Application.File;

public sealed class DeleteFileUseCase(IFileRepository repository)
{
    public bool Execute(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            return false;

        return repository.Delete(filePath);
    }
}
