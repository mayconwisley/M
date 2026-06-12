using M.Domain.Folder;

namespace M.Application.Folder;

public sealed class DeleteSubFoldersUseCase(IFolderRepository repository)
{
    public bool Execute(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            return false;

        return repository.DeleteSubFolders(path);
    }
}
