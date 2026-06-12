using M.Domain.Folder;

namespace M.Application.Folder;

public sealed class MoveFolderUseCase(IFolderRepository repository)
{
    public bool Execute(string origin, string destination)
    {
        if (string.IsNullOrEmpty(origin) || string.IsNullOrEmpty(destination))
            return false;

        return repository.Move(origin, destination);
    }
}
