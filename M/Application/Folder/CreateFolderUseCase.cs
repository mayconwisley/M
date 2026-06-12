using M.Domain.Folder;

namespace M.Application.Folder;

public sealed class CreateFolderUseCase(IFolderRepository repository)
{
    public bool Execute(string path, string name)
    {
        if (string.IsNullOrEmpty(path) || string.IsNullOrWhiteSpace(name))
            return false;
        if (name.Contains('"') || name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            return false;

        return repository.Create(path, name);
    }
}
