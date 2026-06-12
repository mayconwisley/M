using M.Domain.File;

namespace M.Application.File;

public sealed class MoveFileUseCase(IFileRepository repository)
{
    public bool Execute(string filePath, string destinationDirectory)
    {
        if (string.IsNullOrWhiteSpace(filePath) || string.IsNullOrWhiteSpace(destinationDirectory))
            return false;

        return repository.Move(filePath, destinationDirectory);
    }
}
